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
    GameObject CollectionScroll;
    GameObject DeckScroll;
    PlayerInfo PlInfo;
	// Use this for initialization
	void Start () {
        CollectionScroll = GameObject.Find("CollectionScrollView");
        var pn = new ProfileNames();
        pn.InitializeLastProfile();
        var plName = pn.GetCurrentProfileName();
        PlInfo = new PlayerInfo(plName);
        //ItemGameObject is my prefab pointer that i previous made a public property  
        //and  assigned a prefab to it
        //PlInfo.ReadFromFile(Path.GetDirectoryName(Application.dataPath) + "/PlayerInfo/PlayerInfo.dat");
        //var AllCards = Card.Deserialize(Path.GetDirectoryName(Application.dataPath) + "/CardsInfo/AllCards.dat");
        if (DeckMaster.AllCards==null)
            DeckMaster.AllCards = Card.Deserialize(Application.dataPath + "/Resources/CardsInfo/AllCards.dat");
        ChangeContent();
        //foreach (Card.CardData card in DeckMaster.GetCardsOfRace(GameObject.Find("Race").transform.GetChild(0).GetComponent<Text>().text))
        //{
        //    var c = (GameObject)Instantiate(CollectionPrefab, transform);
            
        //    c.GetComponent<Card>().Initialize(card);
        //    var q = c.GetComponent<Card>();
        //    c.GetComponentInChildren<Text>().text = "null";
        //    c.transform.GetChild(1).GetComponent<Image>().sprite = c.GetComponent<Card>().Info.ShipSprite;
        //    c.transform.GetChild(2).GetComponent<Text>().text = c.GetComponent<Card>().Info.Name;
        //    c.transform.GetChild(3).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialAttack;
        //    c.transform.GetChild(4).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialHealth;
        //    c.transform.GetChild(5).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialShield;
        //    //c.GetComponent<Image>().sprite = c.GetComponent<Card>().Info.ShipSprite;
        //    //c.transform.GetChild(0).GetComponent<Image>().sprite = c.GetComponent<Card>().Info.ShipSprite;
        //    //c.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = c.GetComponent<Card>().Info.Name;
        //    //c.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialAttack;
        //    //c.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialHealth;
        //    //c.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialShield;
        //    c.transform.parent =CollectionScroll.transform.GetChild(0).GetChild(0).transform;
        //}
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
            c.GetComponent<Image>().sprite = Resources.Load<Sprite>("CardSprites/CardFronts/CollectionFrame" + GameObject.Find("Race").transform.GetChild(0).GetComponent<Text>().text.ToLower());
        }
    }
    // Update is called once per frame
    public void SaveDeck()
    {
        if (PlayerDeck.Count == 10)
            DeckMaster.SaveDeckToFile(PlayerDeck.ToArray(), Application.dataPath + "/Resources/CardsInfo/Decks/" + PlInfo.DeckName + ".dat");
    }
    public void ChangeContent()
    {
        GameObject.Find("Race").GetComponent<Image>().sprite = Resources.Load<Sprite>("Leaders/"+GameObject.Find("Race").transform.GetChild(0).GetComponent<Text>().text.ToLower());
        for (int i = 0; i < CollectionScroll.transform.GetChild(0).GetChild(0).childCount; i++)
        {
            GameObject.Destroy(CollectionScroll.transform.GetChild(0).GetChild(0).GetChild(i).gameObject);
        }
        foreach (Card.CardData card in DeckMaster.GetCardsOfRace(GameObject.Find("Race").transform.GetChild(0).GetComponent<Text>().text.ToLower()))
        {
            var c = (GameObject)Instantiate(CollectionPrefab, transform);

            c.GetComponent<Card>().Initialize(card);
            var q = c.GetComponent<Card>();
            c.GetComponentInChildren<Text>().text = "null";
            c.GetComponent<Image>().sprite = Resources.Load<Sprite>("CardSprites/CardFronts/CollectionFrame" + GameObject.Find("Race").transform.GetChild(0).GetComponent<Text>().text.ToLower());
            c.transform.GetChild(1).GetComponent<Image>().sprite = c.GetComponent<Card>().Info.ShipSprite;
            c.transform.GetChild(2).GetComponent<Text>().text = c.GetComponent<Card>().Info.Name;
            c.transform.GetChild(3).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialAttack;
            c.transform.GetChild(4).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialHealth;
            c.transform.GetChild(5).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialShield;
            //c.GetComponent<Image>().sprite = c.GetComponent<Card>().Info.ShipSprite;
            //c.transform.GetChild(0).GetComponent<Image>().sprite = c.GetComponent<Card>().Info.ShipSprite;
            //c.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = c.GetComponent<Card>().Info.Name;
            //c.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialAttack;
            //c.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialHealth;
            //c.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialShield;
            c.transform.parent = GameObject.Find("CollectionScrollView").transform.GetChild(0).GetChild(0).transform;
        }
    }
    void Update () {
		
	}
}
