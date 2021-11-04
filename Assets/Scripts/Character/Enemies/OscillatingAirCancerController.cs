using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OscillatingAirCancerController : NormalAirCancerController
{
    public float OscillateStrength = 2f;

	private void Start()
	{
        scoreValue = 3;
	} //end method Start

	private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(-Speed * Time.fixedDeltaTime * 10f, Mathf.Sin(Time.time) * OscillateStrength);
        m_Rigidbody2D.velocity = targetVelocity;
    } //end method FixedUpdate
} //end class OscillatingAirCancerController