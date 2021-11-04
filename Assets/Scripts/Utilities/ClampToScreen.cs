using UnityEngine;

public class ClampToScreen : MonoBehaviour
{
	[SerializeField] private Camera Camera;

	private void Update()
	{
		Rect cameraRect = getCameraRect();

		if (Camera != null)
		{
			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax),
				Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax),
				transform.position.z);
		} //end if
	} //end method Update

	private Rect getCameraRect()
	{
		var bottomLeft = Camera.ScreenToWorldPoint(Vector3.zero);
		var topRight = Camera.ScreenToWorldPoint(new Vector3(Camera.pixelWidth, Camera.pixelHeight));
		return new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
	} //end method getCameraRect
}
