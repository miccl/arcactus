using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HighscoreManager : MonoBehaviour {

	/// <summary>
	/// The position text.
	/// </summary>
    public Text positionText;
	/// <summary>
	/// The score text.
	/// </summary>
    public Text scoreText;
	/// <summary>
	/// The wave text.
	/// </summary>
    public Text waveText;

    internal const string HSCORE = "HScore";
    internal const string HSCOREWAVE = "HScoreWave";

    public int highscoreNumber = 7;

    // Use this for initialization
    void Start () {
        UpdateHighscore();
    }

    /// <summary>
    /// Initializes the highscore manager.
    /// </summary>
    private void Init()
    {
        positionText.text = "";
        scoreText.text = "";
        waveText.text = "";
    }

    /// <summary>
    /// Updates the highscore canvas.
    /// </summary>
    void UpdateHighscore()
    {
        Init();
        for (int i = 0; i < highscoreNumber; i++)
        {
            if (PlayerPrefs.HasKey(i + HSCORE))
            {
                positionText.text += i+1 + "." + "\n";
                scoreText.text += PlayerPrefs.GetInt(i + HSCORE) + "\n";
                waveText.text += PlayerPrefs.GetInt(i + HSCOREWAVE) + "\n";
            }
        }
    }

	/// <summary>
	/// Adds a score to the highscore.
	/// </summary>
	/// <param name="score"> The score to add. </param>
	/// <param name="wave"> The reached wave. </param>
    internal void AddHighscore(int score, int wave)
    {
        int newScore, oldScore;
        int newWave, oldWave;

        newScore = score;
        newWave = wave;
        for (int i = 0; i < highscoreNumber; i++)
        {
            if (PlayerPrefs.HasKey(i + HSCORE))
            {
                if (PlayerPrefs.GetInt(i + HSCORE) < newScore)
                {
                    // new score is higher than the stored score
                    oldScore = PlayerPrefs.GetInt(i + HSCORE);
                    oldWave = PlayerPrefs.GetInt(i + HSCOREWAVE);
                    PlayerPrefs.SetInt(i + HSCORE, newScore);
                    PlayerPrefs.SetInt(i + HSCOREWAVE, newWave);
                    newScore = oldScore;
                    newWave = oldWave;
                }
            }
            else
            {
                PlayerPrefs.SetInt(i + HSCORE, newScore);
                PlayerPrefs.SetInt(i + HSCOREWAVE, newWave);
                break;
            }
        }

        UpdateHighscore();
    }

}
