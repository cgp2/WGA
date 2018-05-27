using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CardDescriptionCollection : MonoBehaviour, IPointerEnterHandler
{
    
	// Use this for initialization
	void Start () {
    
	}
   public void OnPointerEnter(PointerEventData eventData)
    {
        var  CardDetail = GameObject.Find("CardDetail");
        var card = this.transform.GetComponent<Card>();
        CardDetail.transform.GetChild(0).GetComponent<Image>().sprite = card.Info.ShipSprite;
        CardDetail.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 255);
        CardDetail.transform.GetChild(1).GetComponent<Text>().text = card.Info.Name;
       
        string temp = "";
        var obj = this.GetComponent<Card>();
        temp += obj.Info.Description;
        temp += "\n";
        if(obj.Info.BattleCryNames!=null)
        for (int i = 0; i < obj.Info.BattleCryNames.Length; i++)
        {
            temp += obj.Info.BattleCryNames[i] + ". Value: " + obj.Info.BattleCryValue[i] + "\n";
        }
        if(obj.Info.AuraNames!=null)
        for (int i = 0; i < obj.Info.AuraNames.Length; i++)
        {
            temp += obj.Info.AuraNames[i] + ". Value: " + obj.Info.AuraInputValue[i] + "\n";
        }
        if (obj.Info.DeathRattleName != null)
        for (int i = 0; i < obj.Info.DeathRattleName.Length; i++)
        {
            temp += obj.Info.DeathRattleName[i] + ". Value: " + obj.Info.DeathRattleInputValue[i] + "\n";
        }
        temp += obj.Info.ActiveSkillName + ". Value: " + obj.Info.ActiveSkillInput.InputParamsValues[0] + "\n";
       
        CardDetail.transform.GetChild(2).GetComponent<Text>().text = temp;
    }
    // Update is called once per frame
    void Update () {

	}
}
