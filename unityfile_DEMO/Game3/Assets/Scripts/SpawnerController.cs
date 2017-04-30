using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public int WinSceneIndex = 0;
    public int LoseSceneIndex = 1;
    public GameObject PlayerOBJECT;
    public GameObject Enemy1PREFAB;
    public GameObject Enemy2PREFAB;
    public GameObject EnemyBossPREFAB;
    public GameObject obj1PREFAB;
    private GameObject Enemy1B;
    private GameObject Enemy1M;
    private GameObject Enemy1F;
    private GameObject Enemy2B;
    private GameObject Enemy2M;
    private GameObject Enemy2F;
    private GameObject EnemyBoss;
    private GameObject obj1B;
    private GameObject obj1M;
    private GameObject obj1F;
    //private GameObject Player ???

    public int numEmemy1Spawns = 4;
    public int numEnemy2Spawns = 8;
    public int numEnemyBossSpawns = 3;
    //public int BossSpawnsSoFar = 0;
    public int numobjSpawns = 5;
    private int mobspawns;
    public Text scoreText;
    public int score = 0;
    public int NeededScore = 0;
    public int Score4Enemy1 = 10;
    public int Score4Enemy2 = 15;
    public int Score4EnemyBoss = 25;

    public float speed = 2f;
    public float currentSpeed = 2f;

    private float timeSinceEnemy1BLastSpawned = 0f;
    private float timeSinceEnemy1MLastSpawned = 0f;
    private float timeSinceEnemy1FLastSpawned = 0f;
    private float timeSinceEnemy2BLastSpawned = 0f;
    private float timeSinceEnemy2MLastSpawned = 0f;
    private float timeSinceEnemy2FLastSpawned = 0f;
    private float timeSinceObjBEnemyLastSpawned = 0f;
    private float timeSinceObjMEnemyLastSpawned = 0f;
    private float timeSinceObjFEnemyLastSpawned = 0f;
    private float timeSinceEBossLastSpawned = 0f;
    private int EnemyBACKOnScreen = 0;
    private int EnemyMIDOnScreen = 0;
    private int EnemyFOREOnScreen = 0;
    private int Enemy1BACKOnScreen = 0;
    private int Enemy1MIDOnScreen = 0;
    private int Enemy1FOREOnScreen = 0;
    private int Enemy2BACKOnScreen = 0;
    private int Enemy2MIDOnScreen = 0;
    private int Enemy2FOREOnScreen = 0;
    private int objBACKOnScreen = 0;
    private int objMIDOnScreen = 0;
    private int objFOREOnScreen = 0;
    private int EBossBACKOnScreen = 0;
    private int EBossMIDOnScreen = 0;
    private int EBossFOREOnScreen = 0;
    private int OnScreen = 0;
    private int floorSpawn;
    //private int Obj1OnScreen = 0;

    //Postions of Levels, where to spawn and despawn x-wise
    //NOTE: THIS CODE IS MADE FOR SPAWNING RIGHT, DESPAWNING LEFT
    public float ScreenLeft = -10f;
    public float ScreenRight = 10f; 
    public float GBack_y = -2f;
    public float GMid_y = -4f;
    public float GFore_y = -6f;

    private GameObject dummy;

    // Use this for initialization
    void Awake()
    {
        //Formula making min score needed. Adjust as needed
        NeededScore = numEmemy1Spawns * Score4Enemy1 + numEnemy2Spawns * Score4Enemy2 + numEnemyBossSpawns * Score4EnemyBoss;
        mobspawns = numEmemy1Spawns + numEnemy2Spawns + numobjSpawns;
        dummy = GameObject.Instantiate(Enemy1PREFAB, Enemy1PREFAB.transform);
        dummy.transform.position = new Vector2(ScreenRight, GBack_y);
        EnemyBoss = GameObject.Instantiate(EnemyBossPREFAB, dummy.transform);
        Enemy1B = GameObject.Instantiate(Enemy1PREFAB, dummy.transform);
        Enemy2B = GameObject.Instantiate(Enemy2PREFAB, dummy.transform);
        obj1B = GameObject.Instantiate(obj1PREFAB, dummy.transform);
        dummy.transform.position = new Vector2(ScreenRight, GMid_y);
        Enemy1M = GameObject.Instantiate(Enemy1PREFAB, dummy.transform);
        Enemy2M = GameObject.Instantiate(Enemy2PREFAB, dummy.transform);
        obj1M = GameObject.Instantiate(obj1PREFAB, dummy.transform);
        dummy.transform.position = new Vector2(ScreenRight, GFore_y);
        Enemy1F = GameObject.Instantiate(Enemy1PREFAB, dummy.transform);
        Enemy2F = GameObject.Instantiate(Enemy2PREFAB, dummy.transform);
        obj1F = GameObject.Instantiate(obj1PREFAB, dummy.transform);
        /*speed = 2;
        currentSpeed = 2;*/
        score = 0;
        /*scoreText.text = "Score: " + score.ToString();
        PlayerPrefs.SetString("Score", scoreText.text);*/
    }

    // Update is called once per frame
    void Update()
    {
        int totalspawns = numEmemy1Spawns + numEnemy2Spawns + numEnemyBossSpawns + numobjSpawns;
        int dumspawns = numEmemy1Spawns + numEnemy2Spawns + numobjSpawns;
        //check if 1/3 of dumspawns are gone. if they are spawn boss
        if ((mobspawns / 3) >= dumspawns)
        {
            SpawnBoss(FloorController());
        }
        else if (((mobspawns / 3) * 2) >= dumspawns)
        {
            SpawnBoss(FloorController());
        }
        else if (dumspawns == 0)
        {
            SpawnBoss(FloorController());
        }
        //check if spawns are spent. if they are, check if score > Needed score. go to either win or lose scene
        if (totalspawns == 0)
        {
            if (score >= NeededScore)
            {
                //go to win scene
            }
            else
            {
                // go to lose scene
            }
        }
        //ColliderDespawn();
        timeSinceEnemy1BLastSpawned += (Time.deltaTime);
        timeSinceEnemy1MLastSpawned += (Time.deltaTime);
        timeSinceEnemy1FLastSpawned += (Time.deltaTime);
        timeSinceEnemy2BLastSpawned += (Time.deltaTime);
        timeSinceEnemy2MLastSpawned += (Time.deltaTime);
        timeSinceEnemy2FLastSpawned += (Time.deltaTime);
        timeSinceObjBEnemyLastSpawned += (Time.deltaTime);
        timeSinceObjMEnemyLastSpawned += (Time.deltaTime);
        timeSinceObjFEnemyLastSpawned += (Time.deltaTime);
        if (EnemyBACKOnScreen == 0 && EnemyMIDOnScreen == 0 && EnemyFOREOnScreen == 0)
        {
            floorSpawn = Random.Range(0, 3);
            StartCoroutine(SpawnGroundEnemy(floorSpawn, 20f));
        }
        else if (OnScreen >= 1)
        {
            int f = FloorController();
            if (f == 0)
            {
                int r = Random.Range(1, 3);
                if (r == 1)
                {
                    StartCoroutine(SpawnGroundEnemy(1, 0.5f));
                }
                else
                {
                    StartCoroutine(SpawnGroundEnemy(2, 0.5f));
                }
            }
            else if (f == 1)
            {
                int r = Random.Range(1, 3);
                if (r == 1)
                {
                    StartCoroutine(SpawnGroundEnemy(0, 0.5f));
                }
                else
                {
                    StartCoroutine(SpawnGroundEnemy(2, 0.5f));
                }
            }
            else if(f == 2)
            {
                int r = Random.Range(1, 3);
                if (r == 1)
                {
                    StartCoroutine(SpawnGroundEnemy(0, 0.5f));
                }
                else
                {
                    StartCoroutine(SpawnGroundEnemy(1, 0.5f));
                }
            }
        }
        /*if (birdEnemyOnScreen == 0)
        {
            //System.Threading.Thread.Sleep(3000);
            SpawnBirdEnemy();
            timeSinceBirdEnemyLastSpawned = 0;
        }*/
        if (EnemyBACKOnScreen >= 1)
        {
            if(Enemy1BACKOnScreen == 1)
            {
                Enemy1B.transform.position = new Vector2(ScreenRight - (timeSinceEnemy1BLastSpawned * currentSpeed * 2), GBack_y);
                if (ScreenRight - (timeSinceEnemy1BLastSpawned * currentSpeed * 2) <= ScreenLeft)
                {
                    Enemy1BACKOnScreen = 0;
                    EnemyBACKOnScreen -= 1;
                    OnScreen -= 1;
                    numEmemy1Spawns -= 1;
                }
            }
            if(Enemy2BACKOnScreen == 1)
            {
                Enemy2B.transform.position = new Vector2(ScreenRight - (timeSinceEnemy2BLastSpawned*currentSpeed * 2), GBack_y);
                if (ScreenRight - (timeSinceEnemy2BLastSpawned * currentSpeed * 2) <= ScreenLeft)
                {
                    Enemy2BACKOnScreen = 0;
                    EnemyBACKOnScreen -= 1;
                    OnScreen -= 1;
                    numEnemy2Spawns -= 1;
                }
            }
            if(objBACKOnScreen == 1)
            {
                obj1B.transform.position = new Vector2(ScreenRight - (timeSinceObjBEnemyLastSpawned * currentSpeed * 2), GBack_y);
                if(ScreenRight- (timeSinceObjBEnemyLastSpawned * currentSpeed * 2)<= ScreenLeft)
                {
                    objBACKOnScreen = 0;
                    EnemyBACKOnScreen -= 1;
                    OnScreen -= 1;
                    numobjSpawns -= 1;
                }
            }
            /*bear.transform.position = new Vector2(maxX - (timeSinceGroundEnemyLastSpawned * 2 * currentSpeed), minY);
            if (maxX - (timeSinceGroundEnemyLastSpawned * 2 * speed) <= minX)
            {
                bearEnemyOnScreen = 0;
                score += 1;
                scoreText.text = "Score: " + score.ToString();
                PlayerPrefs.SetString("Score", scoreText.text);
            }*/
        }
        if (EnemyMIDOnScreen >= 1)
        {
            if (Enemy1MIDOnScreen == 1)
            {
                Enemy1M.transform.position = new Vector2(ScreenRight - (timeSinceEnemy1MLastSpawned * currentSpeed * 2), GMid_y);
                if (ScreenRight - (timeSinceEnemy1MLastSpawned * currentSpeed * 2) <= ScreenLeft)
                {
                    Enemy1MIDOnScreen = 0;
                    EnemyMIDOnScreen -= 1;
                    OnScreen -= 1;
                    numEmemy1Spawns -= 1;
                }
            }
            if (Enemy2MIDOnScreen == 1)
            {
                Enemy2M.transform.position = new Vector2(ScreenRight - (timeSinceEnemy2MLastSpawned * currentSpeed * 2), GMid_y);
                if (ScreenRight - (timeSinceEnemy2MLastSpawned * currentSpeed * 2) <= ScreenLeft)
                {
                    Enemy2MIDOnScreen = 0;
                    EnemyMIDOnScreen -= 1;
                    OnScreen -= 1;
                    numEnemy2Spawns -= 1;
                }
            }
            if (objMIDOnScreen == 1)
            {
                obj1M.transform.position = new Vector2(ScreenRight - (timeSinceObjMEnemyLastSpawned * currentSpeed * 2), GMid_y);
                if (ScreenRight - (timeSinceObjMEnemyLastSpawned * currentSpeed * 2) <= ScreenLeft)
                {
                    objMIDOnScreen = 0;
                    EnemyMIDOnScreen -= 1;
                    OnScreen -= 1;
                    numobjSpawns -= 1;
                }
            }
            /*sealion.transform.position = new Vector2(maxX - (timeSinceGroundEnemyLastSpawned * 4 * currentSpeed), minY);
            if (maxX - (timeSinceGroundEnemyLastSpawned * 4 * speed) <= minX)
            {
                sealionEnemyOnScreen = 0;
                score += 1;
                /*scoreText.text = "Score: " + score.ToString();
                PlayerPrefs.SetString("Score", scoreText.text);
            }*/
        }
        if (EnemyFOREOnScreen >= 1)
        {
            if (Enemy1FOREOnScreen == 1)
            {
                Enemy1F.transform.position = new Vector2(ScreenRight - (timeSinceEnemy1FLastSpawned * currentSpeed * 2), GFore_y);
                if (ScreenRight - (timeSinceEnemy1FLastSpawned * currentSpeed * 2) <= ScreenLeft)
                {
                    Enemy1FOREOnScreen = 0;
                    EnemyFOREOnScreen -= 1;
                    OnScreen -= 1;
                    numEmemy1Spawns -= 1;
                }
            }
            if (Enemy2FOREOnScreen == 1)
            {
                Enemy2F.transform.position = new Vector2(ScreenRight - (timeSinceEnemy2FLastSpawned * currentSpeed * 2), GFore_y);
                if (ScreenRight - (timeSinceEnemy2FLastSpawned * currentSpeed * 2) <= ScreenLeft)
                {
                    Enemy2FOREOnScreen = 0;
                    EnemyFOREOnScreen -= 1;
                    OnScreen -= 1;
                    numEnemy2Spawns -= 1;
                }
            }
            if (objFOREOnScreen == 1)
            {
                obj1F.transform.position = new Vector2(ScreenRight - (timeSinceObjFEnemyLastSpawned * currentSpeed * 2), GFore_y);
                if (ScreenRight - (timeSinceObjFEnemyLastSpawned * currentSpeed * 2) <= ScreenLeft)
                {
                    objFOREOnScreen = 0;
                    EnemyFOREOnScreen -= 1;
                    OnScreen -= 1;
                    numobjSpawns -= 1;
                }
            }
            /*bird.transform.position = new Vector2(maxX - (timeSinceBirdEnemyLastSpawned * 3 * speed), birdYPosition);
            if (maxX - (timeSinceBirdEnemyLastSpawned * 3 * speed) <= minX)
            {
                birdEnemyOnScreen = 0;
                if (speed < 5)
                {
                    speed += 0.1f;
                }
                score += 1;
                /*scoreText.text = "Score: " + score.ToString();
                PlayerPrefs.SetString("Score", scoreText.text);
            }*/
        }
        if (EBossBACKOnScreen == 1)
        {
            EnemyBoss.transform.position = new Vector2(ScreenRight - (timeSinceEBossLastSpawned * currentSpeed * 2), GBack_y);
            if(ScreenRight - (timeSinceEBossLastSpawned * currentSpeed * 2) <= ScreenLeft)
            {
                EBossBACKOnScreen = 0;
                numEnemyBossSpawns -= 1;
                OnScreen -= 1;
            }
        }
        if (EBossMIDOnScreen == 1)
        {
            EnemyBoss.transform.position = new Vector2(ScreenRight - (timeSinceEBossLastSpawned * currentSpeed * 2), GMid_y);
            if (ScreenRight - (timeSinceEBossLastSpawned * currentSpeed * 2) <= ScreenLeft)
            {
                EBossMIDOnScreen = 0;
                numEnemyBossSpawns -= 1;
                OnScreen -= 1;
            }
        }
        if (EBossFOREOnScreen == 1)
        {
            EnemyBoss.transform.position = new Vector2(ScreenRight - (timeSinceEBossLastSpawned * currentSpeed * 2), GFore_y);
            if (ScreenRight - (timeSinceEBossLastSpawned * currentSpeed * 2) <= ScreenLeft)
            {
                EBossFOREOnScreen = 0;
                numEnemyBossSpawns -= 1;
                OnScreen -= 1;
            }
        }
    }

    void SpawnBoss(int floor) //spawns boss at floor IF THERE ARE BOSSES STILL TO SPAWN
    {
        /*if (numEnemyBossSpawns == 0) use this if spawn boss at random
        {
            //do nothing, can't spawn more bosses
        }*/
        if (floor == 0)
        {
            EnemyBoss.transform.position = new Vector2(ScreenRight, GBack_y);
            EnemyBoss.gameObject.GetComponent<Collider2D>().enabled = true;
            EBossBACKOnScreen = 1;
            timeSinceEBossLastSpawned = 0f;
        }
        else if(floor == 1)
        {
            EnemyBoss.transform.position = new Vector2(ScreenRight, GMid_y);
            EnemyBoss.gameObject.GetComponent<Collider2D>().enabled = true;
            EBossMIDOnScreen = 1;
            timeSinceEBossLastSpawned = 0f;
        }
        else
        {
            EnemyBoss.transform.position = new Vector2(ScreenRight, GFore_y);
            EnemyBoss.gameObject.GetComponent<Collider2D>().enabled = true;
            EBossFOREOnScreen = 1;
            timeSinceEBossLastSpawned = 0f;
        }
        OnScreen += 1;
    }

    int FloorController() //checks if other floors have spawned enemies, returns num of floor with most spawns
    {
        if (EnemyBACKOnScreen >= EnemyMIDOnScreen)
        {
            if(EnemyBACKOnScreen >= EnemyFOREOnScreen)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }
        else
        {
            if (EnemyMIDOnScreen >= EnemyBACKOnScreen)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    void ColliderDespawn() //despawns colliders if player is not on floor level 
    {
        if (PlayerOBJECT.GetComponent<PlayerControler>().gController == 0)
        {
            if (EBossBACKOnScreen == 1)
            {
                EnemyBoss.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                EnemyBoss.GetComponent<BoxCollider2D>().enabled = false;
            }
            Enemy1B.GetComponent<Collider2D>().enabled = true;
            Enemy2B.GetComponent<Collider2D>().enabled = true;
            obj1B.GetComponent<Collider2D>().enabled = true;
            Enemy1M.GetComponent<Collider2D>().enabled = false;
            Enemy2M.GetComponent<Collider2D>().enabled = false;
            obj1M.GetComponent<Collider2D>().enabled = false;
            Enemy1F.GetComponent<Collider2D>().enabled = false;
            Enemy2F.GetComponent<Collider2D>().enabled = false;
            obj1F.GetComponent<Collider2D>().enabled = false;
        }
        else if (PlayerOBJECT.GetComponent<PlayerControler>().gController == 1)
        {
            if (EBossMIDOnScreen == 1)
            {
                EnemyBoss.GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                EnemyBoss.GetComponent<Collider2D>().enabled = false;
            }
            Enemy1B.GetComponent<Collider2D>().enabled = false;
            Enemy2B.GetComponent<Collider2D>().enabled = false;
            obj1B.GetComponent<Collider2D>().enabled = false;
            Enemy1M.GetComponent<Collider2D>().enabled = true;
            Enemy2M.GetComponent<Collider2D>().enabled = true;
            obj1M.GetComponent<Collider2D>().enabled = true;
            Enemy1F.GetComponent<Collider2D>().enabled = false;
            Enemy2F.GetComponent<Collider2D>().enabled = false;
            obj1F.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            if (EBossFOREOnScreen == 1)
            {
                EnemyBoss.GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                EnemyBoss.GetComponent<Collider2D>().enabled = false;
            }
            Enemy1B.GetComponent<Collider2D>().enabled = false;
            Enemy2B.GetComponent<Collider2D>().enabled = false;
            obj1B.GetComponent<Collider2D>().enabled = false;
            Enemy1M.GetComponent<Collider2D>().enabled = false;
            Enemy2M.GetComponent<Collider2D>().enabled = false;
            obj1M.GetComponent<Collider2D>().enabled = false;
            Enemy1F.GetComponent<Collider2D>().enabled = true;
            Enemy2F.GetComponent<Collider2D>().enabled = true;
            obj1F.GetComponent<Collider2D>().enabled = true;
        }
    }

    IEnumerator SpawnGroundEnemy(int floor, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (floor == 0)
        {
            Enemy1B.transform.position = new Vector2(ScreenRight, GBack_y);
            Enemy1B.gameObject.GetComponent<Collider2D>().enabled = true;
            Enemy2B.transform.position = new Vector2(ScreenRight, GBack_y);
            Enemy2B.gameObject.GetComponent<Collider2D>().enabled = true;
            obj1B.transform.position = new Vector2(ScreenRight, GBack_y);
            obj1B.gameObject.GetComponent<Collider2D>().enabled = true;
            int rand = Random.Range(0, 3);
            if ( rand == 0)
            {
                Enemy1BACKOnScreen = 1;
                EnemyBACKOnScreen += 1;
                timeSinceEnemy1BLastSpawned = 0f;
            }
            else if(rand == 1)
            {
                Enemy2BACKOnScreen = 1;
                EnemyBACKOnScreen += 1;
                timeSinceEnemy2BLastSpawned = 0f;
            }
            else
            {
                objBACKOnScreen = 1;
                EnemyBACKOnScreen += 1;
                timeSinceObjBEnemyLastSpawned = 0f;
            }
        }
        else if(floor == 1)
        {
            Enemy1M.transform.position = new Vector2(ScreenRight, GMid_y);
            Enemy1M.gameObject.GetComponent<Collider2D>().enabled = true;
            Enemy2M.transform.position = new Vector2(ScreenRight, GMid_y);
            Enemy2M.gameObject.GetComponent<Collider2D>().enabled = true;
            obj1M.transform.position = new Vector2(ScreenRight, GMid_y);
            obj1M.gameObject.GetComponent<Collider2D>().enabled = true;
            int rand = Random.Range(0, 3);
            if (rand == 0)
            {
                Enemy1MIDOnScreen = 1;
                EnemyMIDOnScreen += 1;
                timeSinceEnemy1MLastSpawned = 0f;
            }
            else if (rand == 1)
            {
                Enemy2MIDOnScreen = 1;
                EnemyMIDOnScreen += 1;
                timeSinceEnemy2MLastSpawned = 0f;
            }
            else
            {
                objMIDOnScreen = 1;
                EnemyMIDOnScreen += 1;
                timeSinceObjMEnemyLastSpawned = 0f;
            }
        }
        else if (floor == 2)
        {
            Enemy1F.transform.position = new Vector2(ScreenRight, GFore_y);
            Enemy1F.gameObject.GetComponent<Collider2D>().enabled = true;
            Enemy2F.transform.position = new Vector2(ScreenRight, GFore_y);
            Enemy2F.gameObject.GetComponent<Collider2D>().enabled = true;
            obj1F.transform.position = new Vector2(ScreenRight, GFore_y);
            obj1F.gameObject.GetComponent<Collider2D>().enabled = true;
            int rand = Random.Range(0, 3);
            if (rand == 0)
            {
                Enemy1FOREOnScreen = 1;
                EnemyFOREOnScreen += 1;
                timeSinceEnemy1FLastSpawned = 0f;
            }
            else if (rand == 1)
            {
                Enemy2FOREOnScreen = 1;
                EnemyFOREOnScreen += 1;
                timeSinceEnemy2FLastSpawned = 0f;
            }
            else
            {
                objFOREOnScreen = 1;
                EnemyFOREOnScreen += 1;
                timeSinceObjFEnemyLastSpawned = 0f;
            }
        }
        /*bear.transform.position = new Vector2(maxX, minY);
        sealion.transform.position = new Vector2(maxX, minY);
        if (Random.Range(1, 10) % 2 == 0)
        {
            bearEnemyOnScreen = 1;
        }
        else
        {
            sealionEnemyOnScreen = 1;
        }*/
        OnScreen += 1;
        currentSpeed = speed;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && coll.gameObject.GetComponent<PlayerControler>().attacking == 1)
        {
            //Attacked! Add to score, spawn it back to out of screen(THIS IS IN PLAYERCONTROLER)
            if (this.gameObject.CompareTag("E1"))
            {
                score += Score4Enemy1 * 10;
            }
            else if (this.gameObject.CompareTag("E2"))
            {
                score += Score4Enemy2 * 10;
            }
            else if (this.gameObject.CompareTag("Boss"))
            {
                score += Score4EnemyBoss * 10;
            }
        }
    }

    /*void SpawnBirdEnemy()
    {
        birdYPosition = Random.Range(minY + 0.5f, maxY);
        bird.transform.position = new Vector2(maxX, birdYPosition);
        birdEnemyOnScreen = 1;
        timeSinceBirdEnemyLastSpawned = 0;
    }*/
}