using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Battle : MonoBehaviour {
    public static int  n = 4;
    public static int m = 3;
    private static Card[,] Board;
    public static Transform[,] coor;
    public static Player turn;
    public static Player pl1;
    public static Player pl2;
    public GameObject go;
    // Use this for initialization
    void Start() {
    
    }
    public static Card Get_Card(int x, int y)
    {
        return Board[x, y] ?? null;
    }
    private void Awake()
    { 
        Board = new Card[n, m];
        coor = new Transform[n, m];
        pl1 = GameObject.Find("Player1").GetComponent<Player>();
        pl2 = GameObject.Find("Player2").GetComponent<Player>();
        turn = pl1;
        
    }
    public static void NextTurn()
    {
        if (turn == pl2)
            turn = pl1;
        else
            turn = pl2;
    }
    public static void RollTheCards()
    {
        if(turn==pl1)
        {
            for (int i = 0; i < pl1.deck.Count; i++)
            {
                pl1.deck[i].GetComponent<Card>().Spin(true);
                pl1.deck[i].GetComponentInChildren<Canvas>().enabled = true;
            }
            for (int i = 0; i < pl2.deck.Count; i++)
                if (!pl2.deck[i].GetComponent<Card>().OnBoard)
                {
                    pl2.deck[i].GetComponent<Card>().Spin(false);
                    pl2.deck[i].GetComponentInChildren<Canvas>().enabled = false;
                }
        }
        else
        {
            for (int i = 0; i < pl2.deck.Count; i++)
            {
                pl2.deck[i].GetComponent<Card>().Spin(true);
                pl2.deck[i].GetComponentInChildren<Canvas>().enabled = true;
            }
            for (int i = 0; i < pl1.deck.Count; i++)
                if (!pl1.deck[i].GetComponent<Card>().OnBoard)
                {
                    pl1.deck[i].GetComponent<Card>().Spin(false);
                    pl1.deck[i].GetComponentInChildren<Canvas>().enabled = false;
                }
        }
        UpdateUI();
    }
    public static void UpdateUI()
    {
        for(int i =0;i<n;i++)
            for(int j=0;j<m;j++)
            {
                //var x = Board[i, j].GetComponentInParent<Canvas>().GetComponent<GUIText>();
                // x.text = "" + Board[i, j].Health;

                if (Board[i, j] != null)
                {
                    //Board[i, j].Health += 1;
                    var x = Board[i, j].transform.GetChild(0);
                        x.GetChild(0).GetComponent<Text>().text =""+ Board[i, j].Health;
                    x.GetChild(1).GetComponent<Text>().text = "" + Board[i, j].Shield;
                    x.GetChild(2).GetComponent<Text>().text = "" + Board[i, j].Attack;
                    x.GetChild(3).GetComponent<Text>().text = "" + Board[i, j].Name;
                    var q = 0;
                }
            }
    }
    public int get_n()
    {
        return n;
    }
    public static void Set_Card(int x, int y, Card tg)
    {
        //Debug.LogWarning("" + x + y);
        //Debug.LogWarning(tg);
        Board[x, y] = tg;

    }
	// Update is called once per frame
	void Update () {
		
	}
}
