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
    /// duration time of the powerUp if activated
    /// </summary>
    public float duration = 5;

    /// <summary>
    /// The powerUp title
    /// </summary>
    public string title;

	/// <summary>
	/// The power up manager.
	/// </summary>
    private PowerUpManager powerUpManager;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            powerUpManager = gameControllerObject.GetComponent<PowerUpManager>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        StartCoroutine(WaitAndDestroy());
        
    }

	/// <summary>
	/// Waits for the lifetime and then destroys the power up.
	/// </summary>
	/// <returns>The and destroy.</returns>
    private IEnumerator WaitAndDestroy()
    {
		yield return new WaitForSeconds(lifetime);
        Dead();
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
	/// Dead this instance.
	/// </summary>
    void Dead()
    {
        Destroy(gameObject);
    }


}
