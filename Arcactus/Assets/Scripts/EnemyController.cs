using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private Rigidbody rb;
    private Transform playerTransform;

    public float speed;
    public int scoreValue;
    public int damage;

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
