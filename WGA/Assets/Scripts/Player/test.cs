using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {
    public bool needtorescaleplus, needtorescaleminus;
    float standartscalex, standartscaley,standartcoorz;
    float deltaxscale, deltayscale;
    
    // Use this for initialization
    void Start () {
        standartscalex = transform.localScale.x;
        standartscaley = transform.localScale.y;
        standartcoorz = transform.position.z;
        deltaxscale = (20.942762f - standartscalex) / 20;
        deltayscale = (20.947458f - standartscaley) / 20;
    }
	
	// Update is called once per frame
	void Update () {
        if (needtorescaleplus)
        {
            transform.localScale = new Vector3(transform.localScale.x + deltaxscale, transform.localScale.y + deltayscale, transform.localScale.z);
        }
        else if(needtorescaleminus)
        {
            transform.localScale = new Vector3(transform.localScale.x - deltaxscale, transform.localScale.y - deltayscale, transform.localScale.z);
        }
        if (IsClose(transform.localScale.x, standartscalex) || IsClose(transform.localScale.y,standartscaley))
        {
            needtorescaleminus = false;
        }
        if (IsClose(transform.localScale.x, 20.942762f) || IsClose(transform.localScale.y,20.947458f))
            needtorescaleplus = false;
	}
    public void SetFalse()
    {
        needtorescaleminus = false;
        needtorescaleplus = false;
    }
    private void OnMouseDown()
    {
        if (gameObject.GetComponent<Card>().Owner == Battle.turn)
        {
            Player.Selectedcard = gameObject;
        }
    }
    private void OnMouseEnter()
    {
        if(gameObject.GetComponent<Card>().Owner == Battle.turn)
        {
            this.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = gameObject.GetComponent<Card>().Info.Description;
            //this.transform.localScale = new Vector3(10.942762f, 10.947458f, 1);
            needtorescaleplus = true;
            needtorescaleminus = false;
        }
    }
    public void OnMouseExit()
    {
        this.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = "";
        if (!this.GetComponent<Card>().OnBoard)
        {
            if (gameObject.GetComponent<Card>().Owner == Battle.turn)
            {
                needtorescaleplus = false;
                needtorescaleminus = true;
            }
           // this.transform.localScale = new Vector3(4.942762f, 4.947458f, 1);
        }
    }
    private bool IsClose(float actual, float ideal)
    {
        if (Mathf.Abs(actual - ideal) < 0.5f)
            return true;
        else
            return false;
    }
}
