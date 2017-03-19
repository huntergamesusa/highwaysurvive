using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PrizeManager : MonoBehaviour {
	public Camera giftCam;
	public RawImage background;
	public GameObject shadow;
	public GameObject emptyBody;
	GameObject currentPrize;
	// Use this for initialization
	void Awake () {
		EnableUI (false);
	}



	void Update(){
		if (Input.GetKeyUp (KeyCode.F)) {

			DestroyGO ("gift");

			EnableUI (true);
			int ran = Random.Range (0, PartsCollections.allAvailableGO.Count);
			if (PartsCollections.allAvailableGO [ran].name.Contains ("Chest")) {
				var myGift = Instantiate (emptyBody, giftCam.transform.position, Quaternion.Euler(0,180,0));

				CharacterParts myParts = myGift.GetComponent<CharacterParts> ();

				myParts.ChangeChest (PartsCollections.allAvailableGO [ran]);

				string key = PartsCollections.allAvailableGO [ran].name.Replace ("Chest", "");
				print (key);
				foreach (GameObject obj in PartsCollections.allResources) {
					if (obj == null)
						continue;

					if (obj.name.StartsWith ("Shoulder")) {
						if(obj.name.EndsWith(key)){
							print(obj.name);
							myParts.ChangeLeftShoulder (obj);
							myParts.ChangeRightShoulder (obj);

						}
					}
					if (obj.name.StartsWith ("Elbow")) {
						if (obj.name.EndsWith (key)) {
							myParts.ChangeLeftElbow (obj);
							myParts.ChangeRightElbow (obj);
						}

					}
					if (obj.name.StartsWith ("Spine")) {
						if (obj.name.EndsWith (key)) {
							myParts.ChangeSpine (obj);

						}
					}
					if (obj.name.StartsWith ("Root")) {
						if (obj.name.EndsWith (key)) {
							myParts.ChangeLowerSpine (obj);

						}
					}
					if (obj.name.StartsWith ("Hip")) {
						if (obj.name.EndsWith (key)) {
							myParts.ChangeLeftHip (obj);
							myParts.ChangeRightHip (obj);

						}

					}
					if (obj.name.StartsWith ("Knee")) {
						if (obj.name.EndsWith (key)) {
							myParts.ChangeLeftKnee (obj);
							myParts.ChangeRightKnee (obj);
						}
					}

				}




				MeshRenderer[] allChildren = myGift.GetComponentsInChildren<MeshRenderer>();
				foreach (MeshRenderer obj in allChildren) {
					obj.gameObject.layer = 20;
				}
				myGift.tag="gift";
				myGift.name = "FreePrize";
				myGift.transform.parent = giftCam.transform;
				myGift.transform.localPosition = new Vector3 (0, -.079772f, .933f);
				LeanTween.rotateAround (myGift, new Vector3 (0, 1, 0), 360, 4).setLoopClamp ();
				currentPrize = myGift;
			} else {
				float rotCorrect;
				if(PartsCollections.allAvailableGO [ran].name.Contains("Weapon")){
					rotCorrect=-90;
				}
					else{
						rotCorrect=0;
					}
				GameObject GOCorrect = new GameObject ();
				GOCorrect.transform.parent = giftCam.transform;
				GOCorrect.transform.localPosition = new Vector3 (0, -.079772f, .933f);
				var myGift2 = Instantiate (PartsCollections.allAvailableGO [ran], giftCam.transform.position, Quaternion.Euler(rotCorrect,180,0));
				GOCorrect.tag="gift";
				GOCorrect.name = "FreePrize";
				myGift2.transform.parent = GOCorrect.transform;
				myGift2.transform.localPosition = new Vector3 (0,0,0);
				myGift2.layer = 20;
				LeanTween.rotateAround (GOCorrect, new Vector3 (0, 1, 0), 360, 4).setLoopClamp ();
				currentPrize=GOCorrect;
			}

		}

	}

	public void EnableUI(bool bol){
		giftCam.enabled = bol;
		background.enabled = bol;
		shadow.SetActive (bol);
		if(!bol){
			if(currentPrize){
				Destroy(currentPrize);
			}
		}
	}

	void DestroyGO(string tag){
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag (tag);

		foreach (GameObject go in gos) {
			Destroy (go);
		}

	}

}
