using UnityEngine;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {

    public float speed;
    public int scoreValue;
    public int damage;

    private Rigidbody rb;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = (playerTransform.position- transform.position).normalized;
        rb.AddForce(direction * speed);

    }
}
