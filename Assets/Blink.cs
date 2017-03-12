using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Blink : MonoBehaviour {
	bool blinking=false;
	Text startImg;
	// Use this for initialization
	void Awake () {
		startImg = GetComponent < Text >();
	}

	void Update(){
		StartCoroutine (Blinkit ());
	}

	void OnEnable(){
		blinking = false;
		startImg.enabled=true;
	}
	
	public IEnumerator Blinkit(){
		if(!blinking){
			blinking=true;
			yield return new WaitForSeconds (.5f);
			startImg.enabled=false;
			yield return new WaitForSeconds (.5f);
			startImg.enabled=true;
			blinking=false;

		}
	}
}
