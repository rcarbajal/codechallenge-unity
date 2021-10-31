using UnityEngine;

public class CameraRunnerScript : MonoBehaviour {
    public Transform player;

	void FixedUpdate () {
        if(player != null)
            transform.position = new Vector3(player.position.x + 1, 0, -10);
	} //end method Update
} //end class CameraRunnerScript 