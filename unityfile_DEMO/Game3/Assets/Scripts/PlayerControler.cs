using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour {

    public float upForce = 400f;
    private bool ground = true;
    public bool Grounded;
    public int health = 100;
    public int damageTaken = 20;
    public int indexOfDeath = 2;
    public int indexOfWin = 3;
    private int djump = 0;
    private Animator anim;
    private Rigidbody2D rb2d;

    public int attacking = 0;

    //Ground coliders Controller Calls
    public int GController = 0;//Currently set for Player starts in GroundMID. Change if needed.
    public float attDelay = 20f;
    private float timeNeeded = 0f;
    public float timer = 0f;
    public bool canAtt = true;
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

    public int score = 0;
    public int NeededScore = 0;
    public int Score4Enemy1 = 10;
    public int Score4Enemy2 = 15;
    public int Score4EnemyBoss = 25;

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
        if (health <= 0)
        {
            SceneManager.LoadScene(indexOfDeath);//
        }
        if (Input.GetMouseButtonDown(0) && ground)
        {
            JumpUP(upForce);
            //anim.SetBool("Grounded", Grounded);
        }
        else if (Input.GetMouseButtonDown(1) && ground)
        {
            timer = 0;
            attacking = 1;
        }
        else if ((Input.GetKey("up")||Input.GetKey("w")) && ground)
        {
            if (GController > 0)
            {
                JumpUP(upForce / 1.3f);
                Invoke("LevelUP", 0.5f);
            }
        }
        else if ((Input.GetKey("down") || Input.GetKey("s")) && ground)
        {
            if (GController < 2)
            {
                JumpDOWN(upForce / 3f);
                Invoke("LevelDOWN", 0.1f);
            }
        }
        Dum();
    }

    void Dum()
    {
        if (attacking == 1&&timer>attDelay)
        {
            attacking = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void LevelUP()
    {
        if (GController == 1)//Player is in GroundMID, Going GroundBACK
        {
            GController -= 1;
            groundM1.enabled = false;
            groundM2.enabled = false;
            groundB1.enabled = true;
            groundB2.enabled = true;
        }
        else if (GController == 2)//Player is in GroundFORE, Going GroundMID
        {
            GController -= 1;
            groundF1.enabled = false;
            groundF2.enabled = false;
            groundM1.enabled = true;
            groundM2.enabled = true;
        }
    }

    void LevelDOWN()
    {
        if (GController == 0)//Player is in GroundBACK, Going GroundMID
        {
            GController += 1;
            groundB1.enabled = false;
            groundB2.enabled = false;
            groundM1.enabled = true;
            groundM2.enabled = true;
        }
        else if (GController == 1)//Player is in GroundMID, Going GroundFORE
        {
            GController += 1;
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
        if (coll.gameObject.CompareTag("GameController")) //collided with obj
        {
            //health is ticked off, (slow??)
            health -= damageTaken / 2;
        }
        if ((coll.gameObject.CompareTag("E1") || coll.gameObject.CompareTag("E2") || coll.gameObject.CompareTag("Boss")) && attacking == 0) //collided with enemy, not attacking
        {
            health -= damageTaken;
        }
        if ((coll.gameObject.CompareTag("E1") || coll.gameObject.CompareTag("E2")) && attacking == 1) //collided with enemy, attacking
        {
            //gain health?
            health += damageTaken / 2;
            if (coll.gameObject.CompareTag("E1"))
            {
                score += Score4Enemy1 * 10;
            }
            else
            {
                score += Score4Enemy2 * 15;
            }
        }
        if (coll.gameObject.CompareTag("Boss") && attacking == 1)
        {
            health += damageTaken;
            score += Score4EnemyBoss * 20;
        }
        /*if (coll.gameObject.CompareTag("GameController"))
        {
        Vector2 Offset = new Vector2(ScreenStart, 0);
            coll.gameObject.GetComponent<Collider2D>().enabled = false;
            coll.gameObject.GetComponent<Transform>().position = (Vector2)transform.position + Offset*2;
            this.gameObject.SetActive(false);
        }*/
    }
}
