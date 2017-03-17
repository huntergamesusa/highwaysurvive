using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftAnimation : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		LeanTween.rotateZ (gameObject,30f, 1).setEaseInOutSine().setLoopPingPong ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
