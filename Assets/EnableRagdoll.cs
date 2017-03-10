using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnableRagdoll : MonoBehaviour {
	public bool isZombie;
	bool isKilled = false;
	GameObject root;
	GameObject parent;
	GameObject ragdollEng;
	AudioClip facepunch;
	bool ragdolloff;

	// Use this for initialization
	void Awake () {
		parent = transform.parent.gameObject;
		root = transform.parent.GetChild (0).gameObject;
		if (!isZombie) {
			Physics.IgnoreLayerCollision (9, 12, true);

		} else {
			Physics.IgnoreLayerCollision (17, 12, true);
			facepunch = Resources.Load ("Sounds/human_face_punch")as AudioClip;

		}

//		}
	}

	public void PauseZombie(bool onoff){
		ragdolloff = onoff;
	}

	public IEnumerator PauseTime(){
		Time.timeScale = 0f;
		yield return new WaitForSecondsRealtime(.1f);
		Time.timeScale = 1;

	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider coll) {
		if (isKilled)
			return;
		if(isZombie){
			//16 is Weapon
		if(coll.gameObject.layer == 16 ){
				StartCoroutine (PauseTime ());
				ScoringManager.UpdateScore (ScoringManager.myScores.killzombie, 1);

				EngageRagdollZombie (coll.gameObject.transform.forward * 10000,false, Vector3.zero);
				PlaySwordHit ();
		}
			if (coll.tag == "bigpowerup") {
				print ("hit powerup zombie");
				print (gameObject.name);
				ScoringManager.UpdateScore (ScoringManager.myScores.killzombie, 1);

				EngageRagdollZombie (coll.gameObject.transform.forward * 10000,false, Vector3.zero);

			}
		}

		if (coll.tag == "carragdoll") {

			if (isZombie) {
				EngageRagdollZombie (Vector3.zero,false,Vector3.zero);
			

			} else {
				if (!ragdolloff) {
					EngageRagdoll ();
				}
			}
		}
		if (coll.tag == "zombietrigger") {
			if (!ragdolloff) {
				
				EngageRagdoll ();
			}
		}

	
	
	}

	void EngageRagdoll(){
		if (!isKilled) {
			isKilled = true;
			FollowZ.target = null;
			Physics.IgnoreLayerCollision (9, 12, false);
			transform.parent.GetComponent<Animator> ().enabled = false;
			Destroy (GetComponent<Rigidbody> ());
			Destroy (GetComponent<CapsuleCollider> ());
			Rigidbody[] allChildren = transform.parent.GetComponentsInChildren<Rigidbody> ();
			foreach (Rigidbody child in allChildren) {
				// do what you want with the rigidbody
				child.isKinematic = false;
				child.useGravity = true;
			}
			root.transform.parent = null;
			Destroy (gameObject);
		}
	}
	public void EngageRagdollZombie(Vector3 addforce,bool explosive,Vector3 explosionPos){
		if (!isKilled) {
			isKilled = true;
			Physics.IgnoreLayerCollision (17, 12, false);
			GameObject.Find ("SpawnZombies").SendMessage ("DestroyZombie", gameObject.name);
			Destroy (transform.parent.GetComponent<ZombieControl> ());
			transform.parent.GetComponent<Animator> ().enabled = false;
			Destroy (transform.parent.GetComponent<Rigidbody> ());
			Destroy (transform.parent.GetComponent<CapsuleCollider> ());
			Destroy (GetComponent<Rigidbody> ());
			Destroy (GetComponent<CapsuleCollider> ());
			Destroy (transform.parent.Find ("Eat").gameObject);
			Rigidbody[] allChildren = transform.parent.GetComponentsInChildren<Rigidbody> ();
			foreach (Rigidbody child in allChildren) {
				// do what you want with the rigidbody
				child.isKinematic = false;
				child.useGravity = true;
				if (!explosive) {
					child.AddForce (addforce.x, Mathf.Abs (addforce.y * 2f), addforce.z);
				} else {
					child.AddExplosionForce(9500f, explosionPos, 50,40.0f);

				}

			}
			if (root) {
				root.transform.parent = null;
			}
			StartCoroutine(SetDestroy (root, 3));

		}
	}

	public IEnumerator SetDestroy(GameObject myZombie,int sec){
		yield return new WaitForSeconds (sec);

		Destroy (myZombie);
	}


	void PlaySwordHit(){
		transform.parent.GetComponent<AudioSource> ().pitch = Random.Range (.8f, 1.2f);
		transform.parent.GetComponent<AudioSource> ().PlayOneShot (facepunch);


	}
}
