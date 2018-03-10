using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml.Serialization;
using System.IO;

public class Card : MonoBehaviour
{
    public int Health;
    public int Shield;
    public int Attack;
  
    public Text Name;
    public Text Description;

    public string BattleCryName;
    public string DeathRattleName;
    public string AuraName;

    public Player Owner;

    public int staticHP;
    public int staticDMG;
    public int staticSHLD;

    public Sprite CardBack;
    public Sprite CardFront;

    public bool OnBoard;

    public void Initialize(Text cardName, int health, int shield, int attack, Text description, Sprite cardFront, string battleCryName = null, string deathRattleName = null, string auraName = null)
    {
        Name = cardName;
        Health = health;
        Shield = shield;
        Attack = attack;

        BattleCryName = battleCryName;
        DeathRattleName = deathRattleName;
        AuraName = auraName;

        CardFront = cardFront;
        OnBoard = false;
    }
    
    private void Awake()
    {
        SpriteRenderer shipSprite = GetComponent<SpriteRenderer>();
        shipSprite.sprite = CardBack;
        shipSprite.size = new Vector2(2f, 3f);
    }
    public void Spin(bool front)
    {
        SpriteRenderer shipSprite = GetComponent<SpriteRenderer>();
        shipSprite.sprite = front ? CardFront : CardBack;
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
