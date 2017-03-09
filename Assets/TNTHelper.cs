using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTHelper : MonoBehaviour {
	public GameObject explosion;
	AudioClip boom;

	void Awake(){
//		ExplosionClip
		boom = Resources.Load ("Sounds/ExplosionClip")as AudioClip;

	}

	void OnTriggerEnter(Collider coll){
		switch (coll.tag) {
		case "zombieparent":
			coll.GetComponent<AudioSource> ().PlayOneShot (boom);
			TNTBoom ();
			break;

		case "car":
			coll.GetComponent<AudioSource> ().PlayOneShot (boom);
			TNTBoom ();

			break;

		}

	}

			public void TNTBoom(){
				//		var speed = 4;
		GameObject boomFab = Instantiate(explosion, new Vector3(transform.position.x,-0.1827998f,transform.position.z),Quaternion.Euler(90,0,0)) as GameObject;
//				//		sonicBoomFab.GetComponent<ParticleSystem>().main.simulationSpeed = speed;
//				for(int i = 0; i < sonicBoomFab.transform.childCount; i++){
//					//			sonicBoomFab.transform.GetChild(i).GetComponent<ParticleSystem>().main.simulationSpeed = speed;
//				}

				//			Camera.main.SendMessage("Shake");
				GetComponent<AudioSource>().PlayOneShot(boom);

				// Applies an explosion force to all nearby rigidbodies
				Vector3 explosionPos  = transform.position;
				Collider[] colliders = Physics.OverlapSphere (explosionPos, 25);

				foreach (Collider hit2 in colliders) {


					switch (hit2.tag) {
					case "zombieparent":
						GameObject ragenable = hit2.transform.Find ("RagdollEnable").gameObject;
						ragenable.GetComponent<EnableRagdoll> ().EngageRagdollZombie (Vector3.zero, true, transform.position);

						break;


					case "car":
						hit2.GetComponent<CarDrive> ().CarHit (true,transform.position,8000000f,50,5f);
						break;
					}

				}
		Destroy (gameObject);
			}

}
