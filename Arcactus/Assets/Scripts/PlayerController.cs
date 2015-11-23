using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.25f;

    private float nextFire;
    private GameController gameController;

    void Start () {

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

    // Update is called once per frame
    void Update () {
	    if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //Instantiate(shot, shotSpawn.position, new Quaternion(0,0,0,1));
  
        }
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject other = hit.gameObject;
        if (other.tag == "Enemy")
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();
            gameController.ApplyDamage(enemyController.damage);
            Destroy(other.gameObject);

        }
    }
}
