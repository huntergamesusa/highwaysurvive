using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using TMPro;
public class NewCharacterManager : MonoBehaviour,IStoreListener {
	GameObject mainCharacter;
	GameObject mainWeapon;
	[Header("Reward Objects")]
	public GameObject rewardCharacter;
	public GameObject rewardWeapon;
	public GameObject rewardCanvas;
	public GameObject flashImage;
	public TextMeshProUGUI rewardName;
	public Camera giftCam;
	bool rewardisCharc;
	string rewardString;
	[Header("Other Objects")]
	public GameObject[] myMeshGO;
	// Use this for initialization
	public  List<Material> allResourcesMaterial = new List<Material>();
	public  List<GameObject> allResourcesGO = new List<GameObject>();

	public static Dictionary <Material, Mesh> myCharacters = new Dictionary<Material, Mesh> ();
	public static List<Material> myCharacterMat = new List<Material> ();
	public static List<string> myCharacterSKU = new List<string> ();
	public static List<float> myCharacterPrices = new List<float> ();
	public static List<string> myCharacterTitles = new List<string> ();

	public static Dictionary <Material, Mesh> myWeapons= new Dictionary<Material, Mesh> ();
	public static List<Material> myWeaponMat = new List<Material> ();
	public static List<string> myWeaponSKU = new List<string> ();
	public static List<string> myWeaponTitles = new List<string> ();
	public static List<float> myWeaponPrices = new List<float> ();

	JSONObject allskus = new JSONObject ();
	GameObject goMakingPurchase;
	int index;
	int indexWeapon;

	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
	private static string kProductIDNonConsumable;
	void Awake(){
		allResourcesMaterial.AddRange (Resources.LoadAll<Material> (""));
		allResourcesGO.AddRange (Resources.LoadAll<GameObject> (""));
		JSONObject C = new JSONObject ();
		JSONObject Ct = new JSONObject ();

		JSONObject W = new JSONObject ();
		JSONObject Wt = new JSONObject ();

		allskus.AddField ("Weapons", W);
		allskus.AddField ("WeaponsTitles", Wt);

		allskus.AddField ("Characters", C);
		allskus.AddField ("CharactersTitles", Ct);

		foreach (Material obj in allResourcesMaterial) {
			if (obj.name.Contains ("_MAT")) {
				
				string mystring;
				mystring = obj.name;
				string[] objstring = mystring.Split ('_'); //Here we assing the splitted string to array by that char
//			
//				myCharacters.Add (obj, int.Parse (objstring [0]));
				myCharacterMat.Add (obj);
				myCharacterPrices.Add (0.99f);
				myCharacterSKU.Add (objstring [1]);
				string characTitle;
				characTitle = ParseString (obj.mainTexture.name);
				myCharacterTitles.Add (characTitle);
				C.Add (objstring [1]);
				Ct.Add (characTitle);

				myCharacters.Add (obj, SendMesh (objstring [1]));
			}
			if (obj.name.Contains ("_WEAPON")) {


				//				myCharacters.Add (obj, int.Parse (objstring [0]));
				myWeaponMat.Add (obj);
				myWeaponPrices.Add (0.99f);
				foreach (GameObject objGO in allResourcesGO) {


						if (objGO.name == obj.name) {
						string mystringSKU;
						mystringSKU = objGO.name;
						string[] objstringSKU= mystringSKU.Split ('_');
						myWeaponSKU.Add (objstringSKU [1]);
						string weapTitle;
						weapTitle = ParseString(obj.mainTexture.name);
						myWeaponTitles.Add (weapTitle);
						Wt.Add (weapTitle);
						W.Add (objstringSKU [1]);

						print (objGO.GetComponent<MeshFilter> ().sharedMesh);
						myWeapons.Add (obj, objGO.GetComponent<MeshFilter>().sharedMesh);
						print (obj.name);

						}
					
				}


			}
		}
		print(allskus.Print (true));
	}



	string ParseString(string mystring){
		if (mystring.Contains ("_")) {
			mystring = mystring.Replace ("_", " ");
		}

		return mystring;

	}

//		myCharacterMat.Sort ();
//		print (myCharacters.Comparer(1));
//		Material = myCharacters.ToList ();




	
	Mesh SendMesh(string matname){

						switch(matname){
		case "snowy":
			return myMeshGO[7].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "archer":
			return myMeshGO[2].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "roxy":
			return myMeshGO[2].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "teachy":
			return myMeshGO[2].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "rumbo":
			return myMeshGO[0].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "monster":
			return myMeshGO[0].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "yeti":
			return myMeshGO[0].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "santa":
			return myMeshGO[5].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "pumpy":
			return myMeshGO[4].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "darkwizard":
			return myMeshGO[6].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "wizard":
			return myMeshGO[6].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "hawkman":
			return myMeshGO[8].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "elfy":
			return myMeshGO[3].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
						}
		return myMeshGO[0].GetComponent<SkinnedMeshRenderer> ().sharedMesh;


					}


