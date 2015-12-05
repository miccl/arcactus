using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LivesManager : MonoBehaviour {

    public int lives_count = 3;
    internal float lives;

    public RawImage life1;
    public RawImage life2;
    public RawImage life3;

    private GameController gameController;
    private ScoreManager scoreManager;

    void Start () {
        lives = lives_count;

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


    }

    // Update is called once per frame
    void Update () {
	
	}


    public void ApplyDamage(float damageValue)
    {
        lives -= damageValue;
        UpdateLives();
        if (lives <= 0)
        {
            Dead();
        }
    }

    void UpdateLives()
    {
        life1.enabled = (lives >= 1);
        life2.enabled = (lives >= 2);
        life3.enabled = (lives >= 3);
    }


    void Dead()
    {
        //TODO Explosion
        gameController.GameOver();
    }

    internal void AddLive()
    {
        if (lives_count < lives)
        {
            lives += 1;
        }
        else
        {
            scoreManager.AddScore(100);
        }
    }
}
