using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UniversalRateNate : MonoBehaviour {
	public string rateTitle;

	public string rateText;
	
	
	//example link to your app on android market
	#if UNITY_ANDROID
	public string rateUrl = "https://play.google.com/store/apps/details?id=com.HunterGames.BuddyToss";
	#endif
	#if UNITY_IOS
	public string rateUrl = "https://itunes.apple.com/us/app/buddy-toss/id980241548?mt=8";

	#endif
	public int plays;

	 int loadnbr;
	int multnbr;

	// Use this for initialization


	void Start () {

			
		plays = PlayerPrefs.GetInt("myAdFrequency");

		loadnbr=PlayerPrefs.GetInt ("loadcount");
		multnbr=PlayerPrefs.GetInt ("multcount");
		loadnbr++;
		PlayerPrefs.SetInt ("loadcount",loadnbr);
		
		
		//ShowBanner ();

		if (PlayerPrefs.GetInt ("myAdFrequency") > 0) {
			if (PlayerPrefs.GetInt ("loadcount") >= plays) {
			
				PlayerPrefs.SetInt ("loadcount", 0);
				if (PlayerPrefs.GetInt ("neverRemindRate") < 1) {
					print ("showing pop");
					RateDialogPopUp ();
				}

			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void RateDialogPopUp() {
		#if UNITY_ANDROID
		if(SmartLocalization_Nate.RateBuddy ==""){

			MobileNativeRateUs rate =new MobileNativeRateUs(rateTitle,rateText);
			rate.SetAndroidAppUrl(rateUrl);
//			rate.SetAndroidAppUrl(androidAppUrl);
			rate.OnComplete += OnRatePopUpClose;

			rate.Start();

		}
		else{
			MobileNativeRateUs rateME =new MobileNativeRateUs(SmartLocalization_Nate.RateBuddy,SmartLocalization_Nate.rateBody);
			rateME.SetAndroidAppUrl(rateUrl);
			//			rate.SetAndroidAppUrl(androidAppUrl);
			rateME.OnComplete += OnRatePopUpClose;

			rateME.Start();

		}

		#endif
		#if UNITY_IOS
		if(SmartLocalization_Nate.RateBuddy ==""){
			IOSRateUsPopUp rate = IOSRateUsPopUp.Create(rateTitle, rateText);
			rate.OnComplete += onRatePopUpClose;
		}
		else{
			IOSRateUsPopUp rateME = IOSRateUsPopUp.Create(SmartLocalization_Nate.RateBuddy, SmartLocalization_Nate.rateBody);
			rateME.OnComplete += onRatePopUpClose;
		}

	
		#endif
	}

	#if UNITY_ANDROID

	private void OnRatePopUpClose(MNDialogResult result) {
		
		switch(result) {
		case MNDialogResult.RATED:
			Debug.Log ("RATED button pressed");
			neverRemindRate();
			break;
		case MNDialogResult.REMIND:
			Debug.Log ("REMIND button pressed");
			PlayerPrefs.SetInt ("remind",0);
			break;
		case MNDialogResult.DECLINED:
			neverRemindRate();
			Debug.Log ("DECLINED button pressed");
			break;
			
		}
		
//		AN_PoupsProxy.showMessage("Result", result.ToString() + " button pressed");
	}
	#endif

	private void onRatePopUpClose(IOSDialogResult result) {
		switch(result) {
		case IOSDialogResult.RATED:
			Debug.Log ("RATED button pressed");
			neverRemindRate();
			break;
		case IOSDialogResult.REMIND:
			Debug.Log ("REMIND button pressed");
			PlayerPrefs.SetInt ("remind",0);
			break;
		case IOSDialogResult.DECLINED:
			neverRemindRate();
			Debug.Log ("DECLINED button pressed");
			break;

		}

	}

	void neverRemindRate(){

		PlayerPrefs.SetInt ("neverRemindRate", 1);
	}


}
