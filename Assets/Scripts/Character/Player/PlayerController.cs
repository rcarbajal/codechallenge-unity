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
	private Rigidbody2D m_Rigidbody2D;
	private Animator animator;

	#region Public methods

	public void Jump()
	{
		controller.Jump();
	} //end method Jump

	public void MoveLeft()
	{
		controller.Move(-Speed * Time.fixedDeltaTime);
	} //end method MoveLeft

	public void MoveRight()
	{
		controller.Move(Speed * Time.fixedDeltaTime);
	} //end method MoveRight

	#endregion

	#region MonoBehaviour methods

	void Start()
	{
		controller = GetComponent<CharacterController2D>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		animator.SetBool("Grounded", controller.isGrounded);
		EventManager.AddEventListener(EventManager.Events.PLAYER_FEET_HIT, (object value) => controller.Jump());
	} //end method Start

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			controller.Jump();

		if (Input.GetKey(KeyCode.D))
			controller.Move(Speed * Time.fixedDeltaTime);
		else if (Input.GetKey(KeyCode.A))
			controller.Move(-Speed * Time.fixedDeltaTime);
		else if (controller.isGrounded)
			controller.Move(-28 * Time.fixedDeltaTime, false);

		animator.SetBool("Grounded", controller.isGrounded);
		animator.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
		animator.SetFloat("hSpeed", m_Rigidbody2D.velocity.x);
	} //end method Update

	#endregion
}
