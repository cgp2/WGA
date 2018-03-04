using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour {
    public int n=4;
    public int m=3;
    private static Card[,] Board;
	// Use this for initialization
	void Start () {
        Board = new Card[n, m];

	}
    public static Card Get_Card(int x, int y)
    {
        return Board[x,y] ?? null;
    }
    public static void Set_Card(int x,int y, Card tg)
    {
        Debug.LogWarning("" + x + y);
        Debug.LogWarning(tg);
        Board[x, y] = tg;

    }
	// Update is called once per frame
	void Update () {
		
	}
}
