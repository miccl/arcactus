using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {


    public Text scoreText;
    public Text newHighscoreText;

    private int score = 0;

    //Boolean, um nur das erste Mal anzuzeigen, dass der Spieler einen neuen Highscore hat.
    private bool newHighscore;
    private int highscore;

    private GameController gameController;

    // Use this for initialization
    void Start () {

        //Init things
        score = 0;
        UpdateScoreText();
        highscore = PlayerPrefs.GetInt("highscore", 0);
        newHighscoreText.text = "";
        newHighscore = true;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void AddScore(int scoreValue)
    {
        if (!gameController.gameOver)
        {
            score += scoreValue;
            if (score > highscore)
            {
                PlayerPrefs.SetInt("highscore", score);
                if (newHighscore)
                {
                    newHighscoreText.text = "New Highscore!";
                    newHighscore = false;
                }
                else
                {
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
