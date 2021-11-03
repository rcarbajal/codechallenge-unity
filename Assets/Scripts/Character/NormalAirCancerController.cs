using UnityEngine;

public class NormalAirCancerController : MonoBehaviour
{
    public float Speed = 25f;
    protected Rigidbody2D m_Rigidbody2D;

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
}
