using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CardDescriptionCollection : MonoBehaviour, IPointerEnterHandler
{
    GameObject CardDetail;
    // Use this for initialization
    void Start () {
        CardDetail = GameObject.Find("CardDetail");
    }
   public void OnPointerEnter(PointerEventData eventData)
    {
        var  CardDetail = GameObject.Find("CardDetail");
        var card = this.transform.GetComponent<Card>();
        CardDetail.transform.GetChild(0).GetComponent<Image>().sprite = card.Info.ShipSprite;
        if(card.Info.ShipSprite==null)
            CardDetail.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0);
        else
        CardDetail.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 255);
        CardDetail.transform.GetChild(1).GetComponent<Text>().text = card.Info.Name;
       
        string temp = "";
        var obj = this.GetComponent<Card>();
        temp += obj.Info.Description;
        temp += "\n";
        if(obj.Data.BattleCryInputValue!=null)
        for (int i = 0; i < obj.Data.BattleCryNames.Length; i++)
        {
            temp += obj.Info.BattleCryNames[i] + ". Value: " + obj.Data.BattleCryInputValue[i] + "\n";
        }
        if(obj.Data.AurasNames!=null)
        for (int i = 0; i < obj.Info.AuraNames.Length; i++)
        {
            temp += obj.Info.AuraNames[i] + ". Value: " + obj.Data.AuraInputValue[i] + "\n";
        }
        if (obj.Data.DeathRattleNames != null)
        for (int i = 0; i < obj.Info.DeathRattleName.Length; i++)
        {
            temp += obj.Info.DeathRattleName[i] + ". Value: " + obj.Data.DeathRattleInputValue[i] + "\n";
        }
        if(obj.Data.ActiveSkillName!=null)
            temp += obj.Data.ActiveSkillName + ". Value: " + obj.Data.ActiveInputValue + "\n";
       
        CardDetail.transform.GetChild(2).GetComponent<Text>().text = temp;
    }
    // Update is called once per frame
    void Update () {

	}
}
