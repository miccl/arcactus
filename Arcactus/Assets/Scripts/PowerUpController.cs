using UnityEngine;
using System.Collections;
using System;

public class PowerUpController : MonoBehaviour {

    public float lives = 1;
    public PowerUpManager.PowerUpTypes type;
    /// <summary>
    /// time the powerUp 
    /// </summary>
    public float lifetime = 5;
    /// <summary>
    /// duration time of the powerUp if activated
    /// </summary>
    public float duration = 5;

    private PowerUpManager powerUpManager;
    private float startTime;

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

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(lifetime);
        Dead();
    }
    public void ApplyDamage(float damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            powerUpManager.ApplyPowerUp(type, duration);
            powerUpManager.DisplayPowerUp(type, duration);
            Dead();
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }


}
