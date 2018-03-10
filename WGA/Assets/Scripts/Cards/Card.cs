using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml.Serialization;
using System.IO;

public class Card : MonoBehaviour
{
    public int Health;
    public int Shield;
    public int Attack;
    public string Name;
    public string Description;
    public string[] DeathRattleName;
    public string[] BattleCryNames;
    public string[] AuraNames;
    public Player Owner;
    public Sprite CardBack;
    public Sprite CardFront;
    public bool OnBoard;
    public int StaticHP;
    public int StaticDMG;
    public int StaticSHLD;

    private SkillMaster skillMaster;
    private ISkillsInput[] battleCryInput;
    private ISkillsInput[] auraInput;
    private ISkillsInput[] deathRattleInput;

    public void Initialize(string cardName, int health, int shield, int attack, string description, Sprite cardFront, SkillMaster skillMaster, string[] battleCryName = null, string[] deathRattleName = null, string[] auraName = null)
    { 
        this.skillMaster = skillMaster;
        Name= cardName;
        Description = description;
        Health = health;
        Shield = shield;
        Attack = attack;

        BattleCryNames = battleCryName;
        DeathRattleName = deathRattleName;
        AuraNames = auraName;
        if (battleCryName != null)
        {
            var t = new List<ISkillsInput>();
            foreach (var n in battleCryName)
                t.Add(skillMaster.GetISkillInputByName(n));
            battleCryInput = t.ToArray();
        }

        if (deathRattleName != null)
        {
            var t = new List<ISkillsInput>();
            foreach (var n in deathRattleName)
                t.Add(skillMaster.GetISkillInputByName(n));
            deathRattleInput = t.ToArray();
        }

        if (auraName != null)
        {
            var t = new List<ISkillsInput>();
            foreach (var n in AuraNames)
                t.Add(skillMaster.GetISkillInputByName(n));
            auraInput = t.ToArray();
        }

        CardFront = cardFront;
        OnBoard = false;
    }

    private void Awake()
    {
        SpriteRenderer shipSprite = GetComponent<SpriteRenderer>();
        shipSprite.sprite = CardBack;
        shipSprite.size = new Vector2(2f, 3f);
    }
    public void Spin(bool front)
    {
        SpriteRenderer shipSprite = GetComponent<SpriteRenderer>();
        shipSprite.sprite = front ? CardFront : CardBack;
    }

    public void Play()
    {
        if (BattleCryNames != null)
        {
            for (var i = 0; i < BattleCryNames.Length; i++)
            {
                skillMaster.ExecuteSkillByInput(this, battleCryInput[i]);
            }
        }

        if (AuraNames != null)
        {
            for (var i = 0; i < AuraNames.Length; i++)
            {
                skillMaster.ExecuteSkillByInput(this, auraInput[i]);
            }
        }
    }

    public void Destroy()
    {
        if (DeathRattleName != null)
        {
            for (var i = 0; i < DeathRattleName.Length; i++)
            {
                skillMaster.ExecuteSkillByInput(this, deathRattleInput[i]);

                if (auraInput != null)
                {
                    foreach (var inp in auraInput)
                    {
                        skillMaster.ReExecuteSkillByInput(this, inp);
                    }
                }
            }
        }
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

}
