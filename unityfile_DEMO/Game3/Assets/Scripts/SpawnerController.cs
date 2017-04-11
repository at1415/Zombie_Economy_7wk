using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject bearPrefab;
    public GameObject sealionPrefab;
    public GameObject birdPrefab;
    public GameObject bear;
    public GameObject sealion;
    public GameObject bird;

    public float speed = 2f;
    public float currentSpeed = 2f;
    public int score = 0;

    public Text scoreText;

    private float timeSinceGroundEnemyLastSpawned = 0f;
    private float timeSinceBirdEnemyLastSpawned = 0f;
    private int bearEnemyOnScreen = 0;
    private int sealionEnemyOnScreen = 0;
    private int birdEnemyOnScreen = 0;
    private float birdYPosition = 0f;
    private float minY = -2f;
    private float maxY = 1.5f;
    private float minX = -14f;
    private float maxX = 14f;

    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector2(maxX, minY);
        sealion = GameObject.Instantiate(sealionPrefab, this.transform);
        bird = GameObject.Instantiate(birdPrefab, this.transform);
        bear = GameObject.Instantiate(bearPrefab, this.transform);
        speed = 2;
        currentSpeed = 2;
        score = 0;
        /*scoreText.text = "Score: " + score.ToString();
        PlayerPrefs.SetString("Score", scoreText.text);*/
    }

    // Update is called once per frame
    void Update()
    {

        timeSinceGroundEnemyLastSpawned += (Time.deltaTime);
        timeSinceBirdEnemyLastSpawned += (Time.deltaTime);
        if (bearEnemyOnScreen == 0 && sealionEnemyOnScreen == 0)
        {
            SpawnGroundEnemy();
            timeSinceGroundEnemyLastSpawned = 0;
        }
        if (birdEnemyOnScreen == 0)
        {
            //System.Threading.Thread.Sleep(3000);
            SpawnBirdEnemy();
            timeSinceBirdEnemyLastSpawned = 0;
        }
        if (bearEnemyOnScreen == 1)
        {
            bear.transform.position = new Vector2(maxX - (timeSinceGroundEnemyLastSpawned * 2 * currentSpeed), minY);
            if (maxX - (timeSinceGroundEnemyLastSpawned * 2 * speed) <= minX)
            {
                bearEnemyOnScreen = 0;
                score += 1;
                /*scoreText.text = "Score: " + score.ToString();
                PlayerPrefs.SetString("Score", scoreText.text);*/
            }
        }
        if (sealionEnemyOnScreen == 1)
        {
            sealion.transform.position = new Vector2(maxX - (timeSinceGroundEnemyLastSpawned * 4 * currentSpeed), minY);
            if (maxX - (timeSinceGroundEnemyLastSpawned * 4 * speed) <= minX)
            {
                sealionEnemyOnScreen = 0;
                score += 1;
                /*scoreText.text = "Score: " + score.ToString();
                PlayerPrefs.SetString("Score", scoreText.text);*/
            }
        }
        if (birdEnemyOnScreen == 1)
        {
            bird.transform.position = new Vector2(maxX - (timeSinceBirdEnemyLastSpawned * 3 * speed), birdYPosition);
            if (maxX - (timeSinceBirdEnemyLastSpawned * 3 * speed) <= minX)
            {
                birdEnemyOnScreen = 0;
                if (speed < 5)
                {
                    speed += 0.1f;
                }
                score += 1;
                /*scoreText.text = "Score: " + score.ToString();
                PlayerPrefs.SetString("Score", scoreText.text);*/
            }
        }

    }

    void SpawnGroundEnemy()
    {
        bear.transform.position = new Vector2(maxX, minY);
        sealion.transform.position = new Vector2(maxX, minY);
        if (Random.Range(1, 10) % 2 == 0)
        {
            bearEnemyOnScreen = 1;
        }
        else
        {
            sealionEnemyOnScreen = 1;
        }


        timeSinceGroundEnemyLastSpawned = 0;
        currentSpeed = speed;
    }

    void SpawnBirdEnemy()
    {
        birdYPosition = Random.Range(minY + 0.5f, maxY);
        bird.transform.position = new Vector2(maxX, birdYPosition);
        birdEnemyOnScreen = 1;
        timeSinceBirdEnemyLastSpawned = 0;
    }
}
