using UnityEngine;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {


    public float speed;
    public int scoreValue;
    public int damage;

    private Rigidbody rb;
    private Transform playerTransform;
    private GameController gameController;


    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

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
            gameController.ApplyDamage(damage);
            Destroy(gameObject);
        }
    }
}
