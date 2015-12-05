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
    public float spawnWait = 0.5f;
    public int waveCount = 10;


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

    internal bool paused;
    internal bool gameOver;
    private bool restart;
    private int currentWave;


    // Use this for initialization
    void Start () {

        gameOver = false;
        restart = false;
        gameOverText.text = "";
        pauseText.text = "";
        paused = false;

        currentWave = 0;
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
            currentWave++;
            for (int i = 0; i < waveCount; i++)
            {
                SpawnEnemy(i);
                yield return new WaitForSeconds(spawnWait);
            }
            for(int i = 0; i < 15; i++)
            {
                SpawnPowerUp();
            }

            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                restart = true;
                break;
            }
        }
    }

    void SpawnEnemy(int wave)
    {
        Vector3 spawnPosition = ComputeSpawnPosition(enemySpawnRadius, enemyYPosMin, enemyYPosMax);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(enemy, spawnPosition, spawnRotation);

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
