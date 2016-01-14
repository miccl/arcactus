using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour {

	/// <summary>
	/// The raycast shooting.
	/// </summary>
	public enum PowerUpTypes {DoubledShotSpeed, HalfedShotSpeed, DoubledScore, HalfedScore, LiveUp, LiveDown, EnemySmaller, EnemyBigger, EnemyFreeze, EnemyDoubledSpeed};

	/// <summary>
	/// The raycast shooting.
	/// </summary>
    private RaycastShooting raycastShooting;
	/// <summary>
	/// The score manager.
	/// </summary>
    private ScoreManager scoreManager;
	/// <summary>
	/// The lives manager.
	/// </summary>
    private LivesManager livesManager;
	/// <summary>
	/// The user interface manager.
	/// </summary>
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

	/// <summary>
	/// Applies the power up.
	/// </summary>
	/// <param name="type">The power up type to apply.</param>
	/// <param name="duration">The duration of the power up.</param>
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
	/// <summary>
	/// Displaies the power up.
	/// </summary>
	/// <param name="type">The power up type.</param>
	/// <param name="duration">The duration of the power up.</param>
    public void DisplayPowerUp(PowerUpTypes type, float duration, string title)
    {
        if (title == "")
        {
            Debug.LogError("no title defined");
            return;
        }

        if (duration != -1)
            title += " (" + duration + "s) !";
        uiManager.ShowItemActivatedEventText(title, 2f);

    }

    /// <summary>
    /// Doubleds the shot speed.
    /// </summary>
    /// <param name="duration">The duration of the power up.</param>
    IEnumerator DoubledShotSpeed(float duration)
    {
        raycastShooting.fireRate /= 2;
        yield return new WaitForSeconds(duration);
        raycastShooting.fireRate *= 2;
    }

	/// <summary>
	/// Halfeds the shot speed.
	/// </summary>
	/// <param name="duration">Duration.</param>
    IEnumerator HalfedShotSpeed(float duration)
    {
        raycastShooting.fireRate *= 2;
        yield return new WaitForSeconds(duration);
        raycastShooting.fireRate /= 2;

    }

	/// <summary>
	/// Doubleds the score.
	/// </summary>
	/// <returns>The score.</returns>
	/// <param name="duration">The duration of the power up.</param>
	IEnumerator DoubledScore(float duration)
    {
        scoreManager.scoreMultiplier *= 2;
        yield return new WaitForSeconds(duration);
        scoreManager.scoreMultiplier /= 2;
    }

	/// <summary>
	/// Halfeds the score.
	/// </summary>
	/// <returns>The score.</returns>
	/// <param name="duration">Duration.</param>
    IEnumerator HalfedScore(float duration)
    {
        scoreManager.scoreMultiplier /= 2;
        yield return new WaitForSeconds(duration);
        scoreManager.scoreMultiplier *= 2;
    }

	/// <summary>
	/// Adds a player live.
	/// </summary>
    private void LiveUp()
    {
        livesManager.AddLive();
    }

	/// <summary>
	/// Removes a player live.
	/// </summary>
    private void LiveDown()
    {
        livesManager.ApplyDamage(1);
    }

	/// <summary>
	/// Makes all enemies smaller
	/// </summary>
	/// <param name="duration">The duration of the power up.</param>
	IEnumerator EnemySmaller(float duration)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        ScaleEnemies(enemies, 0.5f);
        yield return new WaitForSeconds(duration);
        ScaleEnemies(enemies, 2f);
    }

	/// <summary>
	/// Makes all enemies bigger.
	/// </summary>
	/// <param name="duration">The duration of the power up.</param>
	IEnumerator EnemyBigger(float duration)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        ScaleEnemies(enemies, 2f);
        yield return new WaitForSeconds(duration);
        ScaleEnemies(enemies, 0.5f);
    }

	/// <summary>
	/// Scales the enemies size.
	/// </summary>
	/// <param name="enemies">The enemies to scale.</param>
	/// <param name="scale">The scale factor.</param>
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

	/// <summary>
	/// Scales the capsule collider.
	/// </summary>
	/// <param name="collider">The collider to scale.</param>
	/// <param name="scale">The scale factor.</param>
    void ScaleCapsuleCollider(CapsuleCollider collider, float scale)
    {
        collider.height *= scale;
        collider.radius *= scale;
    }

	/// <summary>
	/// Scales the mesh.
	/// </summary>
	/// <param name="mesh">The mesh to scale.</param>
	/// <param name="scale">The scale factor.</param>
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

	/// <summary>
	/// Stops all enemies from moving.
	/// </summary>
	/// <param name="duration">The duration of the power up.</param>
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
    /// Returns a random power up.
    /// </summary>
    /// <returns>The power up game object.</returns>
    internal GameObject PickOne()
    {
        int randomIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(PowerUpTypes)).Length);
        PowerUpTypes powerUpType = (PowerUpTypes) randomIndex;
        return getPowerUpPrefab(powerUpType);

    }

	/// <summary>
	/// Returns the prefab of the power up with the given type.
	/// </summary>
	/// <returns>The power up prefab.</returns>
	/// <param name="type">The type of the power up.</param>
    private GameObject getPowerUpPrefab(PowerUpTypes type)
    {
        string powerUpString = type.ToString();
        return Resources.Load("Prefabs/PowerUps/" + powerUpString) as GameObject;
    }

	/// <summary>
	/// Doubles enemy speed.
	/// </summary>
	/// <param name="duration">The duration of the power up.</param>
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
