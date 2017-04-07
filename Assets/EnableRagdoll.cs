using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnableRagdoll : MonoBehaviour {
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


			Physics.IgnoreLayerCollision (9, 12, true);



//		}
	}

	public void PauseZombie(bool onoff){
		ragdolloff = onoff;
	}

	public IEnumerator PauseTime(){
		Time.timeScale = 0f;
		yield return new WaitForSecondsRealtime(PlayerPrefs.GetFloat("pausehit"));
		Time.timeScale = 1;

	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider coll) {
		if (isKilled)
			return;


		if (coll.tag == "carragdoll") {

				if (!ragdolloff) {
					EngageRagdoll ();
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
			print (gameObject.transform.parent.name + " isdead");
			GameObject myIndicator = transform.parent.Find ("PlayerIndicator").gameObject;
			if (myIndicator) {
				myIndicator.GetComponent<MeshRenderer> ().enabled = false;
			}
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

}
