using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        if (gameObject.GetComponent<Card>().Owner == Battle.turn)
        {
            Player.Selectedcard = gameObject;
        }
    }
}
