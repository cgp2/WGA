using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckMaster : MonoBehaviour {
    Deck actualDeck;
    PlayerInfo pl;
    GameObject prefab;
	// Use this for initialization
	void Start () {
        var AllCards = Card.Deserialize(Application.dataPath + "/CardsInfo/AllCards.dat");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
