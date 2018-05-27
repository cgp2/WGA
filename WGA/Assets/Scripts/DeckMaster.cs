using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

public class DeckMaster{
    public static Card.CardData[] AllCards;
	// Use this for initialization
	void Start () {
        

    }

    public static void SaveDeckToFile(Card.CardData[] deck, string filePath)
    {
        var fileStream = File.Create(filePath);
        var writer = new XmlTextWriter(fileStream, Encoding.GetEncoding(1251))
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
        writer.WriteStartElement("ArrayOfCardInfo");
        foreach (var card in deck)
            card.Serialize(ref writer);
        writer.WriteEndElement();
        writer.WriteEndDocument();

        writer.Close();
        fileStream.Close();
    }

    public static Card.CardData[] GetCardsOfRace(string raceName)
    {
        var ret = new List<Card.CardData>();

        if (AllCards != null)
        {
            ret.AddRange(AllCards.Where(card => card.Race.ToLower() == raceName.ToLower()));
        }

        return ret.ToArray();
    }

    public Card.CardData GetCard(string cardName)
    {
        return AllCards.First(card => card.Name == cardName);
    }

	// Update is called once per frame
	void Update () {
		
	}
}

