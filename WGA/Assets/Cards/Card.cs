using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour
{
    public int Health;
    public int Shield;
    public int Attack;
  
    public Text Name;
    public Text Description;

    public string BattleCryName;
    public string DeathRattleName;
    public string AuraName;

	// Use this for initialization
	void Start ()
    {
		//well done
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
