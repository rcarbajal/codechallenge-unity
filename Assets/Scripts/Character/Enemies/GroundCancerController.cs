using UnityEngine;

public class GroundCancerController : MonoBehaviour
{
    private GameObject _ground;
    public GameObject Ground
	{
        get { return _ground; }
        set
		{
            _ground = value;
            transform.SetParent(_ground.transform);
            transform.localPosition = GetRandomGroundPosition(_ground);
		}
    }

    protected int scoreValue = 1;

    private Animator animator;
    private CircleCollider2D circleCollider;

	private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

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
                circleCollider.enabled = false;
                animator.SetTrigger("Death");
                EventManager.Broadcast(EventManager.Events.PLAYER_FEET_HIT, scoreValue);
            } //end else
        } //end if
    } //end method OnCollisionEnter2D

    private Vector3 GetRandomGroundPosition(GameObject ground)
    {
        GroundPositions positions = ground.GetComponent<GroundPositions>();
        Vector3 pos = positions.Front.transform.localPosition;

        int posInt = UnityEngine.Random.Range(1, 4);
        switch (posInt)
        {
            case 1:
                pos = positions.Front.transform.localPosition;
                break;
            case 2:
                if (positions.Middle == null)
                {
                    posInt = UnityEngine.Random.Range(1, 3);
                    pos = posInt == 1 ? positions.Front.transform.localPosition : positions.Back.transform.localPosition;
                } //end if
                else pos = positions.Middle.transform.localPosition;
                break;
            case 3:
                pos = positions.Back.transform.localPosition;
                break;
        } //end switch

        return pos;
    } //end method GetRandomGroundPosition

    private void Hide()
	{
        gameObject.SetActive(false);
        animator.SetTrigger("Start");
        circleCollider.enabled = true;
    } //end method Hide
} //end class GroundCancerController