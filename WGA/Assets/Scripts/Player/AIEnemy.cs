﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    private Player possesedPlayer;
    private SkillMaster skillMaster;

    private void Awake()
    {

    }

    public Action MakeMove()
    {
        var ret = new Action();
        var maxPlacingActionUt = CalculateCardPlacingUtility();
        var maxMovingActionUt = CalculateMovementUtility();

        ret.Movement = maxMovingActionUt;
        ret.Placing = maxPlacingActionUt;

        ret.IsMoving = maxMovingActionUt.Utillity > maxPlacingActionUt.Utillity;

        return ret;
    }

    private CardPlacingAction CalculateCardPlacingUtility()
    {
        var ret = new CardPlacingAction();
        var maxUtility = float.MinValue;
        for (var i = 0; i < possesedPlayer.deck.Count; i++)
        {
            var crdObj = possesedPlayer.deck[i];

            var crd = crdObj.GetComponent<Card>();

            for (var m = 0; m < Battle.Board.GetLength(0); m++)
            {
                for (var n = 0; n < Battle.Board.GetLength(1); n++)
                {
                    if (!Battle.Board[m, n])
                    {
                        var field = (Card[,])Battle.Board.Clone();
                        var utility = 0;
                        field[m, n] = crd;

                        crd.Play(ref field);
                        //if (crd.Info.BattleCryNames.Length != 0)
                        //{
                        //    foreach (var input in crd.Info.BattleCryInput)
                        //    {
                        //        skillMaster.ExecuteSkillByInput(crd, input, ref field);
                        //        skillMaster.ApplyBufsToBoard(ref field);
                        //    }
                        //}

                        //if (crd.Info.AuraNames.Length != 0)
                        //{
                        //    foreach (var input in crd.Info.AuraInput)
                        //    {
                        //        skillMaster.ExecuteSkillByInput(crd, input, ref field);
                        //        skillMaster.ApplyBufsToBoard(ref field);
                        //    }
                        //}

                        foreach (var card in field)
                        {
                            if (card)
                            {
                                if (card.Owner == possesedPlayer)
                                {
                                    utility += card.Attack + card.Health + card.Shield;
                                }
                                else
                                {
                                    utility -= card.Attack + card.Health + card.Shield;
                                }
                            }
                        }

                        if (crd.Info.BattleCryNames.Length != 0)
                        {
                            foreach (var input in crd.Info.BattleCryInput)
                            {
                                skillMaster.ReExecuteSkillByInput(crd, input, ref field);
                                skillMaster.ApplyBufsToBoard(ref field);
                            }
                        }

                        if (crd.Info.AuraNames.Length != 0)
                        {
                            foreach (var input in crd.Info.AuraInput)
                            {
                                skillMaster.ReExecuteSkillByInput(crd, input, ref field);
                                skillMaster.ApplyBufsToBoard(ref field);
                            }
                        }

                        if (utility > maxUtility)
                        {
                            maxUtility = utility;

                            ret = new CardPlacingAction
                            {
                                Utillity = utility,
                                CardNum = i,
                                Row = m,
                                Col = n,
                            };
                        }
                    }
                }
            }
        }

        return ret;
    }

    private MovementAction CalculateMovementUtility()
    {
        var ret = new MovementAction();

        var fields = new List<Card[,]>();
        var field0 = (Card[,])Battle.Board.Clone();
        var field1 = (Card[,])Battle.Board.Clone();
        var field2 = (Card[,])Battle.Board.Clone();
        var field3 = (Card[,])Battle.Board.Clone();
        fields.Add(GameObject.Find("Field").GetComponent<Battle>().MoveField(field0, Directions.Top));
        fields.Add(GameObject.Find("Field").GetComponent<Battle>().MoveField(field1, Directions.Left));
        fields.Add(GameObject.Find("Field").GetComponent<Battle>().MoveField(field2, Directions.Right));
        fields.Add(GameObject.Find("Field").GetComponent<Battle>().MoveField(field3, Directions.Bottom));

        var maxUtility = float.MinValue;
        for (var i = 0; i < fields.Count; i++)
        {
            var field = fields[i];
            var utility = 0;
            foreach (var card in field)
            {
                if (card)
                {
                    if (card.Owner == possesedPlayer)
                    {
                        utility += card.Attack + card.Health + card.Shield;
                    }
                    else
                    {
                        utility -= card.Attack + card.Health + card.Shield;
                    }
                }
            }

            if (utility > maxUtility)
            {
                maxUtility = utility;

                switch (i)
                {
                    case 0:
                        ret = new MovementAction()
                        {
                            Utillity = utility,
                            Direction = Directions.Top
                        };
                        break;
                    case 1:
                        ret = new MovementAction()
                        {
                            Utillity = utility,
                            Direction = Directions.Left
                        };
                        break;
                    case 2:
                        ret = new MovementAction()
                        {
                            Utillity = utility,
                            Direction = Directions.Right
                        };
                        break;
                    case 3:
                        ret = new MovementAction()
                        {
                            Utillity = utility,
                            Direction = Directions.Bottom
                        };
                        break;
                }
               
            }
        }

        return ret;
    }

    // Use this for initialization
    void Start()
    {
        possesedPlayer = GetComponentInParent<Player>();
        skillMaster = GameObject.Find("Field").GetComponent<SkillMaster>();
    }

    // Update is called once per frameя
    void Update()
    {

    }

    public struct MovementAction
    {
        public int Utillity;
        public Directions Direction;
    }

    public struct CardPlacingAction
    {
        public int Utillity;
        public int CardNum;
        public int Row;
        public int Col;
    }

    public struct Action
    {
        public MovementAction Movement;
        public CardPlacingAction Placing;
        public bool IsMoving;
    }

}