	void Start () {
		// If we haven't set up the Unity Purchasing reference
	
		if (m_StoreController == null)
		{
			// Begin to configure our connection to Purchasing
			InitializePurchasing();
		}

		if (!PlayerPrefs.HasKey ("MyCharacter")) {
			PlayerPrefs.SetInt (myWeaponSKU [0], 1);
			PlayerPrefs.SetInt (myCharacterSKU [0], 1);

			SelectCharacter (myCharacterSKU [0]);
			SelectWeapon (myCharacterSKU [0]);
		} else {
			InitCharacter ();
			InitWeapon ();

		}


	
//		mainCharacter.GetComponent<SkinnedMeshRenderer>().BakeMesh = myCharacters.
	}

	public void InitCharacter(){
		if (!mainCharacter) {
			mainCharacter = GameObject.Find ("MicroMale");
		}
		for (int i = 0; i < myCharacterMat.Count; i++) {
			if (myCharacterMat [i].name.Contains (PlayerPrefs.GetString ("MyCharacter"))) {
				index = i;
			}
		}

		mainCharacter.GetComponent<SkinnedMeshRenderer> ().material = myCharacterMat[index];
		foreach(KeyValuePair<Material,Mesh> keyValue in myCharacters)
		{
			if (myCharacterMat [index] == keyValue.Key) {
				mainCharacter.GetComponent<SkinnedMeshRenderer> ().sharedMesh = keyValue.Value;
			}
		}
			


	}

	void Update(){
		if(Input.GetKeyUp(KeyCode.Y)){
			RewardPrizeRandom();

		}
	}

	void FlashOff(){

		flashImage.SetActive (false);

	}

	public void RewardPrizeRandom(){
		if (PlayerPrefs.GetInt ("Coins") >= 100) {
			flashImage.SetActive (true);
			var myColor = flashImage.GetComponent<Image> ().color;
			flashImage.GetComponent<Image> ().color = new Color (myColor.r, myColor.g, myColor.b, 1);
			LeanTween.alpha (flashImage.GetComponent<RectTransform> (), 0, .3f).setOnComplete (FlashOff);
			rewardCanvas.SetActive (true);
			;
			int randomType = UnityEngine.Random.Range (0, 100);

			if (randomType > 50) {
				rewardWeapon.SetActive (false);
				rewardCharacter.transform.parent.gameObject.SetActive (true);
				int ranCharacterGift = UnityEngine.Random.Range (1, myCharacterSKU.Count);
				rewardName.text = myCharacterTitles [ranCharacterGift];
				rewardisCharc = true;
				giftCam.enabled = true;
				PlayerPrefs.SetInt (myCharacterSKU [ranCharacterGift], 1);
				rewardString = myCharacterSKU [ranCharacterGift];
				rewardCharacter.GetComponent<SkinnedMeshRenderer> ().material = myCharacterMat [ranCharacterGift];
				foreach (KeyValuePair<Material,Mesh> keyValue in myCharacters) {
					if (myCharacterMat [ranCharacterGift] == keyValue.Key) {
						rewardCharacter.GetComponent<SkinnedMeshRenderer> ().sharedMesh = keyValue.Value;
					}
				}
			} else {
				rewardCharacter.transform.parent.gameObject.SetActive (false);
				rewardWeapon.SetActive (true);

				int ranWeaponGift = UnityEngine.Random.Range (1, myWeaponSKU.Count);
				rewardName.text = myWeaponTitles [ranWeaponGift];
				giftCam.enabled = true;
				PlayerPrefs.SetInt (myWeaponSKU [ranWeaponGift], 1);
				rewardString = myWeaponSKU [ranWeaponGift];
				rewardisCharc = false;
				rewardWeapon.GetComponent<MeshRenderer> ().material = myWeaponMat [ranWeaponGift];
				foreach (KeyValuePair<Material,Mesh> keyValue in myWeapons) {
					if (myWeaponMat [ranWeaponGift] == keyValue.Key) {
						rewardWeapon.GetComponent<MeshFilter> ().mesh = keyValue.Value;
					}
				}


			}
			ScoringManager.UpdateCoins (-100);
		}
	}

	public void EquiptReward(){
		if (rewardisCharc) {
			SelectCharacter (rewardString);
		} else {
			SelectWeapon (rewardString);

		}
	}

