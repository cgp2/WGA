using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionCardManager : MonoBehaviour {
    CollectionController CollectionController;
    GameObject Collection;
    GameObject Deck;
    GameObject CardDetail;
    // Use this for initialization
    void Start () {
        CardDetail = GameObject.Find("CardDetail");
        CollectionController = GameObject.Find("CollectionController").GetComponent<CollectionController>();
        Collection = GameObject.Find("CollectionScrollView").transform.GetChild(0).GetChild(0).gameObject;
        Deck = GameObject.Find("DeckScrollView").transform.GetChild(0).GetChild(0).gameObject;
    }
    private void OnMouseEnter()
    {
        var card = this.GetComponent<Card>();
        CardDetail.transform.GetChild(0).GetComponent<Image>().sprite = card.Info.ShipSprite;
        CardDetail.transform.GetChild(1).GetComponent<Text>().text = card.Info.Name;
    }
    private void OnMouseExit()
    {
        var card = this.GetComponent<Card>();
        CardDetail.transform.GetChild(0).GetComponent<Image>().sprite = card.Info.ShipSprite;
        CardDetail.transform.GetChild(1).GetComponent<Text>().text = card.Info.Name;
    }
    public void OnMouseDown()
    {
        int num = -1;
        for(int i =0;i<Deck.transform.childCount;i++)
        {
            if (this.gameObject == Deck.transform.GetChild(i).gameObject)
                num = i;
        }
        if(num==-1)
        {
            if(CollectionController.PlayerDeck.Count<10)
            {
               var c = (GameObject)Instantiate(CollectionController.DeckPrefab, GameObject.Find("DeckScrollView").transform);

                //c.GetComponent<Card>().Initialize(this.GetComponent<Card>().Data);
                //var BCinp = this.GetComponent<Card>().Info.BattleCryInput != null ? this.GetComponent<Card>().Info.BattleCryInput[0].InputParamsValues[0] : null;
                //var DRinp = this.GetComponent<Card>().Info.DeathRattleInput != null ? this.GetComponent<Card>().Info.DeathRattleInput[0].InputParamsValues[0] : null;
                //var AUinp = this.GetComponent<Card>().Info.AuraInput != null ? this.GetComponent<Card>().Info.AuraInput[0].InputParamsValues[0] : null;
                //c.GetComponent<Card>().Initialize(this.GetComponent<Card>().Info.ID, this.GetComponent<Card>().Info.Name, this.GetComponent<Card>().Health, this.GetComponent<Card>().Shield, this.GetComponent<Card>().Attack, this.GetComponent<Card>().Info.Description, null,
                //            BCinp, DRinp, AUinp,
                //            this.GetComponent<Card>().Info.BattleCryNames, this.GetComponent<Card>().Info.DeathRattleName, this.GetComponent<Card>().Info.AuraNames);
                c.GetComponent<Card>().Initialize(this.GetComponent<Card>().Data);
                c.GetComponent<Card>().Info = this.GetComponent<Card>().Info;
                c.GetComponent<Card>().Info.ShipSprite = this.GetComponent<Card>().Info.ShipSprite;
                var q = c.GetComponent<Card>();
                //c.GetComponentInChildren<Text>().text = "null";
                c.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = this.GetComponent<Card>().Info.ShipSprite;
                c.transform.GetChild(2).GetComponent<Text>().text = c.GetComponent<Card>().Info.Name;
                c.transform.GetChild(4).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialAttack;
                c.transform.GetChild(3).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialHealth;
                c.transform.GetChild(5).GetComponent<Text>().text = "" + c.GetComponent<Card>().Info.InitialShield;
                c.transform.parent = GameObject.Find("DeckScrollView").transform.GetChild(0).GetChild(0).transform;
                CollectionController.PlayerDeck.Add(c.GetComponent<Card>().Data);
                
            }
        }
        else
        {
            CollectionController.PlayerDeck.RemoveAt(num);

            GameObject.Destroy(Deck.transform.GetChild(num).gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
