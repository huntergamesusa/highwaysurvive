using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour {
	public string panelType;
	public Camera panelCam;
	public RectTransform panel;
	public RectTransform center;
	public  List <GameObject>  prefab = new List<GameObject>();
	public GameObject genericPrefab;
	public GameObject btnFree;
	public GameObject btnPrice;
	public NewCharacterManager purchasingManager;
	public Text txtName;

//	public Text txtGeneralCash;
//	public Text txtPriceCash;
	public Text txtPriceSold;

	float[] distance;

	bool dragging = false;

	int minButtonNum;
	int currentSelectedPly = -1;

	public float objectScale = 1.7f;
	public int bttnDistance = 300;

	void OnEnable() {
//		txtGeneralCash.text = "" + PlayerPrefs.GetInt ("money", 0);
	}

	void TakeScreen(){
		Application.CaptureScreenshot (prefab [minButtonNum].GetComponent<CharacterProperty> ().sku + ".png");
	}

	void Start(){
		if (panelType == "Character") {
			distance = new float[NewCharacterManager.myCharacterMat.Count];

//		distance = new float[prefab.Length];

			//instatiate the prefab

			for (int i = 0; i < NewCharacterManager.myCharacterMat.Count; i++) {
				prefab.Add (Instantiate (genericPrefab, center.transform.position, Quaternion.Euler(panelCam.transform.eulerAngles.x,panelCam.transform.eulerAngles.y,panelCam.transform.eulerAngles.z)) as GameObject);
				prefab [i].transform.SetParent (panel.transform);
				Vector3 pos = prefab [i].GetComponent<RectTransform> ().anchoredPosition;
				pos.x += (i * bttnDistance);
				prefab [i].GetComponent<RectTransform> ().anchoredPosition = pos; 
				GameObject myMeshGO = prefab [i].transform.GetChild (0).GetChild (0).GetChild (2).gameObject;
				myMeshGO.GetComponent<SkinnedMeshRenderer> ().material = NewCharacterManager.myCharacterMat [i];
				prefab [i].GetComponent<CharacterProperty> ().price = NewCharacterManager.myCharacterPrices [i];
				prefab [i].GetComponent<CharacterProperty> ().sku = NewCharacterManager.myCharacterSKU [i];

				prefab [i].GetComponent<CharacterProperty> ().nameObj = NewCharacterManager.myCharacterTitles [i];

				foreach (KeyValuePair<Material,Mesh> keyValue in NewCharacterManager.myCharacters) {
					if (NewCharacterManager.myCharacterMat [i] == keyValue.Key) {
						myMeshGO.GetComponent<SkinnedMeshRenderer> ().sharedMesh = keyValue.Value;
					}
				}
			}
		}
		
			if (panelType == "Weapon") {
			distance = new float[NewCharacterManager.myWeaponMat.Count];

			for (int i = 0; i < NewCharacterManager.myWeaponMat.Count; i++) {
				prefab.Add (Instantiate (genericPrefab, center.transform.position, panelCam.transform.rotation) as GameObject);
				prefab [i].transform.SetParent (panel.transform);
				Vector3 pos = prefab [i].GetComponent<RectTransform> ().anchoredPosition;
				pos.x += (i * bttnDistance);
				prefab [i].GetComponent<RectTransform> ().anchoredPosition = pos; 
				GameObject myMeshGO = prefab [i].transform.GetChild (0).GetChild (0).gameObject;
				myMeshGO.GetComponent<MeshRenderer> ().material = NewCharacterManager.myWeaponMat [i];
				prefab [i].GetComponent<CharacterProperty> ().price = NewCharacterManager.myWeaponPrices [i];
				prefab [i].GetComponent<CharacterProperty> ().sku = NewCharacterManager.myWeaponSKU [i];

				prefab [i].GetComponent<CharacterProperty> ().nameObj = NewCharacterManager.myWeaponTitles [i];

				foreach (KeyValuePair<Material,Mesh> keyValue in NewCharacterManager.myWeapons) {
					if (NewCharacterManager.myWeaponMat [i] == keyValue.Key) {
						myMeshGO.GetComponent<MeshFilter>().mesh = keyValue.Value;
					}
				}
			}


			}
	}



	void Update(){

		if (Input.GetKeyUp (KeyCode.S)) {
			TakeScreen ();
		}
		//calculate the relative distance
		for(int i=0;i<prefab.Count;i++){
			distance [i] = Mathf.Abs (center.transform.position.x - prefab [i].transform.position.x);
		}

		float minDistance = Mathf.Min (distance);

		// Aplly the scale to object
		for(int a=0;a<prefab.Count;a++){
			if (minDistance == distance [a]) {
				minButtonNum = a;
				if(minButtonNum != currentSelectedPly){
					lookAtPrice (minButtonNum);
					scaleButtonCenter (minButtonNum);
					currentSelectedPly = minButtonNum;
					txtName.text = prefab [minButtonNum].GetComponent<CharacterProperty> ().nameObj;
				}
			}
		}
			
		// if the users aren't dragging the lerp function is called on the prefab
		if(!dragging){
			LerpToBttn (currentSelectedPly* (-bttnDistance));
		}
			
	}

	/*
	 *  Lerp the nearest prefab to center 
	 */
	void LerpToBttn(int position){


		float newX = Mathf.Lerp (panel.anchoredPosition.x,position,Time.deltaTime*3f);
		Vector2 newPosition = new Vector2 (newX,panel.anchoredPosition.y);
		panel.anchoredPosition = newPosition;

//		LeanTween.moveX(panel,position,.3f)
//		LeanTween.value (gameObject, updateValueExampleCallback, panel.anchoredPosition.x, position, 1.5f);

	}
	
			void updateValueExampleCallback( float val ){
				Vector2 newPosition = new Vector2 (val,panel.anchoredPosition.y);
				panel.anchoredPosition = newPosition;


			}
	/*
	 * Set the scale of the prefab on center to 2, other to 1
	 */
	public void scaleButtonCenter (int minButtonNum){
		for (int a = 0; a < prefab.Count; a++) {
			if (a == minButtonNum) {
				StartCoroutine (ScaleTransform(prefab [a].transform,prefab [a].transform.localScale,new Vector3 (objectScale,objectScale,objectScale)));
			} else {
				StartCoroutine (ScaleTransform(prefab [a].transform,prefab [a].transform.localScale,new Vector3 (1f, 1f, 1f)));
			}
		}
	}

	/*
	 * If the prefab is not free, show the price button
	 */
	public void lookAtPrice (int minButtonNum){
		print ("looking at price");
		CharacterProperty chrProperty = prefab [minButtonNum].GetComponent<CharacterProperty> ();
		if (chrProperty.price == 0 || PlayerPrefs.GetInt(chrProperty.sku) == 1) {
			btnFree.SetActive (true);
			btnPrice.SetActive (false);
		} else {
			btnFree.SetActive (false);
			btnPrice.SetActive (true);
//			txtPriceCash.text = "" + ((int)CharacterProperty.CONVERSION_RATE*chrProperty.price);
			txtPriceSold.text = "$"+chrProperty.price;
		}
	}

	/*
	 * Courutine for change the scale
	 */
	IEnumerator ScaleTransform(Transform transformTrg,Vector3 initScale,Vector3 endScale){
		float completeTime = 0.2f;//How much time will it take to scale
		float currentTime = 0.0f;
		bool done = false;

//		txtGeneralCash.color = new Color (255,255,255); // reset color to white

		while (!done){
			float percent = currentTime / completeTime;
			if (percent >= 1.0f){
				percent = 1;
				done = true;
			}
			transformTrg.localScale = Vector3.Lerp(initScale, endScale, percent);
			currentTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	/*
	 * Called by the canvas, set dragging to true for preventing lerp when users are dragging
	 */
	public void StartDrag(){
		dragging = true;
	}

	/*
	 * Called by the canvas, set dragging to true for preventing lerp when users are dragging
	 */
	public void EndDrag(){
		dragging = false;
	}

	/*
	 * Called when character is selected, it change the player model
	 */
	public void CharacterSelected(){
		string nameSelected = prefab [currentSelectedPly].GetComponent<CharacterProperty> ().sku;

		if (panelType == "Character") {
			purchasingManager.SelectCharacter (nameSelected);
		}
		if (panelType == "Weapon") {
			purchasingManager.SelectWeapon (nameSelected);

		}
		transform.parent.gameObject.SetActive (false);
	
	}

	/*
	 * Called when player try to buy character with cash
	 */
	public void buyCharacterWithCash(){
//		CharacterProperty chrProperty = prefab [minButtonNum].GetComponent<CharacterProperty> ();
//		int cashNedeed = (int)(CharacterProperty.CONVERSION_RATE*chrProperty.price);
//		int totalCash = int.Parse (txtGeneralCash.text);
//		if (cashNedeed <= totalCash) {
//			totalCash -= cashNedeed;
//			txtGeneralCash.text = "" + totalCash;
//
//			btnFree.SetActive (true);
//			btnPrice.SetActive (false);
//
//			PlayerPrefs.SetInt (chrProperty.name, 7);
//			PlayerPrefs.SetInt ("money", totalCash);
//		} else {
//			txtGeneralCash.color = new Color (255,0,0);
//		}
	}

	/*
	 * Implement here your pay function
	 */
	public void buyCharacterWithPayment(){
		CharacterProperty chrProperty = prefab [minButtonNum].GetComponent<CharacterProperty> ();
		float price = chrProperty.price;
		string sku = chrProperty.sku;
		purchasingManager.BuyProductID (sku,gameObject);

		/*
		 * Here player payment 
		 */

//		Debug.Log ("You must pay ! ;) Price: " + price + "€"+", sku: "+sku);

		//<--- Call this when player confirm payment !

		/*PlayerPrefs.SetInt (chrProperty.name, 7);
		btnFree.SetActive (true);
		btnPrice.SetActive (false);*/
	}

	public void PurchaseSuccess(){
		btnFree.SetActive (true);
		btnPrice.SetActive (false);

	}
}
