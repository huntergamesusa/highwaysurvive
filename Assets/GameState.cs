using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
	public GameObject currentPlayer;
	public Transform startTrans;
	public GameObject mainPlayer;
	public GameObject spawnZ;
	public GameObject spawnC;
	public GameObject spawnP;
	public GameObject joystick;
	public GameObject selectCanvas;

	void Awake(){
		
		Application.targetFrameRate = 60;
	}

	public void Restart(){
		print ("restart");
//		System.GC.Collect ();

		ScoringManager.ResetScore ();
		GameObject spawnZom = GameObject.Find ("SpawnZombies");
		spawnZom.GetComponent<SpawnZombies> ().enabled = false;
		GameObject spawnCar = GameObject.Find ("SpawnCars");
		spawnCar.GetComponent<SpawnCars> ().enabled = false;
		GameObject spawnColl = GameObject.Find ("SpawnCollectables");
		spawnColl.GetComponent<SpawnCollectables> ().enabled = false;

		DestroyGO ("zombieparent");
		DestroyGO ("zombie");

		DestroyGO ("car");
		DestroyGO ("coin");
		DestroyGO ("powerup");

		Destroy (spawnCar);
		Destroy (spawnZom);
		Destroy (spawnColl);

		Destroy (GameObject.Find ("EmptyBody"));
		Destroy (GameObject.Find ("Root_M"));
		GameObject newPlayer = Instantiate (mainPlayer, startTrans.position, Quaternion.Euler (startTrans.eulerAngles));
		newPlayer.name = "EmptyBody";
		currentPlayer = newPlayer;
		GameObject.Find ("PartSelectionController").SendMessage("RestartCharacter",currentPlayer.GetComponent<CharacterParts>());
		CameraAnimations.target = currentPlayer.transform.GetChild (1).gameObject;
		FollowZ.target = currentPlayer.transform.GetChild (0).gameObject;
		joystick.SetActive (false);
		selectCanvas.SetActive (true);
		CameraAnimations.ChangeCamera ("menu");

	}

	public void StartGame(){
//		System.GC.Collect ();

		ScoringManager.ResetScore ();
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
		Debug.Log (currentPlayer.name);
		currentPlayer.GetComponent<BotControlScript> ().ReceiveJoystick (joystick.transform.GetChild (1), joystick.transform.GetChild (0));

		currentPlayer.GetComponent<BotControlScript> ().enabled = true;

		CameraAnimations.ChangeCamera ("ingame");

	}

	public void ReplaySameGame(){
		print ("replay");
//		System.GC.Collect ();
		GameObject spawnZom = GameObject.Find ("SpawnZombies");
		spawnZom.GetComponent<SpawnZombies> ().enabled = false;
		GameObject spawnCar = GameObject.Find ("SpawnCars");
		spawnCar.GetComponent<SpawnCars> ().enabled = false;
		GameObject spawnColl = GameObject.Find ("SpawnCollectables");
		spawnColl.GetComponent<SpawnCollectables> ().enabled = false;

		DestroyGO ("zombieparent");
		DestroyGO ("zombie");
		DestroyGO ("car");
		DestroyGO ("coin");
		DestroyGO ("powerup");

		Destroy (spawnCar);
		Destroy (spawnZom);
		Destroy (spawnColl);

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
		currentPlayer = newPlayer;
		GameObject.Find ("PartSelectionController").SendMessage("RestartCharacter",currentPlayer.GetComponent<CharacterParts>());
		CameraAnimations.target = currentPlayer.transform.GetChild (1).gameObject;
		FollowZ.target = currentPlayer.transform.GetChild (0).gameObject;
		selectCanvas.SetActive (false);
		CameraAnimations.ChangeCamera ("ingame");
		joystick.SetActive (true);
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

}
