using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour {
	public float speed;
	private float lowPitchRange  = .75F;
	private float  highPitchRange  = 1.5F;
	private float velToVol = .2F;
	private float velocityClipSplit = 10f;
	AudioClip crashSoft;
	AudioClip crashHard;
	AudioClip impactSoft;
	AudioClip impactHard;
	AudioClip engineIdle;
	AudioSource engineSource;
	AudioSource source;
	Transform rayPos;
	Mesh carmesh;
	bool dead;
	GameObject ragdollenable;
	void Awake(){

		speed = PlayerPrefs.GetFloat ("carspeed");

		crashSoft = Resources.Load ("Sounds/vehicle_crash_small")as AudioClip;
		crashHard = Resources.Load ("Sounds/vehicle_crash_large")as AudioClip;
		impactSoft = Resources.Load ("Sounds/IMPACTSMALL")as AudioClip;
		impactHard = Resources.Load ("Sounds/IMPACTLARGE")as AudioClip;
		engineIdle = Resources.Load ("Sounds/engine-loop-1-normalized")as AudioClip;
		GameObject go = new GameObject("EngineSource");
		go.transform.parent = gameObject.transform;
		go.AddComponent<AudioSource> ();
		engineSource = go.GetComponent<AudioSource> ();
		if (engineSource.clip == null) {
			engineSource.clip = engineIdle;
		}
		engineSource.loop = true;
		engineSource.spatialBlend = 1;
		engineSource.minDistance = .1f;
		engineSource.dopplerLevel = 5f;
		engineSource.maxDistance = 2f;
		engineSource.pitch = Random.Range(.25f,1.2f);
		engineSource.volume = Random.Range(.8f,1.2f);
		engineSource.Play ();
		Physics.IgnoreCollision (GetComponent<MeshCollider> (), GameObject.Find ("RoadParentMiddle").GetComponent<BoxCollider> (), true);
		Physics.IgnoreCollision (GetComponent<MeshCollider> (), GameObject.Find ("RoadParentOther").GetComponent<BoxCollider> (), true);
		Physics.IgnoreCollision (GetComponent<MeshCollider> (), GameObject.Find ("RoadParentOther2").GetComponent<BoxCollider> (), true);
		IgnoreColl (true);
		source = GetComponent<AudioSource> ();
		GetComponent<Rigidbody> ().velocity = transform.forward * -speed;
		carmesh = GetComponent<MeshFilter> ().mesh;

		ragdollenable = new GameObject ("ragdoller");
		ragdollenable.AddComponent<BoxCollider> ();
		ragdollenable.AddComponent<Rigidbody> ();
		ragdollenable.GetComponent<Rigidbody> ().isKinematic = true;
		ragdollenable.GetComponent<Rigidbody> ().useGravity = false;
		ragdollenable.GetComponent<BoxCollider> ().isTrigger = true;
		ragdollenable.GetComponent<BoxCollider> ().size = new Vector3 (1, 1, carmesh.bounds.size.x);
		ragdollenable.transform.parent = gameObject.transform;
		ragdollenable.transform.localPosition =new Vector3(0,1, -carmesh.bounds.extents.z);
		ragdollenable.transform.localScale =new Vector3(1,1, 1);
		ragdollenable.layer = 12;
		ragdollenable.tag = "carragdoll";
//		print(carmesh.bounds.size);

	}

	public void CarHit(bool explosion, Vector3 explosionPos,float power,float rad,float upmod){
		dead = true;
					GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
					GetComponent<Rigidbody> ().useGravity = true;
					IgnoreColl (false);
					Destroy (ragdollenable);
					StartCoroutine (DestroyCar (6));

		if (explosion) {
			GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, rad,upmod);
			ScoringManager.UpdateScore (ScoringManager.myScores.killcar, 1);

		}

	}

	void OnTriggerEnter(Collider coll){
		if (coll.tag == "bigpowerup") {
			CarHit (true,new Vector3(coll.transform.position.x,coll.transform.position.y+3,coll.transform.position.z),8000000f,50f,4f);

		}
	}

	void OnCollisionEnter (Collision coll) {
//		if (coll.gameObject.tag == "ragdoll" || coll.gameObject.tag == "car") {
//			
////			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
////			GetComponent<Rigidbody> ().useGravity = true;
////			IgnoreColl (false);
////			StartCoroutine (DestroyCar (6));
//		} else {
////			GetComponent<Rigidbody> ().velocity = transform.forward * -speed;
//
//		}
		source.pitch = Random.Range (lowPitchRange,highPitchRange);
		float hitVol  = coll.relativeVelocity.magnitude * velToVol;
		hitVol = Mathf.Clamp(hitVol,0f,.75f);
		if (coll.relativeVelocity.magnitude < velocityClipSplit)
		if (coll.gameObject.tag == "ragdoll"||coll.gameObject.tag == "zombie") {
			source.PlayOneShot (impactSoft, hitVol);
		} else {
			source.PlayOneShot (crashSoft, hitVol);

		}
		else 
			if (coll.gameObject.tag == "ragdoll"||coll.gameObject.tag == "zombie") {
				source.PlayOneShot (impactHard, hitVol);
			} else {
				source.PlayOneShot (crashHard, hitVol);

			}	}


	public IEnumerator DestroyCar(int seconds){
		yield return new  WaitForSeconds(seconds);
		Destroy (gameObject);
	}
	public void IgnoreColl(bool ign){
		Physics.IgnoreCollision (GetComponent<MeshCollider> (), GameObject.Find ("RoadParentMiddle").GetComponent<BoxCollider> (), ign);
		Physics.IgnoreCollision (GetComponent<MeshCollider> (), GameObject.Find ("RoadParentOther").GetComponent<BoxCollider> (), ign);
		Physics.IgnoreCollision (GetComponent<MeshCollider> (), GameObject.Find ("RoadParentOther2").GetComponent<BoxCollider> (), ign);
	}

	void FixedUpdate(){
		if (!dead){
			GetComponent<Rigidbody> ().velocity = transform.forward * -speed;
	}

	}
}
