using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CartBuilder
{
    [Serializable]
    public class Skill
    {
        public string Name;
        public string Description;
        public string Type;

        override public string ToString()
        {
            return Type + " " + Description;
        }
    }

    [Serializable]
    public class CardInfo
    {
        public enum Class
        {
            People, Insect, Mechanism
        }

        public Class CardClass;
        public string ImagePath;

        public int Health;
        public int Shield;
        public int Attack;

        public string Name;
        public string Description;

        public string[] BattleCryName;
        public string[] DeathRattleName;
        public string[] AuraName;

        public int valueBatterCry = 0;
        public int valueDeathRattle = 0;
        public int valueAura = 0;

        public Guid CardID;

        public CardInfo()
        {
            BattleCryName = new string[0];
            DeathRattleName = new string[0];
            AuraName = new string[0];
        }

        public void AddBattleCry(string newBattleCry)
        {
            var bc = BattleCryName.ToList();
            bc.Add(newBattleCry);
            BattleCryName = bc.ToArray();
        }

        public void AddDeathRattle(string newDeathRattle)
        {
            var bc = DeathRattleName.ToList();
            bc.Add(newDeathRattle);
            DeathRattleName = bc.ToArray();
        }

        public void AddAura(string newAura)
        {
            var bc = AuraName.ToList();
            bc.Add(newAura);
            AuraName = bc.ToArray();
        }

        public string GetBattleCry()
        {
            string res = "";
            if (BattleCryName.Count() == 0)
                return "";

            foreach (string str in BattleCryName)
                res += str + ", ";
            return res.Remove(res.Count() - 2); 
        }

        public string GetDeathRattle()
        {
            string res = "";
            if (DeathRattleName.Count() == 0)
                return "";

            foreach (string str in DeathRattleName)
                res += str + ", ";
            return res.Remove(res.Count() - 2);
        }

        public string GetAura()
        {
            string res = "";
            if (AuraName.Count() == 0)
                return "";

            foreach (string str in AuraName)
                res += str + ", ";
            return res.Remove(res.Count() - 2);
        }

        public string GetClassString()
        {
            switch (CardClass)
            {
                case Class.People:
                    return "People";
                case Class.Insect:
                    return "Insect";
                default:
                    return "Mechanism";
            }
        }

        override public string ToString()
        {
            return Name;
        }
    }
}
