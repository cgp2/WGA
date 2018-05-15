﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

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
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();    
    }
    public void ToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void ReturnToGame()
    {
        CanvGroupMain.blocksRaycasts = true;

        CanvGroupBattleMenu.alpha = 0f;
        CanvGroupBattleMenu.blocksRaycasts = false;
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
