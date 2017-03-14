using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.UI;
public class NetworkEngine_Archived : MonoBehaviour {
	bool setprices = false;
	public static NetworkEngine_Archived instance;
	public static Text coinText;
	public static int storedCost;
	public static string storedPurchasable;
	public static bool isGet;
	public static string results;
	public static bool isPurchasing=false;
	string storeid;
	string [] idSplit;
	public string myid;
	string hash;
	int increm;
	JSONObject myObject= new JSONObject();
	public static DataModel myDataModel = new DataModel();
	private string secretKey = "si2qpi34551jkiqofdjalj2obuslkc"; 
	WWW www;
	// Use this for initialization
	void Awake () {
		instance = this;
		coinText = GameObject.Find ("CoinText").GetComponent<Text> ();
		if (!PlayerPrefs.HasKey ("FirstLaunch")) {
			PlayerPrefs.SetInt ("FirstLaunch", 1);
			storeid = SystemInfo.deviceUniqueIdentifier;
			idSplit = storeid.Split (new string[] { "-" }, System.StringSplitOptions.None);

			print (idSplit);
			for (int i = 0; i < idSplit.Length; i++) {
				myid = myid + idSplit [i]; //each split
			}

			PlayerPrefs.SetString ("DeviceID", myid);
			hash = Md5Sum(PlayerPrefs.GetString ("DeviceID") + secretKey);
			JSONObject deviceIDObj= new JSONObject();
			deviceIDObj.AddField ("deviceID", hash);
			PostCharacterData (deviceIDObj);

		} else {
			isGet = true;
//			hash = Md5Sum(PlayerPrefs.GetString ("DeviceID") + secretKey);
//
//				var URL = "http://localhost:3000/api/products?deviceID=" + hash;
//			GET (URL, GetResult);
			hash = Md5Sum(PlayerPrefs.GetString ("DeviceID") + secretKey);
			JSONObject deviceIDObj= new JSONObject();
			deviceIDObj.AddField ("deviceID", hash);
			PostCharacterData (deviceIDObj);
		}

		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init (InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp ();


		}
	
		

	}
	void PostResult(string result){
		print (result);
		myDataModel =  JsonUtility.FromJson<DataModel>(result);
		myDataModel.ownedItems.Add ("Weapon001");
		if (myDataModel!=null) {
			UpdateSpendable ();
			print ("coins are: " + myDataModel.coins);
			if (!setprices) {
				setprices = true;
				GameObject.Find ("PartSelectionController").SendMessage ("SetPrices", myDataModel);
			}
		
		}

	}


	public static void UpdateSpendable(){
		coinText.text = myDataModel.coins.ToString ();

	}
	public static void EarnedCoin(int mult){
		myDataModel.coins+=mult;
		coinText.text = myDataModel.coins.ToString ();

	}

//	string ParseArray(string stringResult){
//		if (stringResult.EndsWith ("]")) {
//			stringResult = stringResult.Substring(1, stringResult.Length - 2);
//
//		}
//		return stringResult;
//	}

	void GetResult(string result){
		if(result.EndsWith("]")){
			result = result.Substring(1, result.Length - 2);
			print (result);
			myDataModel =  JsonUtility.FromJson<DataModel>(result);


		}
			else{
			myDataModel =  JsonUtility.FromJson<DataModel>(result);

			}
		if (myDataModel!=null) {
			print ("price one are: " + myDataModel.priceTierOne);
//			GameObject.Find ("PartSelectionController").SendMessage ("CheckPurchased", myDataModel);
		}
		print (result);
	}
	void Update(){

		if (Input.GetKeyUp (KeyCode.P)) {
			string myJ = JsonUtilString (myDataModel);
			JSONObject newJson = new JSONObject (myJ);
//			myJ = ParseArray (myJ);
			PostCharacterData (newJson);
		
		}
	}

	public void PostCharacterData(JSONObject j){
		
//		var URL = "http://ec2-54-165-56-14.compute-1.amazonaws.com:8080/api/products";

		var URL = "http://138.197.65.34:3000/api/products";

		POST (URL, j , PostResult);

	}
	public static void PostPurchase(JSONObject j){

		var URL = "http://138.197.65.34:3000/api/products";
		POST (URL, j , PurchaseResult);

	}
	public static void PurchaseResult(string result){
		print (result);
		myDataModel =  JsonUtility.FromJson<DataModel>(result);
		if (myDataModel!=null) {
			UpdateSpendable ();
			print ("coins are: " + myDataModel.coins);
			GameObject.Find ("PartSelectionController").SendMessage ("CheckDataAfterPost",PartSelectionController.purchaseKey);


		}

	}

