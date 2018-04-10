using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    private Player possesedPlayer;
    private SkillMaster skillMaster;
    private GameObject prefab;

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

    private void Awake()
    {

    }

    public Action MakeMove()
    {
        var ret = new Action();

        Battle.SaveBoard();

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
        ret.Utillity = int.MinValue;
        var maxUtility = int.MinValue;

        int initialStr0 = 0, initialStr1 = 0;
        foreach (var card in Battle.Board)
        {
            if (card)
            {
                if (card.Owner == possesedPlayer)
                {
                    initialStr1 += card.Attack + card.Health + card.Shield;
                }
                else
                {
                    initialStr0 -= card.Attack + card.Health + card.Shield;
                }
            }
        }

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
                        var field = new Card[Battle.n, Battle.m];

                        for (int l = 0; l < Battle.n; l++)
                        {
                            for (int j = 0; j < Battle.m; j++)
                            {
                                if (Battle.Board[l, j] != null)
                                {
                                    var c = Instantiate(prefab);

                                    if (Battle.Board[l, j].Owner != Battle.Player2)
                                    {
                                        c.GetComponent<Card>().Owner = Battle.Player1;
                                    }

                                    field[l, j] = new Card();
                             
                                    c.GetComponent<Card>().Initialize(Battle.Board[l, j].Info.Name, Battle.Board[l, j].Health, Battle.Board[l, j].Shield, Battle.Board[l, j].Attack, Battle.Board[l, j].Info.Description, skillMaster,
                                        Battle.Board[l, j].Info.BattleCryNames);
                                    field[l, j] = c.GetComponent<Card>();

                                    field[l, j].OnBoard = true;

                                    Destroy(c);
                                }
                            }
                        }

                        var bufMap = (SlotBuff[,]) GameObject.Find("Field").GetComponent<SkillMaster>().BufMap;
                        var utility = 0;
                        field[m, n] = crd;

                        crd.Play(ref field, ref bufMap);

                        int str0 = 0, str1 = 0;
                        foreach (var card in field)
                        {
                            if (card)
                            {
                                if (card.Owner == possesedPlayer)
                                {
                                    str1 += card.Attack + card.Health + card.Shield;
                                }
                                else
                                {
                                    str0 += card.Attack + card.Health + card.Shield;
                                }
                            }
                        }

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



                        utility = Mathf.RoundToInt(Domination * (str1 - initialStr1) - Fear * (str0 - initialStr0)); 

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
                    }
                }
            }
        }

        Battle.RestoreBoard();
        return ret;
    }

    private MovementAction CalculateMovementUtility()
    {
        var ret = new MovementAction();
        ret.Utillity = int.MinValue;
        var fields = new List<Card[,]>();
  
        var field0 = new Card[Battle.n,Battle.m];
        var field1 = new Card[Battle.n, Battle.m];
        var field2 = new Card[Battle.n, Battle.m];
        var field3 = new Card[Battle.n, Battle.m];

        for (int i = 0; i < Battle.n; i++)
        {
            for (int j = 0; j < Battle.m; j++)
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
                    c.GetComponent<Card>().Initialize(Battle.Board[i, j].Info.Name, Battle.Board[i, j].Health, Battle.Board[i, j].Shield, Battle.Board[i, j].Attack, Battle.Board[i, j].Info.Description, skillMaster,
                        Battle.Board[i, j].Info.BattleCryNames);
                    field0[i, j] = c.GetComponent<Card>();
                    Destroy(c);

                    c = Instantiate(prefab);
                    if (Battle.Board[i, j].Owner != Battle.Player2)
                    {
                        c.GetComponent<Card>().Owner = Battle.Player1;
                    }
                    c.GetComponent<Card>().Initialize(Battle.Board[i, j].Info.Name, Battle.Board[i, j].Health, Battle.Board[i, j].Shield, Battle.Board[i, j].Attack, Battle.Board[i, j].Info.Description, skillMaster,
                        Battle.Board[i, j].Info.BattleCryNames);
                    field1[i, j] = c.GetComponent<Card>();
                    Destroy(c);

                    c = Instantiate(prefab);
                    if (Battle.Board[i, j].Owner != Battle.Player2)
                    {
                        c.GetComponent<Card>().Owner = Battle.Player1;
                    }
                    c.GetComponent<Card>().Initialize(Battle.Board[i, j].Info.Name, Battle.Board[i, j].Health, Battle.Board[i, j].Shield, Battle.Board[i, j].Attack, Battle.Board[i, j].Info.Description, skillMaster,
                        Battle.Board[i, j].Info.BattleCryNames);
                    field2[i, j] = c.GetComponent<Card>();
                    Destroy(c);

                    c = Instantiate(prefab);
                    if (Battle.Board[i, j].Owner != Battle.Player2)
                    {
                        c.GetComponent<Card>().Owner = Battle.Player1;
                    }
                    c.GetComponent<Card>().Initialize(Battle.Board[i, j].Info.Name, Battle.Board[i, j].Health, Battle.Board[i, j].Shield, Battle.Board[i, j].Attack, Battle.Board[i, j].Info.Description, skillMaster,
                        Battle.Board[i, j].Info.BattleCryNames);
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
        var bufMap = (SlotBuff[,]) GameObject.Find("Field").GetComponent<SkillMaster>().BufMap;
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
        var numberOfMax=0;
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

                utility += Mathf.RoundToInt(Aggression * killedDead[i].EnemiesDead - Safeness * killedDead[i].DamageReceived  - (Maddness - 100) * killedDead[i].DamageReceived - Сaution * killedDead[i].AlliesDead);
            }
            else utility = int.MinValue;

            if (utility > maxUtility)
            {
                maxUtility = utility;
                numberOfMax = i;
            }       
        }

        int mult = 1;
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
    }

    public struct Action
    {
        public MovementAction Movement;
        public CardPlacingAction Placing;
        public bool IsMoving;
    }

    public struct ActionCoofs
    {
        public int AlliesDead;
        public int EnemiesDead;
        public int DamageDone;
        public int DamageReceived;
    }

}
