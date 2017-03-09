using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEngage : MonoBehaviour {
	bool attract;
	Transform target;
	// Use this for initialization
	void Update () {
		if (BotControlScript.magnetPowerUp) {
			
			if (attract) {
				transform.parent.position = Vector3.MoveTowards (transform.parent.position, new Vector3 (target.position.x, transform.parent.position.y, target.position.z), Time.deltaTime * 50);
			}
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider coll) {
			if (coll.tag == "Player") {
				target = coll.transform;
				attract = true;

		}
	}
}
