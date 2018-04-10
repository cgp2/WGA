using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Place_Creation : MonoBehaviour {
    int m = Battle.m;
    int n = Battle.n;
    public GameObject field;
    public GameObject card_place;
    public static GameObject[,] card_places;
    public static Vector3 t;
    public const float xMarginStandart = 5, yMarginStandart = 5;

    // Use this for initialization
    void Start () {

        int standartN=4, standartm=4;       
        Vector3 standartsize = new Vector3(0.5f, 1.5f, 1);
        card_places = new GameObject[n, m];
        var collider = field.GetComponent<BoxCollider2D>();
        Vector3 scale = new Vector3(standartsize.x * standartN / n, standartsize.y * standartm / m, 1);
        float xmargin = xMarginStandart * standartm / n;
        float ymargin = yMarginStandart * standartN / m;

        int k = -1 * n/2;
        for (int i = 0; i < n; i++)
        {
            var l = -1 * m/2;
            for (int j = 0; j < m; j++)
            {
                card_places[i, j] = Instantiate(card_place);

                card_places[i, j].name = "Cardplace," + i + "," + j;
                card_places[i, j].transform.parent = gameObject.transform;
                card_places[i, j].transform.localScale = new Vector3(10f, 15f, 1);
                //card_places[i, j].transform.localScale = scale;

                t = card_places[i, j].GetComponent<MeshCollider>().bounds.size;
                var xOffset = field.transform.position.x + l * (xMarginStandart +  t.x) + t.x;
                var yOffset = field.transform.position.y + k * (yMarginStandart + t.y) + t.y;
               
                card_places[i, j].transform.position = new Vector3(xOffset, yOffset, -21);

                Battle.coor[i, j] = card_places[i, j];
                l++;
            }
            k++;
        }
	}

	// Update is called once per frame
	void Update () {

	}
}
