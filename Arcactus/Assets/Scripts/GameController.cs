using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {


    public GameObject enemy;
    public GameObject player;
    public GameObject powerUp;

    [Header("Wave Settings")]
    public float startWait = 1.0f;
    public float waveWait = 4.0f;
    /// <summary>
    /// wait
    /// </summary>
    public float spawnWait = 0.5f;
    /// <summary>
    /// count of the enemies
    /// </summary>
    public int enemyCount = 10;
    /// <summary>
    /// the amount of powerUps spawned per wave
    /// </summary>
    public float powerUpPerWave = 1f;

    [Header("Enemy")]
    public float enemySpawnRadius = 100f;
    public float enemyYPosMin = 0.0f;
    public float enemyYPosMax = 5.0f;

    [Header("PowerUp")]
    public float powerUpSpawnRadius = 80f;
    public float powerUpYPosMin = 5.0f;
    public float powerUpYPosMax = 5.0f;

    [Header("Text")]
    public Text gameOverText;
    public Text pauseText;
    public Text nextWaveText;

    internal bool paused;
    internal bool gameOver;
    private bool restart;
    private int currentWave = 1;

    private float enemyEasyProb = 0.95f;
    private float enemyMediumProb = 0.05f;
    private float enemyHardProb = 0.0f;

   
    // Use this for initialization
    void Start () {

        gameOver = false;
        restart = false;
        gameOverText.text = "";
        pauseText.text = "";
        nextWaveText.text = "";
        paused = false;
        currentWave = 1;

        StartCoroutine(SpawnWaves());
	}
	
	// Update is called once per frame
	void Update () {
        if (restart)
        {            
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        if (Input.GetButtonDown("Pause"))
        {
            if(!paused)
            {
                Time.timeScale = 0;
                pauseText.text = "Paused";
                paused = true;
            }
            else {
                Time.timeScale = 1;
                pauseText.text = "";
                paused = false;
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {


            for (int i = 0; i < enemyCount; i++)
            {
                float alpha = Random.Range(0.0f, 1.0f);
                float powerUpProb = powerUpPerWave / enemyCount;
                if (alpha <= powerUpProb)
                {
                    SpawnPowerUp();
                }
                SpawnEnemy();

                yield return new WaitForSeconds(spawnWait);
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            while (enemies.Length > 0)
            {
                yield return new WaitForSeconds(1.0f);
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
            }

            if (gameOver)
            {
                restart = true;
                break;
            }

            InitializeNextWave();
            yield return new WaitForSeconds(waveWait);
        }
    }

    private void InitializeNextWave()
    {
        if (currentWave <= 11)
        {
            enemyHardProb += currentWave/100.0f;
            enemyEasyProb -= (1.5f*(11 - (currentWave)))/100.0f;
            enemyMediumProb = 1 - enemyHardProb - enemyEasyProb;
            Debug.Log(enemyEasyProb + " ! " + enemyMediumProb + " ! " + enemyHardProb);
        }

        enemyCount += currentWave;
        currentWave++;
        nextWaveText.text = "Next Wave: " + currentWave;


    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = ComputeSpawnPosition(enemySpawnRadius, enemyYPosMin, enemyYPosMax);
        Quaternion spawnRotation = Quaternion.identity;

        float alpha = Random.Range(0.0f, 1.0f);
        if(alpha <= enemyEasyProb)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Enemy Easy"), spawnPosition, spawnRotation) as GameObject;
        }
        else if(alpha >= 1- enemyHardProb)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Enemy Hard"), spawnPosition, spawnRotation) as GameObject;
        }
        else
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Enemy Medium"), spawnPosition, spawnRotation) as GameObject;
        }


        //Instantiate(enemy, spawnPosition, spawnRotation);

    }

    void SpawnPowerUp()
    {
        Vector3 spawnPosition = ComputeSpawnPosition(powerUpSpawnRadius, powerUpYPosMin, powerUpYPosMax);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(powerUp, spawnPosition, spawnRotation);
    }

    private Vector3 ComputeSpawnPosition(float spawnRadius, float yPosMin, float yPosMax)
    {
        float alpha = Random.Range(0, Mathf.PI);

        float xPos = Mathf.Cos(alpha) * spawnRadius;
        float yPos = Random.Range(yPosMin, yPosMax);
        float zPos = Mathf.Sin(alpha) * spawnRadius;

        return player.transform.position + new Vector3(xPos, yPos, zPos);
    }

    internal void GameOver()
    {
        gameOverText.text = "Game Over !";
        gameOver = true;
    }

}
