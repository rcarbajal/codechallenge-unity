using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
	private class GroundQueueHolder
	{
		public GameObject GroundObject { get; set; }
		public Queue<GameObject> OriginQueue { get; set; }
	} //end class GroundQueueHolder

	private static MainController Instance;

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

	private bool isGroundPlaced = false;

	private System.Random rnd;

	#region Monobehaviour methods

	private void Awake()
	{
		Instance = this;
	} //end method Awake

	private void Start()
	{
		/*
		 * collect ground prefab instances into queues
		 */
		flatQueue = new Queue<GameObject>();
		if (FlatGrounds != null && FlatGrounds.Length > 0)
			foreach (GameObject obj in FlatGrounds)
			{
				flatQueue.Enqueue(obj);
				obj.SetActive(false);
			} //end foreach

		smallQueue = new Queue<GameObject>();
		if (SmallHills != null && SmallHills.Length > 0)
			foreach (GameObject obj in SmallHills)
			{
				smallQueue.Enqueue(obj);
				obj.SetActive(false);
			} //end foreach

		tallQueue = new Queue<GameObject>();
		if (TallHills != null && TallHills.Length > 0)
			foreach (GameObject obj in TallHills)
			{
				tallQueue.Enqueue(obj);
				obj.SetActive(false);
			} //end foreach

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

		isGroundPlaced = true;
	} //end method Start

	private void FixedUpdate()
	{
		int pos = (int)Mathf.Round(Player.transform.localPosition.x);

		if (pos >= 20 && pos % 20 == 0 && !isGroundPlaced)
		{
			isGroundPlaced = true;

			//put back ground that has moved out of view
			GroundQueueHolder gqh = mainQueue.Dequeue();
			gqh.GroundObject.SetActive(false);
			gqh.OriginQueue.Enqueue(gqh.GroundObject);

			//get new random ground piece and place in front of player
			gqh = GetRandomGroundObject();
			gqh.GroundObject.transform.localPosition = new Vector3(Player.transform.localPosition.x + 20, 0, 0);
			gqh.GroundObject.SetActive(true);
			mainQueue.Enqueue(gqh);
		} //end if

		if ((pos - 10) % 20 == 0)
			isGroundPlaced = false;
	} //end method FixedUpdate

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

	#endregion
} //end class MainController