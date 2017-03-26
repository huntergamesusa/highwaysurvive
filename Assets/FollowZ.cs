using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowZ : MonoBehaviour {
	public float damp;
	public static GameObject target;
	public bool isFree=false;
	// Use this for initialization
	void Awake () {
//		target = GameObject.Find ("EmptyBody").transform.GetChild (0).gameObject;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (target == null)
			return;
		if (isFree) {
			damp = Mathf.Lerp (damp, 5, Time.deltaTime * 5);

		} else {
			if (Mathf.Abs (target.transform.position.x) < 50) {
				damp = Mathf.Lerp (damp, 5, Time.deltaTime * 5);
			} else {
				damp = Mathf.Lerp (damp, 0, Time.deltaTime * 5);

			}
		}
			if (target) {
				transform.position = Vector3.Lerp (transform.position, new Vector3 (target.transform.position.x, transform.position.y, transform.position.z), Time.deltaTime * damp);
			}
		
	}
}
