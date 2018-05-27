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
    public CardData Data;
    public int Health;
    public int Shield;
    public int Attack;

    public Player Owner;

    public bool OnBoard = false;
    public int StaticHP;
    public int StaticDMG;
    public int StaticSHLD;

    private SkillMaster skillMaster;
    public bool front_rotate = false;
    public bool back_rotate = false;

    public bool IsActiveSkillAvaliable = true;
    

    public void Initialize(Guid id, string cardName, int health, int shield, int attack, string description, SkillMaster skillMaster, 
        string battleCryValue = null, string deathRattleValue = null, string auraValue = null, string[] battleCryName = null, string[] deathRattleName = null, string[] auraName = null, string imgPath="")
    {
        this.skillMaster = skillMaster;

        Info = new CardInfo
        {
            ID = id,
            Name = cardName,
            Description = description,

            InitialHealth = health,
            InitialShield = shield,
            InitialAttack = attack,

            BattleCryNames = battleCryName,
            DeathRattleName = deathRattleName,
            AuraNames = auraName,
            ImagePath = imgPath,
            ShipSprite = Resources.Load<Sprite>("Assets/Content/CardSprites/Ships/" + imgPath)
            // CardFrontSprite = cardFront,
            // CardBackSprite = cardBack,
            //ShipSprite = ship
        };

        StaticHP = Health = health;
        StaticSHLD = Shield = shield;
        StaticDMG = Attack = attack;

        if (skillMaster != null)
        {
            var battleCryInp = new List<string>();
            if (battleCryName != null)
            {
                var t = new List<SkillsInput>();
                foreach (var n in battleCryName)
                {
                    var input = skillMaster.GetISkillInputByName(n);
                    if (battleCryValue != null) input.InputParamsValues[0] = battleCryValue;
                    t.Add(input);
                    battleCryInp.Add(input.InputParamsValues[0]);
                }

                Info.BattleCryInput = t.ToArray();
            }

            Info.BattleCryValue = battleCryInp.ToArray();


            var deathRattleInp = new List<string>();
            if (deathRattleName != null)
            {
                var t = new List<SkillsInput>();
                foreach (var n in deathRattleName)
                {
                    var input = skillMaster.GetISkillInputByName(n);
                    if (deathRattleValue != null) input.InputParamsValues[0] = deathRattleValue;
                    t.Add(input);
                    deathRattleInp.Add(input.InputParamsValues[0]);
                }

                Info.DeathRattleInput = t.ToArray();
            }

            Info.DeathRattleInputValue = deathRattleInp.ToArray();

            var auraInp = new List<string>();
            if (auraName != null)
            {
                var t = new List<SkillsInput>();
                foreach (var n in Info.AuraNames)
                {
                    var input = skillMaster.GetISkillInputByName(n);
                    if (auraValue != null) input.InputParamsValues[0] = auraValue;
                    t.Add(input);
                    auraInp.Add(input.InputParamsValues[0]);
                }

                Info.AuraInput = t.ToArray();
        }

        Info.AuraInputValue = auraInp.ToArray();


            var activeInput = skillMaster.GetISkillInputByName("DmgToCard");
            Info.ActiveSkillInput = activeInput;
            Info.ActiveSkillName = "DmgToCard";
        }
    }

    public void Initialize(CardData data)
    {
        skillMaster = data.SkillM;
       
        Info = new CardInfo
        {
            ID = data.id,
            Name = data.Name,
            Description = data.Desk,

            InitialHealth = data.HP,
            InitialShield = data.Shield,
            InitialAttack = data.Attack,

            BattleCryNames = data.BattleCryNames,
            DeathRattleName = data.DeathRattleNames,
            AuraNames = data.AurasNames,
            ShipSprite = Resources.Load<Sprite>("CardSprites/Ships/" + data.ImagePath),

            ImagePath = data.ImagePath

        };
        Data = new CardData
        {
            HP = Info.InitialHealth,
            Name = data.Name,
            Shield = data.Shield,
            Attack = data.Attack,
            id = data.id,
            ActiveInputValue = data.ActiveInputValue,
            ActiveSkillName = data.ActiveSkillName,
            BattleCryInputValue = data.BattleCryInputValue,
            BattleCryNames = data.BattleCryNames,
            AuraInputValue = data.AuraInputValue,
            AurasNames = data.AurasNames,
            DeathRattleInputValue = data.DeathRattleInputValue,
            DeathRattleNames = data.DeathRattleNames,
            Desk = data.Desk,
            ImagePath = data.ImagePath,
            Race = data.Race,
            SkillM = null
        };
        StaticHP = Health = data.HP;
        StaticSHLD = Shield = data.Shield;
        StaticDMG = Attack = data.Attack;

        if (skillMaster != null)
        {
            var battleCryInp = new List<string>();
            if (data.BattleCryNames.Length != 0)
            {
                var t = new List<SkillsInput>();
                foreach (var n in data.BattleCryNames)
                {
                    var input = skillMaster.GetISkillInputByName(n);
                    input.InputParamsValues[0] = data.BattleCryInputValue[0].ToString();
                    t.Add(input);
                    battleCryInp.Add(input.InputParamsValues[0]);
                }

                Info.BattleCryInput = t.ToArray();
            }

            Info.BattleCryValue = battleCryInp.ToArray();

            var deathRattleInp = new List<string>();
            if (data.DeathRattleNames.Length != 0)
            {
                var t = new List<SkillsInput>();
                foreach (var n in data.DeathRattleNames)
                {
                    var input = skillMaster.GetISkillInputByName(n);
                    input.InputParamsValues[0] = data.DeathRattleInputValue[0].ToString();
                    t.Add(input);
                    deathRattleInp.Add(input.InputParamsValues[0]);
                }

                Info.DeathRattleInput = t.ToArray();
            }

            Info.DeathRattleInputValue = deathRattleInp.ToArray();

            var auraInp = new List<string>();


            if (data.AurasNames.Length != 0)
            {
                var t = new List<SkillsInput>();
                foreach (var n in data.AurasNames)
                {
                    var input = skillMaster.GetISkillInputByName(n);
                    input.InputParamsValues[0] = data.AuraInputValue[0].ToString();
                    t.Add(input);
                    auraInp.Add(input.InputParamsValues[0]);
                }

                Info.AuraInput = t.ToArray();
            }
            Info.AuraInputValue = auraInp.ToArray();
            var activeInput = skillMaster.GetISkillInputByName(data.ActiveSkillName);
            Info.ActiveSkillInput = activeInput;

        }

        Info.ActiveSkillName = data.ActiveSkillName;
    }
    public void InitializeSprites(Sprite cardFront, Sprite cardBack, Sprite ship)
    {
        Info.CardFrontSprite = cardFront;
        Info.CardBackSprite = cardBack;
        if(Info.ShipSprite==null)
            Info.ShipSprite = ship;
    }

    private void Awake()
    {
        //SpriteRenderer shipSprite = GetComponent<SpriteRenderer>();
        //shipSprite.sprite = Info.CardBackSprite;
        //shipSprite.size = new Vector2(2f, 3f);
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

    public bool ExecuteActiveSkill(ref Card[,] board, ref SlotBuff[,] bufMap)
    {
        if (IsActiveSkillAvaliable)
        {
            if (Info.ActiveSkillName != null)
            {
                skillMaster.ExecuteSkillByInput(this, Info.ActiveSkillInput, ref board);
                skillMaster.ApplyBufsToBoard(ref board, ref bufMap);
                IsActiveSkillAvaliable = false;
                Battle.UpdateUI();

                return true;
            }
            
           
        }
        return false;
    }

    public static CardData[] Deserialize(string pathToFile)
    {
        List<CardData> cards = new List<CardData>();
        var reader = new XmlTextReader(pathToFile);
        while (reader.ReadToFollowing("CardInfo"))
        {
            reader.ReadToFollowing("CardClass");
            reader.Read();
            var race = reader.Value;

            reader.ReadToFollowing("ImagePath");
            reader.Read();
            var imgPath = reader.Value;

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

            var BCinp = new List<int>();
            reader.ReadToFollowing("valueBatterCry");
            reader.Read();
            BCinp.Add(int.Parse(reader.Value));

            var DRinp = new List<int>();
            reader.ReadToFollowing("valueDeathRattle");
            reader.Read();
            DRinp.Add(int.Parse(reader.Value));

            var Aurainp = new List<int>();
            reader.ReadToFollowing("valueAura");
            reader.Read();
            Aurainp.Add(int.Parse(reader.Value));

            reader.ReadToFollowing("CardID");
            reader.Read();
            var guid = new Guid(reader.Value);

            var crd = new CardData
            {
                id = guid,
                Name = name,
                ImagePath = imgPath,
                Race = race,
                HP =  int.Parse(health),
                Shield = int.Parse(shield),
                Attack = int.Parse(attack),
                Desk = desk,
                SkillM = Battle.SkillMaster,
                BattleCryNames = battleCry.ToArray(),
                BattleCryInputValue = BCinp.ToArray(),
                DeathRattleNames = deathRattle.ToArray(),
                DeathRattleInputValue = DRinp.ToArray(),
                AurasNames = aura.ToArray(),
                AuraInputValue = Aurainp.ToArray(),
                ActiveInputValue = 1,
                ActiveSkillName = "DmgToCard",
            };

            //crd.Initialize(name, int.Parse(health), int.Parse(shield), int.Parse(attack), desk, GameObject.Find("Field").GetComponent<SkillMaster>(), battleCry.ToArray(), deathRattle.ToArray(), aura.ToArray());
            cards.Add(crd);
        }
        reader.Close();
        return cards.ToArray();
    }
    public List<ASkill> GetSkillsList()
    {
        return skillMaster.SkillsList;
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
    [Serializable]
    public struct CardInfo
    {
        public Guid ID;
        public string Name;
        public string Race;
        public string Description;
        public string ImagePath;

        public int InitialHealth;
        public int InitialShield;
        public int InitialAttack;

        public string[] DeathRattleName;
        public string[] BattleCryNames;
        public string[] AuraNames;
        public string ActiveSkillName;

        public string[] BattleCryValue;
        public string[] AuraInputValue;
        public string[] DeathRattleInputValue;

        public SkillsInput[] BattleCryInput;
        public SkillsInput[] AuraInput;
        public SkillsInput[] DeathRattleInput;
        public SkillsInput ActiveSkillInput;

        public Sprite CardBackSprite;
        public Sprite CardFrontSprite;
        public Sprite ShipSprite;
    }

    public struct CardData
    {
        public Guid id;
        public string Name;
        public string Race;
        public int HP;
        public int Shield;
        public int Attack;
        public string Desk;
        public SkillMaster SkillM;

        public string ImagePath;

        public string[] BattleCryNames;
        public int[] BattleCryInputValue;

        public string[] DeathRattleNames;
        public int[] DeathRattleInputValue;

        public string[] AurasNames;
        public int[] AuraInputValue;

        public string ActiveSkillName;
        public int ActiveInputValue;

        public void Serialize(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("CardInfo");
            {
                writer.WriteStartElement("CardClass");
                writer.WriteRaw(Race);
                writer.WriteEndElement();

                writer.WriteStartElement("ImagePath");
                writer.WriteRaw(ImagePath);
                writer.WriteEndElement();

                writer.WriteStartElement("Health");
                writer.WriteRaw(HP.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("Shield");
                writer.WriteRaw(Shield.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("Attack");
                writer.WriteRaw(Attack.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("Name");
                writer.WriteRaw(Name);
                writer.WriteEndElement();

                writer.WriteStartElement("Description");
                writer.WriteRaw(Desk);
                writer.WriteEndElement();

                writer.WriteStartElement("BattleCryName");
                foreach (var name in BattleCryNames)
                {
                    writer.WriteStartElement("string");
                    writer.WriteRaw(name);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("DeathRattleName");
                foreach (var name in DeathRattleNames)
                {
                    writer.WriteStartElement("string");
                    writer.WriteRaw(name);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("AuraName");
                foreach (var name in DeathRattleNames)
                {
                    writer.WriteStartElement("string");
                    writer.WriteRaw(name);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("valueBatterCry");
                writer.WriteRaw(BattleCryInputValue[0].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("valueDeathRattle");
                writer.WriteRaw(DeathRattleInputValue[0].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("valueAura");
                writer.WriteRaw(AuraInputValue[0].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("CardID");
                writer.WriteRaw(id.ToString());
                writer.WriteEndElement();              
            }

            writer.WriteEndElement();
        }

    }
}
