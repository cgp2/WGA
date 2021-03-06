﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using System.IO;

public class Controller : MonoBehaviour
{
    public CanvasGroup CanGroupBattleEnd;
    public CanvasGroup CanvGroupBattleMenu;
    public CanvasGroup CanvGroupMain;

    // Use this for initialization
    void Start ()
	{
	    var t = GameObject.Find("BattleMenu");
        if (t!=null)
        {
            CanvGroupBattleMenu = t.GetComponentInChildren<CanvasGroup>();
            CanvGroupBattleMenu.alpha = 0f;
            CanvGroupBattleMenu.blocksRaycasts = false;
        }
	    t = GameObject.Find("BattleEndMenu");
        if (t!=null)
        {
            CanGroupBattleEnd = t.GetComponentInChildren<CanvasGroup>();
            CanGroupBattleEnd.alpha = 0f;
            CanGroupBattleEnd.blocksRaycasts = false;
        }
        t = GameObject.Find("Main");
        if (t!= null)
        {
            CanvGroupMain = t.GetComponentInChildren<CanvasGroup>();

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseGame()
    {
        if (Battle.Player1 != null)
            Battle.Player1.PlInfo.SaveToFile(Battle.Player1.PlInfo.Name);
        else
        {
            var pn = new ProfileNames();
            pn.InitializeLastProfile();
            var plName = pn.GetCurrentProfileName();
            GameObject.Find("Main Camera").GetComponent<GlobalPlayerInfo>().pl.SaveToFile(plName);
        }
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();    
    }

    public void MusicButtonClick()
    {
       SoundMaster.ToggleMusicSound();
    }

    public void ToCollection()
    {
        SoundMaster.PauseMusic();
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void ToOptions()
    {
        SoundMaster.PauseMusic();
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
    public void ToMainMenu()
    {
        if(Battle.Player1!=null)
            Battle.Player1.PlInfo.SaveToFile(Battle.Player1.PlInfo.Name);
        SoundMaster.PauseMusic();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void ReturnToGame()
    {
        CanvGroupMain.blocksRaycasts = true;

        CanvGroupBattleMenu.alpha = 0f;
        CanvGroupBattleMenu.blocksRaycasts = false;
    }
    public void ToBattle()
    {
        SoundMaster.ResetMusic();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void OpenCollection()
    {
        var cards = Card.Deserialize(Path.GetDirectoryName(Application.dataPath) + "/CardsInfo/PlayerCards.dat");

    }
    public void ShowMenu()
    {
        CanvGroupBattleMenu.alpha = 1f;
        CanvGroupBattleMenu.blocksRaycasts = true;

        CanvGroupMain.blocksRaycasts = false;
    }

    public void MoveLeft()
    {
        
        Battle.Move(Directions.Left);
    }

    public void MoveRight()
    {
        Battle.Move(Directions.Right);
    }

    public void MoveTop()
    {
        Battle.Move(Directions.Top);
    }

    public void MoveBottom()
    {
        Battle.Move(Directions.Bottom);
    }
}
