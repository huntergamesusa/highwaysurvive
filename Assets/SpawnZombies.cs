using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombies : MonoBehaviour {
	public GameObject[] zombies;
	public  Dictionary<string, int> positionZombies = new Dictionary<string, int>() ;
	public  List <GameObject> activeZombies = new List<GameObject> ();
	public int randomZombie;
	bool spawning;
	bool delay;
	void Awake(){
		FindRoads ();

	}

	void Update(){
		if(Input.GetKeyUp(KeyCode.Z)){
			InitZombie ();
		}
		StartCoroutine (DelayStart ());
		if (delay) {
			StartCoroutine (ZombieSpawnTimer ());
		}
	}

	public IEnumerator DelayStart(){
		if (!delay) {
			yield return new WaitForSeconds (5);

			delay = true;
		}
	}
	public IEnumerator ZombieSpawnTimer(){
		if(!spawning){
			spawning = true;
			InitZombie ();
			yield return new WaitForSeconds(Random.Range(1.5f,5));
			spawning = false;
		}
	}


	void InitZombie(){
		int ranPU =  Random.Range(0,5);
		Vector3 locCoin = SpawnZombie ();
		GameObject spawnable;
		Quaternion myEuler;
		if (ranPU < 1) {
			spawnable = zombies[0];
			myEuler = Quaternion.Euler (0, 0, 0);
		} else {
			spawnable = zombies[0];
			myEuler = Quaternion.Euler  (0, 0, 0);

		}

		GameObject thisCoin = Instantiate (spawnable, locCoin,myEuler )as GameObject;
	
		thisCoin.name = "myzombie" + randomZombie;
		activeZombies.Add (thisCoin);
		if (activeZombies.Count >6) {

			DestroyImmediate (activeZombies [0], true);
			activeZombies.Remove (activeZombies [0]);

		}
	}


	Vector3 SpawnZombie() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("road");
		GameObject thisZombie = null;
		Vector3 coinLocation;
		randomZombie = Random.Range (0, gos.Length);
		thisZombie = gos [randomZombie];
		coinLocation = thisZombie.GetComponent<MeshRenderer>().bounds.center;
		int temp;


		if(positionZombies.TryGetValue("myzombie"+randomZombie,out temp)){
			if (temp == 0) {
				positionZombies.Remove ("myzombie" + randomZombie);
				positionZombies.Add ("myzombie" + randomZombie, 1);


			} else {
				print ("reran spawn");
				DestroyImmediate (activeZombies [activeZombies.Count-1], true);
				activeZombies.Remove (activeZombies [activeZombies.Count-1]);
				InitZombie ();
			
			}
		}
			else{

			}

		return coinLocation;
	}

	public void DestroyZombie(string thisZombie){
		for(int i = 0; i < activeZombies.Count; i++)
		{				
			
			if(activeZombies[i].name == thisZombie)
			{
				print (activeZombies [i].name);
				if (activeZombies [i] != null) {
					Destroy (activeZombies [i]);
				
					activeZombies.Remove (activeZombies [i]);
				}
				positionZombies.Remove ("myzombie" + randomZombie);
				positionZombies.Add ("myzombie" + randomZombie,0);

			}
		}




	}

	void FindRoads(){
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("road");
		int numRoads = 0;
		foreach (GameObject go in gos) {
			numRoads++;
			positionZombies.Add ("myzombie"+numRoads, 0);
		}

	}
}

