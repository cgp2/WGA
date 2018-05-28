using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour {
    public int[] Hand;
    public List<GameObject> deck;
    public GameObject crd;
    public Sprite[] sprites;
    public static GameObject Selectedcard;
    public Sprite frontSprite;
    public Sprite backSprite;
    public bool AI;
    public PlayerInfo PlInfo=null;
    // Use this for initialization
    private void Awake()
    {
        //var cards = Card.Deserialize(Path.GetDirectoryName(Application.dataPath) + "/CardsInfo/Cards.dat");
        deck = new List<GameObject>();
       /* for (int i = 0; i < cards.Length; i++)
        {
            var temp = Instantiate(crd);
            
            deck.Add(temp);
            deck[i].transform.parent = transform;
            deck[i].transform.position = new Vector3(deck[i].transform.position.x + i * 15, -this.transform.position.y, -30);
            deck[i].GetComponent<Card>().Info = cards[i].Info;
            deck[i].GetComponent<Card>().Owner = this;

            if (this.name == "Player2")
            {
                deck[i].transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                deck[i].transform.GetChild(3).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                deck[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            }
            deck[i].name = "card#" + i + "/player=" + this.name;
        }*/
    }

    void Start()
    {
        Card.CardData[] cards;
        if (PlInfo.PathToAvatar != "")
        {
            GameObject.Find("Player1Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(PlInfo.PathToAvatar);
            cards = Card.Deserialize(Application.dataPath + "/Resources/CardsInfo/Decks/" + PlInfo.DeckName + ".dat");
        }
        else
        {
            PlInfo = new PlayerInfo();
            cards = new Card.CardData[0];

            switch (Random.Range(0, 3))
            {
                case 0:
                    cards = Card.Deserialize(Application.dataPath + "/Resources/CardsInfo/Decks/InsectBot.dat");
                    PlInfo.Name = "Kerrigan";
                    GameObject.Find("Player2Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Leaders/insect");
                    GameObject.Find("Player2").GetComponent<AIEnemy>().InitializeParametrs(100f, 40f, 1f,1f, 0.1f, 40f);
                    break;
                case 1:
                    cards = Card.Deserialize(Application.dataPath + "/Resources/CardsInfo/Decks/PeopleBot.dat");
                    PlInfo.Name = "Jaina";
                    GameObject.Find("Player2Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Leaders/people");
                    GameObject.Find("Player2").GetComponent<AIEnemy>().InitializeParametrs(50f, 10f, 30f, 30f, 0.5f, 20f);
                    break;
                case 2:
                    cards = Card.Deserialize(Application.dataPath + "/Resources/CardsInfo/Decks/MechanismBot.dat");
                    PlInfo.Name = "R2-D2";
                    GameObject.Find("Player2Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Leaders/Mechanism");
                    GameObject.Find("Player2").GetComponent<AIEnemy>().InitializeParametrs(30f, 60f, 0f, 30f, 0.01f, 70f);
                    break;
            }      
        }
        var k = 0;
        for (int i =0; i < cards.Length;i++)
        {
            if (i % 2 != 0)
                k++;

            var temp = Instantiate(crd);
            deck.Add(temp);

            deck[i].transform.parent = transform;
            //deck[i].transform.position = new Vector3(deck[i].transform.position.x + i * 12, this.transform.position.y, this.transform.position.z);
            if (this.name == "Player1")
            {
                deck[i].transform.position = new Vector3(93, -37.1f, -30);
                //deck[i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, Directions.Left, -transform.position.x - Mathf.Pow(-1, i) * 15 * k + deck[i].transform.position.x,0,true);
                
                
            }
            else
                deck[i].transform.position = new Vector3(transform.position.x + Mathf.Pow(-1, i) * 6 * k , transform.position.y, -30);
            deck[i].GetComponent<Card>().Initialize(cards[i]);
            deck[i].GetComponent<Card>().Owner = this;

            //if (this.name == "Player2")
            //{
            //    deck[i].transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            //    deck[i].transform.GetChild(3).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            //    deck[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            //}
            deck[i].name = "card#" + i + "/player=" + this.name;

            var cord = deck[i];
            //cord.GetComponent<Card>().Initialize("kek"+i, 2, 2, 2, "lolkek? kekLol", GameObject.Find("Field").GetComponent<SkillMaster>(), new[] { "HPBufBC" }, new [] { "SHLDDebufDR" }, new []{"DMGBufAura"});
            cord.GetComponent<Card>().InitializeSprites(frontSprite, backSprite, cord.GetComponent<Card>().Info.ShipSprite);
            cord.GetComponent<Card>().Owner = this.GetComponent<Player>();
            deck[i].transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = frontSprite;
            deck[i].transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = backSprite;
            if (this.name == "Player1")
            {
                
                //deck[i].GetComponent<Animator>().Play("Zalupon",0);
            }
            var x = cord.transform.GetChild(0);
            x.GetChild(0).GetComponent<Text>().text = "" + cord.GetComponent<Card>().Health;
            x.GetChild(1).GetComponent<Text>().text = "" + cord.GetComponent<Card>().Shield;
            x.GetChild(2).GetComponent<Text>().text = "" + cord.GetComponent<Card>().Attack;
            x.GetChild(3).GetComponent<Text>().text = "" + cord.GetComponent<Card>().Info.Name;
            deck[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = deck[i].GetComponent<Card>().Info.ShipSprite;

           
            
        }
        for(int i=0;i<deck.Count;i++)
        {
            float xFirst = 0;
            if (deck.Count % 2 == 0)
            {
                xFirst -= 9 * deck.Count / 2;
            }
            else
            {
                xFirst = -5 - (9 * deck.Count - 1) / 2;
            }

            //deck[i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, Directions.Left, -transform.position.x - Mathf.Pow(-1, i) * 15 * k + deck[i].transform.position.x, 0);
            deck[i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, Directions.Left, deck[i].transform.position.x - transform.position.x + xFirst + i * 10f, 0, true);
        }
        Battle.UpdateUI();
        //Battle.NextTurn();
        //for (int i = 0; i < Hand.Length; i++)
        //    if (own != Battle.turn)
        //        deck[i].GetComponent<Card>().Spin(true);
        //    else
        //        deck[i].GetComponent<Card>().Spin(false);
       // Battle.RollTheCards();
	}
    public void UpdatePosition()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            float xFirst = 0;
            if (deck.Count % 2 == 0)
            {
                xFirst -= 9 * deck.Count / 2;
            }
            else
            {
                xFirst = -5 - (9 * deck.Count - 1) / 2;
            }

            //deck[i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, Directions.Left, -transform.position.x - Mathf.Pow(-1, i) * 15 * k + deck[i].transform.position.x, 0);
            deck[i].GetComponent<MovementAnimation>().Add_Action(MovementAnimation.Acts.move, Directions.Left, deck[i].transform.position.x - transform.position.x + xFirst + i * 10f, 0, false);
        }
    }
    // Update is called once per frame
    void Update () {     
	}
    public void Delete_Card_From_Deck(GameObject obj)
    {
        //Destroy(obj.gameObject);
        deck.Remove(obj);
    }
}
