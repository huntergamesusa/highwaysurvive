using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCars : MonoBehaviour {
	public  List <Vector3> carPosL = new List<Vector3> ();
	public  List <Vector3> carPosR = new List<Vector3> ();

	public  List <Vector3> carPosAll = new List<Vector3> ();
	public static List <int> carListSpawned = new List<int> ();

	public GameObject cautionGO;



	public  List <GameObject> myCars = new List<GameObject> ();
	bool spawningL = false;
	bool spawningR = false;
	bool spawning = false;
	bool hornbool = false;
	AudioClip horn;
	AudioSource mysource;
	void Awake(){
		horn = Resources.Load ("Sounds/CAR_Horn_Short_5_Meters_Away_mono")as AudioClip;
		mysource = GetComponent<AudioSource> ();
		for (int i = 0; i < carPosAll.Count; i++) {
			carListSpawned.Add (i);
		}
	}

	// Use this for initialization
	void Update () {
		StartCoroutine (SpawnTraffic ());
//		StartCoroutine (SpawnTrafficL ());
//		StartCoroutine (SpawnTrafficR ());
		StartCoroutine (CarHorn ());

	}

	public IEnumerator CarHorn(){
		if (!hornbool) {
			hornbool = true;
			yield return new WaitForSeconds(Random.Range(2,4));
			mysource.pitch = Random.Range (1, 1.2f);
			var vol = Random.Range (.05f, .3f);
			mysource.PlayOneShot (horn, vol);
			yield return new WaitForSeconds (Random.Range(.2f,.3f));
			mysource.PlayOneShot (horn, vol);
			yield return new WaitForSeconds(Random.Range(2,4));
			hornbool = false;
		}

	}
	public IEnumerator SpawnTraffic(){
		if(!spawning){
			spawning = true;
		
				int carint = Random.Range (0, carListSpawned.Count);

			int randomCar =  Random.Range (0, myCars.Count);
			int myRot = 90;

		
			if (carListSpawned.Count > 0) {
				if (carListSpawned [carint] % 2 == 0) {
					myRot = 90;

				} else {
					myRot = -90;

				}
				GameObject myCar = Instantiate (myCars [randomCar], new Vector3 (carPosAll [carListSpawned [carint]].x, -0.1827998f, carPosAll [carListSpawned [carint]].z), Quaternion.Euler (0, myRot, 0)) as GameObject;
				myCar.name = carListSpawned [carint].ToString ();
				Instantiate (cautionGO, new Vector3 (cautionGO.transform.position.x, 0, carPosAll [carListSpawned [carint]].z), Quaternion.identity);
				carListSpawned.Remove (carListSpawned [carint]);
			}
			yield return new WaitForSeconds(Random.Range(PlayerPrefs.GetFloat("MinCarTime"),PlayerPrefs.GetFloat("MaxCarTime")));
			spawning = false;
		}
	}

	public IEnumerator SpawnTrafficL(){
		if(!spawningL){
			spawningL = true;
			int carLint = Random.Range (0, carPosL.Count);
			int randomCar =  Random.Range (0, myCars.Count);
			Instantiate (myCars [randomCar], new Vector3(carPosL [carLint].x,-0.1827998f,carPosL [carLint].z), Quaternion.Euler (0, -90, 0));
			Instantiate (cautionGO, new Vector3 (cautionGO.transform.position.x,0,carPosL [carLint].z), Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(PlayerPrefs.GetFloat("MinCarTime"),PlayerPrefs.GetFloat("MaxCarTime")));
				spawningL = false;
				}
	}
	public IEnumerator SpawnTrafficR(){
					if(!spawningR){
						spawningR = true;
	int carRint = Random.Range (0, carPosR.Count);
	int randomCar =  Random.Range (0, myCars.Count);
			Instantiate (myCars [randomCar], new Vector3(carPosR [carRint].x,-0.1827998f,carPosR [carRint].z), Quaternion.Euler (0, 90, 0));
			Instantiate (cautionGO, new Vector3 (cautionGO.transform.position.x,0,carPosR [carRint].z), Quaternion.identity);

			yield return new WaitForSeconds(Random.Range(PlayerPrefs.GetFloat("MinCarTime"),PlayerPrefs.GetFloat("MaxCarTime")));
	spawningR = false;
	}
	}
}
