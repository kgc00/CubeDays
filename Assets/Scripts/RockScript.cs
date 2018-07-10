using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockScript : MonoBehaviour {
	Material mat;

	Color restingColor;
	Color hoverColor;

	Vector3 destination;
	bool hasADestination = false;

	public GameObject destinationSparkle;

	// Use this for initialization
	void Start () {
		//These are used for the OnMouseEnter, and OnMouseExit functions below
		restingColor = new Color (Random.value, Random.value, Random.value);
		hoverColor = new Color (Random.value, Random.value, Random.value);

		Renderer rend = gameObject.GetComponent<Renderer> ();
		mat = rend.material;

		mat.color = restingColor;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			bool hitSomething = Physics.Raycast (ray, out hit);
			if (hitSomething) {
				//Store the position that we clicked (in the 'destination' variable)
				destination = hit.point;
				destination.y = 0.5f;//This is a bit of a hack
				hasADestination = true;

				//Instantiate a notification of where you clicked
				GameObject sparkle = (GameObject)Instantiate (destinationSparkle, destination, Quaternion.identity);
				Destroy (sparkle, 3);
			}
		}
			
		if (hasADestination) {
			//Rotate toward the destination
			transform.LookAt (destination);

			//Move toward the destination (which is forward because of the LookAt command above)
			transform.position = transform.position + transform.forward * 4f * Time.deltaTime;

			//If we are close to the destination, stop moving toward the destination
			float distanceToDestination = Vector3.Distance (transform.position, destination);
			if (distanceToDestination < 0.1f) {
				hasADestination = false;
			}
		}
	}
		
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Goal") {
			//Load new level, in order for this to work, you need to add "Level 2" 
			//to File->Build Settings...
			SceneManager.LoadScene("Level 2");
		}
	}
		
	void OnMouseDown() {
		Destroy (gameObject);
	}
	void OnMouseEnter() {
		mat.color = hoverColor;
	}
	void OnMouseExit() {
		mat.color = restingColor;
	}
}
