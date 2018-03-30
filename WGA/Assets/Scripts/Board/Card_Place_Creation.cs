using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Place_Creation : MonoBehaviour {
    int m=Battle.m;
    int n = Battle.n;
    public GameObject field;
    public GameObject card_place;
    public static GameObject[,] card_places;

	// Use this for initialization
	void Start () {
        int standartn=4, standartm=3;
        float xmarginstandart=20, ymarginstandart=15;
        Vector3 standartsize = new Vector3(0.5f, 1.5f, 1);
        card_places = new GameObject[n, m];
        var collider = field.GetComponent<BoxCollider2D>();
        Vector3 scale = new Vector3(standartsize.x * standartn / n, standartsize.y * standartm / m, 1);
        float xmargin = xmarginstandart * standartm / n;
        float ymargin = ymarginstandart * standartn / m;

        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
               
                card_places[i, j] = Instantiate(card_place);

                card_places[i,j].name="Cardplace,"+i+","+j;
                card_places[i, j].transform.parent = gameObject.transform;
                //card_places[i, j].transform.localScale = new Vector3(0.5f, 1.5f, 1);
                card_places[i, j].transform.localScale = scale;
                var t = card_places[i, j].GetComponent<MeshCollider>().bounds.size;
                card_places[i, j].transform.position = new Vector3(field.transform.position.x -collider.bounds.size.x/5 + j *(xmargin), field.transform.position.y -  collider.bounds.size.y/4 + i * ymargin, -20);
                Battle.coor[i,j] = card_places[i, j];

            }
	}

	// Update is called once per frame
	void Update () {

	}
}
