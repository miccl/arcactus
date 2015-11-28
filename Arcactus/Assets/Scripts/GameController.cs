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
    public Texture heartTexture;

	//Boolean, um nur das erste Mal anzuzeigen, dass der Spieler einen neuen Highscore hat.

	private bool newHighscore;
	private int highscore;


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
        if(lives <= 0) {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverText.text = "Game Over !";
        gameOver = true;
    }

    void OnGUI()
    {
		Rect r = new Rect(Screen.width/2, Screen.height/2, Screen.width, Screen.height); //Adjust the rectangle position and size for your own needs
        GUILayout.BeginArea(r);
        GUILayout.BeginHorizontal();

        for (int i = 0; i < lives; i++)
            GUILayout.Label(heartTexture); //assign your heart image to this texture

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
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
