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

    public void Play()
    {
        if (Info.BattleCryNames != null)
        {
            for (var i = 0; i < Info.BattleCryNames.Length; i++)
            {
                skillMaster.ExecuteSkillByInput(this, Info.BattleCryInput[i]);
            }
        }

        if (Info.AuraNames != null)
        {
            for (var i = 0; i < Info.AuraNames.Length; i++)
            {
                skillMaster.ExecuteSkillByInput(this, Info.AuraInput[i]);
            }
        }
    }

    public void Destroy()
    {
        if (Info.DeathRattleName != null)
        {
            for (var i = 0; i < Info.DeathRattleName.Length; i++)
            {
                skillMaster.ExecuteSkillByInput(this, Info.DeathRattleInput[i]);

                if (Info.AuraInput != null)
                {
                    foreach (var inp in Info.AuraInput)
                    {
                        skillMaster.ReExecuteSkillByInput(this, inp);
                    }
                }
            }
        }
    }

    public static Card[] Deserialize(string pathToFile)
    {
        List<Card> cards = new List<Card>();
        var reader = new XmlTextReader(pathToFile);
        while (reader.ReadToFollowing("CardInfo"))
        {
            Card crd = new Card();

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

            reader.ReadToFollowing("BattleCryName");
            reader.Read();
            reader.Read();
            var battleCry = new List<string>();
            while (reader.Name == "string")
            {
                reader.Read();
                battleCry.Add(reader.Value);
                reader.Read();
                reader.Read();
                reader.Read();
            }

            reader.ReadToFollowing("DeathRattleName");
            reader.Read();
            reader.Read();
            var deathRattle = new List<string>();
            while (reader.Name == "string")
            {
                reader.Read();
                deathRattle.Add(reader.Value);
                reader.Read();
                reader.Read();
                reader.Read();
            }

            reader.ReadToFollowing("AuraName");
            reader.Read();
            reader.Read();
            var aura = new List<string>();
            while (reader.Name == "string")
            {
                reader.Read();
                aura.Add(reader.Value);
                reader.Read();
                reader.Read();
                reader.Read();
            }


            crd.Initialize(name, int.Parse(health), int.Parse(shield), int.Parse(attack), desk, GameObject.Find("Field").GetComponent<SkillMaster>(), battleCry.ToArray(), deathRattle.ToArray(), aura.ToArray());
            cards.Add(crd);
        }

        return cards.ToArray();
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

        public SkillsInput[] BattleCryInput;
        public SkillsInput[] AuraInput;
        public SkillsInput[] DeathRattleInput;

        public Sprite CardBackSprite;
        public Sprite CardFrontSprite;
        public Sprite ShipSprite;
    }
}
