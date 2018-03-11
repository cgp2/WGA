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

                        int k = i;
                        if (Board[k, j].Owner == turn)
                            {
                            while (k >= 1)
                            {
                                if (Board[k - 1, j] != null)
                                {
                                    if (Board[k - 1, j].Owner != turn)
                                    {

                                        var temp = Fight(Board[k, j], Board[k - 1, j]);
                                        Board[k, j] = temp[0];
                                        Board[k - 1, j] = temp[1];
                                        if (Board[k, j].Health <= 0)
                                        {
                                            DestroyCard(k, j);
                                        }
                                        if (Board[k - 1, j].Health <= 0)
                                        {
                                            DestroyCard(k - 1, j);
                                            if (Board[k, j] != null)
                                            {
                                                Board[k - 1, j] = Board[k, j];
                                                Board[k, j].transform.position = coor[k - 1, j].position;
                                                Board[k, j] = null;
                                            }
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    Board[k - 1, j] = Board[k, j];
                                    Board[k, j].transform.position = coor[k - 1, j].position;
                                    Board[k, j] = null;
                                }
                                k--;
                            }
                      
                        }
                    }
                }
            GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
            NextTurn();
            RollTheCards();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = n - 2; i >= 0; i--)
                for (int j = 0; j < m; j++)
                {
                    if (Board[i, j] != null)
                    {
                        int k = i;

                        if (Board[k, j].Owner == turn)
                        {
                            while (k <= n - 2)
                            {
                                if (Board[k + 1, j] != null)
                                {
                                    if (Board[k + 1, j].Owner != turn)
                                    {
                                        var temp = Fight(Board[k, j], Board[k + 1, j]);
                                        Board[k, j] = temp[0];
                                        Board[k + 1, j] = temp[1];
                                        if (Board[k, j].Health <= 0)
                                        {
                                            DestroyCard(k, j);
                                        }
                                        if (Board[k + 1, j].Health <= 0)
                                        {
                                            DestroyCard(k + 1, j);
                                            if (Board[k, j] != null)
                                            {
                                                Board[k + 1, j] = Board[k, j];
                                                Board[k, j].transform.position = coor[k + 1, j].position;
                                                Board[k, j] = null;
                                            }
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    Board[k + 1, j] = Board[k, j];
                                    Board[k, j].transform.position = coor[k + 1, j].position;
                                    Board[k, j] = null;
                                }
                                k++;
                            }

                        }
                    }

                }
            NextTurn();
            RollTheCards();
            GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (int i = 0; i < n; i++)
                for (int j = 1; j < m; j++)
                {
                    if (Board[i, j] != null)
                    {
                        int k = j;
                        if (Board[i, k].Owner == turn)
                        {
                            while (k >0)
                            {
                                if (Board[i, k - 1] != null)
                                {
                                    if (Board[i, k - 1].Owner != turn)
                                    {
                                        var temp = Fight(Board[i, k], Board[i, k - 1]);
                                        Board[i, k] = temp[0];
                                        Board[i, k - 1] = temp[1];
                                        if (Board[i, k].Health <= 0)
                                        {
                                            DestroyCard(i, k);
                                        }

                                        if (Board[i, k - 1].Health <= 0)
                                        {
                                            DestroyCard(i, k - 1);
                                            if (Board[i, k] != null)
                                            {
                                                Board[i, k - 1] = Board[i, k];
                                                Board[i, k].transform.position = coor[i, k - 1].position;
                                                Board[i, k] = null;
                                            }
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    Board[i, k - 1] = Board[i, k];
                                    Board[i, k].transform.position = coor[i, k - 1].position;
                                    Board[i, k] = null;
                                }

                                k--;
                            }
                        }
                    }
                    
                }
            NextTurn();
            RollTheCards();
            GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            for (int i = 0; i < n; i++)
                for (int j = m - 2; j >= 0; j--)
                {
                    if (Board[i, j] != null)
                    {
                        int k = j;
                        if (Board[i, k].Owner == turn)
                        {
                          
                            while (k <= m - 2)
                            {
                                if (Board[i, k + 1] != null)
                                {
                                    if (Board[i, k + 1].Owner != turn)
                                    {
                                        var temp = Fight(Board[i, k], Board[i, k + 1]);
                                        Board[i, k] = temp[0];
                                        Board[i, k + 1] = temp[1];

                                        if (Board[i, k].Health <= 0)
                                        {
                                            DestroyCard(i, k);
                                        }

                                        if (Board[i, k + 1].Health <= 0)
                                        {
                                            DestroyCard(i, k + 1);
                                            if (Board[i, k] != null)
                                            {
                                                Board[i, k + 1] = Board[i, k];
                                                Board[i, k].transform.position = coor[i, k + 1].position;
                                                Board[i, k] = null;
                                            }
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    Board[i, k + 1] = Board[i, k];
                                    Board[i, k].transform.position = coor[i, k + 1].position;
                                    Board[i, k] = null;
                                }
                                k++;
                            }

                        }
                    }
                }
            NextTurn();
            RollTheCards();
            GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
        }

    }

    public static void DestroyCard(int n, int m)
    {
        if (Board[n, m] != null)
        {
            Board[n, m].Destroy();
            Destroy(Board[n, m].gameObject);
            Board[n, m] = null;
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