	public WWW GET(string url, System.Action<string> onComplete ) {

//		Dictionary<string, string> headers = new Dictionary<string, string>();
//		headers.Add("Content-Type", "application/json");
//		byte[] body = System.Text.Encoding.UTF8.GetBytes(j.ToString());
		WWW www = new WWW( url );

		StartCoroutine (WaitForRequest (www, onComplete));
		return www;
	}



	public static WWW POST(string url,JSONObject j, System.Action<string> onComplete ) {
		WWWForm form = new WWWForm();
		print (url);

//		Hashtable postHeader = new Hashtable();
//		postHeader.Add("Content-Type", "application/json");
//


		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");
		if (isGet) {
			isGet = false;
			headers.Add ("isget", "true");
		}
//		j.AddField("deviceID", 	hash);

		print(j.Print (true));
		byte[] body = System.Text.Encoding.UTF8.GetBytes(j.ToString());


		WWW www = new WWW( url, body, headers );


		instance.StartCoroutine (WaitForRequest (www, onComplete));
		return www;
	}

	public static IEnumerator WaitForRequest(WWW www, System.Action<string> onComplete) {
		yield return www;
		// check for errors
		if (www.error == null) {
			isPurchasing = false;
			results = www.text;
			onComplete(results);
		} else {
			Debug.Log (www.error);
			if (isPurchasing) {
				isPurchasing = false;
				if (myDataModel != null) {
					NetworkEngine.myDataModel.coins += storedCost;
					storedCost = 0;
					NetworkEngine.myDataModel.ownedItems.Remove(storedPurchasable);
					PartSelectionController.purchaseKey = "";
					storedPurchasable = "";
				}
			}
		}
	}

	public  string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	public void FacebookLogin(){
		var perms = new List<string>(){"public_profile", "email"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}
	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;


			PlayerPrefs.SetString ("DeviceID", aToken.UserId.ToString());
//			string json = "{"+'"'+"deviceID"+'"'+":"+'"'+PlayerPrefs.GetString ("DeviceID")+'"'+"}";
//			print (json);
			hash = Md5Sum(PlayerPrefs.GetString ("DeviceID") + secretKey);
			JSONObject overwriteObj = new JSONObject();
			overwriteObj.AddField ("deviceID",hash);
			overwriteObj.AddField ("fID",hash);
			overwriteObj.AddField ("coins",myDataModel.coins);
			overwriteObj.AddField ("gems",myDataModel.gems);

			JSONObject arrOwned = new JSONObject(JSONObject.Type.ARRAY);
			JSONObject arrEnabled = new JSONObject(JSONObject.Type.ARRAY);

			for (int i = 0; i < myDataModel.ownedItems.Count; i++) {
				arrOwned.Add(myDataModel.ownedItems[i]);
			}
			for (int i = 0; i < myDataModel.enabledItems.Count; i++) {
				arrEnabled.Add(myDataModel.enabledItems[i]);
			}
			overwriteObj.AddField ("ownedItems",arrOwned);
			overwriteObj.AddField ("enabledItems",arrEnabled);

			print (overwriteObj.ToString ());
			PostCharacterData(overwriteObj);

			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);

			}

//			GET (URLString, DKHoldemLoginComplete);

			// Print current access token's granted permissions

		} else {
			Debug.Log("User cancelled login");
		}

//		DealwithFBMenus(FB.IsLoggedIn);

	}


	void DealwithFBMenus(bool isLoggedIn){

		if (isLoggedIn) {

			FB.API("/me?fields=first_name,last_name,id", HttpMethod.GET, DisplayName);
			FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, ProfilePic);


		} else {

		}

	}
	void DisplayName(IResult result){
		if (result.Error == null) {
			print(result.ResultDictionary ["first_name"].ToString());
			print(result.ResultDictionary ["last_name"].ToString()) ;

		} else {

		}
	}

	void ProfilePic(IGraphResult result){
		if (result.Texture != null) {
			Debug.Log ("got profilePic");


		} else {
			Debug.Log ("didn't get profilePic");

		}
	}

	DataModel JsonRetrieve(string myModel){
		return JsonUtility.FromJson<DataModel> (myModel);

	}
	string JsonUtilString(DataModel myModel){
		return JsonUtility.ToJson(myModel);

	}

}
