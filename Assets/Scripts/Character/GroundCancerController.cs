using System;
using System.Collections;
using System.Collections.Generic;
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
            transform.parent = _ground.transform;
            transform.localPosition = GetRandomGroundPosition(_ground);
		}
	}

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
} //end class GroundCancerController