	public void InitWeapon(){
		if (!mainWeapon) {
			mainWeapon = GameObject.Find ("WeaponMyCharacter");
		}

		for (int i = 0; i < myWeaponMat.Count; i++) {
			if (myWeaponMat [i].name.Contains (PlayerPrefs.GetString ("MyWeapon"))) {
				indexWeapon = i;
			}
		}

		mainWeapon.GetComponent<MeshRenderer> ().material = myWeaponMat[indexWeapon];
		foreach(KeyValuePair<Material,Mesh> keyValue in myWeapons)
		{
			if (myWeaponMat [indexWeapon] == keyValue.Key) {
				mainWeapon.GetComponent<MeshFilter> ().mesh = keyValue.Value;
			}
		}


	}


	public void InitCharacterReceive(GameObject myGO){
		print ("checking to reinstate character skin");
		mainCharacter = myGO;
		InitCharacter ();
		InitWeapon ();

	}
	public void InitWeaponReceive(GameObject myGO){
		print ("checking to reinstate weapon skin");
		mainWeapon = myGO;
		InitWeapon ();

	}

	public void SelectCharacter(string selectedCharacter){
		PlayerPrefs.SetString ("MyCharacter", selectedCharacter);
		InitCharacter ();

	}
	public void SelectWeapon(string selectedWeapon){
		PlayerPrefs.SetString ("MyWeapon", selectedWeapon);

		InitWeapon ();
	}


	public void SetMaterial(bool forward){
		if (forward) {
			index++;
			if (index >= myCharacterMat.Count) {
				index = 0;
			}

		} else {
			index--;
			if (index == -1) {
				index = myCharacterMat.Count-1;
			}

		}
		print (index);
		mainCharacter.GetComponent<SkinnedMeshRenderer> ().material = myCharacterMat [index];
		foreach(KeyValuePair<Material,Mesh> keyValue in myCharacters)
		{
			if (myCharacterMat [index] == keyValue.Key) {
				mainCharacter.GetComponent<SkinnedMeshRenderer> ().sharedMesh = keyValue.Value;
			}
		}
	}

	void SetInitialOwned(){
		if (!PlayerPrefs.HasKey ("FirstBoot")) {
			PlayerPrefs.SetInt ("FirstBoot", 1);

		}
	}

	public void ReceiveCharacter(GameObject myGO){
		mainCharacter = myGO;
	}





	public void InitializePurchasing() 
	{
		// If we have already connected to Purchasing ...
		if (IsInitialized())
		{
			// ... we are done here.
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		// Add a product to sell / restore by way of its identifier, associating the general identifier
		// with its store-specific identifiers.
		// Continue adding the non-consumable product.
		for (int i = 0; i < myWeaponSKU.Count; i++) {
			builder.AddProduct (myWeaponSKU[i], ProductType.NonConsumable);
			print(myWeaponSKU [i]);
		}
		for (int i = 0; i < myCharacterSKU.Count; i++) {
			builder.AddProduct (myCharacterSKU[i], ProductType.NonConsumable);
		}

		// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
		// and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize(this, builder);
	}


	private bool IsInitialized()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}


	public void BuyProductID(string productId,GameObject askingGO)
	{
		goMakingPurchase = askingGO;
		kProductIDNonConsumable = productId;

		// If Purchasing has been initialized ...
		if (IsInitialized())
		{
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = m_StoreController.products.WithID(productId);

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				m_StoreController.InitiatePurchase(product);
			}
			// Otherwise ...
			else
			{
				// ... report the product look-up failure situation  
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		// Otherwise ...
		else
		{
			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
			// retrying initiailization.
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}


	// Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
	// Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
	public void RestorePurchases()
	{
		// If Purchasing has not yet been set up ...
		if (!IsInitialized())
		{
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}

		// If we are running on an Apple device ... 
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
			Application.platform == RuntimePlatform.OSXPlayer)
		{
			// ... begin restoring purchases
			Debug.Log("RestorePurchases started ...");

			// Fetch the Apple store-specific subsystem.
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			// Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
			// the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
			apple.RestoreTransactions((result) => {
				// The first phase of restoration. If no more responses are received on ProcessPurchase then 
				// no purchases are available to be restored.
				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		// Otherwise ...
		else
		{
			// We are not running on an Apple device. No work is necessary to restore purchases.
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}


	//  
	// --- IStoreListener
	//

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		print (args.purchasedProduct.definition.id);
		// Or ... a non-consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
		{
			PlayerPrefs.SetInt (kProductIDNonConsumable, 1);
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			// TODO: The non-consumable item has been successfully purchased, grant this item to the player.
			goMakingPurchase.SendMessage("PurchaseSuccess");
		}
	
		// Or ... an unknown product has been purchased by this user. Fill in additional products here....
		else 
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		// Return a flag indicating whether this product has completely been received, or if the application needs 
		// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
		// saving purchased products to the cloud, and when that save is delayed. 
		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
		// this reason with the user to guide their troubleshooting actions.
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}




