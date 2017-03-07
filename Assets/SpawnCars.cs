using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCars : MonoBehaviour {
	public  List <Vector3> carPosL = new List<Vector3> ();
	public  List <Vector3> carPosR = new List<Vector3> ();
	public  List <GameObject> myCars = new List<GameObject> ();
	bool spawningL = false;
	bool spawningR = false;
	bool hornbool = false;
	AudioClip horn;
	AudioSource mysource;
	void Awake(){
		horn = Resources.Load ("Sounds/CAR_Horn_Short_5_Meters_Away_mono")as AudioClip;
		mysource = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Update () {
		StartCoroutine (SpawnTrafficL ());
		StartCoroutine (SpawnTrafficR ());
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

	public IEnumerator SpawnTrafficL(){
		if(!spawningL){
			spawningL = true;
			int carLint = Random.Range (0, carPosL.Count);
			int randomCar =  Random.Range (0, myCars.Count);
			Instantiate (myCars [randomCar], new Vector3(carPosL [carLint].x,-0.1827998f,carPosL [carLint].z), Quaternion.Euler (0, -90, 0));
			yield return new WaitForSeconds(Random.Range(0.75f,PlayerPrefs.GetFloat("MaxCarTime")));
				spawningL = false;
				}
	}
	public IEnumerator SpawnTrafficR(){
					if(!spawningR){
						spawningR = true;
	int carRint = Random.Range (0, carPosR.Count);
	int randomCar =  Random.Range (0, myCars.Count);
			Instantiate (myCars [randomCar], new Vector3(carPosR [carRint].x,-0.1827998f,carPosR [carRint].z), Quaternion.Euler (0, 90, 0));
			yield return new WaitForSeconds(Random.Range(0.75f,3));
	spawningR = false;
	}
	}
}
