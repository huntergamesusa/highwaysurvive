using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class VideoAdManager : MonoBehaviour {

	private int rewardAmount = 100;
	public string zoneId;
	public TimeReward timeRewardScript;
	public GameOver myGameOver;
	public GameObject videoUIBar;
	public GameState myGameState;

	public void ShowVideoAd(){
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;
		Advertisement.Show ("rewardedVideo", options);
	}
	public void ShowVideoAdRetry(){
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleRetryResult;
		Advertisement.Show ("rewardedVideo", options);
	}

	private void HandleRetryResult (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			myGameOver.TurnOffContinue ();
			myGameState.ReplaySameGame (false);

			break;
		case ShowResult.Skipped:
			Debug.LogWarning ("Video was skipped.");

			break;
		case ShowResult.Failed:
			myGameOver.AnimateGameOver ();
			Debug.LogError ("Video failed to show.");
			break;
		}
	}

	private void HandleShowResult (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			timeRewardScript.SendMessage("CoinAnim",rewardAmount);
			ScoringManager.UpdateCoins (rewardAmount);
			myGameOver.GetCoinInfo (rewardAmount);
			videoUIBar.SetActive (false);
			Debug.Log ("Video completed. User rewarded " + rewardAmount + " credits.");
			break;
		case ShowResult.Skipped:
			Debug.LogWarning ("Video was skipped.");
			break;
		case ShowResult.Failed:
			Debug.LogError ("Video failed to show.");
			break;
		}
	}
}
