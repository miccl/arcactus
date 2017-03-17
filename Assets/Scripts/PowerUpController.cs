using UnityEngine;
using System.Collections;
using System;

public class PowerUpController : MonoBehaviour {

	/// <summary>
	/// The lives of the power up.
	/// </summary>
    public float lives = 1;
	/// <summary>
	/// The type of the power up.
	/// </summary>
    public PowerUpManager.PowerUpTypes type;
    /// <summary>
    /// The lifetime the power up. 
    /// </summary>
    public float lifetime = 5;
    /// <summary>
    /// The duration time of the powerUp if activated.
    /// </summary>
    public float duration = 5;

    /// <summary>
    /// The powerUp title.
    /// </summary>
    public string title;

    /// <summary>
    /// The sound of a powerUp pop.
    /// </summary>
    public AudioClip powerUpPopSound;

    private PowerUpManager powerUpManager;
    private AudioManager audioManager;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            powerUpManager = gameControllerObject.GetComponent<PowerUpManager>();
            audioManager = gameControllerObject.GetComponent<AudioManager>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        Destroy(gameObject, lifetime);
    }

	/// <summary>
	/// Applies damage to the power up.
	/// </summary>
	/// <param name="damage">The taken damage.</param>
    public void ApplyDamage(float damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            powerUpManager.ApplyPowerUp(type, duration);
            powerUpManager.DisplayPowerUp(type, duration, title);
            Dead();
        }
    }

	/// <summary>
	/// Destroys this instance.
	/// </summary>
    void Dead()
    {
        audioManager.PlaySound(powerUpPopSound);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject, powerUpPopSound.length);

    }


}
