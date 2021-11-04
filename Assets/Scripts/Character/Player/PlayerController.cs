using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerController : MonoBehaviour
{
	public float Speed = 50f;

	[SerializeField] private CircleCollider2D circleCollider;
	[SerializeField] private BoxCollider2D boxCollider;

	public CircleCollider2D Body { get { return circleCollider; } }
	public BoxCollider2D Feet { get { return boxCollider; } }

	private CharacterController2D controller;

	#region MonoBehaviour methods

	void Start()
	{
		controller = GetComponent<CharacterController2D>();
	} //end method Start

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			controller.Jump();

		if (Input.GetKey(KeyCode.D))
			controller.Move(Speed * Time.fixedDeltaTime);
		else if (Input.GetKey(KeyCode.A))
			controller.Move(-Speed * Time.fixedDeltaTime);
		else if (controller.Grounded)
			controller.Move(-28 * Time.fixedDeltaTime, false);
	} //end method Update
	#endregion
}
