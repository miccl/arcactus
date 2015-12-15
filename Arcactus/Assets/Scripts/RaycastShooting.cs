using UnityEngine;
using System.Collections;


public class RaycastShooting : MonoBehaviour {


    /// <summary>
    /// The object to shoot.
    /// </summary>
    public GameObject shot;
    /// <summary>
    /// The spawn point of the shot,
    /// </summary>
    public Transform shotSpawn;

    /// <summary>
    /// The damage of the shot.
    /// </summary>
    public float shotDamage = 1.0f;
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

    void Update()
    {
        // ray in the middle of the play screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //GameObject thorn = (GameObject)Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //ThornController tc = thorn.GetComponent<ThornController>();
            //tc.damage = 2;

            // sends out a ray from the given spawn with the given range
            if (Physics.Raycast(ray, out hit, shotRange))
            {
                // send a message to the strucked object and execute the function "ApplyDamage" with the parameter damage
                hit.transform.SendMessage("ApplyDamage", shotDamage, SendMessageOptions.DontRequireReceiver);
            }

        }
    }

}
