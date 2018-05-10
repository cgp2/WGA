using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Race
{
    Human,
    Insect,
    Robot
}
public class Deck : MonoBehaviour {
    string Name;
    List<Card> cardsInDeck;
    Race race;
    int deckID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
