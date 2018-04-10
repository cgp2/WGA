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
	    if (!GetComponentInParent<Card>().OnBoard)
	    {
	        if (dragnow)
	        {
	            var worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	            this.transform.position = new Vector3(worldpos.x, worldpos.y, this.transform.position.z);

	        }
	    }
	}
    private void OnMouseDown()
    {
            dragnow = true;

        if (!GetComponentInParent<Card>().OnBoard)
        {
            var testscript = this.GetComponent<test>();
            defpos = testscript.defaultPosition;
            if (testscript.needToRescalePlus || testscript.IsClose(this.transform.localScale.x, testscript.defaultXScale) || testscript.IsClose(transform.localScale.y, testscript.defaultYScale))
            {
                this.GetComponent<test>().needToRescalePlus = false;
                this.GetComponent<test>().needToRescaleMinus = true;
            }
        }
    }
    private void OnMouseUp()
    {

        dragnow = false;
        if (!GetComponentInParent<Card>().OnBoard)
        {
            if (!DropCard(Input.mousePosition))
                this.transform.position = defpos;
        }

    }
    private  bool DropCard(Vector3 mousePosition)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if(Physics.Raycast(ray,out hit))
        {
            Transform objectHit = hit.transform;
            var xy = objectHit.name.Split(',');
            if (Battle.Get_Card(int.Parse(xy[1]), int.Parse(xy[2])) != null)
                return false ;
            if (GetComponentInParent<Card>().OnBoard)
                return false;
            
                Battle.Set_Card(int.Parse(xy[1]), int.Parse(xy[2]), Player.Selectedcard.GetComponent<Card>());
            return true;
        }
        return false;
    }
}
