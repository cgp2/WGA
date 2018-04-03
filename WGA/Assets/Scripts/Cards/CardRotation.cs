using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRotation : MonoBehaviour {
    Player owner;
	// Use this for initialization
	void Start () {
        owner = gameObject.transform.parent.GetComponent<Player>();
	}
    private int rotate = 0;
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < owner.deck.Count; i++)
        {
            if (owner.deck[i].GetComponent<Card>().front_rotate)
            {
                rotate++;
                owner.deck[i].transform.Rotate(new Vector3(0, -1, 0), 5f, Space.World);

                if (rotate == 30)
                {
                    rotate = 0;
                    owner.deck[i].transform.rotation = new Quaternion(0, 0, 0, 1);
                    owner.deck[i].GetComponent<Card>().front_rotate = false;
                   
                }
            }
            else if (owner.deck[i].GetComponent<Card>().back_rotate)
            {
                rotate++;
                owner.deck[i].transform.Rotate(new Vector3(0, 1, 0), 5f, Space.World);

                if (rotate == 30)
                {
                    rotate = 0;
                    owner.deck[i].transform.rotation = new Quaternion(0, 180, 0, 1);
                    owner.deck[i].GetComponent<Card>().back_rotate = false;
                   
                }
            }
        }
        
    }
}
