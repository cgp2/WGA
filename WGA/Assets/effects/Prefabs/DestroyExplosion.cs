using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour {
    public float timeAlive;
	// Use this for initialization
	void Start () {
        timeAlive = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timeAlive += Time.deltaTime;
        if (timeAlive > 1)
            Destroy(gameObject);
	}
}
