using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    private Player possesedPlayer;

    private void Awake()
    {
        possesedPlayer = GetComponentInParent<Player>();
    }

    public void MakeMove()
    {

    }

    private void CalculateUtility()
    {

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
