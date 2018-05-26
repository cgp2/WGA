using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalPlayerInfo : MonoBehaviour {
    public  PlayerInfo pl;
    // Use this for initialization

    void Start () {
        pl = new PlayerInfo(Application.dataPath + "/PlayerInfo/PlayerInfo.dat");
        UpdateUI();


    }
	public void UpdateUI()
    {
        GameObject.Find("LvlText").GetComponent<Text>().text = "LVL:" + pl.Level;
        GameObject.Find("ExpText").GetComponent<Text>().text = "EXP:" + pl.Exp;
        GameObject.Find("NameText").GetComponent<Text>().text = ""+pl.Name;
        GameObject.Find("Avatar").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(pl.PathToAvatar);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
