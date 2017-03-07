using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGameObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider coll) {
		if (coll.tag == "car") {
			Destroy (coll.gameObject);
			print ("killed car");
		}
	}
}
