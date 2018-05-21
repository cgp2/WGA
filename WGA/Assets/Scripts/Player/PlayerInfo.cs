using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using System.IO;

[Serializable]
public class PlayerInfo : MonoBehaviour {
    public string Name;
    public Deck Deck = new Deck();
    public int Level;
    public int Exp;
    public int ExpToNextLevel;
    //public Sprite avatar;

    // Use this for initialization
    void Start () {
        
	}
	// Update is called once per frame
	void Update () {
		
	}
    public void SaveToFile()
    {

    }
}

