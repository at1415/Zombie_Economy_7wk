using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float ScrollSpeed = -1.5f;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(ScrollSpeed, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
