using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectCollDetect : MonoBehaviour {
    public bool touched = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*if (this.GetComponent<Collider2D>().enabled == true)
        {
            touched = false;
        }*/
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            touched = true;
            this.GetComponent<Collider2D>().enabled = false;
        }
    }
}
