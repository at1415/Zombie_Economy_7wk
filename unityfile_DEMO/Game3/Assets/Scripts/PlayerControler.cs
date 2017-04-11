using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour {

    public float upForce = 400f;
    private bool ground = true;
    public bool Grounded;
    private int djump = 0;
    private Animator anim;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        djump = 0;
        ground = true;
        Grounded = ground;
        //anim.SetBool("Grounded", Grounded);
    }
	
	// Update is called once per frame. TESTING
	void Update () {
        if (Input.GetMouseButtonDown(0) && ground)
        {
            //Deleted *2 to upForce
            rb2d.AddForce(new Vector2(0, upForce));
            ground = false;
            Grounded = ground;
            //anim.SetBool("Grounded", Grounded);
        }
    }

    //Check for collisions with Player
    void OnCollisionEnter2D(Collision2D coll)
    {
        // If the Collider2D component is enabled on the object we collided with
        if (coll.collider == true)
        {
            ground = true;
            Grounded = ground;
            //anim.SetBool("Grounded", Grounded);
            djump = 0;
        }
        if (coll.gameObject.CompareTag("GameController"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
