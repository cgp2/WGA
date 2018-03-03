using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Sprite CardBack;
    public Sprite CardFront;

    public void Initialize(Text name, int health, int shield, int attack, Text description, Sprite cardFront, string battleCryName = null, string deathRattleName = null, string auraName = null)
    {
        Name = name;
        Health = health;
        Shield = shield;
        Attack = attack;

        BattleCryName = battleCryName;
        DeathRattleName = deathRattleName;
        AuraName = auraName;

        CardFront = cardFront;
    }

    private void Awake()
    {
        SpriteRenderer shipSprite = GetComponent<SpriteRenderer>();
        shipSprite.sprite = CardFront;
        shipSprite.size = new Vector2(2f, 3f);
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
