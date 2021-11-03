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
            transform.position = Vector3.zero;
		}
	}

    private void Start()
    {
    } //end method Start

    private void Update()
    {
    } //end method Update
} //end class GroundCancerController