﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class PartsCollections : MonoBehaviour
{
    public string headKey;
    public string hairKey;
    public string headAccesoriesKey;
    public string shoulderKey;
    public string elbowKey;
    public string weaponKey;
    public string shieldKey;
    public string chestKey;
    public string spineKey;
    public string lowerSpineKey;
    public string hipKey;
    public string kneeKey;
    public PartSelectionController selection;
    public  List<GameObject> allParts;

    public Dictionary<string, GameObject> headObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> hairObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> headAccesoriesObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> shoulderObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> elbowObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> weaponObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> shieldObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> chestObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> spineObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> lowerSpineObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> hipObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> kneeObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, ArmParts> armParts = new Dictionary<string, ArmParts>();
    public Dictionary<string, LegParts> legParts = new Dictionary<string, LegParts>();

	public Dictionary<string, bool> headObjectsPurchased = new Dictionary<string, bool>();
	public Dictionary<string, bool> hairObjectsPurchased = new Dictionary<string, bool>();
	public Dictionary<string, bool> headAccessObjectsPurchased = new Dictionary<string, bool>();
	public Dictionary<string, bool> outfitObjectsPurchased = new Dictionary<string, bool>();
	public Dictionary<string, bool> weaponObjectsPurchased = new Dictionary<string, bool>();
	public Dictionary<string, bool> shieldObjectsPurchased = new Dictionary<string, bool>();


	void Update(){
		if (Input.GetKeyUp (KeyCode.H)) {
			PostJsonParts (headKey);
		}
		if (Input.GetKeyUp (KeyCode.O)) {
			PostJsonParts (chestKey);
		}
	}

	public void PostJsonParts(string keyinput){
		List<GameObject> allResources = new List<GameObject>();
		allResources.AddRange(allParts);
		allResources.AddRange(Resources.LoadAll<GameObject>(""));
		foreach (GameObject obj in allResources)
		{
			if (obj == null)
				continue;
			string key = string.Empty;
			if (obj.name.StartsWith(keyinput))
			{
				NetworkEngine.myDataModel.ownedItems.Add (obj.name);
					
			}
		}

		print (JsonUtility.ToJson(NetworkEngine.myDataModel));
		JSONObject myJson = new JSONObject (JsonUtility.ToJson (NetworkEngine.myDataModel).ToString ());
		GameObject.Find ("NetworkManager").SendMessage("PostCharacterData",myJson);
	}

    void Awake()
    {
        List<GameObject> allResources = new List<GameObject>();
        allResources.AddRange(allParts);
        allResources.AddRange(Resources.LoadAll<GameObject>(""));
		JSONObject getNames = new JSONObject ();
		JSONObject headnames = new JSONObject (JSONObject.Type.OBJECT);
		JSONObject hairnames = new JSONObject (JSONObject.Type.OBJECT);
		JSONObject headaccnames = new JSONObject (JSONObject.Type.OBJECT);
		JSONObject outfitnames = new JSONObject (JSONObject.Type.OBJECT);
		JSONObject weaponnames = new JSONObject (JSONObject.Type.OBJECT);
		JSONObject shieldnames = new JSONObject (JSONObject.Type.OBJECT);

		getNames.AddField ("head", headnames);
		getNames.AddField ("hair", hairnames);
		getNames.AddField ("headacc", headaccnames);
		getNames.AddField ("outfit", outfitnames);
		getNames.AddField ("weapons", weaponnames);
		getNames.AddField ("shield", shieldnames);

        foreach (GameObject obj in allResources)
        {
            if (obj == null)
                continue;
            string key = string.Empty;
            if (obj.name.StartsWith(headKey))
            {
                key = headKey;
                AddToPart(key, obj, headObjects);
				headObjectsPurchased.Add (obj.name, false);
				headnames.Add (obj.name);
            }
            if (obj.name.StartsWith(hairKey))
            {
                key = hairKey;
                AddToPart(key, obj, hairObjects);
				hairObjectsPurchased.Add (obj.name, false);
				hairnames.Add (obj.name);


            }
            if (obj.name.StartsWith(headAccesoriesKey))
            {
                key = headAccesoriesKey;
                AddToPart(key, obj, headAccesoriesObjects);
				headAccessObjectsPurchased.Add (obj.name, false);
				headaccnames.Add (obj.name);


            }
            if (obj.name.StartsWith(shoulderKey))
            {
                key = shoulderKey;
                AddToPart(key, obj, shoulderObjects);
				outfitObjectsPurchased.Add (obj.name, false);
				outfitnames.Add (obj.name);
            }
            if (obj.name.StartsWith(elbowKey))
            {
                key = elbowKey;
                AddToPart(key, obj, elbowObjects);
				outfitObjectsPurchased.Add (obj.name, false);
				outfitnames.Add (obj.name);
            }
            if (obj.name.StartsWith(weaponKey))
            {
                key = weaponKey;
                AddToPart(key, obj, weaponObjects);
				weaponObjectsPurchased.Add (obj.name, false);
				weaponnames.Add (obj.name);


            }
            if (obj.name.StartsWith(shieldKey))
            {
                key = shieldKey;
                AddToPart(key, obj, shieldObjects);
				shieldObjectsPurchased.Add (obj.name, false);
				shieldnames.Add (obj.name);


            }
            if (obj.name.StartsWith(chestKey))
            {
                key = chestKey;
                AddToPart(key, obj, chestObjects);
				outfitObjectsPurchased.Add (obj.name, false);
				outfitnames.Add (obj.name);


            }
            if (obj.name.StartsWith(spineKey))
            {
                key = spineKey;
                AddToPart(key, obj, spineObjects);
				outfitObjectsPurchased.Add (obj.name, false);
				outfitnames.Add (obj.name);
            }
            if (obj.name.StartsWith(lowerSpineKey))
            {
                key = lowerSpineKey;
                AddToPart(key, obj, lowerSpineObjects);
				outfitObjectsPurchased.Add (obj.name, false);
				outfitnames.Add (obj.name);
            }
            if (obj.name.StartsWith(hipKey))
            {
                key = hipKey;
                AddToPart(key, obj, hipObjects);
				outfitObjectsPurchased.Add (obj.name, false);
				outfitnames.Add (obj.name);
            }
            if (obj.name.StartsWith(kneeKey))
            {
                key = kneeKey;
                AddToPart(key, obj, kneeObjects);
				outfitObjectsPurchased.Add (obj.name, false);
				outfitnames.Add (obj.name);
            }
        }
        CreateArmParts();
        CreateLegParts();
		print (getNames.Print (true));
        if (selection != null)
            selection.Init(this);
    }

    void CreateArmParts()
    {
        foreach (string key in shoulderObjects.Keys)
        {
            if (!armParts.ContainsKey(key))
            {
                ArmParts parts = new ArmParts();
                parts.shoulder = shoulderObjects[key];
                if (elbowObjects.ContainsKey(key))
                    parts.elbow = elbowObjects[key];
                else
                    parts.elbow = GetDefaultPart(elbowObjects);
                armParts.Add(key, parts);
            }
        }
    }

    void CreateLegParts()
    {
        foreach (string key in hipObjects.Keys)
        {
            if (!legParts.ContainsKey(key))
            {
                LegParts parts = new LegParts();
                parts.hip = hipObjects[key];
                if (kneeObjects.ContainsKey(key))
                    parts.knee = kneeObjects[key];
                else
                    parts.knee = GetDefaultPart(kneeObjects);
                legParts.Add(key, parts);
            }
        }
    }

    void AddToPart(string key, GameObject obj, Dictionary<string, GameObject> list)
    {
        if (obj == null || !obj.name.StartsWith(key))
            return;
        string listKey = obj.name.Substring(key.Length);
        if (!list.ContainsKey(listKey))
            list.Add(listKey, obj);
    }

    GameObject GetDefaultPart(Dictionary<string, GameObject> list)
    {
        if (list == null || list.Count == 0)
            return null;
        return list.FirstOrDefault().Value;
    }

    public GameObject GetHead(string key)
    {
        if (headObjects.ContainsKey(key))
            return headObjects[key];
        return GetDefaultPart(headObjects);
    }

    public GameObject GetHair(string key)
    {
        if (hairObjects.ContainsKey(key))
            return hairObjects[key];
        return GetDefaultPart(hairObjects);
    }

    public GameObject GetHeadAccesories(string key)
    {
        if (headAccesoriesObjects.ContainsKey(key))
            return headAccesoriesObjects[key];
        return GetDefaultPart(headAccesoriesObjects);
    }

    public GameObject GetShoulder(string key)
    {
        if (shoulderObjects.ContainsKey(key))
            return shoulderObjects[key];
        return GetDefaultPart(shoulderObjects);
    }

    public GameObject GetElbow(string key)
    {
        if (elbowObjects.ContainsKey(key))
            return elbowObjects[key];
        return GetDefaultPart(elbowObjects);
    }

    public GameObject GetWeapon(string key)
    {
        if (weaponObjects.ContainsKey(key))
            return weaponObjects[key];
        return GetDefaultPart(weaponObjects);
    }

    public GameObject GetShield(string key)
    {
        if (shieldObjects.ContainsKey(key))
            return shieldObjects[key];
        return GetDefaultPart(shieldObjects);
    }

    public GameObject GetChest(string key)
    {
        if (chestObjects.ContainsKey(key))
            return chestObjects[key];
        return GetDefaultPart(chestObjects);
    }

    public GameObject GetSpine(string key)
    {
        if (spineObjects.ContainsKey(key))
            return spineObjects[key];
        return GetDefaultPart(spineObjects);
    }

    public GameObject GetLowerSpine(string key)
    {
        if (lowerSpineObjects.ContainsKey(key))
            return lowerSpineObjects[key];
        return GetDefaultPart(lowerSpineObjects);
    }

    public GameObject GetHip(string key)
    {
        if (hipObjects.ContainsKey(key))
            return hipObjects[key];
        return GetDefaultPart(hipObjects);
    }

    public GameObject GetKnee(string key)
    {
        if (kneeObjects.ContainsKey(key))
            return kneeObjects[key];
        return GetDefaultPart(kneeObjects);
    }

    public ArmParts GetArmParts(string key)
    {
        if (armParts.ContainsKey(key))
            return armParts[key];
        return armParts.FirstOrDefault().Value;
    }

    public LegParts GetLegParts(string key)
    {
        if (legParts.ContainsKey(key))
            return legParts[key];
        return legParts.FirstOrDefault().Value;
    }

//	public  void CheckPurchased(DataModel checkModel){
//						List<string> allEnabled = new List<string>();
//
//				
//							
//							string key = string.Empty;
//							if (obj.StartsWith(headKey))
//							{
//							key = headKey;	
//							
//									}
//									if (obj.StartsWith(hairKey))
//									{
//										key = hairKey;
//
//									}
//									if (obj.StartsWith(headAccesoriesKey))
//									{
//										key = headAccesoriesKey;
//
//									}
//							
//									if (obj.StartsWith(weaponKey))
//									{
//										key = weaponKey;
//
//									}
//									if (obj.StartsWith(shieldKey))
//									{
//										key = shieldKey;
//
//									}
//									if (obj.StartsWith(chestKey))
//									{
//										key = chestKey;
//
//									}
//						
//									}
//
//	string CheckModelData(string checkkey){
//		foreach (string obj in NetworkEngine.myDataModel.ownedItems) {
////			if (obj == checkname) {
////				return checkname;
////			}
//			if(obj.StartsWith(checkkey){
//				return
//			}
//
//
//		}
//	}



}
