using UnityEngine;
using System.Collections;

public class ThornController : MonoBehaviour {

    /// <summary>
    /// The movement speed of the thorn.
    /// </summary>
    public float speed = 100;

	/// <summary>
	/// The rigidbody of the thorn.
	/// </summary>
    private Rigidbody rb;

	/// <summary>
	/// Start this instance.
	/// Moving the thorn forward with the given speed.
	/// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (transform.forward) * speed;
    }
}
