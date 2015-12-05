using UnityEngine;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {

    /// <summary>
    /// needed shots from the player to destroy the enemy
    /// </summary>
    public int lives;
    /// <summary>
    /// movement speed of the enemy
    /// </summary>
    public float speed;
    /// <summary>
    /// score value of the enemy if the player destroys it
    /// </summary>
    public int scoreValue;
    /// <summary>
    /// damage of the enemy
    /// </summary>
    public int damage;

    private Rigidbody rb;
    private Transform playerTransform;
    private ScoreManager scoreManager;


    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            scoreManager = gameControllerObject.GetComponent<ScoreManager>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }

    }

    public void ApplyDamage(int taken_damage)
    {
        lives -= taken_damage;
        if(lives <= 0)
        {
            Dead();
        }
    }

    void FixedUpdate()
    {
        Vector3 direction = (playerTransform.position- transform.position).normalized;
        rb.AddForce(direction * speed);

    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == "Player")
        {
            LivesManager lv = other.gameObject.GetComponent<LivesManager>();
            lv.ApplyDamage(damage);
            Destroy(gameObject);
        }
    }

    void Dead()
    {
        scoreManager.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
