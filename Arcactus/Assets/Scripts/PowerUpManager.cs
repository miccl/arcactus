using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour {

    public enum PowerUpType { None, DoubledShotSpeed, HalfedShotSpeed, DoubledScore, HalfedScore, LiveUp, LiveDown, EnemySmaller, EnemyFreeze, EnemyDoubledSpeed, EnemyBigger };
    
    private RaycastShooting raycastShooting;
    private ScoreManager scoreManager;
    private LivesManager livesManager;
    public Text powerUpText;

    void Start () {
        GameObject armObject = GameObject.FindWithTag("Arm");
        if (armObject != null)
        {
            raycastShooting = armObject.GetComponent<RaycastShooting>();
        }
        else
        {
            Debug.Log("Cannot find 'ArmObject' script");
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

        powerUpText.text = "";
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
            case PowerUpType.EnemyBigger:
                StartCoroutine(EnemyBigger(duration));
                break;
            case PowerUpType.EnemyFreeze:
                StartCoroutine(EnemyFreeze(duration));
                break;
            case PowerUpType.EnemyDoubledSpeed:
                StartCoroutine(EnemyDoubledSpeed(duration));
                break;
            default:
                Debug.Log("PowerUp type not implemented");
                break;
        }

        StartCoroutine(DisplayPowerUp(type, duration));

    }

    private IEnumerator DisplayPowerUp(PowerUpType type, float duration)
    {
        String text = "PowerUp '" + type.ToString() + "' got activiated (" + duration + "s) !";
        powerUpText.text = text;
        yield return new WaitForSeconds(1f);
        if(powerUpText.text == text)
        {
            powerUpText.text = "";
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
        ScaleEnemies(enemies, 0.5f);
        yield return new WaitForSeconds(duration);
        ScaleEnemies(enemies, 2f);
    }

    IEnumerator EnemyBigger(float duration)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        ScaleEnemies(enemies, 2f);
        yield return new WaitForSeconds(duration);
        ScaleEnemies(enemies, 0.5f);
    }

    void ScaleEnemies(GameObject[] enemies, float scale)
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                ScaleMesh(enemy.GetComponent<MeshFilter>().mesh, scale);
                ScaleCapsuleCollider(enemy.GetComponent<CapsuleCollider>(), scale);
            }
        }
    }

    void ScaleCapsuleCollider(CapsuleCollider collider, float scale)
    {
        collider.height *= scale;
        collider.radius *= scale;
    }


    void ScaleMesh(Mesh mesh, float scale)
    {
        Vector3[] vertices = mesh.vertices;
        int p = 0;
        while (p < vertices.Length)
        {
            vertices[p] *= scale;
            p++;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }


    IEnumerator EnemyFreeze(float duration)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if(enemy != null)
            {
                Rigidbody rb = enemy.GetComponent<Rigidbody>();
                rb.velocity = new Vector3(0, 0, 0);
                EnemyController enemyController = enemy.gameObject.GetComponent<EnemyController>();
                enemyController.speed = 0;
            }

        }

        yield return new WaitForSeconds(duration);

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                EnemyController enemyController = enemy.gameObject.GetComponent<EnemyController>();
                enemyController.speed = enemyController.startSpeed;
            }
        }
    }

    IEnumerator EnemyDoubledSpeed(float duration)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                EnemyController enemyController = enemy.gameObject.GetComponent<EnemyController>();
                enemyController.speed *= 2;
            }

        }

        yield return new WaitForSeconds(duration);

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                EnemyController enemyController = enemy.gameObject.GetComponent<EnemyController>();
                enemyController.speed /= 2;
            }
            else
            {
                Debug.Log("enemy null");
            }

        }
    }

}
