﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragnDrop : MonoBehaviour {
    public bool dragnow;
    public Vector3 defpos;
    public Texture2D cursor;
    // Use this for initialization
    void Start () {
        dragnow = false;
        //defpos = this.transform.position;
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
        if (!this.GetComponent<Card>().OnBoard && !Battle.cardSeted)
        {
            dragnow = true;
            defpos = this.transform.position;
            if (gameObject.GetComponent<Card>().Owner == Battle.turn)
                Player.Selectedcard = gameObject;
            if (!GetComponentInParent<Card>().OnBoard)
            {
                var testscript = this.GetComponent<test>();
                //defpos = testscript.defaultPosition;

                if (testscript.needToRescalePlus || testscript.IsClose(this.transform.localScale.x, testscript.defaultXScale) || testscript.IsClose(transform.localScale.y, testscript.defaultYScale))
                {
                    this.GetComponent<test>().needToRescalePlus = false;
                    this.GetComponent<test>().needToRescaleMinus = true;
                }
            }
        }
        else
        {
            var temp = this.GetComponent<Card>();
            if (temp.IsActiveSkillAvaliable && !Battle.skillUsed)
            {
                var skillList = temp.GetSkillsList();
                for (int i = 0; i < skillList.Count; i++)
                    if (skillList[i].Type == SkillType.Active)
                        if (skillList[i].Dirs[0] == Directions.Target)
                            Cursor.SetCursor(cursor,new Vector2(32,32),CursorMode.ForceSoftware);
                if (this.GetComponent<Card>().ExecuteActiveSkill(ref Battle.Board, ref GameObject.Find("Field").GetComponent<SkillMaster>().BufMap))
                {
                    Battle.skillUsed = true;
                    Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
                }
                
            }
            else
            {

                //говорим о том, що незя
            }
        }
    }
    private void OnMouseUp()
    {

        dragnow = false;
       
        if (!GetComponentInParent<Card>().OnBoard && !Battle.cardSeted)
        {
            
            if (!DropCard(Input.mousePosition))
            {
                this.transform.position = defpos;
                Player.Selectedcard = null;
            }

        }

    }
    private  bool DropCard(Vector3 mousePosition)
    {
        if (!Battle.cardSeted)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                var xy = objectHit.name.Split(',');
                if (Battle.Get_Card(int.Parse(xy[1]), int.Parse(xy[2])) != null)
                    return false;
                if (GetComponentInParent<Card>().OnBoard)
                    return false;
                if (Player.Selectedcard != null)
                    Battle.Set_Card(int.Parse(xy[1]), int.Parse(xy[2]), Player.Selectedcard.GetComponent<Card>());
                return true;
            }
        }
        return false;
    }
}
