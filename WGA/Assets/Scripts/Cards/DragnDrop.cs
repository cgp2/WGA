using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragnDrop : MonoBehaviour {
    public bool dragnow;
    public Vector3 defpos;
    // Use this for initialization
    void Start () {
        dragnow = false;
        defpos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(dragnow)
        {
            var worldpos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(worldpos.x, worldpos.y, this.transform.position.z);

        }
	}
    private void OnMouseDown()
    {
        dragnow = true;
        
        var testscript = this.GetComponent<test>();
        defpos = testscript.standartpos;
        if (testscript.needtorescaleplus || testscript.IsClose(this.transform.localScale.x,testscript.standartscalex) || testscript.IsClose(transform.localScale.y,testscript.standartscaley))
        {
            this.GetComponent<test>().needtorescaleplus = false;
            this.GetComponent<test>().needtorescaleminus = true;
        }

    }
    private void OnMouseUp()
    {
        dragnow = false;
        if(!DropCard(Input.mousePosition))
            this.transform.position = defpos;

    }
    private bool DropCard(Vector3 mousePosition)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if(Physics.Raycast(ray,out hit))
        {
            Transform objectHit = hit.transform;
            var xy = objectHit.name.Split(',');
            if (Battle.Get_Card(int.Parse(xy[1]), int.Parse(xy[2])) != null)
                return false ;
            Battle.Set_Card(int.Parse(xy[1]), int.Parse(xy[2]), Player.Selectedcard.GetComponent<Card>());
            return true;
        }
        return false;
    }
}
