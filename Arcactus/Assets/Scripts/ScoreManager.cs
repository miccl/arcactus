using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour {


    public Text scoreText;
    public Text newHighscoreText;

    private int score = 0;
    internal int scoreMultiplier = 1;

    //Boolean, um nur das erste Mal anzuzeigen, dass der Spieler einen neuen Highscore hat.
    private bool newHighscore;
    private int highscore;

    private GameController gameController;

    // Use this for initialization
    void Start () {

        //Init things
        score = 0;
        scoreMultiplier = 1;
        UpdateScoreText();
        highscore = PlayerPrefs.GetInt(0 + HighscoreController.HSCORE, 0);
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
            score += scoreValue * scoreMultiplier;
            if (score > highscore)
            {
                //PlayerPrefs.SetInt("highscore", score);
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



    internal void SaveScore(int wave)
    {
        HighscoreController.AddHighscore(score, wave);
    }
}
