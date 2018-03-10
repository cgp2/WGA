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
        
        
        BufMap = new SlotBuff[Battle.n, Battle.m];

        //SerializeSkills();
        //DeserializeSkills();
    }

    public SlotBuff[,] RebuidBufMap()
    {
        var ret = BufMap;


        return ret;
    }



    private void Start()
    {
        
    }

    public void ExecuteSkillByName(int playerID, int cardRow, int cardCol, ISkillsInput input)
    {
       var t = SkillsList.Find((skill => skill.Name == input.ParentFunctionName));
       t.ExecuteSkill(input, cardRow, cardCol, playerID, ref BufMap);
    }

    public ISkillsInput GetISkillInputByName(string skillName)
    {
        var aSkill = SkillsList.Find(skill => skill.Name == skillName);
        return aSkill.Input;
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

    public void DeserializeSkills()
    {
        var reader = new XmlTextReader(Path.GetDirectoryName((Application.dataPath)) + "/CardsInfo/ChangedSkills.xml");
        while (reader.ReadToFollowing("Skill"))
        {
            reader.MoveToAttribute("skillName");
            var name = reader.Value;

            var aSkill = SkillsList.Find(skill => skill.Name == name);

            reader.MoveToAttribute("type");
            aSkill.Type = (SkillType)Enum.Parse(typeof(SkillType), reader.Value);
            reader.MoveToAttribute("Ally");
            aSkill.Ally = bool.Parse(reader.Value);

            var inp = GetISkillInputByName(name);

            reader.ReadToFollowing("Input");
            reader.Read();
            reader.Read();
            var t = new List<string>();
            while (reader.Name == "inp")
            {
                reader.MoveToAttribute("value");
                t.Add(reader.Value);
                reader.Read();
                reader.Read();
            }
            inp.InputParamsValues = t.ToArray();

            reader.ReadToFollowing("Directions");
            var dirs = new List<Directions>();
            reader.Read();
            reader.Read();
            while (reader.Name == "dir")
            {
                reader.MoveToAttribute("type");
                dirs.Add((Directions)Enum.Parse(typeof(Directions), reader.Value));
                reader.Read();
                reader.Read();
            }
            inp.Directions = dirs.ToArray();

            reader.ReadToFollowing("Description");
            aSkill.Description = reader.ReadElementContentAsString();
        }

    }
}

public struct SlotBuff
{
    public int StaticHPBufPlayer0;
    public int StaticDMGBufPlayer0;
    public int StaticShieldBufPlayer0;
    public int FloatingHPBufPlayer0;
    public int FloatingDMGBufPlayer0;
    public int FloatingShieldBufPlayer0;

    public int StaticHPBufPlayer1;
    public int StaticDMGBufPlayer1;
    public int StaticShieldBufPlayer1;
    public int FloatingHPBufPlayer1;
    public int FloatingDMGBufPlayer1;
    public int FloatingShieldBufPlayer1;
};

public enum SkillType
{
    Aura,
    DeathRattle,
    BattleCry
}

