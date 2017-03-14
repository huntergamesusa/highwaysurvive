using System;
using System.Collections.Generic;

[Serializable]

public class DataModel {
	public string _id;
	public string deviceID;
	public string fID;
	public int coins;
	public int gems;

	public Dictionary<string, bool> headObject;
	public Dictionary<string, bool> hairObject;
	public Dictionary<string, bool> headAccessObject;
	public Dictionary<string, bool> outfitObject;
	public Dictionary<string, bool> weaponObject;
	public Dictionary<string, bool> shieldObject;

	public string headenabled;
	public string hairenabled;
	public string headaccessenabled;
	public string outfiteneabled;
	public string weaponenabled;
	public string shieldenabled;

	public List<string> availableItems;
	public List<string> ownedItems;
	public List<string> enabledItems;
	public int priceTierOne;
	public int priceTierTwo;
	public int priceTierThree;

}
