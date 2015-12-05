using UnityEngine;
using System.Collections;

public class ThornController : MonoBehaviour {

    /// <summary>
    /// movement speed of the thorn
    /// </summary>
    public float speed = 100;


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (transform.forward) * speed;
    }
}
