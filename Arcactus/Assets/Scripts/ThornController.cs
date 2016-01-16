using UnityEngine;
using System.Collections;

public class ThornController : MonoBehaviour {

    /// <summary>
    /// The movement speed of the thorn.
    /// </summary>
    public float speed = 100;

    /// <summary>
    /// The sound of a shooted thorn.
    /// </summary>
    public AudioClip thornShootSound;

    /// <summary>
    /// The rigidbody of the thorn.
    /// </summary>
    private Rigidbody rb;
    private AudioManager audioManager;

    void Awake()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            audioManager = gameControllerObject.GetComponent<AudioManager>();
        }
    }

    /// <summary>
    /// Start this instance.
    /// Moving the thorn forward with the given speed.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (transform.forward) * speed;
        audioManager.PlaySound(thornShootSound);
    }
}
