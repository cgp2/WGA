using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Card : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        if(Player.Selectedcard!=null)
        {
           var targetcard= Instantiate(Player.Selectedcard);
            targetcard.transform.position = this.transform.position;
            var xy = this.name.Split(',');
            
            Debug.LogWarning(Player.Selectedcard.GetComponent<Card>());

            Battle.Set_Card(int.Parse(xy[1]), int.Parse(xy[2]), Player.Selectedcard.GetComponent<Card>());
            Player.Delete_Card_From_Deck(Player.Selectedcard);
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 3; j++)
                    Debug.LogWarning("i="+i+", j="+j+Battle.Get_Card(i, j));
            
            Player.Selectedcard = null;
        }
    }
}
