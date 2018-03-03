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
    public string description;
    public ISkillsInput Input;
    public SkillType Type;
    public bool Ally;

    public abstract bool ExecuteSkill(ISkillsInput input);

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
            foreach (string param in Input.InputParamsNames)
            {
                writer.WriteStartElement(param);
                writer.WriteAttributeString("value", " ");
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Directions");
            foreach (Directions dir in Input.Directions)
            {
                writer.WriteStartElement("dir");
                writer.WriteAttributeString("type", dir.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Description");
            writer.WriteRaw(description);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }
}

