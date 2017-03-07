using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCoin : MonoBehaviour {
	public float rotateSpeed;
	// Use this for initialization
	void Start () {
		LeanTween.moveLocalY (gameObject, 6f, .25f).setEaseInCubic();
		LeanTween.moveLocalY (gameObject, 4f, .1f).setEaseOutCubic().setDelay (.25f);

		LeanTween.rotateAround (gameObject, new Vector3 (0, 1, 0), 360, rotateSpeed).setLoopClamp ().setDelay (.25f);
	}
	
	// Update is called once per frame



}
