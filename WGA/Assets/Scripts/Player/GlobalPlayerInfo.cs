using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalPlayerInfo : MonoBehaviour {
    public  PlayerInfo pl;
    // Use this for initialization

    void Start () {
        var pn = new ProfileNames();
        pn.InitializeLastProfile();
        var plName = pn.GetCurrentProfileName();
        pl = new PlayerInfo(plName);
        UpdateUI();

        
    }
	public void UpdateUI()
    {
        GameObject.Find("LvlText").GetComponent<Text>().text = "Level: " + pl.Level;
        GameObject.Find("Experience").GetComponent<Slider>().value = pl.Exp * 100f / pl.ExpToNextLevel;
        GameObject.Find("NameText").GetComponent<Text>().text = ""+pl.Name;
        GameObject.Find("GamesWin").GetComponent<Text>().text = "Games Win: " + pl.GamesWin;
        GameObject.Find("GamesLost").GetComponent<Text>().text = "Games Lost: " + pl.GamesLost;
        GameObject.Find("Avatar").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(pl.PathToAvatar);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
