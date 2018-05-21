using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIEnemy : MonoBehaviour
{
    private Player possesedPlayer;
    private SkillMaster skillMaster;
    private GameObject prefab;
    private int initialStr0, initialStr1;

    //Отвечает за то, насколько ИИ будет стараться убить как можно больше врагов 0<Aggression
    public const float Aggression = 100f;
    //Отвечает за то, насколько ИИ будет стараться нанести как можно больше урона 0<Maddness<100
    public const float Maddness = 10f;
    //Отвечает за то, насколько ИИ будет стараться потерять как можно меньше кораблей 0<Сaution
    public const float Сaution = 1f;
    //Отвечает за то, насколько ИИ будет стараться получить как можно меньше урона 0<Safeness
    public const float Safeness = 1f;
    //Отвечает за то, насколько ИИ будет бояться выставлять корабли при доминации противника 0<Fear<1
    public const float Fear = 0.1f;
    //Отвечает за то, насколько ИИ будет стараться выставлять новые корабли 0<Domination
    public const float Domination = 50f;


    private CardPlacingAction maxPlacingActionUt;
    private MovementAction maxMovingActionUt;
    private ActiveSkillAction maxActiveSkillUt;

    private void Awake()
    {

    }

    public Action MakeMove()
    {       
        Battle.SaveBoard();
        var str = CalculateOnBoardStrength(Battle.Board);
        initialStr0 = str[0];
        initialStr1 = str[1];

        maxPlacingActionUt = CalculateCardPlacingUtility();
        maxPlacingActionUt.IsPlacing = maxPlacingActionUt.Utillity > 10f || Battle.preGameStage;

        maxMovingActionUt = CalculateMovementUtility();
        maxActiveSkillUt = CalculateActiveSkillUtility();
        maxActiveSkillUt.IsAplliedSkill = maxActiveSkillUt.Utillity > 10f;
       // maxActiveSkillUt.IsAplliedSkill = false;

        var ret = new Action
        {
            Movement = maxMovingActionUt,
            Placing = maxPlacingActionUt,
            ActiveSkill = maxActiveSkillUt,
        };

        return ret;
    }

    private int[] CalculateOnBoardStrength(Card[,] board)
    {
        var str = new int[2];
        foreach (var card in board)
        {
            if (card)
            {
                if (card.Owner == possesedPlayer)
                {
                    str[0] += card.Attack + card.Health + card.Shield;
                }
                else
                {
                    str[1] += card.Attack + card.Health + card.Shield; ///WTF
                }
            }
        }

        return str;
    }

    private CardPlacingAction CalculateCardPlacingUtility()
    {
        var ret = new CardPlacingAction
        {
            Utillity = int.MinValue
        };
        var maxUtility = int.MinValue;

        for (var i = 0; i < possesedPlayer.deck.Count; i++)
        {
                   
            var crdObj = possesedPlayer.deck[i];

            var crd = crdObj.GetComponent<Card>();

            var startRow = Battle.preGameStage ? Battle.m / 2 : 0;

            for (var m = startRow; m < Battle.m; m++)
            {
                for (var n = 0; n < Battle.Board.GetLength(1); n++)
                {
                    if (!Battle.Board[m, n])
                    {
                        var field = new Card[Battle.n, Battle.m];

                        for (var l = 0; l < Battle.n; l++)
                        {
                            for (var j = 0; j < Battle.m; j++)
                            {
                                if (Battle.Board[l, j] != null)
                                {
                                    var c = Instantiate(prefab);

                                    if (Battle.Board[l, j].Owner != Battle.Player2)
                                    {
                                        c.GetComponent<Card>().Owner = Battle.Player1;
                                    }

                                    field[l, j] = new Card();
                                    var BCinp = Battle.Board[l, j].Info.BattleCryInput != null ? Battle.Board[l, j].Info.BattleCryInput[0].InputParamsValues[0] : null;
                                    var DRinp = Battle.Board[l, j].Info.DeathRattleInput != null ? Battle.Board[l, j].Info.DeathRattleInput[0].InputParamsValues[0] : null;
                                    var AUinp = Battle.Board[l, j].Info.AuraInput != null ? Battle.Board[l, j].Info.AuraInput[0].InputParamsValues[0] : null;
                                    c.GetComponent<Card>().Initialize(Battle.Board[l, j].Info.Name, Battle.Board[l, j].Health, Battle.Board[l, j].Shield, Battle.Board[l, j].Attack, Battle.Board[l, j].Info.Description, skillMaster,
                                        BCinp, DRinp, AUinp,
                                        Battle.Board[l, j].Info.BattleCryNames, Battle.Board[l, j].Info.DeathRattleName, Battle.Board[l, j].Info.AuraNames);
                                    field[l, j] = c.GetComponent<Card>();

                                    field[l, j].OnBoard = true;

                                    Destroy(c);
                                }
                            }
                        }

                        var bufMap = GameObject.Find("Field").GetComponent<SkillMaster>().BufMap;
                        var utility = 0;
                        field[m, n] = crd;

                        crd.Play(ref field, ref bufMap);

                        var str = CalculateOnBoardStrength(field);
                        var str0 = str[0];
                        var str1 = str[1];

                        if (crd.Info.BattleCryNames.Length != 0)
                        {
                            foreach (var input in crd.Info.BattleCryInput)
                            {
                                skillMaster.ReExecuteSkillByInput(crd, input, ref field);
                                skillMaster.ApplyBufsToBoard(ref field, ref bufMap);
                            }
                        }

                        if (crd.Info.AuraNames.Length != 0)
                        {
                            foreach (var input in crd.Info.AuraInput)
                            {
                                skillMaster.ReExecuteSkillByInput(crd, input, ref field);
                                skillMaster.ApplyBufsToBoard(ref field, ref bufMap);
                            }
                        }

                        utility = Mathf.RoundToInt(Domination * (str0 - initialStr0) - Fear * (str1 - initialStr1));

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
                        else if (utility == maxUtility)
                        {
                            if (Random.value >= 0.5)
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

                        if (maxUtility >= 500)
                            return ret;
                    }
                }
            }
        }

        Battle.RestoreBoard();
        return ret;
    }

    private MovementAction CalculateMovementUtility()
    {
        if (!Battle.preGameStage)
        {
            var ret = new MovementAction();
            ret.Utillity = int.MinValue;
            var fields = new List<Card[,]>();

            var field0 = new Card[Battle.n, Battle.m];
            var field1 = new Card[Battle.n, Battle.m];
            var field2 = new Card[Battle.n, Battle.m];
            var field3 = new Card[Battle.n, Battle.m];

            for (var i = 0; i < Battle.n; i++)
            {
                for (var j = 0; j < Battle.m; j++)
                {
                    if (Battle.Board[i, j] != null)
                    {
                        field0[i, j] = new Card();
                        field1[i, j] = new Card();
                        field2[i, j] = new Card();
                        field3[i, j] = new Card();
                        var c = Instantiate(prefab);

                        if (Battle.Board[i, j].Owner != Battle.Player2)
                        {
                            c.GetComponent<Card>().Owner = Battle.Player1;
                        }

                        var BCinp = Battle.Board[i, j].Info.BattleCryInput != null ? Battle.Board[i, j].Info.BattleCryInput[0].InputParamsValues[0] : null;
                        var DRinp = Battle.Board[i, j].Info.DeathRattleInput != null ? Battle.Board[i, j].Info.DeathRattleInput[0].InputParamsValues[0] : null;
                        var AUinp = Battle.Board[i, j].Info.AuraInput != null ? Battle.Board[i, j].Info.AuraInput[0].InputParamsValues[0] : null;
                        c.GetComponent<Card>().Initialize(Battle.Board[i, j].Info.Name, Battle.Board[i, j].Health, Battle.Board[i, j].Shield, Battle.Board[i, j].Attack, Battle.Board[i, j].Info.Description, skillMaster,
                            BCinp, DRinp, AUinp,
                            Battle.Board[i, j].Info.BattleCryNames, Battle.Board[i, j].Info.DeathRattleName, Battle.Board[i, j].Info.AuraNames);

                        field0[i, j] = c.GetComponent<Card>();
                        Destroy(c);

                        c = Instantiate(prefab);
                        if (Battle.Board[i, j].Owner != Battle.Player2)
                        {
                            c.GetComponent<Card>().Owner = Battle.Player1;
                        }

                        BCinp = Battle.Board[i, j].Info.BattleCryInput != null ? Battle.Board[i, j].Info.BattleCryInput[0].InputParamsValues[0] : null;
                        DRinp = Battle.Board[i, j].Info.DeathRattleInput != null ? Battle.Board[i, j].Info.DeathRattleInput[0].InputParamsValues[0] : null;
                        AUinp = Battle.Board[i, j].Info.AuraInput != null ? Battle.Board[i, j].Info.AuraInput[0].InputParamsValues[0] : null;
                        c.GetComponent<Card>().Initialize(Battle.Board[i, j].Info.Name, Battle.Board[i, j].Health, Battle.Board[i, j].Shield, Battle.Board[i, j].Attack, Battle.Board[i, j].Info.Description, skillMaster,
                            BCinp, DRinp, AUinp,
                            Battle.Board[i, j].Info.BattleCryNames, Battle.Board[i, j].Info.DeathRattleName, Battle.Board[i, j].Info.AuraNames);
                        field1[i, j] = c.GetComponent<Card>();
                        Destroy(c);

                        c = Instantiate(prefab);
                        if (Battle.Board[i, j].Owner != Battle.Player2)
                        {
                            c.GetComponent<Card>().Owner = Battle.Player1;
                        }

                        BCinp = Battle.Board[i, j].Info.BattleCryInput != null ? Battle.Board[i, j].Info.BattleCryInput[0].InputParamsValues[0] : null;
                        DRinp = Battle.Board[i, j].Info.DeathRattleInput != null ? Battle.Board[i, j].Info.DeathRattleInput[0].InputParamsValues[0] : null;
                        AUinp = Battle.Board[i, j].Info.AuraInput != null ? Battle.Board[i, j].Info.AuraInput[0].InputParamsValues[0] : null;
                        c.GetComponent<Card>().Initialize(Battle.Board[i, j].Info.Name, Battle.Board[i, j].Health, Battle.Board[i, j].Shield, Battle.Board[i, j].Attack, Battle.Board[i, j].Info.Description, skillMaster,
                            BCinp, DRinp, AUinp,
                            Battle.Board[i, j].Info.BattleCryNames, Battle.Board[i, j].Info.DeathRattleName, Battle.Board[i, j].Info.AuraNames);
                        field2[i, j] = c.GetComponent<Card>();
                        Destroy(c);

                        c = Instantiate(prefab);
                        if (Battle.Board[i, j].Owner != Battle.Player2)
                        {
                            c.GetComponent<Card>().Owner = Battle.Player1;
                        }

                        BCinp = Battle.Board[i, j].Info.BattleCryInput != null ? Battle.Board[i, j].Info.BattleCryInput[0].InputParamsValues[0] : null;
                        DRinp = Battle.Board[i, j].Info.DeathRattleInput != null ? Battle.Board[i, j].Info.DeathRattleInput[0].InputParamsValues[0] : null;
                        AUinp = Battle.Board[i, j].Info.AuraInput != null ? Battle.Board[i, j].Info.AuraInput[0].InputParamsValues[0] : null;
                        c.GetComponent<Card>().Initialize(Battle.Board[i, j].Info.Name, Battle.Board[i, j].Health, Battle.Board[i, j].Shield, Battle.Board[i, j].Attack, Battle.Board[i, j].Info.Description, skillMaster,
                            BCinp, DRinp, AUinp,
                            Battle.Board[i, j].Info.BattleCryNames, Battle.Board[i, j].Info.DeathRattleName, Battle.Board[i, j].Info.AuraNames);
                        field3[i, j] = c.GetComponent<Card>();
                        Destroy(c);

                        field0[i, j].OnBoard = true;
                        field1[i, j].OnBoard = true;
                        field2[i, j].OnBoard = true;
                        field3[i, j].OnBoard = true;
                    }
                }
            }

            ActionCoofs[] killedDead = new ActionCoofs[4];
            int alDead0, enDead0, dmgDone0, dmgReceived0, alDead1, enDead1, dmgDone1, dmgReceived1, alDead2, enDead2, dmgDone2, dmgReceived2, alDead3, enDead3, dmgDone3, dmgReceived3;
            var bufMap = (SlotBuff[,])GameObject.Find("Field").GetComponent<SkillMaster>().BufMap;
            fields.Add(Battle.MoveField(field0, bufMap, Directions.Top, out enDead0, out alDead0, out dmgDone0, out dmgReceived0));
            killedDead[0].AlliesDead = alDead0;
            killedDead[0].EnemiesDead = enDead0;
            killedDead[0].DamageDone = dmgDone0;
            killedDead[0].DamageReceived = dmgReceived0;
            skillMaster.RebuidBufMap();
            Battle.RestoreBoard();

            bufMap = (SlotBuff[,])GameObject.Find("Field").GetComponent<SkillMaster>().BufMap;
            fields.Add(Battle.MoveField(field1, bufMap, Directions.Left, out enDead1, out alDead1, out dmgDone1, out dmgReceived1));
            killedDead[1].AlliesDead = alDead1;
            killedDead[1].EnemiesDead = enDead1;
            killedDead[1].DamageDone = dmgDone1;
            killedDead[1].DamageReceived = dmgReceived1;
            skillMaster.RebuidBufMap();
            Battle.RestoreBoard();

            bufMap = (SlotBuff[,])GameObject.Find("Field").GetComponent<SkillMaster>().BufMap;
            fields.Add(Battle.MoveField(field2, bufMap, Directions.Right, out enDead2, out alDead2, out dmgDone2, out dmgReceived2));
            killedDead[2].AlliesDead = alDead2;
            killedDead[2].EnemiesDead = enDead2;
            killedDead[2].DamageDone = dmgDone2;
            killedDead[2].DamageReceived = dmgReceived2;
            skillMaster.RebuidBufMap();
            Battle.RestoreBoard();

            bufMap = (SlotBuff[,])GameObject.Find("Field").GetComponent<SkillMaster>().BufMap;
            fields.Add(Battle.MoveField(field3, bufMap, Directions.Bottom, out enDead3, out alDead3, out dmgDone3, out dmgReceived3));
            killedDead[3].AlliesDead = alDead3;
            killedDead[3].EnemiesDead = enDead3;
            killedDead[3].DamageDone = dmgDone3;
            killedDead[3].DamageReceived = dmgReceived3;
            skillMaster.RebuidBufMap();
            Battle.RestoreBoard();

            var maxUtility = int.MinValue;
            var numberOfMax = 0;
            for (var i = 0; i < fields.Count; i++)
            {
                var utility = 0;
                if (fields[i] != null)
                {
                    foreach (var card in fields[i])
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

                    utility += Mathf.RoundToInt(Aggression * killedDead[i].EnemiesDead - Safeness * killedDead[i].DamageReceived - (Maddness - 100) * killedDead[i].DamageReceived -
                                                Сaution * killedDead[i].AlliesDead);
                }
                else utility = int.MinValue;

                if (utility > maxUtility)
                {
                    maxUtility = utility;
                    numberOfMax = i;
                }
            }

            switch (numberOfMax)
            {
                case 0:
                    ret = new MovementAction()
                    {
                        Utillity = maxUtility,
                        Direction = Directions.Top
                    };
                    break;
                case 1:
                    ret = new MovementAction()
                    {
                        Utillity = maxUtility,
                        Direction = Directions.Left
                    };
                    break;
                case 2:
                    ret = new MovementAction()
                    {
                        Utillity = maxUtility,
                        Direction = Directions.Right
                    };
                    break;
                case 3:
                    ret = new MovementAction()
                    {
                        Utillity = maxUtility,
                        Direction = Directions.Bottom
                    };
                    break;
            }

            return ret;
        }
        else
        {
            var ret = new MovementAction
            {
                Utillity = -1000,
                Direction = Directions.Self
            };

            return ret;
        }
    }

    private ActiveSkillAction CalculateActiveSkillUtility()
    {
        var ret = new ActiveSkillAction
        {
            Utillity = int.MinValue
        };
        if (!Battle.preGameStage)
        { 
        var maxUtility = float.MinValue;

            for (var m = 0; m < Battle.n; m++)
            {
                for (var n = 0; n < Battle.m; n++)
                {
                    if (maxUtility >= 100)
                        return ret;

                    if (Battle.Board[m, n])
                    {
                        if (Battle.Get_Card(m, n).IsActiveSkillAvaliable && Battle.Get_Card(m, n).Owner == possesedPlayer)
                        {
                            var field0 = new Card[Battle.n, Battle.m];
                            for (var i = 0; i < Battle.n; i++)
                            {
                                for (var j = 0; j < Battle.m; j++)
                                {
                                    if (Battle.Board[i, j] != null)
                                    {
                                        field0[i, j] = new Card();

                                        var c = Instantiate(prefab);

                                        if (Battle.Board[i, j].Owner != Battle.Player2)
                                        {
                                            c.GetComponent<Card>().Owner = Battle.Player1;
                                        }

                                        var BCinp = Battle.Board[i, j].Info.BattleCryInput != null ? Battle.Board[i, j].Info.BattleCryInput[0].InputParamsValues[0] : null;
                                        var DRinp = Battle.Board[i, j].Info.DeathRattleInput != null ? Battle.Board[i, j].Info.DeathRattleInput[0].InputParamsValues[0] : null;
                                        var AUinp = Battle.Board[i, j].Info.AuraInput != null ? Battle.Board[i, j].Info.AuraInput[0].InputParamsValues[0] : null;
                                        c.GetComponent<Card>().Initialize(Battle.Board[i, j].Info.Name, Battle.Board[i, j].Health, Battle.Board[i, j].Shield, Battle.Board[i, j].Attack,
                                            Battle.Board[i, j].Info.Description, skillMaster,
                                            BCinp, DRinp, AUinp,
                                            Battle.Board[i, j].Info.BattleCryNames, Battle.Board[i, j].Info.DeathRattleName, Battle.Board[i, j].Info.AuraNames);

                                        field0[i, j] = c.GetComponent<Card>();
                                        Destroy(c);

                                        field0[i, j].OnBoard = true;

                                    }
                                }
                            }

                            if (maxPlacingActionUt.IsPlacing)
                            {
                                var i = maxPlacingActionUt.Col;
                                var j = maxPlacingActionUt.Row;
                                var crd = possesedPlayer.deck[maxPlacingActionUt.CardNum].GetComponent<Card>(); ;

                                field0[i, j] = new Card();

                                var c = Instantiate(prefab);
                                c.GetComponent<Card>().Owner = Battle.Player2;

                                var BCinp = crd.Info.BattleCryInput != null ? crd.Info.BattleCryInput[0].InputParamsValues[0] : null;
                                var DRinp = crd.Info.DeathRattleInput != null ? crd.Info.DeathRattleInput[0].InputParamsValues[0] : null;
                                var AUinp = crd.Info.AuraInput != null ? crd.Info.AuraInput[0].InputParamsValues[0] : null;
                                c.GetComponent<Card>().Initialize(crd.Info.Name, crd.Health, crd.Shield, crd.Attack,
                                    crd.Info.Description, skillMaster,
                                    BCinp, DRinp, AUinp,
                                    crd.Info.BattleCryNames, crd.Info.DeathRattleName, crd.Info.AuraNames);

                                field0[i, j] = c.GetComponent<Card>();
                                Destroy(c);

                                field0[i, j].OnBoard = true;
                                initialStr1 += crd.Attack + crd.Shield + crd.Health;
                            }

                            var bufMap = GameObject.Find("Field").GetComponent<SkillMaster>().BufMap;
                            var isSuccess = field0[m, n].ExecuteActiveSkill(ref field0, ref bufMap);
                            if (isSuccess)
                            {
                                var str = CalculateOnBoardStrength(field0);
                                var str0 = str[0];
                                var str1 = str[1];

                                var utility = Mathf.RoundToInt(Maddness * (str0 - initialStr0) - Сaution * (str1 - initialStr1));

                                if (utility > maxUtility)
                                {
                                    maxUtility = utility;

                                    ret = new ActiveSkillAction()
                                    {
                                        Utillity = utility,
                                        Row = m,
                                        Col = n,
                                    };
                                }
                                else if (Math.Abs(utility - maxUtility) < 0.000000001f)
                                {
                                    if (Random.value >= 0.5)
                                    {
                                        maxUtility = utility;

                                        ret = new ActiveSkillAction()
                                        {
                                            Utillity = utility,
                                            Row = m,
                                            Col = n,
                                        };
                                    }
                                }

                                //skillMaster.ReExecuteSkillByInput(field0[m, n], field0[m, n].Info.ActiveSkillInput, ref field0);
                                //skillMaster.ApplyBufsToBoard(ref field0, ref bufMap);
                                skillMaster.RebuidBufMap();
                                Battle.RestoreBoard();
                            }
                        }
                    }
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
        prefab = Instantiate(GameObject.Find("card#0/player=Player2"));
        prefab.GetComponent<CardRotation>().enabled = false;
        prefab.transform.position = new Vector3(0, 0, -10000);
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
        public bool IsPlacing;
    }

    public struct ActiveSkillAction
    {
        public int Utillity;
        public int Row;
        public int Col;
        public bool IsAplliedSkill;
    }

    public struct Action
    {
        public MovementAction Movement;
        public CardPlacingAction Placing;
        public ActiveSkillAction ActiveSkill;
    }

    public struct ActionCoofs
    {
        public int AlliesDead;
        public int EnemiesDead;
        public int DamageDone;
        public int DamageReceived;
    }

}
