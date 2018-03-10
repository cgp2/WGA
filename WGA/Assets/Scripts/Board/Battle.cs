using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine.UI;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public static int n = 4;
    public static int m = 3;
    public static Card[,] Board;
    public static Transform[,] coor = new Transform[n, m];
    public static Player turn;
    public static Player pl1;
    public static Player pl2;
    public GameObject go;
    // Use this for initialization
    void Start()
    {

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
        if (turn == pl1)
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
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                //var x = Board[i, j].GetComponentInParent<Canvas>().GetComponent<GUIText>();
                // x.text = "" + Board[i, j].Health;

                if (Board[i, j] != null)
                {
                    //Board[i, j].Health += 1;
                    var x = Board[i, j].transform.GetChild(0);
                    x.GetChild(0).GetComponent<Text>().text = "" + Board[i, j].Health;
                    x.GetChild(1).GetComponent<Text>().text = "" + Board[i, j].Shield;
                    x.GetChild(2).GetComponent<Text>().text = "" + Board[i, j].Attack;
                    x.GetChild(3).GetComponent<Text>().text = "" + Board[i, j].Name;
                }
            }
        Debug.LogWarning(Board);
        Debug.LogWarning(coor);
    }
    public static void Set_Card(int x, int y, Card tg)
    {
        //Debug.LogWarning("" + x + y);
        //Debug.LogWarning(tg);
        Board[x, y] = tg;
        tg.Play();
        //GameObject.Find("Field").GetComponent<SkillMaster>().ApplyBufsToBoard(out Battle.Board);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (int i = 1; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    if (Board[i, j] != null)
                    {
                        if (Board[i, j].Owner == turn)
                        {
                            if (Board[i -1, j] != null)
                            {
                                if (Board[i - 1, j].Owner != turn)
                                {
                                    var temp = Fight(Board[i, j], Board[i - 1, j]);
                                    Board[i, j] = temp[0];
                                    Board[i - 1, j] = temp[1];
                                    if (Board[i, j].Health <= 0)
                                    {
                                       DestroyCard(i, j);
                                    }
                                    if (Board[i - 1, j].Health <= 0)
                                    {
                                      DestroyCard(i-1, j);
                                        //Board[i + 1, j] = null;
                                        if (Board[i, j] != null)
                                        {
                                            Board[i - 1, j] = Board[i, j];
                                            Board[i, j].transform.position = coor[i - 1, j].position;
                                            Board[i, j] = null;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Board[i - 1, j] = Board[i, j];
                                Board[i, j].transform.position = coor[i - 1, j].position;
                                Board[i, j] = null;
                            }
                        }
                    }
                }
            GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = n - 2; i >= 0; i--)
                for (int j = 0; j < m; j++)
                {
                    if (Board[i, j] != null)
                    {
                        if (Board[i, j].Owner == turn)
                        {
                            if (Board[i + 1, j] != null)
                            {
                                if (Board[i + 1, j].Owner != turn)
                                {
                                    var temp = Fight(Board[i, j], Board[i + 1, j]);
                                    Board[i, j] = temp[0];
                                    Board[i + 1, j] = temp[1];
                                    if (Board[i, j].Health <= 0)
                                    {
                                      DestroyCard(i, j);
                                    }
                                    if (Board[i + 1, j].Health <= 0)
                                    {
                                        DestroyCard(i+1, j);
                                        //Board[i + 1, j] = null;
                                        if (Board[i, j] != null)
                                        {
                                            Board[i + 1, j] = Board[i, j];
                                            Board[i, j].transform.position = coor[i + 1, j].position;
                                            Board[i, j] = null;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Board[i + 1, j] = Board[i, j];
                                Board[i, j].transform.position = coor[i + 1, j].position;
                                Board[i, j] = null;
                            }
                        }
                    }

                }
            GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();        
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (int i = 0; i < n; i++)
                for (int j = 1; j < m; j++)
                {
                    if (Board[i, j] != null)
                        if (Board[i, j].Owner == turn)
                        {
                            if (Board[i, j-1] != null)
                            {
                                if (Board[i, j-1].Owner != turn)
                                {
                                    var temp = Fight(Board[i, j], Board[i, j-1]);
                                    Board[i, j] = temp[0];
                                    Board[i, j-1] = temp[1];
                                    if (Board[i, j].Health <= 0)
                                    {
                                        DestroyCard(i, j);
                                    }

                                    if (Board[i, j-1].Health <= 0)
                                    {                                      
                                        DestroyCard(i, j-1);
                                        //Board[i + 1, j] = null;
                                        if (Board[i, j] != null)
                                        {
                                            Board[i, j-1] = Board[i, j];
                                            Board[i, j].transform.position = coor[i, j-1].position;
                                            Board[i, j] = null;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                Board[i, j-1] = Board[i, j];
                                Board[i, j].transform.position = coor[i, j-1].position;
                                Board[i, j] = null;
                            }
                        }
                }
            GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();         
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            for (int i = 0; i < n; i++)
                for (int j = m - 2; j >= 0; j--)
                {
                    if (Board[i, j] != null)
                        if (Board[i, j].Owner == turn)
                        {
                            if (Board[i, j + 1] != null)
                            {
                                if (Board[i, j + 1].Owner != turn)
                                {
                                    var temp = Fight(Board[i, j], Board[i, j + 1]);
                                    Board[i, j] = temp[0];
                                    Board[i, j + 1] = temp[1];

                                    if (Board[i, j].Health <= 0)
                                    {
                                        DestroyCard(i, j);
                                    }

                                    if (Board[i, j + 1].Health <= 0)
                                    {
                                        DestroyCard(i, j+1);
                                        //Board[i + 1, j] = null;
                                        if (Board[i, j] != null)
                                        {
                                            Board[i, j + 1] = Board[i, j];
                                            Board[i, j].transform.position = coor[i, j + 1].position;
                                            Board[i, j] = null;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                Board[i, j + 1] = Board[i, j];
                                Board[i, j].transform.position = coor[i, j + 1].position;
                                Board[i, j] = null;
                            }
                        }
                }
            GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
        }

    }

    public static void DestroyCard(int n, int m)
    {
        if (Board[n, m] != null)
        {
            Board[n, m].Destroy();
            Destroy(Board[n, m].gameObject);
        }
    }
    
    public List<Card> Fight(Card c1, Card c2)
    {
        var ret = new List<Card>();
        c1.StaticHP = c1.Health = c1.Shield < c2.Attack ? c1.Health - c2.Attack + c1.Shield : c1.Health;
        c1.StaticSHLD = c1.Shield = c1.Shield < c2.Attack ? 0 : c1.Shield - c2.Attack;
        c2.StaticHP = c2.Health = c2.Shield < c1.Attack ? c2.Health - c1.Attack + c2.Shield : c2.Health;
        c2.StaticSHLD = c2.Shield = c2.Shield < c1.Attack ? 0 : c2.Shield - c1.Attack;
        ret.Add(c1);
        ret.Add(c2);
        return ret;
    }
}
