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

    private bool gameOver;
    private bool restart;

    private int score = 0;
    public int lives_count;
    private int lives;

    [Header("Text")]
    public Text scoreText;
    public Text newHighscoreText;
    public Text gameOverText;
    public Text pauseText;
	public RawImage life1;
	public RawImage life2;
	public RawImage life3;
	//Boolean, um nur das erste Mal anzuzeigen, dass der Spieler einen neuen Highscore hat.

	private bool newHighscore;
	private int highscore;

    private bool paused;


    // Use this for initialization
    void Start () {

        score = 0;
        lives = lives_count;
        UpdateScoreText();
		highscore = PlayerPrefs.GetInt("highscore", 0);
        gameOver = false;
        restart = false;
        newHighscoreText.text = "";
        gameOverText.text = "";
        pauseText.text = "";
        paused = false;
		newHighscore = true;

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

        if (Input.GetButtonDown("Start"))
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

    public void AddScore(int scoreValue)
    {
        if(!gameOver)
        {
            score += scoreValue;
			if (score > highscore) {
				PlayerPrefs.SetInt ("highscore", score);
				if (newHighscore) {
					newHighscoreText.text = "New Highscore!";
					newHighscore = false;
				} else {
					newHighscoreText.text = "";
				}
				highscore = score;
			}
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void ApplyDamage(int damageValue)
    {
        lives -= damageValue;
		UpdateLives();
        if(lives <= 0) {
            GameOver();
        }
    }

	void UpdateLives()
	{
		life1.enabled = (lives >= 1);
		life2.enabled = (lives >= 2);
		life3.enabled = (lives >= 3);
	}

    void GameOver()
    {
        gameOverText.text = "Game Over !";
        gameOver = true;
    }

    //void AddHighscore(string name, int score)
    //{
    //    int newScore, oldScore;
    //    string newName, oldName;
    //    const string HSCORE = "HScore";
    //    const string HSCORENAME = "HScoreName";
    //    newScore = score;
    //    newName = name;
    //    for(int i=0;i<10;i++)
    //    {
    //        if(PlayerPrefs.HasKey(i + HSCORE))
    //        {
    //            if(PlayerPrefs.GetInt(i + HSCORE) < newScore)
    //            {
    //                //new score ist higher than stored score
    //                oldScore = PlayerPrefs.GetInt(i + HSCORE);
    //                oldName = PlayerPrefs.GetString(i + HSCORE);
    //                PlayerPrefs.SetInt(i + HSCORE, newScore);
    //                PlayerPrefs.SetString(i + HSCORENAME, newName);

    //                newScore = oldScore;
    //                newName = oldName;
    //            } 
    //            else
    //            {
    //                PlayerPrefs.SetInt(i + HSCORE, newScore);
    //                PlayerPrefs.SetString(i + HSCORENAME, newName);
    //                newScore = 0;
    //                newName = "";
    //            }
    //        }
    //    }
    //}
}
