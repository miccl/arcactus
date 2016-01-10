using UnityEngine;
using System.Collections;

public class ManagerController : MonoBehaviour {
    private GameController gameController;

    public void Start()
    {
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

    /// <summary>
    /// Starts a scene with the given name.
    /// </summary>
    /// <param name="scene">The name of the scene.</param>
    public void StartGame()
    {
        StartGame();
    }

}
