using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using UnityEngine;


class SkillMaster : MonoBehaviour
{
    public List<ASkill> SkillsList;

    private void Awake()
    {
        SkillsList = new List<ASkill>();
        Assets.Scripts.Skills.HPBufBC hpBufBC = new Assets.Scripts.Skills.HPBufBC();
        SkillsList.Add(hpBufBC);

        SerializeSkills();
    }

    private void Start()
    {
        
    }

    public void SerializeSkills()
    {
        XmlTextWriter writer = new XmlTextWriter(Path.GetDirectoryName(Application.dataPath) + "/CardsInfo/Skills.xml", Encoding.GetEncoding(1251));
        writer.Formatting = Formatting.Indented;
        writer.Indentation = 1;
        writer.IndentChar = '\t';
        //writer.WriteStartDocument();

        XmlDocument document = new XmlDocument();
        XmlElement xRoot = document.DocumentElement;
        foreach (ASkill skill in SkillsList)
            skill.Serialize(ref writer);
   
        writer.Close();
    }
}

public enum SkillType
{
    Aura,
    DeathRattle,
    BattleCry
}

