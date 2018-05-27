using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DeckMaster.AllCards = Card.Deserialize(Application.dataPath + "/Resources/CardsInfo/AllCards.dat");
        //DeckMaster.AllCards = Card.Deserialize(Path.GetDirectoryName(Application.dataPath) + "/CardsInfo/lol.xml");   

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
