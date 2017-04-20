using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameState : MonoBehaviour {
	public GameObject[] movingObjects;
	public  List <Vector3> originalPos = new List<Vector3> ();
	public  List <Vector3> eulerStart = new List<Vector3> ();

	public GameObject[] boundries;

	public GameObject currentPlayer;
	public Transform startTrans;
	public GameObject mainPlayer;
	public GameObject spawnZ;
	public GameObject spawnC;
	public GameObject spawnP;
	public GameObject joystick;
	public GameObject actionButtons;
	public GameObject selectCanvas;
	public GameObject MainMenu;
	public GameObject powerupParent;
	public Text[] textObjects;

	public bool dontspawncars=false;
	public GameOver myGameOver;

	void Awake(){
		if (movingObjects != null) {
			for (int i = 0; i < movingObjects.Length; i++) {
				originalPos.Add (movingObjects [i].transform.position);
				eulerStart.Add (movingObjects [i].transform.eulerAngles);
			}
		}


		Application.targetFrameRate = 60;
	}

	void ResetWorld(){
		if (movingObjects != null) {
			
			for (int i = 0; i < movingObjects.Length; i++) {
				print (originalPos [i]);
				movingObjects [i].transform.position = originalPos [i];
				movingObjects [i].transform.eulerAngles = eulerStart [i];
				if (movingObjects [i].tag == "moveGround") {
					movingObjects [i].GetComponent<BoxCollider> ().enabled = true;
				}

			}
		}
	}

	public void Restart(){
		ResetWorld ();
		myGameOver.ToggleActiveParent (false);

		print ("restart");
//		System.GC.Collect ();
		OverridePutBackPowerup ();
		FollowZ.target = null;
		foreach (GameObject obj in boundries) {
			obj.SetActive (false);

		}
		ScoringManager.ResetScore ();
		GameObject spawnZom = GameObject.Find ("SpawnZombies");
		spawnZom.GetComponent<SpawnZombies> ().enabled = false;
		if (dontspawncars) {
		} else {
			GameObject spawnCar = GameObject.Find ("SpawnCars");
			spawnCar.GetComponent<SpawnCars> ().enabled = false;
			Destroy (spawnCar);
		}
		GameObject spawnColl = GameObject.Find ("SpawnCollectables");
		spawnColl.GetComponent<SpawnCollectables> ().enabled = false;

		DestroyGO ("zombieparent");
		DestroyGO ("zombie");

		DestroyGO ("car");
		DestroyGO ("coin");
		DestroyGO ("powerup");
		DestroyGO ("tnt");
		DestroyGO ("caution");

		Destroy (spawnZom);
		Destroy (spawnColl);

		Destroy (GameObject.Find ("EmptyBody"));
		Destroy (GameObject.Find ("Armature"));
		GameObject newPlayer = Instantiate (mainPlayer, startTrans.position, Quaternion.Euler (startTrans.eulerAngles));
		newPlayer.name = "EmptyBody";
		currentPlayer = newPlayer;
		GameObject characterManage = GameObject.Find ("NewCharacterManager");
		characterManage.SendMessage ("InitCharacterReceive", currentPlayer.transform.Find("MicroMale").gameObject);
		characterManage.SendMessage ("InitWeaponReceive", currentPlayer.transform.Find("Armature/Root/Spine/Spine1/Spine2/RightShoulder/RightArm/WeaponR_Parent/WeaponMyCharacter").gameObject);

		currentPlayer.transform.Find ("PlayerIndicator").GetComponent<MeshRenderer> ().enabled = false;

//		GameObject.Find ("PartSelectionController").SendMessage("RestartCharacter",currentPlayer.GetComponent<CharacterParts>());
		CameraAnimations.target = currentPlayer.transform.GetChild(0).GetChild(0).gameObject;
//		FollowZ.target = currentPlayer.transform.GetChild (0).gameObject;
		joystick.SetActive (false);
		if (actionButtons) {
			actionButtons.SetActive (false);
		}
		MainMenu.SetActive (true);
		CameraAnimations.ChangeCamera ("start");
		EnableAlphaText (false, textObjects);


	}

	public void MakeSelection(){
		GameObject.Find ("NewCharacterManager").SendMessage("SelectCharacter");

	}
	public void ChangeMyCamera(string cam){

		CameraAnimations.ChangeCamera (cam);

	}

	public void StartGame(){
//		System.GC.Collect ();

		ScoringManager.ResetScore ();

		GameObject spawningZ = Instantiate (spawnZ, transform.position, Quaternion.identity)as GameObject;
		spawningZ.name = "SpawnZombies";
		if (dontspawncars) {
		} else {
			GameObject spawningC = Instantiate (spawnC, transform.position, Quaternion.identity)as GameObject;
			spawningC.name = "SpawnCars";
		}
		GameObject spawningP = Instantiate (spawnP, transform.position, Quaternion.identity)as GameObject;
		spawningP.name = "SpawnCollectables";
		joystick.SetActive (true);
		selectCanvas.SetActive (false);
		Debug.Log (currentPlayer.name);
		FollowZ.target = currentPlayer.transform.GetChild (0).gameObject;


		currentPlayer.GetComponent<BotControlScript> ().ReceiveJoystick (joystick.transform.GetChild (1), joystick.transform.GetChild (0));
		if (actionButtons) {
			currentPlayer.GetComponent<BotControlScript> ().ReceiveButtons (actionButtons.transform.GetChild (0).gameObject, actionButtons.transform.GetChild (1).gameObject);
			actionButtons.SetActive (true);

		}
		currentPlayer.GetComponent<BotControlScript> ().enabled = true;
		currentPlayer.transform.Find ("PlayerIndicator").GetComponent<MeshRenderer> ().enabled = true;
		CameraAnimations.ChangeCamera ("ingame");
		EnableAlphaText (true, textObjects);
		foreach (GameObject obj in boundries) {
			obj.SetActive (true);

		}
	}

	public void ReplaySameGame(bool restart){
		if (restart) {
			ScoringManager.ResetScore ();
		}
		myGameOver.ToggleActiveParent (false);

		print ("replay");
		OverridePutBackPowerup ();
//		System.GC.Collect ();
		GameObject spawnZom = GameObject.Find ("SpawnZombies");
		spawnZom.GetComponent<SpawnZombies> ().enabled = false;
		if (dontspawncars) {
		} else {
			GameObject spawnCar = GameObject.Find ("SpawnCars");
			spawnCar.GetComponent<SpawnCars> ().enabled = false;
			Destroy (spawnCar);

		}
		GameObject spawnColl = GameObject.Find ("SpawnCollectables");
		spawnColl.GetComponent<SpawnCollectables> ().enabled = false;

		DestroyGO ("zombieparent");
		DestroyGO ("zombie");
		DestroyGO ("car");
		DestroyGO ("coin");
		DestroyGO ("powerup");
		DestroyGO ("tnt");
		DestroyGO ("caution");

		Destroy (spawnZom);
		Destroy (spawnColl);

		Destroy (GameObject.Find ("EmptyBody"));
		Destroy (GameObject.Find ("Armature"));

		GameObject spawningZ = Instantiate (spawnZ, transform.position, Quaternion.identity)as GameObject;
		spawningZ.name = "SpawnZombies";
		GameObject spawningC = Instantiate (spawnC, transform.position, Quaternion.identity)as GameObject;
		spawningC.name = "SpawnCars";
		GameObject spawningP = Instantiate (spawnP, transform.position, Quaternion.identity)as GameObject;
		spawningP.name = "SpawnCollectables";

		GameObject newPlayer = Instantiate (mainPlayer, startTrans.position, Quaternion.Euler (startTrans.eulerAngles));
		newPlayer.name = "EmptyBody";
		currentPlayer = newPlayer;

		GameObject characterManage = GameObject.Find ("NewCharacterManager");

		characterManage.SendMessage ("InitCharacterReceive", currentPlayer.transform.Find("MicroMale").gameObject);
		characterManage.SendMessage ("InitWeaponReceive", currentPlayer.transform.Find("Armature/Root/Spine/Spine1/Spine2/RightShoulder/RightArm/WeaponR_Parent/WeaponMyCharacter").gameObject);

		currentPlayer.transform.Find ("PlayerIndicator").GetComponent<MeshRenderer> ().enabled = true;



		CameraAnimations.target = currentPlayer.transform.GetChild(0).GetChild(0).gameObject;
		FollowZ.target = currentPlayer.transform.GetChild (0).gameObject;
		selectCanvas.SetActive (false);
		CameraAnimations.ChangeCamera ("ingame");
		joystick.SetActive (true);
		if (actionButtons) {
			actionButtons.SetActive (true);
			currentPlayer.GetComponent<BotControlScript> ().ReceiveButtons (actionButtons.transform.GetChild (0).gameObject, actionButtons.transform.GetChild (1).gameObject);

		}
		selectCanvas.SetActive (false);
	
			currentPlayer.GetComponent<BotControlScript> ().enabled = true;
			currentPlayer.GetComponent<BotControlScript> ().ReceiveJoystick (joystick.transform.GetChild (1), joystick.transform.GetChild (0));
	}

	void DestroyGO(string tag){
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag (tag);

		foreach (GameObject go in gos) {
			Destroy (go);
		}

	}

	void EnableAlphaText(bool onoff, Text[] arr){
		for (int i = 0; i < arr.Length; i++) {
			arr [i].enabled = onoff;
		}
	}

	public void OverridePutBackPowerup(){
		LeanTween.cancel (powerupParent);
		currentPlayer.SendMessage ("StopLottery");
		LeanTween.moveY (powerupParent.GetComponent<RectTransform> (), 200f, .01f);
		for (int i = 0; i < powerupParent.transform.GetChild(0).childCount; i++) {
			powerupParent.transform.GetChild (0).GetChild(i).gameObject.SetActive (false);
		}

	}

	public void ToggleControls(bool act){
		joystick.SetActive (act);
	}


}
