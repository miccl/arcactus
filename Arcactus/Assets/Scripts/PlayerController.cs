using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    
    private Vector3 movementVector;
    private CharacterController characterController = null;
    public float movement_speed = 1;
    
    void Start () {
        characterController = gameObject.GetComponent<CharacterController>();
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
