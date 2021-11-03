using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject Spawner;
    [SerializeField] private GameObject[] GroundCells;
    [SerializeField] private GameObject[] AirCells;

    private Queue<GameObject> groundCellsQueue = new Queue<GameObject>();
    private Queue<GameObject> airCellsQueue = new Queue<GameObject>();
    private float halfSpawnerHeight;

    private void Awake()
    {
        if (GroundCells != null && GroundCells.Length > 0)
		{
            foreach(var obj in GroundCells)
			{
                groundCellsQueue.Enqueue(obj);
                obj.SetActive(false);
			} //end foreach
		} //end if

        if (AirCells != null && AirCells.Length > 0)
		{
            foreach(var obj in AirCells)
			{
                airCellsQueue.Enqueue(obj);
                obj.SetActive(false);
            } //end foreach
		} //end if

        EventManager.AddEventListener(EventManager.Events.RECYCLE_GROUND, OnRecycleGround);
        EventManager.AddEventListener(EventManager.Events.GROUND_PLACED, OnGroundPlaced);
        EventManager.AddEventListener(EventManager.Events.RECYCLE_AIR_CELL, OnAirCellRecycle);

        if (Spawner != null)
		{
            RectTransform rt = Spawner.GetComponent<RectTransform>();
            halfSpawnerHeight = rt.rect.height / 2f;

            foreach(var obj in AirCells)
                obj.transform.parent = Spawner.transform;
		} //end if
    } //end method Awake

	private void Start()
	{
        StartCoroutine(SpawnAirCell());
	} //end method Start

	private void OnRecycleGround(object data)
    {
        /*
         * put ground cell back into queue if one exists
         */

        GameObject ground = (GameObject)data;
        Transform currentCellTransform = ground.transform.Find("GroundCancer");

        if (currentCellTransform != null)
        {
            currentCellTransform.gameObject.SetActive(false);
            groundCellsQueue.Enqueue(currentCellTransform.gameObject);
        } //end if
    } //end method OnRecycleGround

    private void OnGroundPlaced(object data)
    {
        bool doAttachGroundCell = Random.Range(1, 4) == 2; // 33% chance of attaching a ground cell
        if (doAttachGroundCell)
        {
            /*
             * get next ground cell from queue and place it
             */

            GameObject ground = (GameObject)data;
            GameObject groundCell = groundCellsQueue.Dequeue();
            GroundCancerController groundController = groundCell.GetComponent<GroundCancerController>();
            groundController.Ground = ground;
            groundCell.SetActive(true);
        } //end if
    } //end method OnGroundPlaced

    private void OnAirCellRecycle(object data)
	{
        /*
         * put air cell back into queue
         */
        GameObject airCell = (GameObject)data;
        airCellsQueue.Enqueue(airCell);
        airCell.SetActive(false);
	} //end method OnAirCellRecycle

    private IEnumerator SpawnAirCell()
	{
        while (true)
        {
            // find random y position within spawner area
            float randY = Random.Range(-halfSpawnerHeight, halfSpawnerHeight);

            // retrieve next air cell and place it
            GameObject airCell = airCellsQueue.Dequeue();
            airCell.transform.localPosition = new Vector2(0, randY);
            airCell.SetActive(true);

            yield return new WaitForSeconds(Random.Range(4f, 8f));
        } //end while
	} //end method SpawnAirCell
} //end class EnemyManager