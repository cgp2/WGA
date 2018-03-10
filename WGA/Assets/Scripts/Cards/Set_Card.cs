using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Card : MonoBehaviour {
    private Player pl1;
    private Player pl2;

	// Use this for initialization
	void Start () {
        pl1 = GameObject.Find("Player1").GetComponent<Player>();
        pl2 = GameObject.Find("Player2").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        if(Player.Selectedcard!=null)
        {
            var xy = this.name.Split(',');
            if (Battle.Get_Card(int.Parse(xy[1]), int.Parse(xy[2])) != null)
                return;
            var targetcard= Player.Selectedcard;
            targetcard.transform.parent = this.transform.parent;
            targetcard.transform.position = this.transform.position;
            targetcard.GetComponent<BoxCollider2D>().enabled = false;
            
            //Debug.LogWarning(Player.Selectedcard.GetComponent<Card>());

            Battle.Set_Card(int.Parse(xy[1]), int.Parse(xy[2]), Player.Selectedcard.GetComponent<Card>());
            if (Battle.pl1 == Battle.turn)
                pl1.deck.Remove(Player.Selectedcard);
            else
                pl2.deck.Remove(Player.Selectedcard);
            //Player.Delete_Card_From_Deck(Player.Selectedcard);
            //for (int i = 0; i < 4; i++)
               // for (int j = 0; j < 3; j++)
                  //  Debug.LogWarning("i="+i+", j="+j+Battle.Get_Card(i, j));
            Player.Selectedcard.GetComponent<Card>().OnBoard = true;
            
            Battle.NextTurn();
            Battle.RollTheCards();
            Debug.LogWarning(Battle.turn);
            Player.Selectedcard = null;          
        }
    }
}
