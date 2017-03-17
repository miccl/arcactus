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
    /// The prefab of the scoreText
    /// </summary>
    public GameObject scoreTextPrefab;

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
	/// The balloon pop sound.
	/// </summary>
    public AudioClip balloonPopSound;
    /// <summary>
    /// The balloon bounce sound.
    /// </summary>
    public AudioClip balloonBounceSound;

    private Color textColor;
    private GameController gameController;
    private AudioManager audioManager;

    void Start()
    {

        speed = startSpeed;
        textColor = ColorUtils.ChangeColorBrightness(GetComponent<MeshRenderer>().material.color, 0.2f);
        playerTransform = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();


        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            scoreManager = gameControllerObject.GetComponent<ScoreManager>();
            gameController = gameControllerObject.GetComponent<GameController>();
            audioManager = gameControllerObject.GetComponent<AudioManager>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

	/// <summary>
	/// Applies damage to the enemy.
	/// </summary>
	/// <param name="taken_damage"> The taken damage.</param>
    public void ApplyDamage(float taken_damage)
    {
        lives -= taken_damage;

        //GetComponent<MeshRenderer>().material.color = ColorUtils.ChangeColorBrightness(GetComponent<MeshRenderer>().material.color, 0.2f);
        if(!gameController.gameOver)
        {
            int scoreMulitplied = scoreManager.AddScore(scoreValue);
            DisplayScoreText(scoreMulitplied);
        }

        if (lives <= 0)
        {
            Dead();
        } else
        {
            audioManager.PlaySound(balloonBounceSound);
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
        audioManager.PlaySound(balloonPopSound);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        Destroy(gameObject, balloonBounceSound.length);
    }

    /// <summary>
    /// Dynamically displays a score text over the enemy.
    /// </summary>
    /// <param name="scoreValue"></param>
	private void DisplayScoreText(int scoreValue) {
        Vector3 verschiebung = new Vector3(0, 1.5f, 0);
		GameObject scoreText = Instantiate(scoreTextPrefab, transform.position + verschiebung , transform.rotation) as GameObject;
		ScoreText scoreTextController = scoreText.GetComponent<ScoreText>();
		scoreTextController.SetText("+" + scoreValue.ToString());
        //Color textColor = GetComponent<MeshRenderer>().material.color;
        scoreTextController.SetTextColor(textColor);

    }
}
