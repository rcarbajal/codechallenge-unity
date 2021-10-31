using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class HorizontalMovementController : MonoBehaviour
{
	#region Properties
	
	[SerializeField] private int Speed = 15;

	private CharacterController2D controller;
	private Rigidbody2D rigidBody;

	#endregion

	#region MonoBehaviour methods

	void Start()
    {
		controller = GetComponent<CharacterController2D>();
		rigidBody = GetComponent<Rigidbody2D>();
    } //end method Start
	
    void Update()
    {
    } //end method Update

	private void FixedUpdate()
	{
		if (!controller.isGrounded)
		{
			rigidBody.gravityScale = 1f;
		} //end if
		else
		{
			if (rigidBody.velocity.y > 0.05f)
				rigidBody.gravityScale = 0f;
		} //end else

		controller.Move(Speed * Time.fixedDeltaTime, false, false);
	} //end method FixedUpdate

	#endregion
} //end class HorizontalMovementController