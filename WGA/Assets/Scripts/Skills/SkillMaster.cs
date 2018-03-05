using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Assets.Scripts.Skills.BattleCry;
using UnityEngine;


class SkillMaster : MonoBehaviour
{
    public List<ASkill> SkillsList;
    public SlotBuff[,] BufMap;
    private Battle battle;

    private void Awake()
    {
        SkillsList = new List<ASkill>();
        var hpBufBC = new HPBufBC();
        SkillsList.Add(hpBufBC);
        battle = GetComponentInParent<Battle>();
        
        BufMap = new SlotBuff[battle.m, battle.n];

        var inp = new HPBufBC.HPBufBCInput
        {
            directions = new [] {Directions.Left},
            inputParamsNames = new []{"HpBuf"},
            inputParamsValues = new [] {"5"},
            parentFunctionName = "HPBufBC"
        };

        ExecuteSkillByName(0, 2, 2, inp);

        SerializeSkills();
    }

    private void Start()
    {
        
    }

    public void ExecuteSkillByName(int playerID, int cardRow, int cardCol, ISkillsInput input)
    {
       var t = SkillsList.Find((skill => skill.Name == input.ParentFunctionName));
       t.ExecuteSkill(input, cardRow, cardCol, playerID, ref BufMap);
    }

    public void SerializeSkills()
    {
        var writer = new XmlTextWriter(Path.GetDirectoryName(Application.dataPath) + "/CardsInfo/Skills.xml", Encoding.GetEncoding(1251))
        {
            Formatting = Formatting.Indented,
            Indentation = 1,
            IndentChar = '\t'
        };
        //writer.WriteStartDocument();

        var document = new XmlDocument();
        var xRoot = document.DocumentElement;
        foreach (var skill in SkillsList)
            skill.Serialize(ref writer);
   
        writer.Close();
    }
}

public struct SlotBuff
{
    public int HPBufPlayer0;
    public int DMGBufPlayer0;
    public int ShieldBufPlayer0;

    public int HPBufPlayer1;
    public int DMGBufPlayer1;
    public int ShieldBufPlayer1;
};

public enum SkillType
{
    Aura,
    DeathRattle,
    BattleCry
}

