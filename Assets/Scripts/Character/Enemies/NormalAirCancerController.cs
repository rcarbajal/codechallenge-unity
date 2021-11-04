using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NormalAirCancerController : MonoBehaviour
{
    public float Speed = 25f;

    protected Rigidbody2D m_Rigidbody2D;
    protected int scoreValue = 2;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    } //end method Awake

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(-Speed * Time.fixedDeltaTime * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = targetVelocity;
    } //end method FixedUpdate

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AirRecycler")
            EventManager.Broadcast(EventManager.Events.RECYCLE_AIR_CELL, transform.gameObject);
    } //end method OnTriggerEnter2D

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Player")
		{
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (collision.collider == player.Body)
			{
                EventManager.Broadcast(EventManager.Events.PLAYER_BODY_HIT, scoreValue);
			} //end if
            else
			{
                EventManager.Broadcast(EventManager.Events.PLAYER_FEET_HIT, scoreValue);
			} //end else
		} //end if
	} //end method OnCollisionEnter2D

	private void Die()
	{
        Debug.Log("Dying...");
	}
}
