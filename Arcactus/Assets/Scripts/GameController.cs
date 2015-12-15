﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

	/// <summary>
	/// The player.
	/// </summary>
    public GameObject player;

    [Header("Wave Settings")]

    /// <summary>
    /// wait time before the wave.
    /// </summary>
    public float startWait = 1.0f;
    /// <summary>
    /// wait time after the wave.
    /// </summary>
    public float waveWait = 4.0f;
    /// <summary>
    /// wait time between the enemy spawns.
    /// </summary>
    public float spawnWait = 0.5f;

    [Header("Enemy")]
    /// <summary>
    /// count of the enemies at the start.
    /// </summary>
    public int enemyStartCount = 10;
    /// <summary>
    /// spawn distance between player and enemy.
    /// </summary>
    public float enemySpawnRadius = 100f;
    /// <summary>
    /// spawn angle from the player perspective.
    /// </summary>
    public int enemySpawnAngle = 160;
    /// <summary>
    /// minimal spawn position on the y-axis.
    /// </summary>
    public float enemyYPosMin = 0.0f;
    /// <summary>
    /// maximal spawn position on the y-axis.
    /// </summary>
    public float enemyYPosMax = 5.0f;

    [Header("PowerUp")]
	/// <summary>
	/// The amount of power ups spawned per wave.
	/// </summary>
	public float powerUpPerWave = 1f;
	/// <summary>
	/// The power up spawn radius.
	/// </summary>
    public float powerUpSpawnRadius = 80f;
	/// <summary>
	/// The power up spawn angle.
	/// </summary>
    public int powerUpSpawnAngle = 160;
	/// <summary>
	/// The minimal spawn position on the y-axis.
	/// </summary>
    public float powerUpYPosMin = 5.0f;
	/// <summary>
	/// The maximal spawn position on the y-axis.
	/// </summary>
    public float powerUpYPosMax = 5.0f;

	/// <summary>
	/// Whether the game is paused or not.
	/// </summary>
    internal bool paused;
	/// <summary>
	/// Whether the game is over or not.
	/// </summary>
    internal bool gameOver;
	/// <summary>
	/// Whether it is possible to restart or not.
	/// </summary>
    private bool restart;
	/// <summary>
	/// The current wave.
	/// </summary>
    private int currentWave = 1;
	/// <summary>
	/// Whether the higscore is shown or not.
	/// </summary>
    private bool highscoreShown;

	/// <summary>
	/// The spawn probality of the easy enemy.
	/// </summary>
    private float enemyEasyProb = 0.95f;
	/// <summary>
	/// The spawn probality of the medium enemy.
	/// </summary>
    private float enemyMediumProb = 0.05f;
	/// <summary>
	/// The spawn probality of the hard enemy.
	/// </summary>
    private float enemyHardProb = 0.0f;

	/// <summary>
	/// The score manager.
	/// </summary>
    private ScoreManager scoreManager;
	/// <summary>
	/// The user interface manager.
	/// </summary>
    private UIManager uiManager;
	/// <summary>
	/// The power up manager.
	/// </summary>
    private PowerUpManager powerUpManager;

    void Start () {

        gameOver = false;
        restart = false;
        paused = false;
        currentWave = 1;
        highscoreShown = false;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            scoreManager = gameControllerObject.GetComponent<ScoreManager>();
            uiManager = gameControllerObject.GetComponent<UIManager>();
            powerUpManager = gameControllerObject.GetComponent<PowerUpManager>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        StartCoroutine(SpawnWaves());
	}
	
	void Update () {
        if (restart)
        {            
            if (Input.GetButtonDown("Restart"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        if (gameOver)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if(!highscoreShown)
                {
                    //uiManager.ShowHUD(false);
                    uiManager.RemoveEventText();
                    uiManager.ShowHighscore(true);
                    //uiManager.ShowCrosshair(false);
                    highscoreShown = true;
                }
            }
        }

        if (Input.GetButtonDown("Pause"))
        {
            if(!paused)
            {
                Time.timeScale = 0;
                uiManager.ShowEventText("Paused");
                paused = true;
            }
            else {
                Time.timeScale = 1;
                uiManager.RemoveEventText();
                paused = false;
            }
        }
    }
    /// <summary>
    /// Spawning Waves including enemies and powerUps
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for (int i = 0; i < enemyStartCount; i++)
            {
                float alpha = UnityEngine.Random.Range(0.0f, 1.0f);
                float powerUpProb = powerUpPerWave / enemyStartCount;
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
            uiManager.RemoveEventText();
        }
    }
    
    /// <summary>
    /// Initializes the next wave.
    /// Calculating the enemy spawn probabilities, increment current wave and show the next wave text.
    /// </summary>
    private void InitializeNextWave()
    {
        if (currentWave <= 10)
        {
            enemyHardProb += currentWave/100.0f;
            enemyEasyProb -= (1.5f*(11 - (currentWave)))/100.0f;
            enemyMediumProb = 1 - enemyHardProb - enemyEasyProb;
        }

        //enemyCount += currentWave;
        currentWave++;
        uiManager.ShowEventText("Next Wave: " + currentWave);
    }

	/// <summary>
	/// Spawns a random enemy.
	/// </summary>
    void SpawnEnemy()
    {
        Vector3 spawnPosition = ComputeSpawnPosition(enemySpawnRadius, enemySpawnAngle, enemyYPosMin, enemyYPosMax);
        Quaternion spawnRotation = Quaternion.identity;

        float alpha = UnityEngine.Random.Range(0.0f, 1.0f);
        if(alpha <= enemyEasyProb)
        {
            Instantiate(Resources.Load("Prefabs/Enemies/EnemyEasy"), spawnPosition, spawnRotation);

        }
        else if(alpha >= 1- enemyHardProb)
        {
            Instantiate(Resources.Load("Prefabs/Enemies/EnemyHard"), spawnPosition, spawnRotation);
        }
        else
        {
            Instantiate(Resources.Load("Prefabs/Enemies/EnemyMedium"), spawnPosition, spawnRotation);
        }


        //Instantiate(enemy, spawnPosition, spawnRotation);

    }

	/// <summary>
	/// Spawns a random power up.
	/// </summary>
    void SpawnPowerUp()
    {
        GameObject powerUp = powerUpManager.PickOne();
        Vector3 spawnPosition = ComputeSpawnPosition(powerUpSpawnRadius, powerUpSpawnAngle, powerUpYPosMin, powerUpYPosMax);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(powerUp, spawnPosition, spawnRotation);
    }
    /// <summary>
    /// Computes a random spawn position with the given parameters
    /// </summary>
    /// <param name="spawnDistance"> the spawn distance from the player </param>
    /// <param name="spawnAngle"> the spawn angle in front of the player </param>
    /// <param name="yPosMin"> the minimum spawn y-position </param>
    /// <param name="yPosMax"> the maximum spawn y-position </param>
    /// <returns></returns>
    private Vector3 ComputeSpawnPosition(float spawnDistance, int spawnAngle, float yPosMin, float yPosMax)
    {
        // random value in the defined spawnAngle
        float alphaDeg = UnityEngine.Random.Range(90 - spawnAngle / 2, 90 + spawnAngle / 2);

        // convert from degree to radiance
        float alphaRad = alphaDeg * Mathf.Deg2Rad;

        // compute x and z position based on the random value, y pos is a random value between given yPosMin and xPosMax
        float xPos = Mathf.Cos(alphaRad) * spawnDistance;
        float yPos = UnityEngine.Random.Range(yPosMin, yPosMax);
        float zPos = Mathf.Sin(alphaRad) * spawnDistance;

        return player.transform.position + new Vector3(xPos, yPos, zPos);
    }

	/// <summary>
	/// Makes the game Game over !.
	/// Showing Game Over text and saving the made score.
	/// </summary>
    internal void GameOver()
    {
        if(!gameOver)
        {
            uiManager.ShowEventText("Game Over !");
            gameOver = true;
            scoreManager.SaveScore(currentWave);
        }
    }

}
