using UnityEngine;
using System.Collections;

public class ToggleModel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Starting toggle model!");
	}

	#region UNITY_MONOBEHAVIOUR_METHODS

	// Update is called once per frame
	void Update () {
		// Debug.Log ("Toggling!");

//		var renderers = GetComponentsInChildren();
//		foreach(Renderer r in renderers) {
//			//  r.enabled = !r.enabled;
//		}
	}

	#endregion // UNITY_MONOBEHAVIOUR_METHODS

	#region PUBLIC_METHODS

	void OnMouseDown(GameObject gO)
	{
		Debug.Log("Pressed left click.");
		gO.SetActive(true);
	}

	#endregion

}
