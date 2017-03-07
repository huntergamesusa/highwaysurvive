using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
	GameObject currentPlayer;
	Transform startTrans;
	public GameObject mainPlayer;
	public GameObject spawnZ;
	public GameObject spawnC;
	public GameObject spawnP;
	public GameObject joystick;
	public GameObject selectCanvas;

	void Awake(){
		startTrans = mainPlayer.transform;
		Application.targetFrameRate = 60;
	}

	public void Restart(){
		ScoringManager.ResetScore ();
		GameObject spawnZom = GameObject.Find ("SpawnZombies");
		spawnZom.GetComponent<SpawnZombies> ().enabled = false;
		GameObject spawnCar = GameObject.Find ("SpawnCars");
		spawnCar.GetComponent<SpawnCars> ().enabled = false;
		GameObject spawnColl = GameObject.Find ("SpawnCollectables");
		spawnColl.GetComponent<SpawnCollectables> ().enabled = false;

		DestroyGO ("zombie");
		DestroyGO ("car");
		DestroyGO ("coin");
		DestroyGO ("powerup");

		Destroy (spawnCar);
		Destroy (spawnZom);

		Destroy (GameObject.Find ("EmptyBody"));
		Destroy (GameObject.Find ("Root_M"));
		GameObject newPlayer = Instantiate (mainPlayer, startTrans.position, Quaternion.Euler (startTrans.eulerAngles));
		newPlayer.name = "EmptyBody";
		GameObject.Find ("PartSelectionController").SendMessage("RestartCharacter",newPlayer.GetComponent<CharacterParts>());
		CameraAnimations.target = newPlayer.transform.GetChild (1).gameObject;
		FollowZ.target = newPlayer.transform.GetChild (0).gameObject;
		selectCanvas.SetActive (true);
		CameraAnimations.ChangeCamera ("menu");

	}

	public void StartGame(){
		ScoringManager.ResetScore ();
		currentPlayer = GameObject.Find ("EmptyBody");
		GameObject.Find ("PartSelectionController").SendMessage("RestartCharacter",currentPlayer.GetComponent<CharacterParts>());
//		GameObject.Find ("SpawnZombies").GetComponent<SpawnZombies> ().enabled = true;
//		GameObject.Find ("SpawnCars").GetComponent<SpawnCars> ().enabled = true;
		GameObject spawningZ = Instantiate (spawnZ, transform.position, Quaternion.identity)as GameObject;
		spawningZ.name = "SpawnZombies";
		GameObject spawningC = Instantiate (spawnC, transform.position, Quaternion.identity)as GameObject;
		spawningC.name = "SpawnCars";
		GameObject spawningP = Instantiate (spawnP, transform.position, Quaternion.identity)as GameObject;
		spawningP.name = "SpawnCollectables";
		joystick.SetActive (true);
		selectCanvas.SetActive (false);
		if (currentPlayer != null) {
			currentPlayer.GetComponent<BotControlScript> ().enabled = true;
			currentPlayer.GetComponent<BotControlScript> ().ReceiveJoystick (joystick.transform.GetChild (1).gameObject, joystick.transform.GetChild (0).gameObject);
		}
		CameraAnimations.ChangeCamera ("ingame");

	}

	public void ReplaySameGame(){
		GameObject spawnZom = GameObject.Find ("SpawnZombies");
		spawnZom.GetComponent<SpawnZombies> ().enabled = false;
		GameObject spawnCar = GameObject.Find ("SpawnCars");
		spawnCar.GetComponent<SpawnCars> ().enabled = false;
		GameObject spawnColl = GameObject.Find ("SpawnCollectables");
		spawnColl.GetComponent<SpawnCollectables> ().enabled = false;

		DestroyGO ("zombie");
		DestroyGO ("car");
		DestroyGO ("coin");
		DestroyGO ("powerup");

		Destroy (spawnCar);
		Destroy (spawnZom);

		Destroy (GameObject.Find ("EmptyBody"));
		Destroy (GameObject.Find ("Root_M"));

		GameObject spawningZ = Instantiate (spawnZ, transform.position, Quaternion.identity)as GameObject;
		spawningZ.name = "SpawnZombies";
		GameObject spawningC = Instantiate (spawnC, transform.position, Quaternion.identity)as GameObject;
		spawningC.name = "SpawnCars";
		GameObject spawningP = Instantiate (spawnP, transform.position, Quaternion.identity)as GameObject;
		spawningP.name = "SpawnCollectables";

		GameObject newPlayer = Instantiate (mainPlayer, startTrans.position, Quaternion.Euler (startTrans.eulerAngles));
		newPlayer.name = "EmptyBody";
		GameObject.Find ("PartSelectionController").SendMessage("RestartCharacter",newPlayer.GetComponent<CharacterParts>());
		CameraAnimations.target = newPlayer.transform.GetChild (1).gameObject;
		FollowZ.target = newPlayer.transform.GetChild (0).gameObject;
		selectCanvas.SetActive (false);
		CameraAnimations.ChangeCamera ("ingame");
		joystick.SetActive (true);
		selectCanvas.SetActive (false);
		if (newPlayer != null) {
			newPlayer.GetComponent<BotControlScript> ().enabled = true;
			newPlayer.GetComponent<BotControlScript> ().ReceiveJoystick (joystick.transform.GetChild (1).gameObject, joystick.transform.GetChild (0).gameObject);
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
