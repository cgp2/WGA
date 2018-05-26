using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using System.IO;
using UnityEngine.UI;
public class CollectionController : MonoBehaviour {
    public GameObject CollectionPrefab;
    public GameObject DeckPrefab;
    PlayerInfo PlInfo;
	// Use this for initialization
	void Start () {
        PlInfo = new PlayerInfo();
        //ItemGameObject is my prefab pointer that i previous made a public property  
        //and  assigned a prefab to it
        PlInfo.ReadFromFile(Path.GetDirectoryName(Application.dataPath) + "/PlayerInfo/PlayerInfo.dat");
        //var AllCards = Card.Deserialize(Path.GetDirectoryName(Application.dataPath) + "/CardsInfo/AllCards.dat");
        if(DeckMaster.AllCards==null)
            DeckMaster.AllCards = Card.Deserialize(Path.GetDirectoryName(Application.dataPath) + "/CardsInfo/AllCards.dat");
        foreach (Card.CardData card in DeckMaster.AllCards)
        {
            var c = (GameObject)Instantiate(CollectionPrefab, transform);
            
            c.GetComponent<Card>().Initialize(card);
            var q = c.GetComponent<Card>();
            //c.GetComponentInChildren<Text>().text = "null";
            c.GetComponentInChildren<Text>().text = c.GetComponent<Card>().Info.Name;
            c.transform.parent = GameObject.Find("Content").transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
