using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	/// <summary>
	/// The movement speed of the player.
	/// </summary>
	public float movement_speed = 1;

    /// <summary>
    /// The movement vector.
    /// </summary>
    private Vector3 movementVector;
	/// <summary>
	/// The character controller.
	/// </summary>
    private CharacterController characterController = null;

    
    void Start () {
        characterController = gameObject.GetComponent<CharacterController>();
    }

	/// <summary>
	/// Moving the player with the given movement speed.
	/// </summary>
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * movement_speed);

    }




}
