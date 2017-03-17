using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LivesManager : MonoBehaviour
{

	/// <summary>
	/// The initial lives of the player.
	/// </summary>
    public int startLives = 3;
	/// <summary>
	/// The current lives of the player.
	/// </summary>
    internal float lives;

    public RawImage life1;
    public RawImage life2;
    public RawImage life3;
    public RawImage halflife1;
    public RawImage halflife2;
    public RawImage halflife3;

	/// <summary>
	/// The game controller.
	/// </summary>
    private GameController gameController;
	/// <summary>
	/// The score manager.
	/// </summary>
    private ScoreManager scoreManager;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
            scoreManager = gameControllerObject.GetComponent<ScoreManager>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        Init();
    }

    /// <summary>
    /// Initializes the lives manager.
    /// </summary>
    public void Init()
    {
        lives = startLives;
        UpdateLives();
    }

    /// <summary>
    /// Applies the damage to the player.
    /// </summary>
    /// <param name="damageValue"> The taken damage </param>
    ///
    ///
    ///



    public void ApplyDamage(float damageValue)
    {
        if (!gameController.gameOver)
        {
            lives -= damageValue;
            ScreenFlash sf = gameObject.GetComponentInChildren<ScreenFlash>();
            //sf.StartFlash();
            UpdateLives();
            if (lives <= 0)
            {
                Dead();
            }
        }
    }

	/// <summary>
	/// Updates the lives hud.
	/// </summary>
    void UpdateLives()
    {
        halflife1.enabled = (lives >= .5f && lives < 1);
        life1.enabled = (lives >= 1);
        halflife2.enabled = (lives >= 1.5f && lives < 2);
        life2.enabled = (lives >= 2);
        halflife3.enabled = (lives >= 2.5f && lives < 3);
        life3.enabled = (lives >= 3);
    }

	/// <summary>
	/// Dead this instance.
	/// </summary>
    void Dead()
    {
        //TODO Explosion
        gameController.GameOver();
    }

	/// <summary>
	/// Adds a live.
	/// </summary>
    internal void AddLive()
    {
        if (startLives < lives)
        {
            lives += 1;
        }
        else
        {
            scoreManager.AddScore(100);
        }
    }
}
