using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour {
    public int n=4;
    public int m=3;
    public static Card[,] Board;
	// Use this for initialization
	void Start () {
        Board = new Card[n, m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                Debug.LogWarning(Board[i, j]);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
