using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is same as RepeatingGround.cs

public class BackgroundReposition : MonoBehaviour
{

    private BoxCollider2D GroundCollider;
    private float groundHorizontalLength;

    // Use this for initialization
    private void Awake()
    {
        GroundCollider = GetComponent<BoxCollider2D>();
        groundHorizontalLength = GroundCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -groundHorizontalLength)
        {
            RepositionGround();
        }
    }

    private void RepositionGround()
    {
        Vector2 groundOffset = new Vector2(groundHorizontalLength * 2f, 0);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
