using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Place_Creation : MonoBehaviour {
    public int n;
    public int m;
    public GameObject Field;
    public GameObject Card_Place;
    private 

	// Use this for initialization
	void Start () {
        //Good work braaaaaaaaah
        var x = Field.GetComponent<Collider2D>();
        //Field.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        var y = Field.GetComponent<BoxCollider2D>().size;

        Debug.LogWarning(x.bounds.size);
        Debug.LogWarning(y);
        var pos = Field.GetComponent<Collider2D>().transform;
        var _as = gameObject.GetComponent<Battle>();
        _as.m = 2;
        int n = 10;

	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.LogWarning("123");
            var pos = Field.GetComponent<BoxCollider2D>();
            Debug.LogWarning(pos);
            var x = Instantiate(Card_Place);
            var y = Instantiate(Card_Place);
            Debug.LogWarning(Card_Place);
            //Card_Place.transform.SetParent(Field.transform);
            x.transform.position = pos.transform.position+pos.transform.localScale/2;
            x.transform.position = new Vector3(x.transform.position.x, x.transform.position.y, -10);
            x.transform.parent = gameObject.transform;
            x.transform.localScale = new Vector3(0.5f, 1.5f, 1);


            
        }
	}
}
