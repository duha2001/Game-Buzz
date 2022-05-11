using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SaveLoad.Load();

        Debug.Log(GameData.playerName);
        Debug.Log(GameData.playerLevel);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
