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
    public GameObject groundBACK1;//GroundBACK int value is 0
    public GameObject groundBACK2;
    public GameObject groundMID1;//GroundMID int value is 1
    public GameObject groundMID2;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundB1 = groundBACK1.GetComponent<BoxCollider2D>();
        groundB2 = groundBACK2.GetComponent<BoxCollider2D>();
        groundM1 = groundMID1.GetComponent<BoxCollider2D>();
        groundM2 = groundMID2.GetComponent<BoxCollider2D>();
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
        else if (Input.GetKey("up") && ground)
        {
            if (gController == 1)//Player is in GroundMID, Going GroundBACK
            {
                gController -= 1;
                JumpUP(upForce / 2f);
                ExecuteAfterTime(0.25f);
                groundM1.enabled = false;
                groundM2.enabled = false;
                groundB1.enabled = true;
                groundB2.enabled = true;
            }
            //NEED TO ADD FOR GROUNDFOR
        }
        else if (Input.GetKey("down") && ground)
        {
            if (gController == 0)//Player is in GroundBACK, Going GroundMID
            {
                gController += 1;
                JumpDOWN(upForce / 3f);
                ExecuteAfterTime(0.2f);
                groundB1.enabled = false;
                groundB2.enabled = false;
                groundM1.enabled = true;
                groundM2.enabled = true;
            }
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void JumpUP(float force)
    {
        rb2d.AddForce(new Vector2(0, force));
        ground = false;
        Grounded = ground;
    }

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
