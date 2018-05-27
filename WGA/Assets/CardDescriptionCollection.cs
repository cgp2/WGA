using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardDescriptionCollection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private void OnMouseEnter()
    {
        var  CardDetail = GameObject.Find("CardDetail");
        var card = this.transform.parent.GetComponent<Card>();
        CardDetail.transform.GetChild(0).GetComponent<Image>().sprite = card.Info.ShipSprite;
        CardDetail.transform.GetChild(1).GetComponent<Text>().text = card.Info.Name;
    }
    private void OnMouseOver()
    {
        var CardDetail = GameObject.Find("CardDetail");
        var card = this.GetComponent<Card>();
        CardDetail.transform.GetChild(0).GetComponent<Image>().sprite = card.Info.ShipSprite;
        CardDetail.transform.GetChild(1).GetComponent<Text>().text = card.Info.Name;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
