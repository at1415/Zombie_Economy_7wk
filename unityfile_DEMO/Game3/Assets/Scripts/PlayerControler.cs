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

    //Ground coliders Controller Calls
    public int gController = 1;//Currently set for Player starts in GroundMID. Change if needed.
    private BoxCollider2D groundB1;
    private BoxCollider2D groundB2;
    private BoxCollider2D groundM1;
    private BoxCollider2D groundM2;
    private BoxCollider2D groundF1;
    private BoxCollider2D groundF2;
    public GameObject groundBACK1;//GroundBACK int value is 0
    public GameObject groundBACK2;
    public GameObject groundMID1;//GroundMID int value is 1
    public GameObject groundMID2;
    public GameObject groundFORE1;//GroundFORE int value is 2
    public GameObject groundFORE2;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundB1 = groundBACK1.GetComponent<BoxCollider2D>();
        groundB2 = groundBACK2.GetComponent<BoxCollider2D>();
        groundM1 = groundMID1.GetComponent<BoxCollider2D>();
        groundM2 = groundMID2.GetComponent<BoxCollider2D>();
        groundF1 = groundFORE1.GetComponent<BoxCollider2D>();
        groundF2 = groundFORE2.GetComponent<BoxCollider2D>();
        djump = 0;
        ground = true;
        Grounded = ground;
        //anim.SetBool("Grounded", Grounded);
    }
	
	// Update is called once per frame. TESTING
	void Update () {
        if (Input.GetMouseButtonDown(0) && ground)
        {
            JumpUP(upForce);
            //anim.SetBool("Grounded", Grounded);
        }
        else if ((Input.GetKey("up")||Input.GetKey("w")) && ground)
        {
            if (gController > 0)
            {
                JumpUP(upForce / 1.3f);
                Invoke("LevelUP", 0.5f);
            }
        }
        else if ((Input.GetKey("down") || Input.GetKey("s")) && ground)
        {
            if (gController < 2)
            {
                JumpDOWN(upForce / 3f);
                Invoke("LevelDOWN", 0.1f);
            }
        }
    }

    void LevelUP()
    {
        if (gController == 1)//Player is in GroundMID, Going GroundBACK
        {
            gController -= 1;
            groundM1.enabled = false;
            groundM2.enabled = false;
            groundB1.enabled = true;
            groundB2.enabled = true;
        }
        else if (gController == 2)//Player is in GroundFORE, Going GroundMID
        {
            gController -= 1;
            groundF1.enabled = false;
            groundF2.enabled = false;
            groundM1.enabled = true;
            groundM2.enabled = true;
        }
    }

    void LevelDOWN()
    {
        if (gController == 0)//Player is in GroundBACK, Going GroundMID
        {
            gController += 1;
            groundB1.enabled = false;
            groundB2.enabled = false;
            groundM1.enabled = true;
            groundM2.enabled = true;
        }
        else if (gController == 1)//Player is in GroundMID, Going GroundFORE
        {
            gController += 1;
            groundM1.enabled = false;
            groundM2.enabled = false;
            groundF1.enabled = true;
            groundF2.enabled = true;
        }
    }

    //Jumps Up by given force
    void JumpUP(float force)
    {
        rb2d.AddForce(new Vector2(0, force));
        ground = false;
        Grounded = ground;
    }

    //Jumps Down by given force
    void JumpDOWN(float force)
    {
        rb2d.AddForce(new Vector2(0, -force));
        ground = false;
        Grounded = ground;
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
