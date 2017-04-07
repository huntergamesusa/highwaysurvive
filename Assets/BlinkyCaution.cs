using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyCaution : MonoBehaviour {
	GameObject mainCam;
	bool blinky;
	// Use this for initialization
	void Awake () {
		mainCam = Camera.main.gameObject;
	}

	void OnEnable(){
		Blink.blinkbool += this.MyBlink;
	}
	void OnDisable(){
		Blink.blinkbool -= this.MyBlink;

	}

	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3 (mainCam.transform.position.x, transform.position.y,transform.position.z );
	}

	void MyBlink(bool blinkme){

		foreach (SpriteRenderer sprite in gameObject.GetComponentsInChildren<SpriteRenderer>()) {
			sprite.enabled = blinkme;
		}

	}



	void OnTriggerEnter(Collider coll){
		if(coll.gameObject.tag=="car"){
			Destroy (gameObject);
		}

	}
}
