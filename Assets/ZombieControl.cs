using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : MonoBehaviour {
	Animator zombieAnim;
	Transform target;
	Rigidbody mainRigid;
	float speed;
	float damping;
	bool engaging=false;
	// Use this for initialization
	void Awake () {
		damping = Random.Range (2f, 5f);
//		speed = Random.Range (3, 7f);
		speed = Random.Range (PlayerPrefs.GetFloat("minzombiespeed"), PlayerPrefs.GetFloat("maxzombiespeed"));
		zombieAnim = GetComponent<Animator> ();
		zombieAnim.speed = (speed * .6f)/2;
		mainRigid = GetComponent<Rigidbody> ();
		target = GameObject.Find ("ZombieTarget").transform;

		transform.LookAt (target);
//		GetComponent<CapsuleCollider> ().isTrigger = true;
		var rotation = Quaternion.LookRotation(target.position - transform.position);
		rotation.x = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "playerragdoll") {
			StopCoroutine (LookAtEnemyAgain ());
			zombieAnim.speed = (speed * .6f)*PlayerPrefs.GetFloat("multzombiespeed");
			engaging = true;

		}


	}
	void OnTriggerExit(Collider coll){
		if (coll.gameObject.tag == "playerragdoll") {
			zombieAnim.speed = (speed * .6f)/2;
			engaging = false;

		}


	}
	
	// Update is called once per frame
	void Update () {



		if (zombieAnim.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
			
			mainRigid.velocity = transform.forward * speed;
		}
		if (target == null||!engaging)
			return;
		if (zombieAnim.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
//			transform.LookAt (target);
			var rotation = Quaternion.LookRotation(target.position - transform.position);
			rotation.x = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
		
		}
	}

	void FixedUpdate(){
		if (engaging)
			return;
			
		RaycastHit hit;
		Vector3 offsetpos = new Vector3 (transform.position.x, transform.position.y+2, transform.position.z);

		if (Physics.Raycast (offsetpos, transform.forward,out hit,2)) {
			if (hit.transform.tag == "worldbounds") {
				StartCoroutine (LookAtEnemyAgain ());
			}
		}
	}

	IEnumerator LookAtEnemyAgain(){
		engaging = true;
		yield return new WaitForSecondsRealtime(2);
		engaging = false;;

	}



}
