using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using System.IO;
using UnityEngine.UI;
public class CollectionController : MonoBehaviour {
    public GameObject CollectionPrefab;
    public GameObject DeckPrefab;
    public List<Card.CardData> PlayerDeck;
    PlayerInfo PlInfo;
	// Use this for initialization
	void Start () {
        
        PlInfo = new PlayerInfo(Application.dataPath + "/PlayerInfo/PlayerInfo.dat");
        //ItemGameObject is my prefab pointer that i previous made a public property  
        //and  assigned a prefab to it
        //PlInfo.ReadFromFile(Path.GetDirectoryName(Application.dataPath) + "/PlayerInfo/PlayerInfo.dat");
        //var AllCards = Card.Deserialize(Path.GetDirectoryName(Application.dataPath) + "/CardsInfo/AllCards.dat");
        if(DeckMaster.AllCards==null)
            DeckMaster.AllCards = Card.Deserialize(Application.dataPath + "/Resources/CardsInfo/AllCards.dat");
        foreach (Card.CardData card in DeckMaster.AllCards)
        {
            var c = (GameObject)Instantiate(CollectionPrefab, transform);
            
            c.GetComponent<Card>().Initialize(card);
            var q = c.GetComponent<Card>();
            //c.GetComponentInChildren<Text>().text = "null";
            c.transform.GetChild(1).GetComponent<Image>().sprite = c.GetComponent<Card>().Info.ShipSprite;
            c.transform.GetChild(2).GetComponent<Text>().text = c.GetComponent<Card>().Info.Name;
            c.transform.GetChild(3).GetComponent<Text>().text = ""+c.GetComponent<Card>().Info.InitialAttack;
            c.transform.GetChild(4).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialHealth;
            c.transform.GetChild(5).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialShield;
            c.transform.parent = GameObject.Find("CollectionScrollView").transform.GetChild(0).GetChild(0).transform;
        }
        PlayerDeck = new List<Card.CardData>(Card.Deserialize(Application.dataPath +"/Resources/CardsInfo/Decks/" + PlInfo.DeckName + ".dat"));
        foreach (Card.CardData card in PlayerDeck)
        {
            var c = (GameObject)Instantiate(DeckPrefab, GameObject.Find("DeckScrollView").transform);

            c.GetComponent<Card>().Initialize(card);
            var q = c.GetComponent<Card>();
            //c.GetComponentInChildren<Text>().text = "null";
            c.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = c.GetComponent<Card>().Info.ShipSprite; 
            c.transform.GetChild(2).GetComponent<Text>().text = c.GetComponent<Card>().Info.Name;
            c.transform.GetChild(4).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialAttack;
            c.transform.GetChild(3).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialHealth;
            c.transform.GetChild(5).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialShield;
            c.transform.parent = GameObject.Find("DeckScrollView").transform.GetChild(0).GetChild(0).transform;
        }
    }
    // Update is called once per frame
    public void SaveDeck()
    {
        if (PlayerDeck.Count == 10)
            DeckMaster.SaveDeckToFile(PlayerDeck.ToArray(), Application.dataPath + "/Resources/CardsInfo/Decks/" + PlInfo.DeckName + ".dat");
    }
    void Update () {
		
	}
}
