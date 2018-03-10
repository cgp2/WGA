using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour {
    public int[] Hand;
    public List<GameObject> deck;
    public GameObject crd;

    public static GameObject Selectedcard;
    // Use this for initialization
    private void Awake()
    {
        deck = new List<GameObject>();
        for (int i = 0; i < Hand.Length; i++)
        {
            var temp = Instantiate(crd);

            deck.Add(temp);
            deck[i].transform.parent = transform;
            deck[i].transform.position = new Vector3(deck[i].transform.position.x + i * 15, -this.transform.position.y, -30);
              
            if(this.name=="Player2")
            {
                deck[i].GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            }

            deck[i].name = "card#" + i + "/player=" + this.name;
        }
    }
    void Start() {
        foreach (var cord in deck)
        {
            cord.GetComponent<Card>().Initialize("kek", 2, 3, 2, "lolkek? kekLol", crd.GetComponent<Card>().CardFront, GameObject.Find("Field").GetComponent<SkillMaster>(), new[] { "HPBufBC" });
            cord.GetComponent<Card>().Owner = this.GetComponent<Player>();
            var x = cord.transform.GetChild(0);
            x.GetChild(0).GetComponent<Text>().text = "" + cord.GetComponent<Card>().Health;
            x.GetChild(1).GetComponent<Text>().text = "" + cord.GetComponent<Card>().Shield;
            x.GetChild(2).GetComponent<Text>().text = "" + cord.GetComponent<Card>().Attack;
            x.GetChild(3).GetComponent<Text>().text = "" + cord.GetComponent<Card>().Name;
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
