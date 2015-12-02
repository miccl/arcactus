using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour {

    public int lives_count = 3;
    private int lives;

    public RawImage life1;
    public RawImage life2;
    public RawImage life3;

    private GameController gameController;


    // Use this for initialization
    void Start () {
        lives = lives_count;

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

    public void ApplyDamage(int damageValue)
    {
        lives -= damageValue;
        UpdateLives();
        if (lives <= 0)
        {
            gameController.GameOver();
        }
    }

    void UpdateLives()
    {
        life1.enabled = (lives >= 1);
        life2.enabled = (lives >= 2);
        life3.enabled = (lives >= 3);
    }


}
