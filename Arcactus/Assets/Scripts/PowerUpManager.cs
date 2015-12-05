using UnityEngine;
using System.Collections;
using System;

public class PowerUpManager : MonoBehaviour {

    public enum PowerUpType { None, DoubledShotSpeed, HalfedShotSpeed, DoubledScore, HalfedScore, LiveUp, LiveDown, EnemySmaller, EnemyFreeze };
    
    private RaycastShooting raycastShooting;
    private ScoreManager scoreManager;
    private LivesManager livesManager;

    void Start () {
        GameObject armObject = GameObject.FindWithTag("Arm");
        if (armObject != null)
        {
            raycastShooting = armObject.GetComponent<RaycastShooting>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            scoreManager = gameControllerObject.GetComponent<ScoreManager>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if(playerControllerObject != null)
        {
            livesManager = playerControllerObject.GetComponent<LivesManager>();
        }
    }


    public void ApplyPowerUp(PowerUpType type, float duration)
    {
        switch (type)
        {
            case PowerUpType.None:
                Debug.Log("No powerUp type selected!");
                break;
            case PowerUpType.DoubledShotSpeed:
                StartCoroutine(DoubledShotSpeed(duration));
                break;
            case PowerUpType.HalfedShotSpeed:
                StartCoroutine(HalfedScore(duration));
                break;
            case PowerUpType.DoubledScore:
                StartCoroutine(DoubledScore(duration));
                break;
            case PowerUpType.HalfedScore:
                StartCoroutine(HalfedScore(duration));
                break;
            case PowerUpType.LiveUp:
                LiveUp();
                break;
            case PowerUpType.LiveDown:
                LiveDown();
                break;
            case PowerUpType.EnemySmaller:
                StartCoroutine(EnemySmaller(duration));
                break;
            case PowerUpType.EnemyFreeze:
                StartCoroutine(EnemyFreeze(duration));
                break;
            default:
                Debug.Log("PowerUp type not implemented");
                break;
        }
    }

    IEnumerator DoubledShotSpeed(float duration)
    {
        raycastShooting.fireRate /= 2;
        yield return new WaitForSeconds(duration);
        raycastShooting.fireRate *= 2;
    }

    IEnumerator HalfedShotSpeed(float duration)
    {
        raycastShooting.fireRate *= 2;
        yield return new WaitForSeconds(duration);
        raycastShooting.fireRate /= 2;

    }

    IEnumerator DoubledScore(float duration)
    {
        scoreManager.scoreMultiplier *= 2;
        yield return new WaitForSeconds(duration);
        scoreManager.scoreMultiplier /= 2;
    }

    IEnumerator HalfedScore(float duration)
    {
        scoreManager.scoreMultiplier /= 2;
        yield return new WaitForSeconds(duration);
        scoreManager.scoreMultiplier *= 2;
    }

    private void LiveUp()
    {
        livesManager.AddLive();
    }

    private void LiveDown()
    {
        livesManager.ApplyDamage(1);
    }

    IEnumerator EnemySmaller(float duration)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            Debug.Log("Hallo");
            enemy.transform.localScale += new Vector3(50, 5, 5);
        }
        yield return new WaitForSeconds(duration);


        GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies2.Length; i++)
        {
            enemies2[i].transform.localScale *= 2;
        }

    }

    IEnumerator EnemyFreeze(float duration)
    {
        Debug.Log("EnemyFreeze");        
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(enemy != null)
            {
                EnemyController enemyController = enemy.gameObject.GetComponent<EnemyController>();
                enemyController.speed = 0;
            } else
            {
                Debug.Log("enemy null");
            }

        }

        yield return new WaitForSeconds(duration);


    }

}
