using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour {
    public int n = 4;
    public int m = 3;
    private static Card[,] Board;
    public static Player turn;
    public static Player pl1;
    public static Player pl2;
    // Use this for initialization
    void Start() {
        RollTheCards();
    }
    public static Card Get_Card(int x, int y)
    {
        return Board[x, y] ?? null;
    }
    private void Awake()
    {
        Board = new Card[n, m];
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
                pl1.deck[i].GetComponent<Card>().Spin(true);
            for (int i = 0; i < pl2.deck.Count; i++)
                if (!pl2.deck[i].GetComponent<Card>().OnBoard)
                    pl2.deck[i].GetComponent<Card>().Spin(false);
        }
        else
        {
            for (int i = 0; i < pl2.deck.Count; i++)
                pl2.deck[i].GetComponent<Card>().Spin(true);
            for (int i = 0; i < pl1.deck.Count; i++)
                if (!pl1.deck[i].GetComponent<Card>().OnBoard)
                    pl1.deck[i].GetComponent<Card>().Spin(false);
        }
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
