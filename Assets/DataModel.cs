using System;
using System.Collections.Generic;

[Serializable]

public class DataModel {
	public string _id;
	public string deviceID;
	public string fID;
	public int coins;
	public int gems;
	public List<string> ownedItems;
	public List<string> enabledItems;
	public int priceTierOne;
	public int priceTierTwo;
	public int priceTierThree;

}
