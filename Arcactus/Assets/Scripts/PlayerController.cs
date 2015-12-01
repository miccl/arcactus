using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    
    public GameObject shot;
    public Transform shotSpawn;
	[Range(0.1f,2)]
    public float fireRate = 0.5f;

    private float nextFire;
    private Vector3 movementVector;
    private CharacterController characterController = null;
    public float movement_speed = 1;



    void Start () {

        characterController = gameObject.GetComponent<CharacterController>();
       


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

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * movement_speed);

    }


}
