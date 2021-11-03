using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] GroundCells;

    private Queue<GameObject> groundCellsQueue = new Queue<GameObject>();

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

        EventManager.AddEventListener(EventManager.Events.RECYCLE_GROUND, OnRecycleGround);
        EventManager.AddEventListener(EventManager.Events.GROUND_PLACED, OnGroundPlaced);
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
        bool doAttachGroundCell = Utils.GetRandom(1, 3) == 2; // 33% chance of attaching a ground cell
        if (doAttachGroundCell)
        {
            /*
             * get next ground cell from queue and place it
             */

            GameObject ground = (GameObject)data;
            GameObject groundCell = groundCellsQueue.Dequeue();

            groundCell.transform.parent = ground.transform;
            groundCell.transform.localPosition = GetRandomGroundPosition(ground);
            groundCell.SetActive(true);
        }
    } //end method OnGroundPlaced

    private Vector3 GetRandomGroundPosition(GameObject ground)
    {
        GroundPositions positions = ground.GetComponent<GroundPositions>();
        Vector3 pos = positions.Front.transform.localPosition;

        int posInt = Utils.GetRandom(1, 3);
        switch(posInt)
		{
            case 1:
                pos = positions.Front.transform.localPosition;
                break;
            case 2:
                if (positions.Middle == null)
                {
                    posInt = Utils.GetRandom(1, 2);
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
} //end class EnemyManager