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
    public Sprite fronSprite;
    public Sprite backSprite;

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
        var cards = Card.Deserialize(Path.GetDirectoryName(Application.dataPath) + "/CardsInfo/Cards.dat");
        for (int i =0;i<cards.Length;i++)
        {
            var temp = Instantiate(crd);
            deck.Add(temp);
            deck[i].transform.parent = transform;
            deck[i].transform.position = new Vector3(deck[i].transform.position.x + i * 15, -this.transform.position.y, -30);
            deck[i].GetComponent<Card>().Initialize(cards[i]);
            deck[i].GetComponent<Card>().Owner = this;

            if (this.name == "Player2")
            {
                deck[i].transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                deck[i].transform.GetChild(3).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                deck[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            }
            deck[i].name = "card#" + i + "/player=" + this.name;

            var cord = deck[i];
            //cord.GetComponent<Card>().Initialize("kek"+i, 2, 2, 2, "lolkek? kekLol", GameObject.Find("Field").GetComponent<SkillMaster>(), new[] { "HPBufBC" }, new [] { "SHLDDebufDR" }, new []{"DMGBufAura"});
            cord.GetComponent<Card>().InitializeSprites(fronSprite, backSprite, sprites[0]);
            cord.GetComponent<Card>().Owner = this.GetComponent<Player>();
            deck[i].transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = fronSprite;
            deck[i].transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = backSprite;
            var x = cord.transform.GetChild(0);
            x.GetChild(0).GetComponent<Text>().text = "" + cord.GetComponent<Card>().Health;
            x.GetChild(1).GetComponent<Text>().text = "" + cord.GetComponent<Card>().Shield;
            x.GetChild(2).GetComponent<Text>().text = "" + cord.GetComponent<Card>().Attack;
            x.GetChild(3).GetComponent<Text>().text = "" + cord.GetComponent<Card>().Info.Name;
            deck[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprites[0];
  
        }
        Battle.UpdateUI();
        //for (int i = 0; i < Hand.Length; i++)
        //    if (own != Battle.turn)
        //        deck[i].GetComponent<Card>().Spin(true);
        //    else
        //        deck[i].GetComponent<Card>().Spin(false);
        Battle.RollTheCards();
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
