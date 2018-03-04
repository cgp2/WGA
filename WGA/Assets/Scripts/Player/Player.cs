using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {
    public int[] Hand;
    private  static List<GameObject> deck;
    public GameObject crd;

    public static GameObject Selectedcard;
    // Use this for initialization
	void Start () {
        deck = new List<GameObject>();
        for (int i = 0; i < Hand.Length; i++)
        {
            var temp = Instantiate(crd);
            deck.Add(temp);
            deck[i].transform.parent = this.transform;
            deck[i].transform.position = new Vector3(this.transform.position.x + 20 * i,deck[i].transform.position.y,-30);
        }
            
	}

    // Update is called once per frame
    void Update () {
       
            
	}
    public static void Delete_Card_From_Deck(GameObject obj)
    {
        Destroy(obj.gameObject);
        deck.Remove(obj);
    }
}
