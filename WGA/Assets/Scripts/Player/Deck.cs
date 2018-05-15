using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Race
{
    Human=1,
    Insect=2,
    Robot=3
}
public class Deck : MonoBehaviour
{
    public string DeckName;
    public List<Card> cardsInDeck;
    public Race race;
    public int deckID;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReadFromFile()
    {
        var temp = Card.Deserialize("CardsInfo/DeckForGame.dat");
        cardsInDeck = new List<Card>();
        foreach (Card.CardData c in temp)
        {
            var newCard = new Card();
            newCard.Initialize(c);
            cardsInDeck.Add(newCard);
        }
    }
    public Card.CardData[] ToCardData()
    {
        return Card.Deserialize("CardsInfo/DeckForGame.dat");
        //Card.CardData[] ret = new Card.CardData[cardsInDeck.Count];
        //for (int i = 0; i < cardsInDeck.Count; i++)
        //{
            
        //    var crd = new Card.CardData
        //    {
        //        Name = name,
        //        Race = cardsInDeck[i].Info.Race,
        //        HP = cardsInDeck[i].Info.InitialHealth,
        //        Shield = cardsInDeck[i].Info.InitialShield,
        //        Attack = cardsInDeck[i].Info.InitialAttack,
        //        Desk = cardsInDeck[i].Info.Description,
        //        SkillM = GameObject.Find("Field").GetComponent<SkillMaster>(),
        //        BattleCryNames=cardsInDeck[i].Info.BattleCryNames,
        //        BattleCryInputValue=IcardsInDeck[i].Info.BattleCryValue,


        //        HP = int.Parse(health),
        //        Shield = int.Parse(shield),
        //        Attack = int.Parse(attack),
        //        Desk = desk,
        //        SkillM = GameObject.Find("Field").GetComponent<SkillMaster>(),
        //        BattleCryNames = battleCry.ToArray(),
        //        BattleCryInputValue = BCinp.ToArray(),
        //        DeathRattleNames = deathRattle.ToArray(),
        //        DeathRattleInputValue = DRinp.ToArray(),
        //        AurasNames = aura.ToArray(),
        //        AuraInputValue = Aurainp.ToArray(),
        //        ActiveInputValue = 5,
        //        ActiveSkillName = "DmgToCard",
        //    };
        //}
    }
}
