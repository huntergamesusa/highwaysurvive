using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectables : MonoBehaviour {
	public GameObject CoinGO;
	public GameObject PowerUpGO;
	public  Dictionary<string, int> positionCollectable = new Dictionary<string, int>() ;
	public  List <GameObject> activeCoins = new List<GameObject> ();
	public int randomCoin;
	bool delay=false;
	bool spawning=false;

	void Awake(){
		FindRoads ();

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
			InitCoin ();
			yield return new WaitForSeconds(Random.Range(1.5f,5));
			spawning = false;
		}
	}

	void Update(){
		if(Input.GetKeyUp(KeyCode.P)){
			InitCoin ();
		}
		StartCoroutine (DelayStart ());
		if (delay) {
			StartCoroutine (ZombieSpawnTimer ());
		}
	}

	void InitCoin(){
		int ranPU =  Random.Range(0,20);
		Vector3 locCoin = SpawnCoin ();
		GameObject spawnable;
		Quaternion myEuler;
		if (ranPU < 1) {
			spawnable = PowerUpGO;
			myEuler = Quaternion.Euler (0, 0, 0);
		} else {
			spawnable = CoinGO;
			myEuler = Quaternion.Euler  (0, 180, 180);

		}

		GameObject thisCoin = Instantiate (spawnable, locCoin,myEuler )as GameObject;
	
		thisCoin.name = "roadcoin" + randomCoin;
		activeCoins.Add (thisCoin);
		if (activeCoins.Count > 50) {

			DestroyImmediate (activeCoins [0], true);
			activeCoins.Remove (activeCoins [0]);

		}
	}


	Vector3 SpawnCoin() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("road");
		GameObject myCoin = null;
		Vector3 coinLocation;
		randomCoin = Random.Range (0, gos.Length);
		myCoin = gos [randomCoin];
		coinLocation = myCoin.GetComponent<MeshRenderer>().bounds.center;
		int temp;


		if(positionCollectable.TryGetValue("roadcoin"+randomCoin,out temp)){
			if (temp == 0) {
				positionCollectable.Remove ("roadcoin" + randomCoin);
				positionCollectable.Add ("roadcoin" + randomCoin, 1);


			} else {
				print ("reran spawn");
				DestroyImmediate (activeCoins [activeCoins.Count-1], true);
				activeCoins.Remove (activeCoins [activeCoins.Count-1]);
				InitCoin ();
			
			}
		}
			else{

			}

		return coinLocation;
	}

	 public void DestroyCoin(string mycoin){
		for(int i = 0; i < activeCoins.Count; i++)
		{				
			
			if(activeCoins[i].name == mycoin)
			{
				if (activeCoins [i] != null) {
					
					Destroy (activeCoins [i]);
					activeCoins.Remove (activeCoins [i]);
				}
				positionCollectable.Remove ("roadcoin" + randomCoin);
				positionCollectable.Add ("roadcoin" + randomCoin,0);

			}
		}




	}

	void FindRoads(){
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("road");
		int numRoads = 0;
		foreach (GameObject go in gos) {
			numRoads++;
			positionCollectable.Add ("roadcoin"+numRoads, 0);
		}

	}
}

