using UnityEngine;

public class ScaleScript : MonoBehaviour
{
	void Update()
	{
		// If there are two touches on the device...
		if (Input.touchCount == 2) {
			
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			// Find the position in the previous frame of each touch
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			// Now scale the object accordingly
			Vector3 newScale = new Vector3 (5, 5, 5); //Factor of increase
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
				if (deltaMagnitudeDiff < 0) {
					gameObject.transform.localScale += newScale;
				} else {
					gameObject.transform.localScale -= newScale;
				}
			}
		}
	}
}