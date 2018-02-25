using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int Health;
    public int Shield;
    public int Attack;
  
    public string Name;
    public string Description;

    public string BattleCryName;
    public string DeathRattleName;
    public string AuraName;

    public Card(string name)
    {
        Name = name;
    }
	// Use this for initialization
	void Start ()
    {
        
		//well done 
        //thx
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
