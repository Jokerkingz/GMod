using System.Collections;
using UnityEngine;

public class NewMovement : MonoBehaviour {

	public CharacterController MyController;
	public PlayerInput pi;
	public float GroundSpeed = 3f;
	public float aerialSpeed = 2f;
	public float GravityStrength = 5f;
	public float JumpSpeed = 10f;
	public Transform CameraTransform;
	bool canJump = false;
	float verticalVelocity;
	Vector3 velocity;
	Vector3 groundedVelocity;
	Vector3 normal;
	bool onWall = false;

	void Update ()
	{
		Vector3 myVector = Vector3.zero;
		//get input from player
		Vector3 input = Vector3.zero;
		input.x = pi.direction.x;
		input.z = pi.direction.z;
		input = Vector3.ClampMagnitude (input, 1f);

		if (MyController.isGrounded) {
			myVector = input;
			Quaternion inputRotation = Quaternion.LookRotation (Vector3.ProjectOnPlane (CameraTransform.forward, Vector3.up), Vector3.up);
			myVector = inputRotation * myVector;//rotate input by direction of cam
			myVector *= GroundSpeed;
		} else {
			myVector = groundedVelocity;
			myVector += input * aerialSpeed;
		}
		myVector = Vector3.ClampMagnitude (myVector, GroundSpeed); //clamp speed
		myVector = myVector * Time.deltaTime;


		verticalVelocity = verticalVelocity - GravityStrength * Time.deltaTime;
		if (Input.GetButtonDown ("Jump")) {
			if (onWall) {
				Vector3 reflection = Vector3.Reflect (velocity, normal);
				Vector3 projected = Vector3.ProjectOnPlane (reflection, Vector3.up);
				groundedVelocity = projected.normalized * GroundSpeed + normal * aerialSpeed; //jumpaway from wall
			}
			//add jump speed to vertical velocity
			if (canJump)
				verticalVelocity += JumpSpeed;


			//EXPERIMENTAL
			//reduced jump speed on wall

			//if (onWall == true) {
				//GravityStrength = GravityStrength * .7f;
		//	}
			//	else
		//	GravityStrength = 10f;
		//	if (Input.GetButtonDown ("Jump") == true){
			//	GravityStrength = 5f}

			}
		myVector.y = verticalVelocity * Time.deltaTime;

		CollisionFlags flags = MyController.Move (myVector);
		velocity = myVector / Time.deltaTime;
		//use flags to determine if character can jump or not
		//if on ground set canJump to true
		if ((flags & CollisionFlags.Below) != 0) {
			groundedVelocity = Vector3.ProjectOnPlane (velocity, Vector3.up);
			canJump = true;
			verticalVelocity = -3f;//keep character grounded
			onWall = false;
		} else if ((flags & CollisionFlags.Sides)	!= 0) {
			canJump = true;
			onWall = true;
		} else {
			canJump = false;
			onWall = false;
		}

	}
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		normal = hit.normal;

	}

}
