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


public class SkillMaster : MonoBehaviour
{
    public List<ASkill> SkillsList;
    public SlotBuff[,] BufMap;
    private Battle battle;

    private void Awake()
    {
        SkillsList = new List<ASkill>();
        var hpBufBC = new HPBufBC();
        SkillsList.Add(hpBufBC);
            
        BufMap = new SlotBuff[Battle.n, Battle.m];

        //SerializeSkills();
        //DeserializeSkills();
    }

    private void Start()
    {

    }

    public void ApplyBufsToField(out Card[,] field)
    {
        var ret = new Card[Battle.n, Battle.m];

        for (var i = 0; i < Battle.n; i++)
        {
            for (var j = 0; j < Battle.m; j++)
            {
                if (Battle.Board[i, j] != null)
                {
                    if (Battle.Board[i,j].Owner.name == "Player1")
                    {
                        Battle.Board[i, j].staticHP += BufMap[i, j].StaticHPBufPlayer1;
                        Battle.Board[i, j].Attack += BufMap[i, j].StaticDMGBufPlayer1;
                        Battle.Board[i, j].Shield += BufMap[i, j].StaticShieldBufPlayer1;

                        Battle.Board[i, j].Health = Battle.Board[i, j].staticHP + BufMap[i, j].FloatingHPBufPlayer1;
                        Battle.Board[i, j].Attack = Battle.Board[i, j].staticDMG + BufMap[i, j].FloatingDMGBufPlayer1;
                        Battle.Board[i, j].Shield = Battle.Board[i, j].staticSHLD + BufMap[i, j].FloatingShieldBufPlayer1;
                    }
                    else
                    {
                        Battle.Board[i, j].staticHP += BufMap[i, j].StaticHPBufPlayer2;
                        Battle.Board[i, j].Attack += BufMap[i, j].StaticDMGBufPlayer2;
                        Battle.Board[i, j].Shield += BufMap[i, j].StaticShieldBufPlayer2;

                        Battle.Board[i, j].Health = Battle.Board[i, j].staticHP + BufMap[i, j].FloatingHPBufPlayer2;
                        Battle.Board[i, j].Attack = Battle.Board[i, j].staticDMG + BufMap[i, j].FloatingDMGBufPlayer2;
                        Battle.Board[i, j].Shield = Battle.Board[i, j].staticSHLD + BufMap[i, j].FloatingShieldBufPlayer2;
                    }

                    BufMap[i, j].StaticHPBufPlayer1 = 0;
                    BufMap[i, j].StaticDMGBufPlayer1 = 0;
                    BufMap[i, j].StaticShieldBufPlayer1 = 0;
                    BufMap[i, j].StaticHPBufPlayer2 = 0;
                    BufMap[i, j].StaticDMGBufPlayer2 = 0;
                    BufMap[i, j].StaticShieldBufPlayer2 = 0;
                }
            }
        }


        field = ret;
    }

    public SlotBuff[,] RebuidBufMap()
    {
        var ret = BufMap;

        return ret;
    }

    public void ExecuteSkillByInput(Card card, ISkillsInput input)
    {
        var playerID = (card.Owner.name == "Player1") ? 0 : 1;

        int cardRow = 0, cardCol = 0;
        for (var i = 0; i < Battle.Board.GetLength(0); i++)
        {
            for (var j = 0; j < Battle.Board.GetLength(1); j++)
            {
                if (Battle.Board[i, j] == card)
                {
                    cardRow = i;
                    cardCol = j;
                    break;
                }
            }
        }

        var t = SkillsList.Find((skill => skill.Name == input.ParentFunctionName));
        t.ExecuteSkill(input, cardRow, cardCol, playerID, ref BufMap);
        ApplyBufsToField(out Battle.Board);
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
    public int StaticHPBufPlayer1;
    public int StaticDMGBufPlayer1;
    public int StaticShieldBufPlayer1;
    public int FloatingHPBufPlayer1;
    public int FloatingDMGBufPlayer1;
    public int FloatingShieldBufPlayer1;

    public int StaticHPBufPlayer2;
    public int StaticDMGBufPlayer2;
    public int StaticShieldBufPlayer2;
    public int FloatingHPBufPlayer2;
    public int FloatingDMGBufPlayer2;
    public int FloatingShieldBufPlayer2;
};

public enum SkillType
{
    Aura,
    DeathRattle,
    BattleCry
}

