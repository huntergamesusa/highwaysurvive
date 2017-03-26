using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewCharacterManager : MonoBehaviour {
	GameObject mainCharacter;
	public GameObject[] myMeshGO;
	// Use this for initialization
	public  List<Material> allResources = new List<Material>();

	public static Dictionary <Material, Mesh> myCharacters = new Dictionary<Material, Mesh> ();

	public List<Material> myMat = new List<Material> ();

	int index;

	void Awake(){
		allResources.AddRange(Resources.LoadAll<Material>(""));
		foreach (Material obj in allResources) {
			if(obj.name.Contains("MAT")){
				
				string mystring;
				mystring = obj.name;
				string[] objstring =  mystring.Split('_'); //Here we assing the splitted string to array by that char
//			
//				myCharacters.Add (obj, int.Parse (objstring [0]));
				myMat.Add (obj);
				myCharacters.Add (obj, SendMesh (objstring [1]));
			}
		}
//		myMat.Sort ();
//		print (myCharacters.Comparer(1));
//		Material = myCharacters.ToList ();



	}


					Mesh SendMesh(string matname){

						switch(matname){
		case "snowyMAT":
			return myMeshGO[7].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "archerMAT":
			return myMeshGO[2].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "girlsruleMAT":
			return myMeshGO[2].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "teachyMAT":
			return myMeshGO[2].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "rumboMAT":
			return myMeshGO[0].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "monsterMAT":
			return myMeshGO[0].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "yetiMAT":
			return myMeshGO[0].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "santaMAT":
			return myMeshGO[5].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "pumpyMAT":
			return myMeshGO[4].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "darkwizardMAT":
			return myMeshGO[6].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "wizzywizMAT":
			return myMeshGO[6].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "hawkmanMAT":
			return myMeshGO[8].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
		case "elfyMAT":
			return myMeshGO[3].GetComponent<SkinnedMeshRenderer> ().sharedMesh;
			break;
						}
		return myMeshGO[0].GetComponent<SkinnedMeshRenderer> ().sharedMesh;


					}


	void Start () {

		if(!PlayerPrefs.HasKey("MyCharacter")){
			SelectCharacter();
		}

		InitCharacter ();
	
//		mainCharacter.GetComponent<SkinnedMeshRenderer>().BakeMesh = myCharacters.
	}

	public void InitCharacter(){
		mainCharacter = GameObject.Find ("MicroMale");

		for (int i = 0; i < myMat.Count; i++) {
			if (myMat [i].name.Contains (PlayerPrefs.GetString ("MyCharacter"))) {
				index = i;
			}
		}

		mainCharacter.GetComponent<SkinnedMeshRenderer> ().material = myMat[index];
		foreach(KeyValuePair<Material,Mesh> keyValue in myCharacters)
		{
			if (myMat [index] == keyValue.Key) {
				mainCharacter.GetComponent<SkinnedMeshRenderer> ().sharedMesh = keyValue.Value;
			}
		}


	}
	public void InitCharacterReceive(GameObject myGO){
		print ("checking to reinstate character skin");
		mainCharacter = myGO;
		for (int i = 0; i < myMat.Count; i++) {
			if (myMat [i].name.Contains (PlayerPrefs.GetString ("MyCharacter"))) {
				index = i;
			}
		}

		myGO.GetComponent<SkinnedMeshRenderer> ().material = myMat[index];
		foreach(KeyValuePair<Material,Mesh> keyValue in myCharacters)
		{
			if (myMat [index] == keyValue.Key) {
				myGO.GetComponent<SkinnedMeshRenderer> ().sharedMesh = keyValue.Value;
			}
		}


	}

	public void SelectCharacter(){
		string mystring;
		mystring = myMat [index].name;
		string[] objstring =  mystring.Split('_'); //Here we assing the splitted string to array by that char
		mystring = objstring[1].Replace("MAT","");
		PlayerPrefs.SetString ("MyCharacter", mystring);
	}



	public void SetMaterial(bool forward){
		if (forward) {
			index++;
			if (index >= myMat.Count) {
				index = 0;
			}

		} else {
			index--;
			if (index == -1) {
				index = myMat.Count-1;
			}

		}
		print (index);
		mainCharacter.GetComponent<SkinnedMeshRenderer> ().material = myMat [index];
		foreach(KeyValuePair<Material,Mesh> keyValue in myCharacters)
		{
			if (myMat [index] == keyValue.Key) {
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
}
