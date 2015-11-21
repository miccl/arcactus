using UnityEngine;
using System.Collections;

public class EnemyMover : MonoBehaviour {

    private Rigidbody rb;
    private Transform playerTransform;

    public float speed = 1;    

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = (playerTransform.position- transform.position).normalized;
        Debug.Log(direction);
        rb.AddForce(direction * speed);

    }
}
