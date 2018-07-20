using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {

	public Text displayText;
	[SerializeField]
	float timerMax;
	float currTimer;
	[SerializeField]
	bool timerOn;
	[SerializeField]
	string[] tutTextArray = new string[6];
	[SerializeField]
	int currentTextIndex;
	public PlayerContollerEC pc;
	public PlayerScriptEC ps;

	void Start (){	
		Initialize ();	
		Tutorial ();
	}

	void Initialize(){
		currTimer = 0f;
		currentTextIndex = 0;
		timerMax = 5f;
		timerOn = false;
		tutTextArray [0] = "Welcome!  This is a game about cube life in a cube world...";
		tutTextArray [1] = "By left-clicking on the ground, you can walk around...";
		tutTextArray [2] = "By left-clicking during dialogue, you can exit it...";
		tutTextArray [3] = "These little cubes are cube-citizens.  They'll give helpful hints.";
		tutTextArray [4] = "This is the keysphere, you'll use it to unlock the exit...";
		tutTextArray [5] = "You can fly!  Try clicking on the ground above you...";
		tutTextArray [6] = "This big cube is the exit!  Get to it!";
		pc.canMove = false;
		ps = FindObjectOfType<PlayerScriptEC>();
	}

	public void ResetValues(){			
		currentTextIndex++;
		currTimer = 0;		
		if (currentTextIndex < 3) {
			Tutorial ();	
		} else {
			timerOn = false;
			displayText.enabled = false;
			//ps.SetDialogueValue(false, "tutorial", false);
			ps.TutorialHack();
		}
	}

	public void Tutorial (){
		if (currentTextIndex <= tutTextArray.Length) {
			//ps.SetDialogueValue(true, "tutorial");
			timerOn = true;
		}
	}

	void TimerLogic(){
		if (currTimer < timerMax) {
			currTimer += Time.deltaTime;
		} else if (currTimer >= timerMax) {
			ResetValues ();
		}
	}

	public string ReturnCurrentTutorial(){
		return tutTextArray[currentTextIndex];
	}

		public int ReturnCurrentTutorialIndex(){
		return currentTextIndex;
	}

	public float ReturnWaitTime(){
		return 5.0f;
	}

	void Update(){
		if (timerOn) {
			TimerLogic ();
		}
	}
}
