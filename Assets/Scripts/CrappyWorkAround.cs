using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrappyWorkAround : MonoBehaviour {
	
	Image parentImage;
	Text ourText;

	// Use this for initialization
	void Start () {
		parentImage = GetComponentInParent<Image>();
		ourText = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ourText.isActiveAndEnabled) {
			parentImage.enabled = true;
		} else {
			parentImage.enabled = false;
		}
	}

	public void AnyInteractLogic(){
		//transform.parent.position = this.transform.position;
	}
}
