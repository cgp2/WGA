using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMovementAnimation : MonoBehaviour
{

    //Use this for initialization
    public bool nextTurn = false;
    void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {
        if (nextTurn)
        {
            bool temp = true ;
            for (int i = 0; i < Battle.n; i++)
                for (int j =0;j<Battle.m; j++)
                    if(Battle.Board[i,j]!=null)
                        if (Battle.Board[i, j].GetComponent<MovementAnimation>().actions.Count != 0)
                            temp = false;
            if (temp)
            {
                Battle.NextTurn();
                nextTurn = false;
            }
        }
    }
    public static Directions ReverseDirection(Directions dir)
    {
        switch (dir)
        {
            case Directions.Top:
                return Directions.Bottom;
            case Directions.Bottom:
                return Directions.Top;
            case Directions.Left:
                return Directions.Right;
            case Directions.Right:
                return Directions.Left;
            default:
                return Directions.Map;
        }
    }
    //public static void Move(Card[,] Board, Directions dir)
    //{
    //    switch (dir)
    //    {
    //        case Directions.Top:
    //            {
    //                for (int i = 0; i < Battle.m; i++)
    //                {
    //                    int j = Battle.n - 2;
    //                    int k = j;
    //                    while (j >= 0)
    //                    {
    //                        k = j;
    //                        if (Board[j, i] != null)
    //                        {
    //                            if (Board[j, i].Owner == Battle.turn)
    //                            {
    //                                if (Board[j + 1, i] == null)
    //                                {

    //                                    Board[j, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart + Card_Place_Creation.t.y);
    //                                    Board[j + 1, i] = Board[j, i];
    //                                    Board[j, i] = null;
    //                                    for (k = j; k >= 0; k--)
    //                                    {
    //                                        if (Board[k, i] != null)
    //                                        {
    //                                            if (Board[k, i].Owner == Battle.turn)
    //                                            {
    //                                                if (Board[k + 1, i] == null)
    //                                                {
    //                                                    Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart + Card_Place_Creation.t.y);
    //                                                    Board[k + 1, i] = Board[k, i];
    //                                                    Board[k, i] = null;
    //                                                    k += 2;
    //                                                    if (k > Battle.n - 1)
    //                                                        k = Battle.n - 2;
    //                                                }
    //                                                else
    //                                                {
    //                                                    if (Board[k + 1, i].Owner == Battle.turn)
    //                                                        Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0);
    //                                                    else
    //                                                    {
    //                                                        Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, (Card_Place_Creation.yMarginStandart + Card_Place_Creation.t.y) / 2 - 1);
    //                                                        Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
    //                                                        Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), (Card_Place_Creation.yMarginStandart + Card_Place_Creation.t.y) / 2 - 1);
    //                                                        Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
    //                                                        if (Board[k, i].Health > 0)
    //                                                        {
    //                                                            if (Board[k + 1, i].Health <= 0)
    //                                                            {
    //                                                                Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
    //                                                                Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, (Card_Place_Creation.yMarginStandart + Card_Place_Creation.t.y) / 2 + 1);
    //                                                                Board[k + 1, i] = Board[k, i];
    //                                                                Board[k, i] = null;
    //                                                            }
    //                                                            else
    //                                                            {
    //                                                                Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, (Card_Place_Creation.yMarginStandart + Card_Place_Creation.t.y) / 2 - 1);
    //                                                                Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), (Card_Place_Creation.yMarginStandart + Card_Place_Creation.t.y) / 2 - 1);
    //                                                            }
    //                                                        }
    //                                                        else
    //                                                        {
    //                                                            Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
    //                                                            if (Board[k + 1, i].Health <= 0)
    //                                                            {
    //                                                                Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
    //                                                            }
    //                                                            else
    //                                                            {
    //                                                                Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, (Card_Place_Creation.yMarginStandart + Card_Place_Creation.t.y) / 2 - 1);

    //                                                            }

    //                                                        }
    //                                                    }
    //                                                }
    //                                            }
    //                                        }
    //                                    }
    //                                    j += 2;
    //                                    if (j > Battle.n - 1)
    //                                        j = Battle.n - 2;
    //                                }
    //                                else
    //                                {
    //                                    if (Board[j + 1, i].Owner == Battle.turn)
    //                                    {
    //                                        Board[j, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0);
    //                                        for (k = j; k >= 0; k--)
    //                                        {
    //                                            if (Board[k, i] != null)
    //                                            {
    //                                                if (Board[k, i].Owner == Battle.turn)
    //                                                {
    //                                                    if (Board[k + 1, i] == null)
    //                                                    {
    //                                                        Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart);
    //                                                        Board[k + 1, i] = Board[k, i];
    //                                                        Board[k, i] = null;
    //                                                        k += 2;
    //                                                        if (k > Battle.n - 1)
    //                                                            k = Battle.n - 2;
    //                                                    }
    //                                                    else
    //                                                    {
    //                                                        if (Board[k + 1, i].Owner == Battle.turn)
    //                                                            Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0);
    //                                                        else
    //                                                        {
    //                                                            Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart / 2 - 1);
    //                                                            Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
    //                                                            Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), Card_Place_Creation.yMarginStandart / 2 - 1);
    //                                                            Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
    //                                                            if (Board[k, i].Health > 0)
    //                                                            {
    //                                                                if (Board[k + 1, i].Health <= 0)
    //                                                                {
    //                                                                    Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
    //                                                                    Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart / 2 + 1);
    //                                                                    Board[k + 1, i] = Board[k, i];
    //                                                                    Board[k, i] = null;
    //                                                                }
    //                                                                else
    //                                                                {
    //                                                                    Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, Card_Place_Creation.yMarginStandart - 1);
    //                                                                    Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), Card_Place_Creation.yMarginStandart / 2 - 1);
    //                                                                }
    //                                                            }
    //                                                            else
    //                                                            {
    //                                                                Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
    //                                                                if (Board[k + 1, i].Health <= 0)
    //                                                                {
    //                                                                    Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
    //                                                                }
    //                                                                else
    //                                                                {
    //                                                                    Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, Card_Place_Creation.yMarginStandart - 1);

    //                                                                }

    //                                                            }
    //                                                        }
    //                                                    }
    //                                                }
    //                                            }
    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        Board[j, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, (Card_Place_Creation.yMarginStandart) / 2 - 1);
    //                                        Board[j, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
    //                                        Board[j + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), -(Card_Place_Creation.yMarginStandart) / 2 - 1);
    //                                        Board[j + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
    //                                        Battle.Fight(Board[j, i], Board[j + 1, i]);
    //                                        if (Board[j, i].Health > 0)
    //                                        {
    //                                            if (Board[j + 1, i].Health <= 0)
    //                                            {
    //                                                Board[j + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
    //                                                Board[j, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, (Card_Place_Creation.yMarginStandart + Card_Place_Creation.t.y) / 2 + 1);
    //                                                Board[j + 1, i] = Board[j, i];
    //                                                Board[j, i] = null;
    //                                            }
    //                                            else
    //                                            {
    //                                                Board[j + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart + Card_Place_Creation.t.y - 1);
    //                                                Board[j, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), -(Card_Place_Creation.yMarginStandart + Card_Place_Creation.t.y) / 2 - 1);
    //                                            }
    //                                        }
    //                                        else
    //                                        {
    //                                            Board[j, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
    //                                            if (Board[j + 1, i].Health <= 0)
    //                                            {
    //                                                Board[j + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
    //                                            }
    //                                            else
    //                                            {
    //                                                Board[j + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart + Card_Place_Creation.t.y - 1);

    //                                            }



    //                                        }
    //                                        for (k = j-1; k >= 0; k--)
    //                                        {
    //                                            if (Board[k, i] != null)
    //                                            {
    //                                                if (Board[k, i].Owner == Battle.turn)
    //                                                {
    //                                                    if (Board[k + 1, i] == null)
    //                                                    {
    //                                                        Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart+Card_Place_Creation.t.y);
    //                                                        Board[k + 1, i] = Board[k, i];
    //                                                        Board[k, i] = null;
    //                                                        k += 2;
    //                                                        if (k > Battle.n - 1)
    //                                                            k = Battle.n - 2;
    //                                                    }
    //                                                    else
    //                                                    {
    //                                                        if (Board[k + 1, i].Owner == Battle.turn)
    //                                                            Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0);
    //                                                        else
    //                                                        {
    //                                                            Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart / 2 - 1);
    //                                                            Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
    //                                                            Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), Card_Place_Creation.yMarginStandart / 2 - 1);
    //                                                            Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
    //                                                            if (Board[k, i].Health > 0)
    //                                                            {
    //                                                                if (Board[k + 1, i].Health <= 0)
    //                                                                {
    //                                                                    Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
    //                                                                    Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart / 2 + 1);
    //                                                                    Board[k + 1, i] = Board[k, i];
    //                                                                    Board[k, i] = null;
    //                                                                }
    //                                                                else
    //                                                                {
    //                                                                    Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, Card_Place_Creation.yMarginStandart - 1);
    //                                                                    Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), Card_Place_Creation.yMarginStandart / 2 - 1);
    //                                                                }
    //                                                            }
    //                                                            else
    //                                                            {
    //                                                                Board[k, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
    //                                                                if (Board[k + 1, i].Health <= 0)
    //                                                                {
    //                                                                    Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
    //                                                                }
    //                                                                else
    //                                                                {
    //                                                                    Board[k + 1, i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, Card_Place_Creation.yMarginStandart - 1);

    //                                                                }

    //                                                            }
    //                                                        }
    //                                                    }
    //                                                }
    //                                            }
    //                                        }
    //                                    }
    //                                }





    //                            }
    //                        }
    //                        j--;
    //                    }
    //                }
    //                break;

    //            }
    //    }
    //}
    public Card[,] Move(Card[,] Board, Directions dir)
    {
        
        int n = Battle.n;
        int m = Battle.m;
        var coor = Battle.coor;

        switch (dir)
        {
            case Directions.Top:
                {
                    bool moved = false;
                    for (int i = n - 2; i >= 0; i--)
                        for (int j = 0; j < m; j++)
                        {
                            if (Board[i, j] != null)
                            {
                                int k = i;

                                if (Board[k, j].Owner == Battle.turn)
                                {
                                    while (k <= n - 2)
                                    {
                                        if (Board[k + 1, j] != null)
                                        {
                                            if (Board[k + 1, j].Owner != Battle.turn)
                                            {
                                                if (Board[k + 1, j].GetComponent<MovementAnimation>().totalTime < Board[k, j].GetComponent<MovementAnimation>().totalTime)
                                                    Board[k + 1, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0, Board[k, j].GetComponent<MovementAnimation>().totalTime - Board[k + 1, j].GetComponent<MovementAnimation>().totalTime);
                                                if (Board[k, j].GetComponent<MovementAnimation>().totalTime < Board[k + 1, j].GetComponent<MovementAnimation>().totalTime)
                                                    Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0, Board[k + 1, j].GetComponent<MovementAnimation>().totalTime - Board[k, j].GetComponent<MovementAnimation>().totalTime);
                                                Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart / 2 - 1);
                                                Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
                                                Board[k + 1, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), Card_Place_Creation.yMarginStandart / 2 - 1);
                                                Board[k + 1, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);

                                                for (int q = i - 1; q >= 0; q--)
                                                    if (Board[q, j] != null)
                                                        if (Board[q, j].Owner == Battle.turn)
                                                        {
                                                            int w = q;
                                                            while (w < k - 1 && Board[w + 1, j] == null)
                                                            {
                                                                Board[w, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, coor[w + 1, j].transform.position.y - coor[w, j].transform.position.y);
                                                                Board[w + 1, j] = Board[w, j];
                                                                Board[w, j] = null;
                                                                w++;
                                                            }
                                                            Board[w, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0);
                                                            if (w != n - 1)
                                                                i = w + 1; //ОСТОРОЖНО, ЛУЧШЕ НЕ ПОВТОРЯТЬ ЭТУ ХУЙНЮ САМОСТОЯТЕЛЬНО

                                                        }

                                                var temp = Battle.Fight(Board[k, j], Board[k + 1, j]);
                                                Board[k, j] = temp[0];
                                                Board[k + 1, j] = temp[1];
                                                if (Board[k, j].Health <= 0)
                                                {
                                                    //Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
                                                    Board[k, j] = null;
                                                }
                                                else
                                                {
                                                    Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), Card_Place_Creation.yMarginStandart / 2 - 1);
                                                }
                                                if (Board[k + 1, j].Health <= 0)
                                                {
                                                    //Board[k + 1, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.destroy, dir, 0);
                                                    Board[k + 1, j] = null;
                                                    if (Board[k, j] != null)
                                                    {
                                                        Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, coor[k + 1, j].transform.position.y - coor[k, j].transform.position.y);
                                                        Board[k + 1, j] = Board[k, j];
                                                        //Board[k, j].transform.position = coor[k + 1, j].transform.position;
                                                        Board[k, j] = null;
                                                    }
                                                }
                                                else
                                                {
                                                    Board[k + 1, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart / 2 - 1);
                                                }

                                                moved = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, coor[k + 1, j].transform.position.y - coor[k, j].transform.position.y);
                                            Board[k + 1, j] = Board[k, j];
                                            //Board[k, j].transform.position = coor[k + 1, j].transform.position;
                                            Board[k, j] = null;
                                            moved = true;
                                            k++;
                                        }
                                    }

                                }
                            }

                        }

                    if(moved)
                    {
                        nextTurn = true;
                        GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
                        //Battle.NextTurn();

                    }
                    break;
                }
            case Directions.Bottom:
                {
                    bool moved = false;
                    for (int i = 1; i < n; i++)
                        for (int j = 0; j < m; j++)
                        {

                            if (Board[i, j] != null)
                            {

                                int k = i;
                                if (Board[k, j].Owner == Battle.turn)
                                {
                                    while (k >= 1)
                                    {
                                        if (Board[k - 1, j] != null)
                                        {
                                            if (Board[k - 1, j].Owner != Battle.turn)
                                            {
                                                if (Board[k - 1, j].GetComponent<MovementAnimation>().totalTime < Board[k, j].GetComponent<MovementAnimation>().totalTime)
                                                    Board[k - 1, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0, Board[k, j].GetComponent<MovementAnimation>().totalTime - Board[k - 1, j].GetComponent<MovementAnimation>().totalTime);
                                                if (Board[k, j].GetComponent<MovementAnimation>().totalTime < Board[k - 1, j].GetComponent<MovementAnimation>().totalTime)
                                                    Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0, Board[k - 1, j].GetComponent<MovementAnimation>().totalTime - Board[k, j].GetComponent<MovementAnimation>().totalTime);
                                                Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart / 2 - 1);
                                                Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
                                                Board[k - 1, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), Card_Place_Creation.yMarginStandart / 2 - 1);
                                                Board[k - 1, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);

                                                for (int q = i + 1; q < n; q++)
                                                    if (Board[q, j] != null)
                                                        if (Board[q, j].Owner == Battle.turn)
                                                        {
                                                            int w = q;
                                                            while (w < k + 1 && Board[w - 1, j] == null)
                                                            {
                                                                Board[w, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, coor[w - 1, j].transform.position.y - coor[w, j].transform.position.y);
                                                                Board[w - 1, j] = Board[w, j];
                                                                Board[w, j] = null;
                                                                w--;
                                                            }
                                                            Board[w, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0);
                                                            if (w != 0)
                                                                i = w - 1; //ОСТОРОЖНО, ЛУЧШЕ НЕ ПОВТОРЯТЬ ЭТУ ХУЙНЮ САМОСТОЯТЕЛЬНО

                                                        }


                                                var temp = Battle.Fight(Board[k, j], Board[k - 1, j]);
                                                Board[k, j] = temp[0];
                                                Board[k - 1, j] = temp[1];
                                                if (Board[k, j].Health <= 0)
                                                {
                                                    Board[k, j] = null;
                                                }
                                                else
                                                {
                                                    Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), Card_Place_Creation.yMarginStandart / 2 - 1);
                                                }
                                                if (Board[k - 1, j].Health <= 0)
                                                {
                                                    //DestroyCard(k - 1, j);
                                                    Board[k - 1, j] = null;
                                                    if (Board[k, j] != null)
                                                    {
                                                        Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, -coor[k - 1, j].transform.position.y + coor[k, j].transform.position.y);

                                                        Board[k - 1, j] = Board[k, j];

                                                        Board[k, j] = null;
                                                    }
                                                }
                                                else
                                                {
                                                    Board[k - 1, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.yMarginStandart / 2 - 1);
                                                }

                                                moved = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Board[k, j].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, -coor[k - 1, j].transform.position.y + coor[k, j].transform.position.y);
                                            Board[k - 1, j] = Board[k, j];
                                            //Board[k, j].transform.position = coor[k - 1, j].transform.position;
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
                        nextTurn = true;
                        //Battle.NextTurn();
                    }
                    break;
                }
            case Directions.Left:
                {
                    bool moved = false;
                    for (int i = 0; i < n; i++)
                        for (int j = 1; j < m; j++)
                        {

                            if (Board[i, j] != null)
                            {
                                int k = j;
                                if (Board[i, k].Owner == Battle.turn)
                                {
                                    while (k > 0)
                                    {
                                        if (Board[i, k - 1] != null)
                                        {
                                            if (Board[i, k - 1].Owner != Battle.turn)
                                            {
                                                if (Board[i, k - 1].GetComponent<MovementAnimation>().totalTime < Board[i, k].GetComponent<MovementAnimation>().totalTime)
                                                    Board[i, k - 1].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0, Board[i, k].GetComponent<MovementAnimation>().totalTime - Board[i, k - 1].GetComponent<MovementAnimation>().totalTime);
                                                if (Board[i, k].GetComponent<MovementAnimation>().totalTime < Board[i, k].GetComponent<MovementAnimation>().totalTime)
                                                    Board[i, k].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0, Board[i, k - 1].GetComponent<MovementAnimation>().totalTime - Board[i, k].GetComponent<MovementAnimation>().totalTime);
                                                Board[i, k].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.xMarginStandart / 2 - 1);
                                                Board[i, k].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
                                                Board[i, k - 1].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), Card_Place_Creation.xMarginStandart / 2 - 1);
                                                Board[i, k - 1].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
                                                
                                                for (int q = j + 1; q < m; q++)
                                                    if (Board[i, q] != null)
                                                        if (Board[i, q].Owner == Battle.turn)
                                                        {
                                                            int w = q;
                                                            while (w < k + 1 && Board[i, w - 1] == null)
                                                            {
                                                                Board[i, w].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, coor[i, w - 1].transform.position.x - coor[i, w].transform.position.x);
                                                                Board[i, w - 1] = Board[i, w];
                                                                Board[i, w] = null;
                                                                w--;
                                                            }
                                                            Board[i, w].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0);
                                                            if (w != 0)
                                                                j = w -1; //ОСТОРОЖНО, ЛУЧШЕ НЕ ПОВТОРЯТЬ ЭТУ ХУЙНЮ САМОСТОЯТЕЛЬНО

                                                        }
                                                
                                                var temp = Battle.Fight(Board[i, k], Board[i, k - 1]);
                                                Board[i, k] = temp[0];
                                                Board[i, k - 1] = temp[1];
                                                if (Board[i, k].Health <= 0)
                                                {
                                                    //DestroyCard(i, k);
                                                    Board[i, k] = null;
                                                }
                                                else
                                                {
                                                    Board[i, k].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), Card_Place_Creation.xMarginStandart / 2 - 1);
                                                }
                                                if (Board[i, k - 1].Health <= 0)
                                                {
                                                    //DestroyCard(i, k - 1);
                                                    if (Board[i, k] != null)
                                                    {
                                                        Board[i, k - 1] = Board[i, k];
                                                        Board[i, k].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, -coor[i, k - 1].transform.position.x + coor[i, k].transform.position.x);
                                                        //Board[i, k].transform.position = coor[i, k - 1].transform.position;
                                                        Board[i, k] = null;

                                                    }
                                                }
                                                else
                                                {
                                                    Board[i, k - 1].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.xMarginStandart / 2 - 1);
                                                }

                                                moved = true;
                                                break;
                                            }

                                            
                                        }
                                        else
                                        {
                                            Board[i, k - 1] = Board[i, k];
                                            Board[i, k].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, coor[i, k + 1].transform.position.x - coor[i, k].transform.position.x);
                                            //Board[i, k].transform.position = coor[i, k - 1].transform.position;
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
                        nextTurn = true;
                        GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
                    }
                    break;
                }
            case Directions.Right:
                {
                    bool moved = false;
                    for (int i = 0; i < n; i++)
                        for (int j = m - 2; j >= 0; j--)
                        {
                            if (Board[i, j] != null)
                            {
                                int k = j;
                                if (Board[i, k].Owner == Battle.turn)
                                {

                                    while (k <= m - 2)
                                    {           
                                        if (Board[i, k + 1] != null)
                                        {
                                            
                                            if (Board[i, k + 1].Owner != Battle.turn)
                                            {
                                                if (Board[i, k + 1].GetComponent<MovementAnimation>().totalTime < Board[i, k].GetComponent<MovementAnimation>().totalTime)
                                                    Board[i, k + 1].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0, Board[i, k].GetComponent<MovementAnimation>().totalTime - Board[i, k + 1].GetComponent<MovementAnimation>().totalTime);
                                                if (Board[i, k].GetComponent<MovementAnimation>().totalTime < Board[i, k].GetComponent<MovementAnimation>().totalTime)
                                                    Board[i, k].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0, Board[i, k + 1].GetComponent<MovementAnimation>().totalTime - Board[i, k].GetComponent<MovementAnimation>().totalTime);
                                                Board[i, k].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.xMarginStandart / 2 - 1);
                                                Board[i, k].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);
                                                Board[i, k + 1].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), Card_Place_Creation.xMarginStandart / 2 - 1);
                                                Board[i, k + 1].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.fight, dir, 0);

                                                for (int q = j - 1; q >= 0; q--)
                                                    if (Board[i, q] != null)
                                                        if (Board[i, q].Owner == Battle.turn)
                                                        {
                                                            int w = q;
                                                            while (w < k - 1 && Board[i, w + 1] == null)
                                                            {
                                                                Board[i, w].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, coor[i, w + 1].transform.position.x - coor[i, w].transform.position.x);
                                                                Board[i, w + 1] = Board[i, w];
                                                                Board[i, w] = null;
                                                                w++;
                                                            }
                                                            Board[i, w].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.stop, dir, 0);
                                                            if (w != m-1)
                                                                j = w + 1; //ОСТОРОЖНО, ЛУЧШЕ НЕ ПОВТОРЯТЬ ЭТУ ХУЙНЮ САМОСТОЯТЕЛЬНО

                                                        }
                                                
                                                var temp = Battle.Fight(Board[i, k], Board[i, k + 1]);
                                                Board[i, k] = temp[0];
                                                Board[i, k + 1] = temp[1];

                                                if (Board[i, k].Health <= 0)
                                                {
                                                    Board[i, k] = null;
                                                    //DestroyCard(i, k);
                                                }
                                                else
                                                {
                                                    Board[i, k].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, ReverseDirection(dir), Card_Place_Creation.xMarginStandart / 2 - 1);
                                                }
                                                if (Board[i, k + 1].Health <= 0)
                                                {
                                                    //[p;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;DestroyCard(i, k + 1);
                                                    Board[i, k + 1] = null;
                                                    if (Board[i, k] != null)
                                                    {
                                                        Board[i, k + 1] = Board[i, k];
                                                        Board[i, k].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, coor[i, k+1].transform.position.x - coor[i, k].transform.position.x);
                                                        //Board[i, k].transform.position = coor[i, k + 1].transform.position;
                                                        Board[i, k] = null;
                                                    }
                                                }
                                                else
                                                {
                                                    Board[i, k+1].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, Card_Place_Creation.xMarginStandart / 2 - 1);
                                                }
                                                moved = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Board[i, k + 1] = Board[i, k];
                                            Board[i, k].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, dir, coor[i, k + 1].transform.position.x - coor[i, k].transform.position.x);
                                            //Board[i, k].transform.position = coor[i, k + 1].transform.position;
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
                        nextTurn = true;
                        GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
                    }
                    break;
                }
        }
        return Board;
    }
}

