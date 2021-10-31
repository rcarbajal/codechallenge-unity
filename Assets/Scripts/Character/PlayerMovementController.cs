using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovementController : MonoBehaviour
{
	public float Speed = 50f;

	private CharacterController2D controller;
	private Rigidbody2D rigidBody;

	#region MonoBehaviour methods

	void Start()
	{
		controller = GetComponent<CharacterController2D>();
		rigidBody = GetComponent<Rigidbody2D>();
	} //end method Start

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			controller.Jump();

		if (Input.GetKey(KeyCode.D))
			controller.Move(Speed * Time.fixedDeltaTime);
		else if (Input.GetKey(KeyCode.A))
			controller.Move(-Speed * Time.fixedDeltaTime);
		else
			controller.Move(0);
	} //end method Update

	#endregion
}
