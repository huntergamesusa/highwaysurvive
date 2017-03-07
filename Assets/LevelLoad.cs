using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoad : MonoBehaviour {

	public void LoadMyLevel(int level){
		SceneManager.LoadScene (level);
	}
}
