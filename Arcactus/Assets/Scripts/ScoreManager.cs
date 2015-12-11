using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour {


    public Text scoreText;

    private int score = 0;
    internal int scoreMultiplier = 1;

    //Boolean, um nur das erste Mal anzuzeigen, dass der Spieler einen neuen Highscore hat.
    private bool newHighscore;
    private int highscore;

    private GameController gameController;
    private UIManager uiManager;

    // Use this for initialization
    void Start () {

        //Init things
        score = 0;
        scoreMultiplier = 1;
        UpdateScoreText();
        highscore = PlayerPrefs.GetInt(0 + HighscoreController.HSCORE, 0);
        newHighscore = true;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
            uiManager = gameControllerObject.GetComponent<UIManager>();
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
                    uiManager.ShowEventText("New Highscore !", 2f);
                    newHighscore = false;
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
