using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGController : MonoBehaviour {

    /*
    NEED TO: figure how to PUBLICLY controll when sprites change, when repeat ends,
    MAYBE how to call next scene here? NEW!: this will also do scroll

    HOW TO: 
    1) Set int to keep track of when to change sprites
    2) Call diferent sprites/prefabs
    3) Stop everything and go to next screen

    KNOWN ERROR(S):
    ! - Spawns small gap between end of 'first' and start of 'second' 
        sprites. Gets larger with increased ScrollSpeed. NEED HELP!!!!!
    ! - Sprites that are called later spawn in middle of screen.
        Poss fixes: Messing with Sorting Order on sprites,
        OR dissabling GameObject until called (?)
    */

    private BoxCollider2D GroundCollider;
    private float groundHorizontalLength;
    //Speed of objects movement. NEEDS TO BE NEGATIVE!
    public float ScrollSpeed = -1.5f;

    //after changeVal, use transSprite then diffSprite
    public int changeVal;
    //after endVal, stop everything. NOTE:starts decrement AFTER changeVal over
    public int endVal;
    //positions sprite to the needed y value given
    public float yVal;
    //positions sprite to the needed x value TO START
    public float xVal;
    //why not set these values?so to use for both background and ground :D

    //Sprite/Prefab that is used in the beginning of floor
    public GameObject startSpritePREFAB;
    //Sprite/Prefab that is used between start and diff sprites/prefabs
    public GameObject transSpritePREFAB;
    //Sprite/Prefab that is used in the end of level
    public GameObject diffSpritePREFAB;

    //These will be called in the code
    private GameObject startSprite;
    private GameObject startCopy;
    private GameObject transSprite;
    private GameObject diffSprite;
    private GameObject diffCopy;

    //Currently used sprites to scroll with
    private GameObject first;
    private GameObject second;

    /*
    IF NEED OF MORE TRANSITIONS:
    Make a different script. Copy this script and paste onto new Ground/Background controller empty object script component. Add new changeVal1, changeVal2, ect.
    Add new GameObjects for transition/background sprites. Add new private calls for prefabs. Edit new script as needed.
    DO NOT EDIT THIS SCRIPT PLEASE. STILL FIXING.
    */

    void Start()
    {
        this.transform.position = new Vector2(xVal, yVal);
        startSprite = GameObject.Instantiate(startSpritePREFAB, this.transform);
        GroundCollider = startSprite.GetComponent<BoxCollider2D>();
        groundHorizontalLength = GroundCollider.size.x;
        this.transform.position = new Vector2(groundHorizontalLength, yVal);
        startCopy = GameObject.Instantiate(startSpritePREFAB, this.transform);

        //needs to have these to call for later. REWORK(???)
        transSprite = GameObject.Instantiate(transSpritePREFAB, this.transform);
        diffSprite = GameObject.Instantiate(diffSpritePREFAB, this.transform);
        diffCopy = GameObject.Instantiate(diffSpritePREFAB, this.transform);

        first = startSprite;
        second = startCopy;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeVal > 0)
        {
            Scroll();
            if (first.transform.position.x < -groundHorizontalLength)
            {
                first.transform.position = new Vector2(groundHorizontalLength, yVal);
                changeVal -= 1;
            }
            else if(second.transform.position.x < -groundHorizontalLength)
            {
                second.transform.position = new Vector2(groundHorizontalLength, yVal);
                changeVal -= 1;
            }
        }
        else if (changeVal == 0)
        {
            Scroll();
            if (first.transform.position.x < -groundHorizontalLength)
            {
                first = transSprite;
                first.transform.position = new Vector2(groundHorizontalLength, yVal);
                changeVal -= 1;
            }
            else if (second.transform.position.x < -groundHorizontalLength)
            {
                second = transSprite;
                second.transform.position = new Vector2(groundHorizontalLength, yVal);
                changeVal -= 1;
            }
        }
        else if (changeVal == -1)
        {
            Scroll();
            if (first.transform.position.x < -groundHorizontalLength)
            {
                first = diffSprite;
                first.transform.position = new Vector2(groundHorizontalLength, yVal);
                changeVal -= 1;
                endVal -= 1;
            }
            else if (second.transform.position.x < -groundHorizontalLength)
            {
                second = diffSprite;
                second.transform.position = new Vector2(groundHorizontalLength, yVal);
                changeVal -= 1;
                endVal -= 1;
            }
        }
        else if (changeVal == -2)
        {
            Scroll();
            if (first.transform.position.x < -groundHorizontalLength)
            {
                first = diffCopy;
                first.transform.position = new Vector2(groundHorizontalLength, yVal);
                changeVal -= 1;
                endVal -= 1;
            }
            else if (second.transform.position.x < -groundHorizontalLength)
            {
                second = diffCopy;
                second.transform.position = new Vector2(groundHorizontalLength, yVal);
                changeVal -= 1;
                endVal -= 1;
            }
        }
        else if (endVal > 0)
        {
            Scroll();
            if (first.transform.position.x < -groundHorizontalLength)
            {
                first.transform.position = new Vector2(groundHorizontalLength, yVal);
                endVal -= 1;
            }
            else if (second.transform.position.x < -groundHorizontalLength)
            {
                second.transform.position = new Vector2(groundHorizontalLength, yVal);
                endVal -= 1;
            }
        }
        /*
        else if (endVal == 0)
        {
            END EVERYTHING (scene change to score elevator???)
        }
        */
    }

    //Found this code gets used a lot. Moves current sprites to make the ground 'move'
    private void Scroll()
    {
        first.transform.position = new Vector2(first.transform.position.x + ScrollSpeed, yVal);
        second.transform.position = new Vector2(second.transform.position.x + ScrollSpeed, yVal);
    }
}
