using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Place_Creation : MonoBehaviour {
    public int m=3;
    public int n = 4;
    public float xmargin = 10;
    public float ymargin = 10;
    public GameObject field;
    public GameObject card_place;
    public static GameObject[,] card_places;
    private static int i_pos;
    private static int j_pos;

	// Use this for initialization
	void Start () {
        card_places = new GameObject[n, m];
        float diffx = 0;
        float diffy = 0;
        var collider = field.GetComponent<BoxCollider2D>();
        diffx = (collider.bounds.size.x-2*xmargin)/m;
        diffy = (collider.bounds.size.y-2*ymargin)/(n);
        //Debug.LogWarning("diffx=" + diffx);
        //Debug.LogWarning("diffy = " + diffy);
        //Debug.LogWarning("size = " + collider.bounds.size);
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                
                i_pos = i;
                j_pos = j;
                card_places[i, j] = Instantiate(card_place);
                card_places[i,j].name="Card,"+j+","+i;
                card_places[i, j].transform.parent = gameObject.transform;
                card_places[i, j].transform.localScale = new Vector3(0.5f, 1.5f, 1);
                card_places[i, j].transform.position = new Vector3(card_places[i, j].transform.position.x-collider.bounds.size.x/2+(i+1)*diffx+xmargin, card_places[i,j].transform.position.y-collider.bounds.size.y/2+(j+0.3f)*diffy+ymargin, -card_places[i, j].transform.position.z);
                Battle.coor[j,i] = card_places[i, j];

            }
	}
    public static Vector2 Get_Position_In_table()
    {
        return new Vector2(i_pos, j_pos);
    }
	// Update is called once per frame
	void Update () {

	}
}
