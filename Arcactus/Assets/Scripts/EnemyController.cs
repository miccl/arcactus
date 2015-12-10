using UnityEngine;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {

    /// <summary>
    /// needed shots from the player to destroy the enemy
    /// </summary>
    public float lives;

    /// <summary>
    /// speed initialised in the creation of the enemy
    /// </summary>
    public float startSpeed;


    /// <summary>
    /// current movement speed of the enemy
    /// </summary>
    internal float speed;

    /// <summary>
    /// score value of the enemy if the player destroys it
    /// </summary>
    public int scoreValue;
    /// <summary>
    /// damage of the enemy
    /// </summary>
    public float damage;

    private Rigidbody rb;
    private Transform playerTransform;
    private ScoreManager scoreManager;
    private TextMesh scoreText;

    AudioSource balloonPopSound;
    AudioSource balloonBounce;

    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        balloonPopSound = audios[0];
        balloonBounce = audios[1];

        speed = startSpeed;
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

        scoreText = GetComponentInChildren<TextMesh>();
        scoreText.text = "";

    }

    public void ApplyDamage(float taken_damage)
    {
        lives -= taken_damage;
        if(lives <= 0)
        {
            Dead();
        } else
        {
            balloonBounce.Play();
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
        scoreText.text = scoreValue.ToString();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        rb.velocity = new Vector3(0, 0, 0);
        speed = 0;
        balloonPopSound.Play();
        StartCoroutine(startExplosion());
    }

    private IEnumerator startExplosion()
    {

        yield return new WaitForSeconds(1f);
        scoreText.text = "";
        Destroy(gameObject);

    }
}
