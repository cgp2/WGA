using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            deck[i].GetComponent<Card>().Owner = this.GetComponent<Player>();
            deck[i].name = "card#" + i + "/player=" + this.name;
        }
    }
    void Start() {
        var own = deck[0].GetComponent<Card>().Owner;
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
