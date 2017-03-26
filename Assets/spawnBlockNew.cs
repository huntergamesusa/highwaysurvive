using UnityEngine;
using System.Collections;
using System.Linq;

public class spawnBlockNew : MonoBehaviour {

	float recordDirection;
	public bool isForward;

	public GameObject[] myBlocks;
	public GameObject[] myBlocksPos;

	bool canMoveBlock;


	private float spreadDistance;
	// Use this for initialization
	void Awake () {
		FindGO ("moveGround");
		sortMe ();
	}

	void FindGO(string tag){
		myBlocks = GameObject.FindGameObjectsWithTag (tag);
	}


	void FixedUpdate(){
		if (transform.position.x <= 0) {
			isForward = true;
		} else {
			if (recordDirection < transform.position.x) {
				recordDirection = transform.position.x;
				isForward = true;
			} else {
				isForward = false;

			}
		}

	}


	void OnTriggerExit(Collider coll){
		///NEED TO FIGURE OUT HOW TO ELIMATE WHEN GOING IN REVERSE POPS FORWARD
		/// 
		/// 
		/// 
		if (isForward) {
			if (coll.gameObject.tag == "moveGround") {

				coll.GetComponent<BoxCollider> ().enabled = false;

			myBlocksPos [0].transform.position = new Vector3 (myBlocksPos [myBlocksPos.Length - 1].transform.GetChild (0).transform.position.x, myBlocksPos [0].transform.position.y,myBlocksPos [0].transform.position.z );
				myBlocksPos [0].GetComponent<BoxCollider> ().enabled = true;

	
				sortMe ();

			}

		}

	}





	void sortMe(){

		myBlocksPos = myBlocks.OrderBy(myBlock => myBlock.transform.position.x).ToArray();


	}


}
