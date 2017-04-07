using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TapStartBlink : MonoBehaviour {
	 Text startImg;
	void Awake(){
		startImg =	GetComponent<Text> ();
	}
	void OnEnable(){
		Blink.blinkbool += this.MyBlink;
	}
	void OnDisable(){
		Blink.blinkbool -= this.MyBlink;

	}

	void MyBlink(bool blinkme){

		startImg.enabled = blinkme;
	}
}
