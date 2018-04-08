using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    private Player possesedPlayer;
    private SkillMaster skillMaster;
    private GameObject prefab;
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

  
        var bufMap = (SlotBuff[,]) GameObject.Find("Field").GetComponent<SkillMaster>().BufMap;
        fields.Add(Battle.MoveField(field0, bufMap, Directions.Top));
        skillMaster.RebuidBufMap();
        Battle.RestoreBoard();
    
        bufMap = (SlotBuff[,])GameObject.Find("Field").GetComponent<SkillMaster>().BufMap;
        fields.Add(Battle.MoveField(field1, bufMap, Directions.Left));
        skillMaster.RebuidBufMap();
        Battle.RestoreBoard();
    
        bufMap = (SlotBuff[,])GameObject.Find("Field").GetComponent<SkillMaster>().BufMap;
        fields.Add(Battle.MoveField(field2, bufMap, Directions.Right));
        skillMaster.RebuidBufMap();
        Battle.RestoreBoard();
     
        bufMap = (SlotBuff[,])GameObject.Find("Field").GetComponent<SkillMaster>().BufMap;
        fields.Add(Battle.MoveField(field3, bufMap, Directions.Bottom));
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

}
