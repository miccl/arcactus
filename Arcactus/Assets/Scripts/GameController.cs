using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {


    public GameObject enemy;
    public GameObject player;

    [Header("Wave Settings")]
    public float yPosMin = 0.0f;
    public float yPosMax = 5.0f;
    public float startWait = 1.0f;
    public float waveWait = 4.0f;
    public float spawnWait = 0.5f;
    public int waveCount = 10;

    public float spawnRadius = 100f;



    [Header("Text")]
    public Text gameOverText;
    public Text pauseText;


    internal bool paused;
    internal bool gameOver;
    private bool restart;


    // Use this for initialization
    void Start () {

        gameOver = false;
        restart = false;
        gameOverText.text = "";
        pauseText.text = "";
        paused = false;

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
            for (int i = 0; i < waveCount; i++)
            {
                float alpha = Random.Range(0, Mathf.PI);

                float xPos = Mathf.Cos(alpha) * spawnRadius;
                float yPos = Random.Range(yPosMin, yPosMax);
                float zPos = Mathf.Sin(alpha) * spawnRadius;
               
                Vector3 spawnPosition = player.transform.position + new Vector3(xPos, yPos, zPos);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemy, spawnPosition, spawnRotation);
             
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                restart = true;
                break;
            }
        }
    }



    internal void GameOver()
    {
        gameOverText.text = "Game Over !";
        gameOver = true;
    }

}
