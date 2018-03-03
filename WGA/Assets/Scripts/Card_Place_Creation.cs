using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Place_Creation : MonoBehaviour {
    public int n;
    public int m;
    public GameObject Field;
    public GameObject Card_Place;


	// Use this for initialization
	void Start () {
        //Good work braaaaaaaaah
        var x = Field.GetComponent<Collider2D>();
        //Field.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        var y = Field.GetComponent<BoxCollider2D>().size;

        Debug.LogWarning(x.bounds.size);
        Debug.LogWarning(y);
        int n = 10;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
