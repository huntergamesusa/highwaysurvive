using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Blink : MonoBehaviour {
	bool blinking=false;

	public delegate void startBlinkEventHandler (bool blinkme);
	public static event startBlinkEventHandler blinkbool;

	// Use this for initialization
	void Awake () {
		Blink.blinkbool += this.MyBlink;
	}

	void MyBlink(bool blinkme){

	}

	void Update(){
		StartCoroutine (Blinkit ());
	}

	void OnEnable(){
		blinking = false;
	}
	
	public IEnumerator Blinkit(){
		if(!blinking){
			blinking=true;

			yield return new WaitForSeconds (.5f);
			blinkbool (false);
			yield return new WaitForSeconds (.5f);
			blinkbool (true);

			blinking=false;

		}
	}
}
