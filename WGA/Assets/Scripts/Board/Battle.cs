using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public static Player Player1;
    public static Player Player2;
    public GameObject go;

    public static int TurnNumber;

    private static bool lockedInput = false;
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

        Player1 = GameObject.Find("Player1").GetComponent<Player>();
        Player2 = GameObject.Find("Player2").GetComponent<Player>();
        turn = Player1;
        TurnNumber = 1;
        GameObject.Find("RaundText").GetComponent<Text>().text = "Raund " + TurnNumber;
    }
    public static void NextTurn()
    {
        if (turn == Player2)
        {
            turn = Player1;
            TurnNumber++;
            GameObject.Find("RaundText").GetComponent<Text>().text = "Raund " + TurnNumber;
            //for (var i = 0; i < Board.GetLength(0); i++)
            //{
            //    for (var j = 0; j < Board.GetLength(1); j++)
            //    {
            //        var crd = Board[i, j];
            //        if (crd != null)
            //        {
            //            if (crd.Info.InitialShield > crd.Shield)
            //            {
            //                crd.Shield++;
            //                if (crd.Owner.name == "Player1")
            //                {
            //                    GameObject.Find("Field").GetComponent<SkillMaster>().BufMap[i, j].StaticShieldBufPlayer1++;
            //                }
            //                else
            //                {
            //                    GameObject.Find("Field").GetComponent<SkillMaster>().BufMap[i, j].StaticShieldBufPlayer2++;
            //                }

            //                var x = crd.transform.GetChild(0);
            //                x.GetChild(1).GetComponent<Text>().text = "" + crd.Shield;
            //            }
            //        }
            //    }
            //}

            //UpdateUI();
        }
        else
            turn = Player2;
     
        if (TurnNumber == 20)
        {
            var winner = CalculateWiningPlayer();
            lockedInput = true;
            GameObject.Find("WinnerText").GetComponent<Text>().text = "Winner is  "+winner.name;
        }
    }

    private static Player CalculateWiningPlayer()
    {
        var pl1Score = 0;
        var pl2Score = 0;

        foreach (var card in Board)
        {
            if (card != null)
            {
                if (card.Owner.name == "Player1")
                {
                    pl1Score += card.Attack + card.Health + card.Shield;
                }
                else
                    pl2Score += card.Attack + card.Health + card.Shield;
            }
        }

        var ret = pl1Score >= pl2Score ? Player1 : Player2;
        return ret;
    }

    public static void RollTheCards()
    {
        if (turn == Player1)
        {
            for (int i = 0; i < Player1.deck.Count; i++)
            {
                Player1.deck[i].GetComponent<Card>().Spin(true);
                Player1.deck[i].GetComponentInChildren<Canvas>().enabled = true;
                Player1.deck[i].transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            }
            for (int i = 0; i < Player2.deck.Count; i++)
                if (!Player2.deck[i].GetComponent<Card>().OnBoard)
                {
                    Player2.deck[i].GetComponent<Card>().Spin(false);
                    Player2.deck[i].GetComponentInChildren<Canvas>().enabled = false;
                    Player2.deck[i].transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false ;
                }
        }
        else
        {
            for (int i = 0; i < Player2.deck.Count; i++)
            {
                Player2.deck[i].GetComponent<Card>().Spin(true);
                Player2.deck[i].GetComponentInChildren<Canvas>().enabled = true;
                Player2.deck[i].transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            }
            for (int i = 0; i < Player1.deck.Count; i++)
                if (!Player1.deck[i].GetComponent<Card>().OnBoard)
                {
                    Player1.deck[i].GetComponent<Card>().Spin(false);
                    Player1.deck[i].GetComponentInChildren<Canvas>().enabled = false;
                    Player1.deck[i].transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
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
                // x.text = "" + Board[i, j].InitialHealth;

                if (Board[i, j] != null)
                {
                    //Board[i, j].InitialHealth += 1;
                    var x = Board[i, j].transform.GetChild(0);
                    x.GetChild(0).GetComponent<Text>().text = "" + Board[i, j].Health;
                    x.GetChild(1).GetComponent<Text>().text = "" + Board[i, j].Shield;
                    x.GetChild(2).GetComponent<Text>().text = "" + Board[i, j].Attack;
                    x.GetChild(3).GetComponent<Text>().text = "" + Board[i, j].Info.Name;
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
        if (!lockedInput)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                bool moved = false;
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

                                        moved = true;
                                    }

                                    break;
                                }
                                else
                                {
                                    Board[k - 1, j] = Board[k, j];
                                    Board[k, j].transform.position = coor[k - 1, j].position;
                                    Board[k, j] = null;
                                    moved = true;
                                }

                                k--;
                            }

                        }
                    }
                }

                if (moved)
                {
                    GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
                    NextTurn();
                    RollTheCards();
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                bool moved = false;
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

                                        moved = true;
                                    }

                                    break;
                                }

                                Board[k + 1, j] = Board[k, j];
                                Board[k, j].transform.position = coor[k + 1, j].position;
                                Board[k, j] = null;
                                moved = true;
                                k++;
                            }

                        }
                    }

                }

                if (moved)
                {
                    NextTurn();
                    RollTheCards();
                    GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                bool moved = false;
                for (int i = 0; i < n; i++)
                for (int j = 1; j < m; j++)
                {

                    if (Board[i, j] != null)
                    {
                        int k = j;
                        if (Board[i, k].Owner == turn)
                        {
                            while (k > 0)
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

                                        moved = true;
                                    }

                                    break;
                                }
                                else
                                {
                                    Board[i, k - 1] = Board[i, k];
                                    Board[i, k].transform.position = coor[i, k - 1].position;
                                    Board[i, k] = null;
                                    moved = true;
                                }

                                k--;
                            }
                        }
                    }

                }

                if (moved)
                {
                    NextTurn();
                    RollTheCards();
                    GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                bool moved = false;
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

                                        moved = true;
                                    }

                                    break;
                                }
                                else
                                {
                                    Board[i, k + 1] = Board[i, k];
                                    Board[i, k].transform.position = coor[i, k + 1].position;
                                    Board[i, k] = null;
                                    moved = true;
                                }

                                k++;
                            }

                        }
                    }
                }

                if (moved)
                {
                    NextTurn();
                    RollTheCards();
                    GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
                }
            }
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
