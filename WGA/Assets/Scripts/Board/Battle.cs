using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public static int n = 4;
    public static int m = 4;
    public static Card[,] Board;
    public static GameObject[,] coor = new GameObject[n, m];
    public static Player turn;
    public static Player Player1;
    public static Player Player2;
    static Vector3 rescalecard;
    public static int TurnNumber;
    public static bool preGameStage;
    public static bool isInputLocked = false;
    private static Card[,] savedBoard;
    public static bool cardSeted;
    public static bool skillUsed;
    private static Battle instance;
    public static SkillMaster SkillMaster;

    // Use this for initialization
    void Start()
    {
        //float defaultscalex = 0.2601453f;
        //float defaultscaley = 0.5f;
        //RollTheCards();
        //rescalecard = new Vector3(defaultscalex * 4 / Battle.n, defaultscaley * 4 / Battle.m, 1);
        //rescalecard = new Vector3(5, 5, 1);

        rescalecard = new Vector3(3.2f, 2.75f, 1);
        preGameStage = true;
       

        var t = GameObject.Find("BattleStageInfo");
        StartCoroutine(ShowCanvasForSeconds(t, 2f));

        instance = this;
    }
    public static Card Get_Card(int x, int y)
    {
        return Board[x, y] ?? null;
    }
    private void Awake()
    {
        SkillMaster = GameObject.Find("Field").GetComponent<SkillMaster>();
        Board = new Card[n, m];
        Player1 = GameObject.Find("Player1").GetComponent<Player>();
        Player1.PlInfo = new PlayerInfo(Application.dataPath + "/PlayerInfo/PlayerInfo.dat");
        Player2 = GameObject.Find("Player2").GetComponent<Player>();
        if (Player2.GetComponent<AIEnemy>() != null)
            Player2.AI = true;
        turn = Player1;
        TurnNumber = 1;
        RoundUIUpdate();
    }
    private static void RoundUIUpdate()
    {
        GameObject.Find("RoundText").GetComponent<Text>().text = "Round " + TurnNumber;
    }
    public static void NextTurn()
    { 
        isInputLocked = false;
        cardSeted = false;
        skillUsed = false;

        if (turn == Player2)
        {
            turn = Player1;
            TurnNumber++;

            for (var i = 0; i < Board.GetLength(0); i++)
            {
                for (var j = 0; j < Board.GetLength(1); j++)
                {
                    var crd = Board[i, j];
                    if (crd != null)
                    {
                        if (crd.Info.InitialShield > crd.Shield)
                        {
                            crd.Shield++;
                            if (crd.Owner.name == "Player1")
                            {
                                GameObject.Find("Field").GetComponent<SkillMaster>().BufMap[i, j].StaticShieldBufPlayer1++;
                            }
                            else
                            {
                                GameObject.Find("Field").GetComponent<SkillMaster>().BufMap[i, j].StaticShieldBufPlayer2++;
                            }

                            var x = crd.transform.GetChild(0);
                            x.GetChild(1).GetComponent<Text>().text = "" + crd.Shield;
                        }
                    }
                }
            }
            GameObject.Find("Field").GetComponent<SkillMaster>().ApplyBufsToBoard(ref Board, ref GameObject.Find("Field").GetComponent<SkillMaster>().BufMap);
            UpdateUI();

        }
        else
        {
            EndTurn();
            var pl2HasCard = Player2.deck.Count != 0;
            foreach (var card in Board)
            {
                if (card != null)
                {
                    if (card.Owner == Player2)
                    {
                        pl2HasCard = true;
                    }
                }
            }
            if (pl2HasCard)
            {
                turn = Player2;
                Player2.GetComponent<AIEnemy>().MakeMove();                             
                //var r = Player2.GetComponent<AIEnemy>().MakeMove();
                //if (r.Placing.IsPlacing)
                //{
                //    var col = r.Placing.Col;
                //    var row = r.Placing.Row;
                //    var crd = r.Placing.CardNum; //Номер карты в руке
                //    Player.Selectedcard = Player2.deck[crd];
                //    Set_Card(row, col, Player.Selectedcard.GetComponent<Card>());
                //}
                //if (r.ActiveSkill.IsAplliedSkill)
                //{
                //    Board[r.ActiveSkill.Row, r.ActiveSkill.Col].ExecuteActiveSkill(ref Board, ref GameObject.Find("Field").GetComponent<SkillMaster>().BufMap);
                //}
                //if (!preGameStage)
                //{
                //    var dir = r.Movement.Direction;
                //    Move(dir);
                //}
            }
        }

    }

    public static void EndTurn()
    {
        if (TurnNumber == 20)
        {
            var winner = CalculateWiningPlayer();
            if (winner == Player1)
            {
                Player1.PlInfo.Exp += 123123;
                Player1.PlInfo.SaveToFile(Application.dataPath + "/PlayerInfo/PlayerInfo.dat");
            }
            Player1.PlInfo.Exp += 123123;
            isInputLocked = true;
            //GameObject.Find("WinnerText").GetComponent<Text>().text = (winner.name == "Player1") ? "You Win!" : "You Loose!";

            var t = GameObject.Find("BattleEndMenu");
            var CanvGroupBattleMenu = t.GetComponentInChildren<CanvasGroup>();
            CanvGroupBattleMenu.alpha = 1f;
            CanvGroupBattleMenu.blocksRaycasts = true;

            t = GameObject.Find("Main");
            t.GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            bool pl1HasCard = Player1.deck.Count != 0;
            bool pl2HasCard = Player2.deck.Count != 0;
            foreach (var card in Board)
            {
                if (card != null)
                {
                    if (card.Owner == Player1)
                    {
                        pl1HasCard = true;
                    }
                    else if (card.Owner == Player2)
                    {
                        pl2HasCard = true;
                    }
                }
            }

            if (!pl1HasCard && pl2HasCard)
            {
                var winner = Player2;
                isInputLocked = true;
                GameObject.Find("WinnerText").GetComponent<Text>().text = (winner.name == "Player1") ? "You Win!" : "You Loose!";

                var t = GameObject.Find("BattleEndMenu");
                var CanvGroupBattleMenu = t.GetComponentInChildren<CanvasGroup>();
                CanvGroupBattleMenu.alpha = 1f;
                CanvGroupBattleMenu.blocksRaycasts = true;

                t = GameObject.Find("Main");
                t.GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;
            }
            else if (!pl2HasCard && pl1HasCard)
            {
                var winner = Player1;
                Player1.PlInfo.Exp += 123123;
                Player1.PlInfo.SaveToFile(Application.dataPath + "/PlayerInfo/PlayerInfo.dat");
                isInputLocked = true;
                GameObject.Find("WinnerText").GetComponent<Text>().text = (winner.name == "Player1") ? "You Win!" : "You Loose!";

                var t = GameObject.Find("BattleEndMenu");
                var canvGroupBattleMenu = t.GetComponentInChildren<CanvasGroup>();
                canvGroupBattleMenu.alpha = 1f;
                canvGroupBattleMenu.blocksRaycasts = true;

                t = GameObject.Find("Main");
                t.GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;
            }
        }

        if (preGameStage && TurnNumber >= 5)
        {
            preGameStage = false;
            TurnNumber = 1;
            for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                if (Board[i, j] != null)
                    Board[i, j].GetComponentInChildren<Canvas>().enabled = true;
            //GameObject.Find("Fog").GetComponent<SpriteRenderer>().enabled = false;
            Destroy(GameObject.Find("FogCanvas"));
            var t = GameObject.Find("BattleStageInfo");
            instance.StartCoroutine(ShowCanvasForSeconds(t, 2f));
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
                if (Player1.deck[i].transform.rotation.y != 0)
                    Player1.deck[i].GetComponent<Card>().front_rotate = true;
            }
            for (int i = 0; i < Player2.deck.Count; i++)
                if (!Player2.deck[i].GetComponent<Card>().OnBoard)
                {
                    Player2.deck[i].GetComponent<Card>().Spin(false);
                    Player2.deck[i].GetComponentInChildren<Canvas>().enabled = false;
                    Player2.deck[i].transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
                    Player2.deck[i].GetComponent<Card>().back_rotate = true;
                }
        }
        else
        {
            for (int i = 0; i < Player2.deck.Count; i++)
            {
                Player2.deck[i].GetComponent<Card>().Spin(true);
                Player2.deck[i].GetComponentInChildren<Canvas>().enabled = true;
                Player2.deck[i].transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
                Player2.deck[i].GetComponent<Card>().front_rotate = true;
            }
            for (int i = 0; i < Player1.deck.Count; i++)
                if (!Player1.deck[i].GetComponent<Card>().OnBoard)
                {
                    Player1.deck[i].GetComponent<Card>().Spin(false);
                    Player1.deck[i].GetComponentInChildren<Canvas>().enabled = false;
                    Player1.deck[i].transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
                    Player1.deck[i].GetComponent<Card>().back_rotate = true;
                }
        }
        UpdateUI();
    }
 
    public static void Move(Directions dir)
    {       
        if (!preGameStage)
        {
            if (GameObject.Find("Field").GetComponent<CreateMovementAnimation>().Move(Board, dir))
            {
                isInputLocked = true;
                skillUsed = true;
                cardSeted = true;
            }
            else
                isInputLocked = false;
        }
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
    }
    public static void Set_Card(int x, int y, Card tg)
    {
        if (!cardSeted)
        {
            var targetcard = Player.Selectedcard;
            targetcard.transform.parent = coor[x, y].transform.parent;
            targetcard.transform.position = coor[x, y].transform.position;
            // targetcard.GetComponent<BoxCollider2D>().enabled = false;
            Player.Selectedcard.transform.localScale = rescalecard;
            Player.Selectedcard.transform.rotation = new Quaternion(0, 0, 0, 1);
            if (turn == Player2 && preGameStage)
                Player.Selectedcard.GetComponentInChildren<Canvas>().enabled = false;
            else
                Player.Selectedcard.GetComponentInChildren<Canvas>().enabled = true;
            Player.Selectedcard.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            targetcard.GetComponent<Card>().Owner.deck.Remove(targetcard);
            turn.UpdatePosition();
            targetcard.GetComponent<Card>().OnBoard = true;
            targetcard.GetComponent<test>().SetFalse();

            Board[x, y] = tg;
            tg.Play(ref Board, ref GameObject.Find("Field").GetComponent<SkillMaster>().BufMap);
            //if (tg.Owner == Player2)
            //{
            //    var r = targetcard.GetComponents<Canvas>();
            //    foreach (var text in r)
            //    {
            //    //    if (text.name == "Name")
            //    //    {
            //    //        text.color = Color.red;
            //    //    }
            //    }
            //}
            //GameObject.Find("Field").GetComponent<SkillMaster>().ApplyBufsToBoard(out Battle.Board);
            Player.Selectedcard = null;
            cardSeted = true;
            if (preGameStage)
                Battle.NextTurn();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isInputLocked)
        {
            if (Input.GetKeyDown("escape"))
            {
                //UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
            else if (!isInputLocked && !preGameStage)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Move(Directions.Bottom);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Move(Directions.Top);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Move(Directions.Left);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Move(Directions.Right);
                }
            }

        }
    }
    public static Card[,] MoveField(Card[,] field, SlotBuff[,] bufMap, Directions dir, out int enemiesKilled, out int alliesDead, out int damageDone, out int damageReceived)
    {
        enemiesKilled = 0;
        alliesDead = 0;
        damageDone = 0;
        damageReceived = 0;
        int initialHP0 = 0, initialHP1 = 0;
        switch (dir)
        {
            case Directions.Top:
                {
                    bool moved = false;
                    for (int i = n - 2; i >= 0; i--)
                        for (int j = 0; j < m; j++)
                        {

                            if (field[i, j] != null)
                            {
                                int k = i;

                                if (field[k, j].Owner == turn)
                                {
                                    while (k <= n - 2)
                                    {
                                        if (field[k + 1, j] != null)
                                        {
                                            if (field[k + 1, j].Owner != turn)
                                            {
                                                initialHP0 = field[k, j].Health + field[k, j].Shield;
                                                initialHP1 = field[k + 1, j].Health + field[k + 1, j].Shield;

                                                var temp = Fight(field[k, j], field[k + 1, j]);

                                                if (field[k, j].Owner == Player1)
                                                    damageDone += initialHP0 - field[k, j].Health - field[k, j].Shield;
                                                else
                                                    damageReceived += initialHP0 - field[k, j].Health - field[k, j].Shield;

                                                if (field[k + 1, j].Owner == Player1)
                                                    damageDone += initialHP1 - field[k + 1, j].Health - field[k + 1, j].Shield;
                                                else
                                                    damageReceived += initialHP1 - field[k + 1, j].Health - field[k + 1, j].Shield;

                                                field[k, j] = temp[0];
                                                field[k + 1, j] = temp[1];
                                                if (field[k, j].Health <= 0)
                                                {
                                                    //DestroyCard(k, j);
                                                    if (field[k, j].Owner == Player1)
                                                        enemiesKilled++;
                                                    else
                                                        alliesDead++;

                                                    field[k, j].Destroy(ref field, ref bufMap);
                                                    field[k, j] = null;
                                                }

                                                if (field[k + 1, j].Health <= 0)
                                                {
                                                    //DestroyCard(k + 1, j);
                                                    if (field[k + 1, j].Owner == Player1)
                                                        enemiesKilled++;
                                                    else
                                                        alliesDead++;

                                                    field[k + 1, j].Destroy(ref field, ref bufMap);
                                                    field[k + 1, j] = null;
                                                    if (field[k, j] != null)
                                                    {
                                                        field[k + 1, j] = field[k, j];
                                                        //result[k, j].transform.position = coor[k + 1, j].position;
                                                        field[k, j] = null;
                                                    }
                                                }

                                                moved = true;
                                            }

                                            break;
                                        }

                                        field[k + 1, j] = field[k, j];
                                        //result[k, j].transform.position = coor[k + 1, j].position;
                                        field[k, j] = null;
                                        moved = true;
                                        k++;
                                    }

                                }
                            }

                        }

                    if (moved)
                    {
                        return field;
                        //NextTurn();
                        //RollTheCards();
                        //GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
                    }
                    else
                        return null;
                }
            case Directions.Bottom:
                {
                    bool moved = false;
                    for (int i = 1; i < n; i++)
                        for (int j = 0; j < m; j++)
                        {

                            if (field[i, j] != null)
                            {

                                int k = i;
                                if (field[k, j].Owner == turn)
                                {
                                    while (k >= 1)
                                    {
                                        if (field[k - 1, j] != null)
                                        {
                                            if (field[k - 1, j].Owner != turn)
                                            {
                                                initialHP0 = field[k, j].Health + field[k, j].Shield;
                                                initialHP1 = field[k - 1, j].Health + field[k - 1, j].Shield;

                                                var temp = Fight(field[k, j], field[k - 1, j]);

                                                if (field[k, j].Owner == Player1)
                                                    damageDone += initialHP0 - field[k, j].Health - field[k, j].Shield;
                                                else
                                                    damageReceived += initialHP0 - field[k, j].Health - field[k, j].Shield;

                                                if (field[k + -1, j].Owner == Player1)
                                                    damageDone += initialHP1 - field[k - 1, j].Health - field[k - 1, j].Shield;
                                                else
                                                    damageReceived += initialHP1 - field[k - 1, j].Health - field[k - 1, j].Shield;

                                                field[k, j] = temp[0];
                                                field[k - 1, j] = temp[1];
                                                if (field[k, j].Health <= 0)
                                                {
                                                    if (field[k, j].Owner == Player1)
                                                        enemiesKilled++;
                                                    else
                                                        alliesDead++;
                                                    //DestroyCard(k, j);//переписать DestroyCard
                                                    field[k, j].Destroy(ref field, ref bufMap);
                                                    //Destroy(result[n, m].gameObject);
                                                    field[k, j] = null;
                                                }

                                                if (field[k - 1, j].Health <= 0)
                                                {
                                                    if (field[k - 1, j].Owner == Player1)
                                                        enemiesKilled++;
                                                    else
                                                        alliesDead++;
                                                    //DestroyCard(k - 1, j);
                                                    field[k - 1, j].Destroy(ref field, ref bufMap);
                                                    field[k - 1, j] = null;
                                                    if (field[k, j] != null)
                                                    {
                                                        field[k - 1, j] = field[k, j];
                                                        //result[k, j].transform.position = coor[k - 1, j].position;
                                                        field[k, j] = null;
                                                    }
                                                }

                                                moved = true;
                                            }

                                            break;
                                        }
                                        else
                                        {
                                            field[k - 1, j] = field[k, j];
                                            //result[k, j].transform.position = coor[k - 1, j].position;
                                            field[k, j] = null;
                                            moved = true;
                                        }

                                        k--;
                                    }

                                }
                            }
                        }

                    if (moved)
                    {
                        return field;
                        //GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
                        // NextTurn();
                        //RollTheCards();
                    }
                    else
                        return null;

                }
            case Directions.Left:
                {
                    bool moved = false;
                    for (int i = 0; i < n; i++)
                        for (int j = 1; j < m; j++)
                        {

                            if (field[i, j] != null)
                            {
                                int k = j;
                                if (field[i, k].Owner == turn)
                                {
                                    while (k > 0)
                                    {
                                        if (field[i, k - 1] != null)
                                        {
                                            if (field[i, k - 1].Owner != turn)
                                            {
                                                initialHP0 = field[i, k].Health + field[i, k].Shield;
                                                initialHP1 = field[i, k - 1].Health + field[i, k - 1].Shield;

                                                var temp = Fight(field[i, k], field[i, k - 1]);

                                                if (field[i, k].Owner == Player1)
                                                    damageDone += initialHP0 - field[i, k].Health - field[i, k].Shield;
                                                else
                                                    damageReceived += initialHP0 - field[i, k].Health - field[i, k].Shield;

                                                if (field[i, k - 1].Owner == Player1)
                                                    damageDone += initialHP1 - field[i, k - 1].Health - field[i, k - 1].Shield;
                                                else
                                                    damageReceived += initialHP1 - field[i, k - 1].Health - field[i, k - 1].Shield;

                                                field[i, k] = temp[0];
                                                field[i, k - 1] = temp[1];
                                                if (field[i, k].Health <= 0)
                                                {
                                                    if (field[i, k].Owner == Player1)
                                                        enemiesKilled++;
                                                    else
                                                        alliesDead++;
                                                    //DestroyCard(i, k);
                                                    field[i, k].Destroy(ref field, ref bufMap);
                                                    field[i, k] = null;
                                                }

                                                if (field[i, k - 1].Health <= 0)
                                                {
                                                    if (field[i, k - 1].Owner == Player1)
                                                        enemiesKilled++;
                                                    else
                                                        alliesDead++;
                                                    //DestroyCard(i, k - 1);
                                                    field[i, k - 1].Destroy(ref field, ref bufMap);
                                                    field[i, k - 1] = null;
                                                    if (Board[i, k] != null)
                                                    {
                                                        field[i, k - 1] = field[i, k];
                                                        //result[i, k].transform.position = coor[i, k - 1].position;
                                                        field[i, k] = null;

                                                    }
                                                }

                                                moved = true;
                                            }

                                            break;
                                        }
                                        else
                                        {
                                            field[i, k - 1] = field[i, k];
                                            //result[i, k].transform.position = coor[i, k - 1].position;
                                            field[i, k] = null;
                                            moved = true;
                                        }

                                        k--;
                                    }
                                }
                            }

                        }

                    if (moved)
                    {
                        return field;
                        //NextTurn();
                        //RollTheCards();

                    }
                    else
                        return null;
                }
            case Directions.Right:
                {
                    bool moved = false;
                    for (int i = 0; i < n; i++)
                        for (int j = m - 2; j >= 0; j--)
                        {
                            if (field[i, j] != null)
                            {
                                int k = j;
                                if (field[i, k].Owner == turn)
                                {

                                    while (k <= m - 2)
                                    {
                                        if (field[i, k + 1] != null)
                                        {
                                            if (field[i, k + 1].Owner != turn)
                                            {
                                                initialHP0 = field[i, k].Health + field[i, k].Shield;
                                                initialHP1 = field[i, k + 1].Health + field[i, k + 1].Shield;

                                                var temp = Fight(field[i, k], field[i, k + 1]);

                                                if (field[i, k].Owner == Player1)
                                                    damageDone += initialHP0 - field[i, k].Health - field[i, k].Shield;
                                                else
                                                    damageReceived += initialHP0 - field[i, k].Health - field[i, k].Shield;

                                                if (field[i, k + 1].Owner == Player1)
                                                    damageDone += initialHP1 - field[i, k + 1].Health - field[i, k + 1].Shield;
                                                else
                                                    damageReceived += initialHP1 - field[i, k + 1].Health - field[i, k + 1].Shield;

                                                field[i, k] = temp[0];
                                                field[i, k + 1] = temp[1];

                                                if (field[i, k].Health <= 0)
                                                {
                                                    if (field[i, k].Owner == Player1)
                                                        enemiesKilled++;
                                                    else
                                                        alliesDead++;
                                                    // DestroyCard(i, k);
                                                    field[i, k].Destroy(ref field, ref bufMap);
                                                    field[i, k] = null;
                                                }

                                                if (field[i, k + 1].Health <= 0)
                                                {
                                                    if (field[i, k + 1].Owner == Player1)
                                                        enemiesKilled++;
                                                    else
                                                        alliesDead++;
                                                    //DestroyCard(i, k + 1);
                                                    field[i, k + 1].Destroy(ref field, ref bufMap);
                                                    field[i, k + 1] = null;
                                                    if (field[i, k] != null)
                                                    {
                                                        field[i, k + 1] = Board[i, k];
                                                        //result[i, k].transform.position = coor[i, k + 1].position;
                                                        field[i, k] = null;
                                                    }
                                                }

                                                moved = true;
                                            }

                                            break;
                                        }
                                        else
                                        {
                                            field[i, k + 1] = field[i, k];
                                            //result[i, k].transform.position = coor[i, k + 1].position;
                                            field[i, k] = null;
                                            moved = true;
                                        }

                                        k++;
                                    }

                                }
                            }
                        }

                    if (moved)
                    {
                        return field;
                        //NextTurn();
                        //RollTheCards();
                        // GameObject.Find("Field").GetComponent<SkillMaster>().RebuidBufMap();
                    }
                    else
                        return null;
                }
        }
        return field;
    }
    public static void DestroyCard(int n, int m)
    {
        if (Board[n, m] != null)
        {
            Board[n, m].Destroy(ref Board, ref GameObject.Find("Field").GetComponent<SkillMaster>().BufMap);
            Destroy(Board[n, m].gameObject);
            Board[n, m] = null;
        }
    }

    public static void SaveBoard()
    {
        savedBoard = new Card[n, m];
        for (int l = 0; l < n; l++)
        {
            for (int j = 0; j < m; j++)
            {
                if (Board[l, j] != null)
                {
                    savedBoard[l, j] = Board[l, j];
                }
            }
        }
    }

    private static IEnumerator ShowCanvasForSeconds(GameObject obj, float seconds)
    {
        var canvasGroup = obj.GetComponentInChildren<CanvasGroup>();
        obj.GetComponentInChildren<Text>().text = preGameStage ? "Tactical Stage" : "Battle Stage";

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        yield return new WaitForSeconds(seconds);
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    

    public static void RestoreBoard()
    {
        Board = new Card[n, m];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (savedBoard[i, j] != null)
                {
                    Board[i, j] = savedBoard[i, j];
                }
            }
        }
    }

    public static List<Card> Fight(Card c1, Card c2)
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
