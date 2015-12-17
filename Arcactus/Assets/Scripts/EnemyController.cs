using UnityEngine;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {

    /// <summary>
    /// The lives of the enemy.
    /// </summary>
    public float lives;

    /// <summary>
    /// The initial movement speed of the enemy.
    /// </summary>
    public float startSpeed;


    /// <summary>
    /// The current movement speed of the enemy
    /// </summary>
    internal float speed;

    /// <summary>
    /// The score value of the enemy if the player defeats it.
    /// </summary>
    public int scoreValue;
    /// <summary>
    /// The damage of the enemy.
    /// </summary>
    public float damage;

	/// <summary>
	/// The rigidbody of the enemy.
	/// </summary>
    private Rigidbody rb;
	/// <summary>
	/// The player transform to calculate the movement direction.
	/// </summary>
    private Transform playerTransform;
	/// <summary>
	/// The score manager.
	/// </summary>
    private ScoreManager scoreManager;
	/// <summary>
	/// The score text.
	/// </summary>
    private TextMesh scoreText;

	/// <summary>
	/// The balloon pop sound.
	/// </summary>
    AudioSource balloonPopSound;
	/// <summary>
	/// The balloon bounce sound.
	/// </summary>
    AudioSource balloonBounceSound;

    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        balloonPopSound = audios[0];
        balloonBounceSound = audios[1];

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

	/// <summary>
	/// Applies damage to the enemy.
	/// </summary>
	/// <param name="taken_damage"> The taken damage.</param>
    public void ApplyDamage(float taken_damage)
    {
        lives -= taken_damage;
        if(lives <= 0)
        {
            Dead();
        } else
        {
            balloonBounceSound.Play();
        }
    }

	/// <summary>
	/// Updates the movement of the enemy.
	/// </summary>
    void FixedUpdate()
    {
        Vector3 direction = (playerTransform.position- transform.position).normalized;
        rb.AddForce(direction * speed);
    }

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collision"> The collision. </param>
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

	/// <summary>
	/// Dead function.
	/// Destroys enemy, add score, plays sound...
	/// </summary>
    void Dead()
    {
        int scoreMulitplied = scoreManager.AddScore(scoreValue);
        scoreText.text = scoreMulitplied.ToString();

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        rb.velocity = new Vector3(0, 0, 0);
        speed = 0;

        balloonPopSound.Play();
        StartCoroutine(startExplosion());
    }

	/// <summary>
	/// Starts the explosion of the enemy.
	/// </summary>
	/// <returns>The explosion.</returns>
    private IEnumerator startExplosion()
    {

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }
}
