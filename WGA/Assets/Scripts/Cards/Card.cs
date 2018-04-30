using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public class Card : MonoBehaviour
{
    public CardInfo Info;

    public int Health;
    public int Shield;
    public int Attack;

    public Player Owner;

    public bool OnBoard;
    public int StaticHP;
    public int StaticDMG;
    public int StaticSHLD;

    private SkillMaster skillMaster;
    public bool front_rotate = false;
    public bool back_rotate = false;

    public bool IsActiveSkillAvaliable = true;
    

    public void Initialize(string cardName, int health, int shield, int attack, string description, SkillMaster skillMaster, string[] battleCryName = null, string[] deathRattleName = null, string[] auraName = null)
    {
        this.skillMaster = skillMaster;

        Info = new CardInfo
        {
            Name = cardName,
            Description = description,

            InitialHealth = health,
            InitialShield = shield,
            InitialAttack = attack,

            BattleCryNames = battleCryName,
            DeathRattleName = deathRattleName,
            AuraNames = auraName,

            // CardFrontSprite = cardFront,
            // CardBackSprite = cardBack,
            //ShipSprite = ship
        };

        StaticHP = Health = health;
        StaticSHLD = Shield = shield;
        StaticDMG = Attack = attack;

        if (battleCryName != null)
        {
            var t = new List<SkillsInput>();
            foreach (var n in battleCryName)
                t.Add(skillMaster.GetISkillInputByName(n));
            Info.BattleCryInput = t.ToArray();
        }

        if (deathRattleName != null)
        {
            var t = new List<SkillsInput>();
            foreach (var n in deathRattleName)
                t.Add(skillMaster.GetISkillInputByName(n));
            Info.DeathRattleInput = t.ToArray();
        }

        if (auraName != null)
        {
            var t = new List<SkillsInput>();
            foreach (var n in Info.AuraNames)
                t.Add(skillMaster.GetISkillInputByName(n));
            Info.AuraInput = t.ToArray();
        }

        OnBoard = false;
    }

    public void Initialize(CardData data)
    {
        this.skillMaster = data.SkillM;

        Info = new CardInfo
        {
            Name = data.Name,
            Description = data.Desk,

            InitialHealth = data.HP,
            InitialShield = data.Shield,
            InitialAttack =data.Attack,

            BattleCryNames = data.BattleCryNames,
            DeathRattleName = data.DeathRattleNames,
            AuraNames = data.AurasNames,

            // CardFrontSprite = cardFront,
            // CardBackSprite = cardBack,
            //ShipSprite = ship
        };

        StaticHP = Health = data.HP;
        StaticSHLD = Shield = data.Shield;
        StaticDMG = Attack = data.Attack;

        if (data.BattleCryNames.Length !=0)
        {
            var t = new List<SkillsInput>();
            foreach (var n in data.BattleCryNames)
                t.Add(skillMaster.GetISkillInputByName(n));
            Info.BattleCryInput = t.ToArray();
        }

        if (data.DeathRattleNames.Length != 0)
        {
            var t = new List<SkillsInput>();
            foreach (var n in data.DeathRattleNames)
                t.Add(skillMaster.GetISkillInputByName(n));
            Info.DeathRattleInput = t.ToArray();
        }

        if (data.AurasNames.Length != 0)
        {
            var t = new List<SkillsInput>();
            foreach (var n in data.AurasNames)
                t.Add(skillMaster.GetISkillInputByName(n));
            Info.AuraInput = t.ToArray();
        }

        OnBoard = false;
    }
    public void InitializeSprites(Sprite cardFront, Sprite cardBack, Sprite ship)
    {
        Info.CardFrontSprite = cardFront;
        Info.CardBackSprite = cardBack;
        Info.ShipSprite = ship;
    }

    private void Awake()
    {
        SpriteRenderer shipSprite = GetComponent<SpriteRenderer>();
        shipSprite.sprite = Info.CardBackSprite;
        shipSprite.size = new Vector2(2f, 3f);
    }
    public void Spin(bool front)
    {
        SpriteRenderer shipSprite = GetComponent<SpriteRenderer>();
        shipSprite.sprite = front ? Info.CardFrontSprite : Info.CardBackSprite;
    }

    public void Play(ref Card[,] board, ref SlotBuff[,] bufMap)
    {
        if (Info.BattleCryNames != null)
        {
            for (var i = 0; i < Info.BattleCryNames.Length; i++)
            {
                skillMaster.ExecuteSkillByInput(this, Info.BattleCryInput[i], ref board);
                skillMaster.ApplyBufsToBoard(ref board, ref bufMap);
            }
        }

        if (Info.AuraNames != null)
        {
            for (var i = 0; i < Info.AuraNames.Length; i++)
            {
                skillMaster.ExecuteSkillByInput(this, Info.AuraInput[i], ref board);
                skillMaster.ApplyBufsToBoard(ref board, ref bufMap);
            }
        }

        Battle.UpdateUI();
    }

    public void Destroy(ref Card[,] board, ref SlotBuff[,] bufMap)
    {
        if (Info.DeathRattleName != null)
        {
            for (var i = 0; i < Info.DeathRattleName.Length; i++)
            {
                skillMaster.ExecuteSkillByInput(this, Info.DeathRattleInput[i], ref board);
                skillMaster.ApplyBufsToBoard(ref board, ref bufMap);

                if (Info.AuraInput != null)
                {
                    foreach (var inp in Info.AuraInput)
                    {
                        skillMaster.ReExecuteSkillByInput(this, inp, ref board);
                        skillMaster.ApplyBufsToBoard(ref board, ref bufMap);
                    }
                }
            }
        }
    }

    public void ActiveSkill(ref Card[,] board, ref SlotBuff[,] bufMap)
    {
        if (IsActiveSkillAvaliable)
        {
            if (Info.ActiveSkillName != null)
            {
                skillMaster.ExecuteSkillByInput(this, Info.ActiveSkillInput, ref board);
                IsActiveSkillAvaliable = false;
            }
        }
    }

    public static CardData[] Deserialize(string pathToFile)
    {
        List<CardData> cards = new List<CardData>();
        var reader = new XmlTextReader(pathToFile);
        while (reader.ReadToFollowing("CardInfo"))
        {
            reader.ReadToFollowing("Health");
            reader.Read();
            var health = reader.Value;

            reader.ReadToFollowing("Shield");
            reader.Read();
            var shield = reader.Value;

            reader.ReadToFollowing("Attack");
            reader.Read();
            var attack = reader.Value;

            reader.ReadToFollowing("Name");
            reader.Read();
            var name = reader.Value;

            reader.ReadToFollowing("Description");
            reader.Read();
            var desk = reader.Value;

            var battleCry = new List<string>();
            reader.ReadToFollowing("BattleCryName");
            if (!reader.IsEmptyElement)
            {
                reader.Read();
                reader.Read();
                while (reader.Name == "string")
                {
                    reader.Read();
                    battleCry.Add(reader.Value);
                    reader.Read();
                    reader.Read();
                    reader.Read();
                }
            }
       
            var deathRattle = new List<string>();
            reader.ReadToFollowing("DeathRattleName");  
            if (!reader.IsEmptyElement)
            {
                reader.Read();
                reader.Read();
                while (reader.Name == "string")
                {
                    reader.Read();
                    deathRattle.Add(reader.Value);
                    reader.Read();
                    reader.Read();
                    reader.Read();
                }
            }

            var aura = new List<string>();
            reader.ReadToFollowing("AuraName");
            if (!reader.IsEmptyElement)
            {
                reader.Read();
                reader.Read();
                while (reader.Name == "string")
                {
                    reader.Read();
                    aura.Add(reader.Value);
                    reader.Read();
                    reader.Read();
                    reader.Read();
                }
            }

            var crd = new CardData { Name = name,HP =  int.Parse(health), Shield= int.Parse(shield), Attack = int.Parse(attack), Desk = desk, SkillM = GameObject.Find("Field").GetComponent<SkillMaster>(), BattleCryNames = battleCry.ToArray(), DeathRattleNames = deathRattle.ToArray(), AurasNames =aura.ToArray() };

            //crd.Initialize(name, int.Parse(health), int.Parse(shield), int.Parse(attack), desk, GameObject.Find("Field").GetComponent<SkillMaster>(), battleCry.ToArray(), deathRattle.ToArray(), aura.ToArray());
            cards.Add(crd);
        }

        return cards.ToArray();
    }
    public struct CardData
    {
        public string Name;
        public int HP;
        public int Shield;
        public int Attack;
        public string Desk;
        public SkillMaster SkillM;
        public string[] BattleCryNames;
        public string[] DeathRattleNames;
        public string[] AurasNames;
        public string ActiveSkillName;

    }
    // Use this for initialization
    void Start()
    {
        //well done 
        //thx   
    }

    // Update is called once per frame
    void Update()
    {

    }

    public struct CardInfo
    {
        public string Name;
        public string Description;

        public int InitialHealth;
        public int InitialShield;
        public int InitialAttack;

        public string[] DeathRattleName;
        public string[] BattleCryNames;
        public string[] AuraNames;
        public string ActiveSkillName;

        public SkillsInput[] BattleCryInput;
        public SkillsInput[] AuraInput;
        public SkillsInput[] DeathRattleInput;
        public SkillsInput ActiveSkillInput;

        public Sprite CardBackSprite;
        public Sprite CardFrontSprite;
        public Sprite ShipSprite;
    }
}
