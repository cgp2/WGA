using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveLeft()
    {
        Battle.Move(Directions.Left);
    }

    public void MoveRight()
    {
        Battle.Move(Directions.Right);
    }

    public void MoveTop()
    {
        Battle.Move(Directions.Top);
    }

    public void MoveBottom()
    {
        Battle.Move(Directions.Bottom);
    }
}
