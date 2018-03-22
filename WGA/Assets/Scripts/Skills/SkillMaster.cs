using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Assets.Scripts.Skills.Aura;
using Assets.Scripts.Skills.BattleCry;
using Assets.Scripts.Skills.DeathRattle;
using UnityEngine;


public class SkillMaster : MonoBehaviour
{
    public List<ASkill> SkillsList;
    public SlotBuff[,] BufMap;

    private void Awake()
    {
        SkillsList = new List<ASkill>();

        var hpBufBC = new HPBufBC();
        SkillsList.Add(hpBufBC);

        var dmgBufAura = new DmgBufAura();
        SkillsList.Add(dmgBufAura);

        var dmgDebufAura = new DmgDebufAura();
        SkillsList.Add(dmgDebufAura);

        var shldDebufDR = new ShldDebufDR();
        SkillsList.Add(shldDebufDR);

        BufMap = new SlotBuff[Battle.n, Battle.m];

        for (var i = 0; i < BufMap.GetLength(0); i++)
        {
            for (var j = 0; j < BufMap.GetLength(1); j++)
            {
                BufMap[i, j].Row = i;
                BufMap[i, j].Col = j;
            }
        }

        SerializeSkills();
        //DeserializeSkills();
    }

    private void Start()
    {

    }

    public void ApplyBufsToBoard(out Card[,] field)
    {
        var ret = Battle.Board;


        for (var i = 0; i < Battle.n; i++)
        {
            for (var j = 0; j < Battle.m; j++)
            {
                if (ret[i, j] != null)
                {
                    if (ret[i,j].Owner.name == "Player1")
                    {
                        ret[i, j].StaticHP += BufMap[i, j].StaticHPBufPlayer1;
                        ret[i, j].StaticDMG += BufMap[i, j].StaticDMGBufPlayer1;
                        ret[i, j].StaticSHLD = Math.Max(0, BufMap[i, j].StaticShieldBufPlayer1 + ret[i, j].StaticSHLD);

                        if(ret[i, j].StaticHP + BufMap[i, j].FloatingHPBufPlayer1 <= 0)
                        {
                           // Battle.DestroyCard(i, j);
                        }
                        else
                        {
                            ret[i, j].Health = ret[i, j].StaticHP + BufMap[i, j].FloatingHPBufPlayer1;
                        }

                        ret[i, j].Attack = Math.Max(ret[i, j].StaticDMG + BufMap[i, j].FloatingDMGBufPlayer1, 0);
                        ret[i, j].Shield = Math.Max(ret[i, j].StaticSHLD + BufMap[i, j].FloatingShieldBufPlayer1, 0);
                    }
                    else
                    {
                        ret[i, j].StaticHP += BufMap[i, j].StaticHPBufPlayer2;
                        ret[i, j].StaticDMG += BufMap[i, j].StaticDMGBufPlayer2;
                        ret[i, j].StaticSHLD = Math.Max(0, BufMap[i, j].StaticShieldBufPlayer2 + ret[i, j].StaticSHLD);

                        if (ret[i, j].StaticHP + BufMap[i, j].FloatingHPBufPlayer2 <= 0)
                        {
                           // Battle.DestroyCard(i, j);
                        }
                        else
                        {
                            ret[i, j].Health = ret[i, j].StaticHP + BufMap[i, j].FloatingHPBufPlayer2;
                        }

                        ret[i, j].Attack = Math.Max(ret[i, j].StaticDMG + BufMap[i, j].FloatingDMGBufPlayer2, 0);
                        ret[i, j].Shield = Math.Max(ret[i, j].StaticSHLD + BufMap[i, j].FloatingShieldBufPlayer2, 0);
                    }
                }

                BufMap[i, j].StaticHPBufPlayer1 = 0;
                BufMap[i, j].StaticDMGBufPlayer1 = 0;
                BufMap[i, j].StaticShieldBufPlayer1 = 0;
                BufMap[i, j].StaticHPBufPlayer2 = 0;
                BufMap[i, j].StaticDMGBufPlayer2 = 0;
                BufMap[i, j].StaticShieldBufPlayer2 = 0;
            }
        }

        field = ret;
        Battle.UpdateUI();
    }

    public void RebuidBufMap()
    {
        BufMap = new SlotBuff[Battle.n, Battle.m];
        for (var i = 0; i < BufMap.GetLength(0); i++)
        {
            for (var j = 0; j < BufMap.GetLength(1); j++)
            {
                BufMap[i, j].Row = i;
                BufMap[i, j].Col = j;
            }
        }

        for (var i = 0; i < Battle.n; i++)
        {
            for (var j = 0; j < Battle.m; j++)
            {
                if (Battle.Board[i, j] != null)
                {
                    foreach (var inp in Battle.Board[i, j].Info.AuraInput)
                    {
                        ExecuteSkillByInput(Battle.Board[i, j], inp);
                    }
                }
            }
        }
        ApplyBufsToBoard(out Battle.Board);
    }

    public void ExecuteSkillByInput(Card card, SkillsInput input)
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
    }

    public void ReExecuteSkillByInput(Card card, SkillsInput input)
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
        t.ReExecuteSkill(input, cardRow, cardCol, playerID, ref BufMap);
        ApplyBufsToBoard(out Battle.Board);
    }

    public SkillsInput GetISkillInputByName(string skillName)
    {
        try
        {
            var aSkill = SkillsList.Find(skill => skill.Name == skillName);
            return aSkill.Input;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }   
    }

    public void SerializeSkills()
    {
        var writer = new XmlTextWriter(Path.GetDirectoryName(Application.dataPath) + "/CardsInfo/Skills.xml", Encoding.GetEncoding(1251))
        {
            Formatting = Formatting.Indented,
            Indentation = 1,
            IndentChar = '\t'
        };
     

        var document = new XmlDocument();
        var xRoot = document.DocumentElement;
        writer.WriteStartDocument();
        //var r = "<? xml version = 1.0"?";
        //writer.WriteRaw();
        writer.WriteStartElement("ArrayOfSkill");
        foreach (var skill in SkillsList)
            skill.Serialize(ref writer);
        writer.WriteEndElement();
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

            reader.ReadToFollowing("Dirs");
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

    public int Row;
    public int Col;
};

public enum SkillType
{
    Aura,
    DeathRattle,
    BattleCry
}

