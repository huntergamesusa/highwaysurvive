using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour {
	public AudioClip coinsound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		switch (col.tag){

		case "coin":
			NetworkEngine.EarnedCoin ();
			GetComponent<AudioSource> ().PlayOneShot (coinsound);
			GameObject.Find ("SpawnCollectables").SendMessage ("DestroyCoin", col.name);
			print (col.name);
			break;


		}

	}


}
