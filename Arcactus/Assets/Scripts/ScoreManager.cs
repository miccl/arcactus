using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour {

	/// <summary>
	/// The score text.
	/// </summary>
    public Text scoreText;

	/// <summary>
	/// The total score.
	/// </summary>
    private int score = 0;
	/// <summary>
	/// The score multiplier.
	/// </summary>
    internal float scoreMultiplier = 1;

	/// <summary>
	/// Whether the score text was shown before or not.
	/// </summary>
    private bool newHighscore;
	/// <summary>
	/// The current highscore.
	/// </summary>
    private int highscore;

	/// <summary>
	/// The game controller.
	/// </summary>
    private GameController gameController;
	/// <summary>
	/// The user interface manager.
	/// </summary>
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

	/// <summary>
	/// Adds a score value to the total score.
	/// </summary>
	/// <param name="scoreValue">The score value.</param>
    public int AddScore(int scoreValue)
    {
        int currScoreValue = 0;
        if (!gameController.gameOver)
        {
            currScoreValue = (int) (scoreValue * scoreMultiplier);
            score += currScoreValue;
            if (score > highscore)
            {
                //PlayerPrefs.SetInt("highscore", score);
                if (newHighscore)
                {
                    uiManager.ShowEventText("New Highscore !", 2.0f);
                    newHighscore = false;
                }
                highscore = score;
            }
            UpdateScoreText();
        }

        return currScoreValue;
    }
	/// <summary>
	/// Updates the score text.
	/// </summary>
    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }


	/// <summary>
	/// Saves the current score with the reached wave using <see cref="HighscoreController"/>.
	/// </summary>
	/// <param name="wave">The reached wave</param>
    internal void SaveScore(int wave)
    {
        HighscoreController.AddHighscore(score, wave);
    }
}
