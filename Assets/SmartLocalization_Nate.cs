using UnityEngine;
using System.Collections;
using SmartLocalization;
using UnityEngine.UI;
public class SmartLocalization_Nate : MonoBehaviour {
	public string stringKey;
	private string localizedString ;
	public static string RateBuddy;
	public static string RateToss;
	public static string rateBody;
	public static string rateNever;
	public static string rateLater;
	public static string rateNow;
	public static string cupsLeft;
	public static string bullsEyeTrans;
	public static string giftInTrans;
	public static string claimGiftTrans;
	public static string niceTossTrans;

	public static string bestScoreWindTrans;
	public static string bestScoreTNTTrans;
	public static string bestTimePongTrans;
	public static string justUnlockedTrans;
	// Use this for initialization
	void Awake () {
		LanguageManager languageManager = LanguageManager.Instance;
//		if (gameObject.name == "SmartLocalization_Nate") {
			languageManager.OnChangeLanguage += OnChangeLanguage;
//		}
		if (PlayerPrefs.GetString ("CurrentLanguage") == "") {
			PlayerPrefs.SetString ("CurrentLanguage", "en");
		} else {
			
		}
		languageManager.ChangeLanguage (PlayerPrefs.GetString ("CurrentLanguage"));



	}


	


	public void ChangetheLanguage(string language ){
		LanguageManager languageManager = LanguageManager.Instance;
		PlayerPrefs.SetString ("CurrentLanguage", language);
		languageManager.ChangeLanguage(PlayerPrefs.GetString ("CurrentLanguage"));
		languageManager.OnChangeLanguage += OnChangeLanguage;
	

	}


	void OnDestroy(){
		if (LanguageManager.HasInstance)
			LanguageManager.Instance.OnChangeLanguage -= OnChangeLanguage;
	}

	void OnChangeLanguage(LanguageManager thisLanguageManager){
		if (gameObject.GetComponent<Text> ()!=null) {
			localizedString = thisLanguageManager.GetTextValue(stringKey);
			gameObject.GetComponent<Text>().text = localizedString;

		}
//		RateBuddy = thisLanguageManager.GetTextValue("BT.buddy")+" " +thisLanguageManager.GetTextValue("BT.toss");
//		RateToss = thisLanguageManager.GetTextValue("BT.toss");
//		rateBody=thisLanguageManager.GetTextValue("BT.ratingbody");
//		rateNever=thisLanguageManager.GetTextValue("BT.ratenever");
//		rateLater=thisLanguageManager.GetTextValue("BT.ratelater");
//		rateNow=thisLanguageManager.GetTextValue("BT.ratenow");

//		giftInTrans=thisLanguageManager.GetTextValue("BT.giftin");
//		claimGiftTrans = thisLanguageManager.GetTextValue("BT.claimgift");
		giftInTrans = thisLanguageManager.GetTextValue("BT.giftin");

//		justUnlockedTrans=thisLanguageManager.GetTextValue("BT.justunlocked");



	}
}
