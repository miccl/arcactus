using UnityEngine;
using System.Collections;

public class ThornController : MonoBehaviour {

    public float speed;

    private Rigidbody rb;
    private GameController gameController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (transform.up) * speed;

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
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            gameController.AddScore(enemyController.scoreValue);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}
