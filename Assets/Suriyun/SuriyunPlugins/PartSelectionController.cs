using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PartSelectionController : MonoBehaviour
{
	public GameObject headCostButton;
	public GameObject hairCostButton;
	public GameObject headAccCostButton;
	public GameObject outfitCostButton;
	public GameObject weaponCostButton;
	public GameObject shieldCostButton;
	public static int purchaseIndex;
	public static string purchaseKey;

	public static bool headAvailable;
	public static bool hairAvailable;
	public static bool headAccAvailable;
	public static bool outfitAvailable;
	public static bool weaponAvailable;
	public static bool shieldAvailable;

    public CharacterParts characterParts;
    public Text headIndexText;
    public Text hairIndexText;
    public Text headAccesoriesIndexText;
    public Text leftShoulderIndexText;
    public Text leftElbowIndexText;
    public Text leftWeaponIndexText;
    public Text leftShieldIndexText;
    public Text rightShoulderIndexText;
    public Text rightElbowIndexText;
    public Text rightWeaponIndexText;
    public Text chestIndexText;
    public Text spineIndexText;
    public Text lowerSpineIndexText;
    public Text leftHipIndexText;
    public Text leftKneeIndexText;
    public Text rightHipIndexText;
    public Text rightKneeIndexText;
    public Text armPartsIndexText;
    public Text legPartsIndexText;


    PartsCollections collections;
    int headIndex;
    int hairIndex;
    int headAccesoriesIndex;
    int leftShoulderIndex;
    int leftElbowIndex;
    int leftWeaponIndex;
    int leftShieldIndex;
    int rightShoulderIndex;
    int rightElbowIndex;
    int rightWeaponIndex;
    int chestIndex;
    int spineIndex;
    int lowerSpineIndex;
    int leftHipIndex;
    int leftKneeIndex;
    int rightHipIndex;
    int rightKneeIndex;
    int armPartsIndex;
    int legPartsIndex;


    List<string> headKeys = new List<string>();
    List<string> hairKeys = new List<string>();
    List<string> headAccesoriesKeys = new List<string>();
    List<string> shoulderKeys = new List<string>();
    List<string> elbowKeys = new List<string>();
    List<string> weaponKeys = new List<string>();
    List<string> shieldKeys = new List<string>();
    List<string> chestKeys = new List<string>();
    List<string> spineKeys = new List<string>();
    List<string> lowerSpineKeys = new List<string>();
    List<string> hipKeys = new List<string>();
    List<string> kneeKeys = new List<string>();
    List<string> armPartsKeys = new List<string>();
    List<string> legPartsKeys = new List<string>();


	public Dictionary<string, int> headPrices = new Dictionary<string, int>();
	public Dictionary<string, int> hairPrices = new Dictionary<string, int>();
	public Dictionary<string, int> headAccessPrices = new Dictionary<string, int>();
	public Dictionary<string, int> outfitPrices = new Dictionary<string, int>();
	public Dictionary<string, int> weaponPrices = new Dictionary<string, int>();
	public Dictionary<string, int> shieldPrices = new Dictionary<string, int>();

	int count=0;

    public void Init(PartsCollections collections)
	{
		characterParts = GameObject.Find ("EmptyBody").GetComponent<CharacterParts> ();
        this.collections = collections;
        if (collections != null)
        {
            headKeys.AddRange(collections.headObjects.Keys);
            hairKeys.AddRange(collections.hairObjects.Keys);
            headAccesoriesKeys.AddRange(collections.headAccesoriesObjects.Keys);
            shoulderKeys.AddRange(collections.shoulderObjects.Keys);
            elbowKeys.AddRange(collections.elbowObjects.Keys);
            weaponKeys.AddRange(collections.weaponObjects.Keys);
            shieldKeys.AddRange(collections.shieldObjects.Keys);
            chestKeys.AddRange(collections.chestObjects.Keys);
            spineKeys.AddRange(collections.spineObjects.Keys);
            lowerSpineKeys.AddRange(collections.lowerSpineObjects.Keys);
            hipKeys.AddRange(collections.hipObjects.Keys);
            kneeKeys.AddRange(collections.kneeObjects.Keys);
            armPartsKeys.AddRange(collections.armParts.Keys);
            legPartsKeys.AddRange(collections.legParts.Keys);
            // Init neccesary parts
//            ChangeHeadPart();
//            ChangeHairPart();
//            ChangeChestPart();
//            ChangeSpinePart();
//            ChangeLowerSpinePart();
//            ChangeArmParts();
//            ChangeLegParts();

//			EnableHeadPart ();
//			EnableHairPart();
//			EnableHeadAccPart ();
//			EnableOutfitParts ();
//			EnableWeaponParts ();
//			EnableShieldParts ();
			EnableCharacterSelection();
			UpdateStoreUI ();
        }
    }

	public void RestartCharacter(CharacterParts theparts){
		characterParts = theparts;
		EnableCharacterSelection();

	}

	public void SetPrices(DataModel myModel){
		float testFloat;

		foreach (string obj in headKeys) {
			string newString = obj.Substring (0, 3);
			float.TryParse (newString, out testFloat);
			testFloat /= 100;

			if (testFloat < .11f) {
				headPrices.Add (collections.GetHead (headKeys [count]).name, myModel.priceTierOne);

			}
			if (testFloat >= .11f&&testFloat < .22f) {
				headPrices.Add (collections.GetHead (headKeys [count]).name, myModel.priceTierTwo);

			}
			if (testFloat >= .22f) {
				headPrices.Add (collections.GetHead (headKeys [count]).name, myModel.priceTierThree);

			}
//			thisKey(collections.GetHead (headKeys [count]).name,headPrices);
			count++;
		}
		count = 0;
		foreach (string obj in hairKeys) {
			string newString = obj.Substring (0, 3);
			float.TryParse (newString, out testFloat);
			testFloat /= 100;

			if (testFloat < .11f) {
				hairPrices.Add (collections.GetHair (hairKeys [count]).name, myModel.priceTierOne);

			}
			if (testFloat >= .11f&&testFloat < .22f) {
				hairPrices.Add (collections.GetHair (hairKeys [count]).name, myModel.priceTierTwo);

			}
			if (testFloat >= .22f) {
				hairPrices.Add (collections.GetHair (hairKeys [count]).name, myModel.priceTierThree);

			}
//			thisKey(collections.GetHair (hairKeys [count]).name,hairPrices);
			count++;
		}
		count = 0;
		foreach (string obj in chestKeys) {
			string newString = obj.Substring (0, 3);
			float.TryParse (newString, out testFloat);
			testFloat /= 100;

			if (testFloat < .11f) {
				outfitPrices.Add (collections.GetChest (chestKeys [count]).name, myModel.priceTierOne);

			}
			if (testFloat >= .11f&&testFloat < .22f) {
				outfitPrices.Add (collections.GetChest (chestKeys [count]).name, myModel.priceTierTwo);

			}
			if (testFloat >= .22f) {
				outfitPrices.Add (collections.GetChest (chestKeys [count]).name, myModel.priceTierThree);

			}
//			thisKey(collections.GetChest (chestKeys [count]).name,outfitPrices);
			count++;
		}
		count = 0;
		foreach (string obj in headAccesoriesKeys) {
			string newString = obj.Substring (0, 3);
			float.TryParse (newString, out testFloat);
			testFloat /= 100;

			if (testFloat < .11f) {
				headAccessPrices.Add (collections.GetHeadAccesories (headAccesoriesKeys [count]).name, myModel.priceTierOne);

			}
			if (testFloat >= .11f&&testFloat < .22f) {
				headAccessPrices.Add (collections.GetHeadAccesories (headAccesoriesKeys [count]).name, myModel.priceTierTwo);

			}
			if (testFloat >= .22f) {
				headAccessPrices.Add (collections.GetHeadAccesories (headAccesoriesKeys [count]).name, myModel.priceTierThree);

			}
//			thisKey(collections.GetHeadAccesories (headAccesoriesKeys [count]).name,headAccessPrices);
			count++;
		}
		count = 0;
		foreach (string obj in weaponKeys) {
			string newString = obj.Substring (0, 3);
			float.TryParse (newString, out testFloat);
			testFloat /= 100;

			if (testFloat < .11f) {
				weaponPrices.Add (collections.GetWeapon (weaponKeys [count]).name, myModel.priceTierOne);

			}
			if (testFloat >= .11f&&testFloat < .22f) {
				weaponPrices.Add (collections.GetWeapon (weaponKeys [count]).name, myModel.priceTierTwo);

			}
			if (testFloat >= .22f) {
				weaponPrices.Add (collections.GetWeapon (weaponKeys [count]).name, myModel.priceTierThree);

			}
//			thisKey(collections.GetWeapon (weaponKeys [count]).name,weaponPrices);
			count++;
		}
		count = 0;
		foreach (string obj in shieldKeys) {
			string newString = obj.Substring (0, 3);
			float.TryParse (newString, out testFloat);
			testFloat /= 100;

			if (testFloat < .11f) {
				shieldPrices.Add (collections.GetShield (shieldKeys [count]).name, myModel.priceTierOne);

			}
			if (testFloat >= .11f&&testFloat < .22f) {
				shieldPrices.Add (collections.GetShield (shieldKeys [count]).name, myModel.priceTierTwo);

			}
			if (testFloat >= .22f) {
				shieldPrices.Add (collections.GetShield (shieldKeys [count]).name, myModel.priceTierThree);

			}
//			thisKey(collections.GetShield (shieldKeys [count]).name,shieldPrices);
			count++;
		}

		CheckDataAfterPost ("");


	
	}

	void CheckDataAfterPost(string checkString){
//
//		switch (checkString) {
//		case "head":
//			PlayerPrefs.SetInt ("Head", purchaseIndex);
//			break;
//		case "hair":
//			PlayerPrefs.SetInt ("Hair", purchaseIndex);
//
//			break;
//		case "headaccess":
//			PlayerPrefs.SetInt ("HairAccess", purchaseIndex);
//
//			break;
//		case "outfit":
//			PlayerPrefs.SetInt ("Outfit", purchaseIndex);
//
//			break;
//		case "weapon":
//			PlayerPrefs.SetInt ("Weapon", purchaseIndex);
//
//			break;
//		case "shield":
//			PlayerPrefs.SetInt ("Shield", purchaseIndex);
//
//			break;
//		default:
//			break;
//		
//		}

//
//		if (!PlayerPrefs.HasKey ("Head")) {
//			PlayerPrefs.SetInt ("Head", 0);
//		}
//		if (!PlayerPrefs.HasKey ("Outfit")) {
//			PlayerPrefs.SetInt ("Outfit", 0);
//		}
//		if (!PlayerPrefs.HasKey ("Weapon")) {
//			PlayerPrefs.SetInt ("Weapon", 0);
	
//		}
//		CheckHeadData (collections.GetHead (headKeys [PlayerPrefs.GetInt ("Head")]).name);
//		CheckOutfitData (collections.GetChest (chestKeys [PlayerPrefs.GetInt ("Outfit")]).name);
//
//
//
//		if (PlayerPrefs.HasKey ("Hair")) {
//			CheckHairData (collections.GetHair (hairKeys [PlayerPrefs.GetInt ("Hair")]).name);
//
//		} else {
//			CheckHairData (collections.GetHair (hairKeys [hairIndex]).name);
//		}
//		if (PlayerPrefs.HasKey ("HairAccess")) {
//			CheckHeadAccData (collections.GetHeadAccesories (headAccesoriesKeys [PlayerPrefs.GetInt ("HairAccess")]).name);
//
//		} else {
//			CheckHeadAccData (collections.GetHeadAccesories (headAccesoriesKeys [headAccesoriesIndex]).name);
//
//
//		}
//		if (PlayerPrefs.HasKey ("Weapon")) {
//			CheckWeaponData (collections.GetWeapon (weaponKeys [PlayerPrefs.GetInt ("Weapon")]).name);
//
//		} else {
//			CheckWeaponData (collections.GetWeapon (weaponKeys [rightWeaponIndex]).name);
//
//
//		}
//		if (PlayerPrefs.HasKey ("Shield")) {
//			CheckShieldData (collections.GetShield (shieldKeys [PlayerPrefs.GetInt ("Shield")]).name);
//
//		} else {
//			CheckShieldData (collections.GetShield (shieldKeys [leftShieldIndex]).name);
//
//
//		}



	}


	public int CheckPricing(string mykey,Dictionary<string, int> myDic)
	{

		foreach(KeyValuePair<string,int> keyValue in myDic)
		{
			if (mykey == keyValue.Key) 

			
			return keyValue.Value;

		}

		return 600;
	}

	int GetIndex(List<string> keys, string keylookup){
//		return keys.FindIndex (SystemforeachPredicate<>(keylookup));
		for (int i = 0; i < keys.Count; i++) {
			if (keys [i].Contains(keylookup)) {
				return i;
			}
		}
		return 0;


	}

	void Update(){

		if (Input.GetKeyUp (KeyCode.N)) {
			string weapstring = "Weapon001";
			weapstring = weapstring.Replace (collections.weaponKey, "");
			print(GetIndex (weaponKeys,weapstring));
			print (collections.GetHead (headKeys [0]));

	}
	}

	public void EnableCharacterSelection(){



		headIndex = GetIndex (headKeys, PlayerPrefs.GetString ("Head").Replace(collections.headKey,""));
		string chestString;
		chestString = PlayerPrefs.GetString ("Outfit").Replace (collections.chestKey, "");
		chestIndex = GetIndex (chestKeys,chestString);
		armPartsIndex = GetIndex (chestKeys,chestString);
		legPartsIndex = GetIndex (chestKeys,chestString);
		lowerSpineIndex = GetIndex (chestKeys,chestString);
		spineIndex = GetIndex (chestKeys,chestString);

		characterParts.ChangeHead(collections.GetHead(headKeys[headIndex]));
		characterParts.ChangeChest(collections.GetChest(chestKeys[chestIndex]));
		characterParts.ChangeArmParts(collections.GetArmParts(armPartsKeys[chestIndex]));
		characterParts.ChangeLegParts(collections.GetLegParts(legPartsKeys[chestIndex]));
		characterParts.ChangeLowerSpine(collections.GetLowerSpine(lowerSpineKeys[chestIndex]));
		characterParts.ChangeSpine(collections.GetSpine(spineKeys[chestIndex]));

		if (PlayerPrefs.GetString ("Hair")!="") {
			hairIndex = GetIndex (hairKeys,PlayerPrefs.GetString ("Hair").Replace(collections.hairKey,""));

			characterParts.ChangeHair (collections.GetHair (hairKeys [hairIndex]));
		} else {
			characterParts.RemoveHair ();
		}
		if (PlayerPrefs.GetString ("HairAccess")!="") {
			hairIndex = GetIndex (hairKeys,PlayerPrefs.GetString ("HairAccess").Replace(collections.hairKey,""));
			characterParts.ChangeHeadAccesories (collections.GetHeadAccesories (headAccesoriesKeys [hairIndex]));
		} else {
			characterParts.RemoveHeadAccesories ();

		}
		if (PlayerPrefs.GetString ("Weapon")!="") {
			rightWeaponIndex = GetIndex (weaponKeys,PlayerPrefs.GetString ("Weapon").Replace(collections.weaponKey,"")) ;

			characterParts.ChangeRightWeapon (collections.GetWeapon (weaponKeys [rightWeaponIndex]));
		} else {
			characterParts.RemoveRightWeapon ();

		}
		if (PlayerPrefs.GetString ("Shield")!="") {
			leftShieldIndex = GetIndex (shieldKeys,PlayerPrefs.GetString ("Shield").Replace(collections.shieldKey,""));

			characterParts.ChangeLeftShield (collections.GetShield (shieldKeys [leftShieldIndex]));
		} else {
			characterParts.RemoveLeftShield ();

		}
	}



	public void UpdateStoreUI(){
		print ("Updating UI");
		if (headIndexText != null)
			headIndexText.text = (headIndex + 1) + "/" + headKeys.Count;
		if (hairIndexText != null)
			hairIndexText.text = (hairIndex + 1) + "/" + hairKeys.Count;
		if (headAccesoriesIndexText != null)
			headAccesoriesIndexText.text = (headAccesoriesIndex + 1) + "/" + headAccesoriesKeys.Count;
		if (leftShoulderIndexText != null)
			leftShoulderIndexText.text = (leftShoulderIndex + 1) + "/" + shoulderKeys.Count;
		if (leftElbowIndexText != null)
			leftElbowIndexText.text = (leftElbowIndex + 1) + "/" + elbowKeys.Count;
		if (leftWeaponIndexText != null)
			leftWeaponIndexText.text = (leftWeaponIndex + 1) + "/" + weaponKeys.Count;
		if (leftShieldIndexText != null)
			leftShieldIndexText.text = (leftShieldIndex + 1) + "/" + shieldKeys.Count;
		if (rightShoulderIndexText != null)
			rightShoulderIndexText.text = (rightShoulderIndex + 1) + "/" + shoulderKeys.Count;
		if (rightElbowIndexText != null)
			rightElbowIndexText.text = (rightElbowIndex + 1) + "/" + elbowKeys.Count;
		if (rightWeaponIndexText != null)
			rightWeaponIndexText.text = (rightWeaponIndex + 1) + "/" + weaponKeys.Count;
		if (chestIndexText != null)
			chestIndexText.text = (chestIndex + 1) + "/" + chestKeys.Count;
		if (spineIndexText != null)
			spineIndexText.text = (spineIndex + 1) + "/" + spineKeys.Count;
		if (lowerSpineIndexText != null)
			lowerSpineIndexText.text = (lowerSpineIndex + 1) + "/" + lowerSpineKeys.Count;
		if (leftHipIndexText != null)
			leftHipIndexText.text = (leftHipIndex + 1) + "/" + hipKeys.Count;
		if (leftKneeIndexText != null)
			leftKneeIndexText.text = (leftKneeIndex + 1) + "/" + kneeKeys.Count;
		if (rightHipIndexText != null)
			rightHipIndexText.text = (rightHipIndex + 1) + "/" + hipKeys.Count;
		if (rightKneeIndexText != null)
			rightKneeIndexText.text = (rightKneeIndex + 1) + "/" + kneeKeys.Count;
		if (armPartsIndexText != null)
			armPartsIndexText.text = (armPartsIndex + 1) + "/" + armPartsKeys.Count;
		if (legPartsIndexText != null)
			legPartsIndexText.text = (legPartsIndex + 1) + "/" + legPartsKeys.Count;



	}

    public int GetPartKeyIndex(bool isPrev, int currentIndex, List<string> keys)
    {
        if (keys == null || keys.Count == 0)
            return -1;
        if (isPrev)
        {
            --currentIndex;
            if (currentIndex < 0)
                currentIndex = keys.Count - 1;
        }
        else
        {
            ++currentIndex;
            if (currentIndex >= keys.Count)
                currentIndex = 0;
        }
        return currentIndex;
    }

    public void ChangeHeadPart(bool isPrev = false)
    {
		//this is where to look
        headIndex = GetPartKeyIndex(isPrev, headIndex, headKeys);
		CheckHeadData (collections.GetHead (headKeys [headIndex]).name);
		if (headAvailable) {
			print (collections.GetHead (headKeys [headIndex]).name);
			PlayerPrefs.SetString ("Head",collections.GetHead (headKeys [headIndex]).name);
		}
        characterParts.ChangeHead(collections.GetHead(headKeys[headIndex]));
    }

    public void ChangeHairPart(bool isPrev = false)
    {
        hairIndex = GetPartKeyIndex(isPrev, hairIndex, hairKeys);
		CheckHairData (collections.GetHair (hairKeys [hairIndex]).name);
		if (hairAvailable) {
			PlayerPrefs.SetString ("Hair", collections.GetHair (hairKeys [hairIndex]).name);
		}
        characterParts.ChangeHair(collections.GetHair(hairKeys[hairIndex]));
    }

    public void ChangeHeadAccesoriesPart(bool isPrev = false)
    {
        headAccesoriesIndex = GetPartKeyIndex(isPrev, headAccesoriesIndex, headAccesoriesKeys);
		CheckHeadAccData (collections.GetHeadAccesories (headAccesoriesKeys [headAccesoriesIndex]).name);
		if (headAccAvailable) {
			PlayerPrefs.SetString ("HairAccess", collections.GetHeadAccesories (headAccesoriesKeys [headAccesoriesIndex]).name);
		}
        characterParts.ChangeHeadAccesories(collections.GetHeadAccesories(headAccesoriesKeys[headAccesoriesIndex]));
    }

    public void ChangeLeftShoulderPart(bool isPrev = false)
    {
        leftShoulderIndex = GetPartKeyIndex(isPrev, leftShoulderIndex, shoulderKeys);
        characterParts.ChangeLeftShoulder(collections.GetShoulder(shoulderKeys[leftShoulderIndex]));
    }

    public void ChangeLeftElbowPart(bool isPrev = false)
    {
        leftElbowIndex = GetPartKeyIndex(isPrev, leftElbowIndex, elbowKeys);
        characterParts.ChangeLeftElbow(collections.GetElbow(elbowKeys[leftElbowIndex]));
    }

    public void ChangeLeftWeaponPart(bool isPrev = false)
    {
        leftWeaponIndex = GetPartKeyIndex(isPrev, leftWeaponIndex, weaponKeys);
        characterParts.ChangeLeftWeapon(collections.GetWeapon(weaponKeys[leftWeaponIndex]));
    }

    public void ChangeLeftShieldPart(bool isPrev = false)
    {
        leftShieldIndex = GetPartKeyIndex(isPrev, leftShieldIndex, shieldKeys);
		CheckShieldData (collections.GetShield (shieldKeys [leftShieldIndex]).name);
		if (shieldAvailable) {
			PlayerPrefs.SetString ("Shield", collections.GetShield (shieldKeys [leftShieldIndex]).name);
		}
        characterParts.ChangeLeftShield(collections.GetShield(shieldKeys[leftShieldIndex]));
    }

    public void ChangeRightShoulderPart(bool isPrev = false)
    {
        rightShoulderIndex = GetPartKeyIndex(isPrev, rightShoulderIndex, shoulderKeys);
        characterParts.ChangeRightShoulder(collections.GetShoulder(shoulderKeys[rightShoulderIndex]));
    }

    public void ChangeRightElbowPart(bool isPrev = false)
    {
        rightElbowIndex = GetPartKeyIndex(isPrev, rightElbowIndex, elbowKeys);
        characterParts.ChangeRightElbow(collections.GetElbow(elbowKeys[rightElbowIndex]));
    }

    public void ChangeRightWeaponPart(bool isPrev = false)
    {
        rightWeaponIndex = GetPartKeyIndex(isPrev, rightWeaponIndex, weaponKeys);
		CheckWeaponData (collections.GetWeapon (weaponKeys [rightWeaponIndex]).name);
		if (weaponAvailable) {
			PlayerPrefs.SetString ("Weapon", collections.GetWeapon (weaponKeys [rightWeaponIndex]).name);
		}
        characterParts.ChangeRightWeapon(collections.GetWeapon(weaponKeys[rightWeaponIndex]));
    }

    public void ChangeChestPart(bool isPrev = false)
    {
        chestIndex = GetPartKeyIndex(isPrev, chestIndex, chestKeys);
		CheckOutfitData (collections.GetChest (chestKeys [chestIndex]).name);
		if (outfitAvailable) {
			PlayerPrefs.SetString ("Outfit", collections.GetChest (chestKeys [chestIndex]).name);
		}

        characterParts.ChangeChest(collections.GetChest(chestKeys[chestIndex]));
    }

    public void ChangeSpinePart(bool isPrev = false)
    {
        spineIndex = GetPartKeyIndex(isPrev, spineIndex, spineKeys);
        characterParts.ChangeSpine(collections.GetSpine(spineKeys[spineIndex]));
    }

    public void ChangeLowerSpinePart(bool isPrev = false)
    {
        lowerSpineIndex = GetPartKeyIndex(isPrev, lowerSpineIndex, lowerSpineKeys);
        characterParts.ChangeLowerSpine(collections.GetLowerSpine(lowerSpineKeys[lowerSpineIndex]));
    }

    public void ChangeLeftHipPart(bool isPrev = false)
    {
        leftHipIndex = GetPartKeyIndex(isPrev, leftHipIndex, hipKeys);
        characterParts.ChangeLeftHip(collections.GetHip(hipKeys[leftHipIndex]));
    }

    public void ChangeLeftKneePart(bool isPrev = false)
    {
        leftKneeIndex = GetPartKeyIndex(isPrev, leftKneeIndex, kneeKeys);
        characterParts.ChangeLeftKnee(collections.GetKnee(kneeKeys[leftKneeIndex]));
    }

    public void ChangeRightHipPart(bool isPrev = false)
    {
        rightHipIndex = GetPartKeyIndex(isPrev, rightHipIndex, hipKeys);
        characterParts.ChangeRightHip(collections.GetHip(hipKeys[rightHipIndex]));
    }

    public void ChangeRightKneePart(bool isPrev = false)
    {
        rightKneeIndex = GetPartKeyIndex(isPrev, rightKneeIndex, kneeKeys);
        characterParts.ChangeRightKnee(collections.GetKnee(kneeKeys[rightKneeIndex]));
    }

    public void ChangeArmParts(bool isPrev = false)
    {
        armPartsIndex = GetPartKeyIndex(isPrev, armPartsIndex, armPartsKeys);
        characterParts.ChangeArmParts(collections.GetArmParts(armPartsKeys[armPartsIndex]));
    }

    public void ChangeLegParts(bool isPrev = false)
    {
        legPartsIndex = GetPartKeyIndex(isPrev, legPartsIndex, legPartsKeys);
        characterParts.ChangeLegParts(collections.GetLegParts(legPartsKeys[legPartsIndex]));
    }
	public void ChangeOutfitParts(bool isPrev = false){
		ChangeChestPart (isPrev);ChangeSpinePart (isPrev);ChangeLowerSpinePart (isPrev);ChangeArmParts (isPrev);ChangeLegParts (isPrev);

	}

	public void ChangeEntireSpineParts(bool isPrev = false){
		ChangeChestPart (isPrev);ChangeSpinePart (isPrev);ChangeLowerSpinePart (isPrev);

	}

		void CheckHeadData(string checkkey){
			headAvailable = false;
			headCostButton.SetActive (true);

			if (PlayerPrefs.GetInt(checkkey) >0){

					headAvailable = true;
					headCostButton.SetActive (false);
				}
				else {
					headCostButton.transform.GetChild(0).GetComponent<Text>().text ="$0.99" ;

				
		}
	}
	void CheckHairData(string checkkey){
			hairAvailable = false;
			hairCostButton.SetActive (true);

			if (PlayerPrefs.GetInt(checkkey) >0){
				hairAvailable = true;
				hairCostButton.SetActive (false);
			} else {
				hairCostButton.transform.GetChild (0).GetComponent<Text> ().text = "$0.99";

			}
				

	}
	void CheckHeadAccData(string checkkey){
			headAccAvailable = false;
			headAccCostButton.SetActive (true);
		if (PlayerPrefs.GetInt(checkkey) >0){
				headAccAvailable = true;
				headAccCostButton.SetActive (false);
			} else {
				headAccCostButton.transform.GetChild (0).GetComponent<Text> ().text = "$0.99";


		}
	}
	void CheckOutfitData(string checkkey){
			outfitAvailable = false;
			outfitCostButton.SetActive (true);
		if (PlayerPrefs.GetInt(checkkey) >0){
				outfitAvailable = true;
				outfitCostButton.SetActive (false);
			} else {
				outfitCostButton.transform.GetChild (0).GetComponent<Text> ().text = "$0.99";


		}
	}
	void CheckWeaponData(string checkkey){
			weaponAvailable = false;
			weaponCostButton.SetActive (true);
			if (PlayerPrefs.GetInt(checkkey) >0){
				weaponAvailable = true;
				weaponCostButton.SetActive (false);
			} else {
				weaponCostButton.transform.GetChild (0).GetComponent<Text> ().text = "$0.99";

			
		}
	}
	void CheckShieldData(string checkkey){
			shieldAvailable = false;
			shieldCostButton.SetActive (true);
		if (PlayerPrefs.GetInt(checkkey) >0){
				shieldAvailable = true;
				shieldCostButton.SetActive (false);
			} else {
				shieldCostButton.transform.GetChild (0).GetComponent<Text> ().text = "$0.99";


		}
	}

	public void MakePurchase(string key,int index){
		switch(key){
		case "head":
//				OwnedManager.myDataModel.ownedItems.Add (collections.GetHead (headKeys [headIndex]).name);
//				OwnedManager.myDataModel.coins -= cost;
//				OwnedManager.storedPurchasable = collections.GetHead (headKeys [headIndex]).name;
//				OwnedManager.storedCost = cost;
//				JSONObject myJson = new JSONObject (JsonUtility.ToJson (OwnedManager.myDataModel).ToString ());
//				purchaseIndex = headIndex;
//				purchaseKey = key;
//				OwnedManager.PostPurchase (myJson);
//
		break;
		case "hair":
	
			break;
		case "headaccess":
	
			break;
		case "outfit":

			break;
		case "weapon":
	
			break;
		case "shield":

			break;
		

		}

	}

	public bool CheckFunds(string mykey,Dictionary<string, int> myDic)
	{
		bool hasfunds;
		foreach(KeyValuePair<string,int> keyValue in myDic)
		{
			if (mykey == keyValue.Key)
			if (OwnedManager.myDataModel.coins >= keyValue.Value) {
				return hasfunds = true;
			} else {
				return hasfunds = false;

			}
		}

		return hasfunds = false;
	}
}
