using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour {

    public enum PowerUpTypes {DoubledShotSpeed, HalfedShotSpeed, DoubledScore, HalfedScore, LiveUp, LiveDown, EnemySmaller, EnemyBigger, EnemyFreeze, EnemyDoubledSpeed};
    
    private RaycastShooting raycastShooting;
    private ScoreManager scoreManager;
    private LivesManager livesManager;
    private UIManager uiManager;

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
            uiManager = gameControllerObject.GetComponent<UIManager>();
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


    public void ApplyPowerUp(PowerUpTypes type, float duration)
    {
        switch (type)
        {
            case PowerUpTypes.DoubledShotSpeed:
                StartCoroutine(DoubledShotSpeed(duration));
                break;
            case PowerUpTypes.HalfedShotSpeed:
                StartCoroutine(HalfedScore(duration));
                break;
            case PowerUpTypes.DoubledScore:
                StartCoroutine(DoubledScore(duration));
                break;
            case PowerUpTypes.HalfedScore:
                StartCoroutine(HalfedScore(duration));
                break;
            case PowerUpTypes.LiveUp:
                LiveUp();
                break;
            case PowerUpTypes.LiveDown:
                LiveDown();
                break;
            case PowerUpTypes.EnemySmaller:
                StartCoroutine(EnemySmaller(duration));
                break;
            case PowerUpTypes.EnemyBigger:
                StartCoroutine(EnemyBigger(duration));
                break;
            case PowerUpTypes.EnemyFreeze:
                StartCoroutine(EnemyFreeze(duration));
                break;
            case PowerUpTypes.EnemyDoubledSpeed:
                StartCoroutine(EnemyDoubledSpeed(duration));
                break;
            default:
                Debug.Log("PowerUp type not implemented");
                break;
        }
    }

    public void DisplayPowerUp(PowerUpTypes type, float duration)
    {
        String text = "PowerUp '" + type.ToString() + "' got activiated (" + duration + "s) !";
        uiManager.ShowEventText(text, 1f);
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

    /// <summary>
    /// randomly picks a power up and return the powerUpType 
    /// </summary>
    /// <returns></returns>
    internal GameObject PickOne()
    {
        int randomIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(PowerUpTypes)).Length);
        PowerUpTypes powerUpType = (PowerUpTypes) randomIndex;
        return getPowerUpPrefab(powerUpType);

    }

    private GameObject getPowerUpPrefab(PowerUpTypes type)
    {
        string powerUpString = type.ToString();
        return Resources.Load("Prefabs/PowerUps/" + powerUpString) as GameObject;
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
