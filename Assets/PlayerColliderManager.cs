using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerColliderManager : MonoBehaviour {
	public AudioClip coinsound;
	public static string powerup;
	public AudioClip beep;
	public AudioClip finish;
	GameObject powerupParent;
	int increment=0;
	// Use this for initialization

	void Awake(){
		increment = 0;
		powerupParent = GameObject.Find ("BubblePowerUp");
		for (int i = 0; i < powerupParent.transform.childCount; i++) {
			powerupParent.transform.GetChild (i).gameObject.SetActive (false);
		}
	}

	void Update(){

		if (Input.GetKeyUp (KeyCode.P)) {
			StartCoroutine (RunPowerUpLottery ());

		}

	}

	public void StopLottery(){
		StopCoroutine (RunPowerUpLottery ());
		powerup = "";

	}

	IEnumerator RunPowerUpLottery(){
		LeanTween.moveY (powerupParent.transform.parent.GetComponent<RectTransform> (), -5f, .33f).setEaseInOutBounce ();
		increment = Random.Range (0, powerupParent.transform.childCount);
		for (int i = 0; i <=Random.Range(15,32); i++) {
			powerupParent.transform.localScale = new Vector3 (1f, 1f, 1f);
			yield return new WaitForSecondsRealtime(.03f);


		if (increment >= powerupParent.transform.childCount) {
			increment = 0;
		}
			for (int j = 0; j < powerupParent.transform.childCount; j++) {
				powerupParent.transform.GetChild (j).gameObject.SetActive (false);

		}
		powerupParent.transform.GetChild (increment).gameObject.SetActive (true);
			powerupParent.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
		increment++;
			GetComponent<AudioSource> ().PlayOneShot (beep);
			yield return new WaitForSecondsRealtime(.03f);
		}
		if (increment > powerupParent.transform.childCount) {
			increment = 0;
		} else {
			increment--;
		}
		powerup = powerupParent.transform.GetChild (increment).name;
		GetComponent<AudioSource> ().PlayOneShot (finish);

		print ("powerup is " + powerup);
		powerupParent.transform.localScale = new Vector3 (1f, 1f, 1f);

	}



	void OnTriggerEnter(Collider col){
		switch (col.tag){

		case "coin":
			GetComponent<AudioSource> ().PlayOneShot (coinsound);
			GameObject.Find ("SpawnCollectables").SendMessage ("DestroyCoin", col.name);
			int mult;
			if (BotControlScript.doublecoins) {
				mult = 2;
			} else {
				mult=1;
			}
			ScoringManager.UpdateCoins (mult);

			ScoringManager.UpdateScore (25, mult,new Vector3(1000,1000,1000));
			print (col.name);
			break;
		case "powerup":
			StartCoroutine (RunPowerUpLottery ());

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

	



