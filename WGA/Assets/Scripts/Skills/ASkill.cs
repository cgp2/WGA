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
    public ISkillsInput Input;
    public SkillType Type;
    public bool Ally;

    public abstract bool ExecuteSkill(ISkillsInput input, int row, int col, int playerID, ref SlotBuff[,] bufMap);
    public abstract bool ReExecuteSkill(ISkillsInput input, int row, int col, int playerID, ref SlotBuff[,] bufMap);

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

    //    XmlElement dirRootElement = document.CreateElement("Directions");
    //    foreach (Directions dir in Input.Directions)
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
            writer.WriteAttributeString("name", Name);
            writer.WriteAttributeString("type", Type.ToString());
            writer.WriteAttributeString("Ally", Ally.ToString());

            writer.WriteStartElement("Input");
            foreach (var param in Input.InputParamsNames)
            {
                writer.WriteStartElement("inp");
                writer.WriteAttributeString("name", param);
                writer.WriteAttributeString("value", " ");
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Directions");
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

    protected SlotBuff[] GetCardSlotsInDirections(SlotBuff[,] bufMap, Directions[] directionses, int row, int col)
    {
        var slots = new List<SlotBuff>();
        foreach (var dir in directionses)
        {
            switch (dir)
            {
                case Directions.Bottom:
                    if(row+1 < bufMap.GetLength(0))
                        slots.Add(bufMap[row+1, col]);
                    break;
                case Directions.Left:
                    if (col - 1 >= 0)
                        slots.Add(bufMap[row, col-1]);
                    break;
                case Directions.Top:
                    if (row - 1 >= 0)
                        slots.Add(bufMap[row-1 , col]);
                    break;
                case Directions.Right:
                    if (col + 1 < bufMap.GetLength(1))
                        slots.Add(bufMap[row, col+1]);
                    break;
                case Directions.LeftTop:
                    if ((col - 1 >= 0) && (row - 1 >= 0))
                        slots.Add(bufMap[row -1, col - 1]);
                    break;
                case Directions.LeftBottom:
                    if ((col - 1 >= 0) && (row + 1 < bufMap.GetLength(0)))
                        slots.Add(bufMap[row + 1, col - 1]);
                    break;
                case Directions.RightTop:
                    if ((col + 1 < bufMap.GetLength(1)) && (row - 1 >= 0))
                        slots.Add(bufMap[row - 1, col + 1]);
                    break;
                case Directions.RightBottom:
                    if ((col + 1 < bufMap.GetLength(1)) && (row + 1 < bufMap.GetLength(0)))
                        slots.Add(bufMap[row + 1, col + 1]);
                    break;
                case Directions.Map:
                    foreach (var slot in bufMap)
                    {
                        slots.Add(slot);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return slots.ToArray();
    }
}

