using UnityEngine;
using System.Collections;

public class ThornController : MonoBehaviour {

    /// <summary>
    /// movement speed of the thorn
    /// </summary>
    public float speed = 100;

    /// <summary>
    /// damage caused by hit
    /// </summary>
    public int damage = 1;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (transform.forward) * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            enemyController.ApplyDamage(damage);
            //Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}
