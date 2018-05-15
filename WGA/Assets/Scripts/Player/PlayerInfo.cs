using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using System.IO;

[Serializable]
public class PlayerInfo : MonoBehaviour {
    public string Name;
    public Deck deck = new Deck();
    public int level;
    public int exp;
    //public Sprite avatar;

	// Use this for initialization
	void Start () {
        
	}
	// Update is called once per frame
	void Update () {
		
	}
    
}

