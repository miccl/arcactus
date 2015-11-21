using UnityEngine;
using System.Collections;

public class ThornMover : MonoBehaviour {

    private Rigidbody rb;

    public float speed = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (transform.forward) * speed;
    }
}
