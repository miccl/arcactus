using UnityEngine;
using System.Collections;

/// <summary>
/// Script to implement raycast shooting.
/// </summary>
public class RaycastShooting : MonoBehaviour {


    /// <summary>
    /// The object to shoot.
    /// </summary>
    public GameObject shot;

    /// <summary>
    /// The damage of the shot.
    /// </summary>
	public float shotDamage = 1.0f;

    public Transform ShotSpawn;

    /// <summary>
    /// The range of the shot.
    /// </summary>
    int shotRange = 100;



    /// <summary>
    /// The fire rate of the ray.
    /// </summary>
    [Range(0.1f, 2)]
    public float fireRate = 0.5f;


    /// <summary>
    /// The allowed time to shot the next fire under the given fireRate
    /// </summary>
    private float nextFire;

	/// <summary>
	/// The hit.
	/// </summary>
    RaycastHit hit;

    /// <summary>
    /// the radius of the raycast
    /// </summary>
    [Range(0.1f, 0.5f)]
    public float raycastRadius = 0.25f;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }


    void Update()
    {
        // ray in the middle of the play screen
        Vector3 shotSpawnPosition = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f);
        Ray ray = Camera.main.ScreenPointToRay(shotSpawnPosition);

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && !gameController.paused && !gameController.gameOver && gameController.gameRunning)
        {
            nextFire = Time.time + fireRate;

			Instantiate(shot, ShotSpawn.position, ShotSpawn.rotation);
        }
    }

}
