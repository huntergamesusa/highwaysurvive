using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonBlink : MonoBehaviour {
	Image myImage;

	void Start(){
		myImage = GetComponent<Image> ();
	}
	// Use this for initialization
	void OnEnable(){
		Blink.blinkbool += this.MyBlink;
	}
	void OnDisable(){
		Blink.blinkbool -= this.MyBlink;
		myImage.color = Color.white;

	}
	
	void MyBlink(bool blinkme){

		if (blinkme) {
			myImage.color = Color.red;
		} else {
			myImage.color = Color.white;
		}

	}

}
