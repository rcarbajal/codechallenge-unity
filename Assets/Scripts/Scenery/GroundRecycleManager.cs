using System.Collections.Generic;
using UnityEngine;

public class GroundRecycleManager : MonoBehaviour
{
	private class GroundQueueHolder
	{
		public GameObject GroundObject { get; set; }
		public Queue<GameObject> OriginQueue { get; set; }
	} //end class GroundQueueHolder

	[Header("Player")]
	[SerializeField] private Transform Player;

	[Header("Ground objects")]
	[SerializeField] private GameObject[] FlatGrounds;
	[SerializeField] private GameObject[] SmallHills;
	[SerializeField] private GameObject[] TallHills;

	private Queue<GameObject> flatQueue;
	private Queue<GameObject> smallQueue;
	private Queue<GameObject> tallQueue;
	private Queue<GroundQueueHolder> mainQueue;

	private GameObject lastPlacedGround;

	private System.Random rnd;

	#region Monobehaviour methods

	private void Awake()
	{
		EventManager.AddEventListener(EventManager.Events.RECYCLE_GROUND, this.Recycle);
	}

	private void Start()
	{
		/*
		 * collect ground prefab instances into queues
		 */
		flatQueue = new Queue<GameObject>();
		if (FlatGrounds != null && FlatGrounds.Length > 0)
		{
			foreach (GameObject obj in FlatGrounds)
			{
				flatQueue.Enqueue(obj);
				obj.SetActive(false);
			} //end foreach
		} //end if

		smallQueue = new Queue<GameObject>();
		if (SmallHills != null && SmallHills.Length > 0)
		{
			foreach (GameObject obj in SmallHills)
			{
				smallQueue.Enqueue(obj);
				obj.SetActive(false);
			} //end foreach
		} //end if

		tallQueue = new Queue<GameObject>();
		if (TallHills != null && TallHills.Length > 0)
		{
			foreach (GameObject obj in TallHills)
			{
				tallQueue.Enqueue(obj);
				obj.SetActive(false);
			} //end foreach
		} //end if

		//initialize
		rnd = new System.Random();
		mainQueue = new Queue<GroundQueueHolder>();

		//pick two ground objects and place them
		GroundQueueHolder gqh = GetRandomGroundObject();
		gqh.GroundObject.transform.localPosition = new Vector3(0, 0, 0);
		gqh.GroundObject.SetActive(true);
		mainQueue.Enqueue(gqh);

		gqh = GetRandomGroundObject();
		gqh.GroundObject.transform.localPosition = new Vector3(20, 0, 0);
		gqh.GroundObject.SetActive(true);
		mainQueue.Enqueue(gqh);

		lastPlacedGround = gqh.GroundObject;
	} //end method Start

	#endregion

	#region Private methods

	private GroundQueueHolder GetRandomGroundObject()
	{
		GroundQueueHolder gqh;
		GameObject obj;
		int listNum = rnd.Next(1, 4);
		switch (listNum)
		{
			case 1:
				obj = flatQueue.Dequeue();
				gqh = new GroundQueueHolder
				{
					GroundObject = obj,
					OriginQueue = flatQueue
				};
				break;
			case 2:
				obj = smallQueue.Dequeue();
				gqh = new GroundQueueHolder
				{
					GroundObject = obj,
					OriginQueue = smallQueue
				};
				break;
			case 3:
				obj = tallQueue.Dequeue();
				gqh = new GroundQueueHolder
				{
					GroundObject = obj,
					OriginQueue = tallQueue
				};
				break;
			default:
				obj = flatQueue.Dequeue();
				gqh = new GroundQueueHolder
				{
					GroundObject = obj,
					OriginQueue = flatQueue
				};
				break;
		} //end switch

		return gqh;
	} //end method GetRandomGroundObject
	
	private void Recycle()
	{
		//put back ground that has moved out of view
		GroundQueueHolder gqh = mainQueue.Dequeue();
		gqh.GroundObject.SetActive(false);
		gqh.OriginQueue.Enqueue(gqh.GroundObject);

		//get new random ground piece and place in front of player
		gqh = GetRandomGroundObject();
		gqh.GroundObject.transform.localPosition = new Vector3(lastPlacedGround.transform.localPosition.x + 20, 0, 0);
		gqh.GroundObject.SetActive(true);
		mainQueue.Enqueue(gqh);

		lastPlacedGround = gqh.GroundObject;
	} //end method Recycle

	#endregion
}
