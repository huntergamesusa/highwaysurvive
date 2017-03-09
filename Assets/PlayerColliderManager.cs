using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour {
	public AudioClip coinsound;
	public static string powerup;
	// Use this for initialization

	
	// Update is called once per frame


	void OnTriggerEnter(Collider col){
		switch (col.tag){

		case "coin":
			NetworkEngine.EarnedCoin ();
			GetComponent<AudioSource> ().PlayOneShot (coinsound);
			GameObject.Find ("SpawnCollectables").SendMessage ("DestroyCoin", col.name);
			ScoringManager.UpdateScore (25, 1);
			print (col.name);
			break;
		case "powerup":
			powerup = "sonicboom";
			GameObject.Find ("SpawnCollectables").SendMessage ("DestroyCoin", col.name);


			break;

		}

	}




//				if(hit2.tag == "traffic"){
//					if (hit2 && hit2.GetComponent<Rigidbody>()  ){
//						hit2.GetComponent<Collider>().isTrigger = false;
//						hit2.GetComponent<Rigidbody>().useGravity =true;
//
//						if(hit2.transform.GetChild(0).GetComponent<Collider>()){
//							hit2.transform.GetChild(0).GetComponent<Collider>().enabled = false;
//						}
//						if(hit2.transform.GetChild(1).GetComponent<Collider>()){
//							hit2.transform.GetChild(1).GetComponent<Collider>().enabled = false;
//						}
//						if(hit2.transform.GetChild(2).GetComponent<Collider>()){
//							hit2.transform.GetChild(2).GetComponent<Collider>().enabled = false;
//						}		
//						hit2.SendMessage("StopMoving");
//						hit2.GetComponent<Rigidbody>().AddExplosionForce(5000f, explosionPos, 25,26.0f);
//						print("EXPLODE!!!");
//					}
//				}

		}

	



