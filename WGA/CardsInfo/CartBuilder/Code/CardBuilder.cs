using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CartBuilder
{
    public class CardBuilder
    {
        const string FileForSaveCards = "AllCards.dat";

        public CardInfo[] CardArray { get; private set; }

        public CardBuilder()
        {
            if (!File.Exists(FileForSaveCards))
            {
                CardArray = new CardInfo[0];
                return;
            }

            using (FileStream fs = new FileStream(FileForSaveCards, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(CardInfo[]));
                CardArray = (CardInfo[])formatter.Deserialize(fs);
            }
        }

        public void Save(CardInfo[] newCardArr)
        {
            CardArray = newCardArr;
            if (File.Exists(FileForSaveCards))
                File.Delete(FileForSaveCards);
            using (FileStream fs = new FileStream(FileForSaveCards, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(CardInfo[]));
                formatter.Serialize(fs, CardArray);
            }
        }

        public void Save(ListBox.ObjectCollection items)
        {
            List<CardInfo> list = new List<CardInfo>();
            foreach (var it in items)
                list.Add((CardInfo)it);
            Save(list.ToArray());
        }
    }
}
