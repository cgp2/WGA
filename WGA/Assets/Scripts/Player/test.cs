using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public bool needToRescalePlus, needToRescaleMinus;
    public float defaultXScale, defaultYScale, defaultZCoordinats;
    public Vector3 defaultPosition;
    float deltaXScale, deltaYScale, deltaYPosition;
    int counter;
    // Use this for initialization
    void Start()
    {
        counter = 0;
        defaultXScale = transform.localScale.x;
        defaultYScale = transform.localScale.y;
        defaultZCoordinats = transform.position.z;
        deltaXScale = (20.942762f - defaultXScale) / 20;
        deltaYScale = (20.947458f - defaultYScale) / 20;
        deltaYPosition = transform.position.y / Mathf.Abs(transform.position.y) * 25 / 20;
        defaultPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsClose(transform.localScale.x, defaultXScale) || IsClose(transform.localScale.y, defaultYScale))
        {
            needToRescaleMinus = false;
        }
        if (IsClose(transform.localScale.x, 20.942762f) || IsClose(transform.localScale.y, 20.947458f))
            needToRescalePlus = false;

        if (needToRescalePlus)
            counter++;
        if (counter >= 30)
        {
            if (needToRescalePlus)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - deltaYPosition, transform.position.z - 1);
                transform.localScale = new Vector3(transform.localScale.x + deltaXScale, transform.localScale.y + deltaYScale, transform.localScale.z);
            }
        }
        if (needToRescaleMinus)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + deltaYPosition, transform.position.z + 1);
            transform.localScale = new Vector3(transform.localScale.x - deltaXScale, transform.localScale.y - deltaYScale, transform.localScale.z);
        }

    }
    public void SetFalse()
    {
        needToRescaleMinus = false;
        needToRescalePlus = false;
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
        if (!this.GetComponent<DragnDrop>().dragnow)
        {
            //this.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = gameObject.GetComponent<Card>().Info.Description;
            var im = GameObject.Find("CardDetail").GetComponentsInChildren<Image>();
            foreach (var image in im)
            {
                if (image.name == "ShipImage")
                    image.sprite = gameObject.GetComponent<Card>().Info.ShipSprite;
            }

            var t = GameObject.Find("CardDetail").GetComponentsInChildren<Text>();
            foreach (var txt in t)
            {
                if (txt.name == "CardName")
                {
                    txt.text = gameObject.GetComponent<Card>().Info.Name;
                }
                if (txt.name == "Description")
                {
                    txt.text = gameObject.GetComponent<Card>().Info.Description;
                }
            }

            var canvasGroup = GameObject.Find("CardDetail").GetComponentInChildren<CanvasGroup>();
            if (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha = 1f;
            }

        }
    }
    public void OnMouseExit()
    {
        //if (!this.GetComponent<DragnDrop>().dragnow)
        //{
        //    counter = 0;
        //    this.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = "";
        //    if (!this.GetComponent<Card>().OnBoard)
        //    {
        //        if (gameObject.GetComponent<Card>().Owner == Battle.turn)
        //        {
        //            needtorescaleplus = false;
        //            needtorescaleminus = true;
        //        }
        //        // this.transform.localScale = new Vector3(4.942762f, 4.947458f, 1);
        //    }
        //    counter = 0;
        //}
    }
    public bool IsClose(float actual, float ideal)
    {
        if (Mathf.Abs(actual - ideal) < 0.5f)
            return true;
        else
            return false;
    }
}
