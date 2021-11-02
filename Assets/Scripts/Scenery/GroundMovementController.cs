using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GroundMovementController : MonoBehaviour
{
    [HideInInspector] public float Speed = 10;

    private Rigidbody2D m_Rigidbody2D;

	private void Awake()
	{
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

	private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(-Speed * Time.fixedDeltaTime * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = targetVelocity;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Recycler")
            EventManager.Broadcast(EventManager.Events.RECYCLE_GROUND);
	}
}
