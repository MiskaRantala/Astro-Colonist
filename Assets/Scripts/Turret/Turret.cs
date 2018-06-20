using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private SpriteRenderer mySpriteRenderer;


	// Use this for initialization
	void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }
}
