using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragnDrop : MonoBehaviour {
    public bool dragnow;
    private Vector3 defpos;
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
        defpos = this.transform.position;
        if(this.GetComponent<test>().needtorescaleplus)
        {
            this.GetComponent<test>().needtorescaleplus = false;
            this.GetComponent<test>().needtorescaleminus = true;
        }

    }
    private void OnMouseUp()
    {
        dragnow = false;
        this.transform.position = defpos;
    }
}
