using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public abstract class ASkill
{
    public string Name;
    public string Description;
    public SkillsInput Input;
    public SkillType Type;
    public bool Ally;

    public string[] InputParametrs;
    public Directions[] Dirs;
    public string[] InputValues;

    public abstract bool ExecuteSkill(SkillsInput input, int row, int col, int playerID, ref SlotBuff[,] bufMap);
    public abstract bool ReExecuteSkill(SkillsInput input, int row, int col, int playerID, ref SlotBuff[,] bufMap);

    //public void Serialize(ref XmlDocument document)
    //{
    //    XmlElement skillElement = document.CreateElement("Skill");
    //    skillElement.SetAttribute("type", Type.ToString());
    //    skillElement.SetAttribute("name", Name);

    //    XmlElement inputElem = document.CreateElement("Input");
    //    foreach(string param in Input.InputParamsNames)
    //    {
    //        XmlElement el = document.CreateElement(param);
    //        el.SetAttribute("value", " ");
    //        inputElem.AppendChild(el);
    //    }

    //    XmlElement dirRootElement = document.CreateElement("Dirs");
    //    foreach (Dirs dir in Input.Dirs)
    //    {
    //        XmlElement dirElement = document.CreateElement("dir");
    //        XmlText txt = document.CreateTextNode(dir.ToString());
    //        dirElement.AppendChild(txt);
    //        dirRootElement.AppendChild(dirElement);
    //    }

    //    skillElement.AppendChild(inputElem);
    //    skillElement.AppendChild(dirRootElement);
    //}

    public void Serialize(ref XmlTextWriter writer)
    {
        writer.WriteStartElement("Skill");
        {
            writer.WriteStartElement("Name");
            writer.WriteRaw(Name);
            writer.WriteEndElement();

            writer.WriteStartElement("Type");
            writer.WriteRaw(Type.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Ally");
            writer.WriteRaw(Ally.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Input");
            for (var i = 0; i < Input.InputParamsNames.Length; i++)
            {
                var param = Input.InputParamsNames[i];
                writer.WriteStartElement("inp");
                writer.WriteAttributeString("name", param);
                writer.WriteAttributeString("value", Input.InputParamsValues[i]);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteStartElement("Dirs");
            foreach (var dir in Input.Directions)
            {
                writer.WriteStartElement("dir");
                writer.WriteAttributeString("type", dir.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Description");
            writer.WriteRaw(Description);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    protected SlotBuff[] GetCardSlotsInDirections(ref SlotBuff[,] bufMap, Directions[] directionses, int playerID, int row, int col)
    {
        var slots = new List<SlotBuff>();
        foreach (var dir in directionses)
        {
            switch (dir)
            {
                case Directions.Bottom:
                    if (playerID == 0)
                    {
                        if (row - 1 >= 0)
                            slots.Add(bufMap[row - 1, col]);
                    }
                    else
                    {
                        if (row + 1 < bufMap.GetLength(0))
                            slots.Add(bufMap[row + 1, col]);
                    }
                    break;
                case Directions.Left:
                    if (playerID == 0)
                    {
                        if (col - 1 >= 0)
                            slots.Add(bufMap[row, col - 1]);
                    }
                    else
                    {
                        if (col + 1 < bufMap.GetLength(1))
                            slots.Add(bufMap[row, col + 1]);
                    }
                    break;
                case Directions.Top:
                    if (playerID == 0)
                    {
                        if (row + 1 < bufMap.GetLength(0))
                            slots.Add(bufMap[row + 1, col]);
                    }
                    else
                    {
                        if (row - 1 >= 0)
                            slots.Add(bufMap[row - 1, col]);
                    }
                    break;
                case Directions.Right:
                    if (playerID == 0)
                    {
                        if (col + 1 < bufMap.GetLength(1))
                            slots.Add(bufMap[row, col + 1]);
                    }
                    else
                    {
                        if (col - 1 >= 0)
                            slots.Add(bufMap[row, col - 1]);
                    }
                    break;
                case Directions.LeftTop:
                    if (playerID == 0)
                    {
                        if ((col - 1 >= 0) && (row + 1 < bufMap.GetLength(0)))
                            slots.Add(bufMap[row + 1, col - 1]);
                    }
                    else
                    {
                        if ((col + 1 < bufMap.GetLength(1)) && (row - 1 >= 0))
                            slots.Add(bufMap[row - 1, col + 1]);
                    }
                    break;
                case Directions.LeftBottom:
                    if (playerID == 0)
                    {
                        if ((col - 1 >= 0) && (row - 1 >= 0))
                            slots.Add(bufMap[row - 1, col - 1]);
                    }
                    else
                    {
                        if ((col + 1 < bufMap.GetLength(1)) && (row + 1 < bufMap.GetLength(0)))
                            slots.Add(bufMap[row + 1, col + 1]);
                    }

                    break;
                case Directions.RightTop:
                    if (playerID == 0)
                    {
                        if ((col + 1 < bufMap.GetLength(1)) && (row + 1 < bufMap.GetLength(0)))
                            slots.Add(bufMap[row + 1, col + 1]);
                    }
                    else
                    {
                        if ((col - 1 >= 0) && (row - 1 >= 0))
                            slots.Add(bufMap[row - 1, col - 1]);
                    }

                    break;
                case Directions.RightBottom:
                    if (playerID == 0)
                    {
                        if ((col + 1 < bufMap.GetLength(1)) && (row - 1 >= 0))
                            slots.Add(bufMap[row - 1, col + 1]);
                    }
                    else
                    {
                        if ((col - 1 >= 0) && (row + 1 < bufMap.GetLength(0)))
                            slots.Add(bufMap[row + 1, col - 1]);
                    }

                    break;
                case Directions.Map:
                    foreach (var slot in bufMap)
                    {
                        slots.Add(slot);
                    }
                    break;
                case Directions.Self:
                    slots.Add(bufMap[row, col]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return slots.ToArray();
    }

    protected void ApplyBufToBufMap(SlotBuff[] buffedSlots, ref SlotBuff[,] bufMap)
    {
        foreach (var slot in buffedSlots)
        {
            bufMap[slot.Row, slot.Col] = slot;
        }
       
    }
}

