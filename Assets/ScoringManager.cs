using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[Serializable]

public class ScoresModel {
	public int killzombie = 100;
	public int killcar =100;

}

public class ScoringManager : MonoBehaviour {


	public static ScoresModel myScores  = new ScoresModel (); 
	public static Text ingamescore;
	public static int score;
	// Use this for initialization




	void Awake () {
		

		ingamescore = GameObject.Find ("GameScore").GetComponent<Text>();
		score = 0;
		ingamescore.text = score.ToString("#000000000");

	}
	
	// Update is called once per frame
	public static void UpdateScore (int mainscore, int mult) {
		print (mainscore);
		score += (mainscore * mult);
		ingamescore.text = score.ToString("#000000000");

	}
	public static void ResetScore () {
		score = 0;
		ingamescore.text = score.ToString("#000000000");

	}

}
