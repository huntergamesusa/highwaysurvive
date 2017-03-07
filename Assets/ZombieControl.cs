using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : MonoBehaviour {
	Animator zombieAnim;
	Transform target;
	Rigidbody mainRigid;
	float speed;
	float damping;
	// Use this for initialization
	void Awake () {
		damping = Random.Range (.5f, 5f);
//		speed = Random.Range (3, 7f);
		speed = Random.Range (PlayerPrefs.GetFloat("minzombiespeed"), PlayerPrefs.GetFloat("maxzombiespeed"));
		zombieAnim = GetComponent<Animator> ();
		zombieAnim.speed = speed * .6f;
		mainRigid = GetComponent<Rigidbody> ();
		target = GameObject.Find ("ZombieTarget").transform;

		transform.LookAt (target);
//		GetComponent<CapsuleCollider> ().isTrigger = true;


	}
	
	// Update is called once per frame
	void Update () {
		if (zombieAnim.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
//			transform.LookAt (target);
			var rotation = Quaternion.LookRotation(target.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
		
			mainRigid.velocity = transform.forward * speed;
		}
	}

}
