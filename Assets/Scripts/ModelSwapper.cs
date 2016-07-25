using UnityEngine;
using System.Collections;
using Vuforia;

public class ModelSwapper : MonoBehaviour {
	public TrackableBehaviour theTrackable;

	private bool mSwapModel = false;
	private int count = 1;
	// Use this for initialization
	void Start () {
		if (theTrackable == null)
		{
			Debug.Log ("Warning: Trackable not set !!");
		}
	}
	// Update is called once per frame
	void Update () {
		if (mSwapModel && theTrackable != null) {
			SwapModel();
			mSwapModel = false;
		}
	}
	void OnGUI() {
		if (GUI.Button (new Rect(50,50,120,40), "Swap Model")) {
			mSwapModel = true;
		}
	}
	private void SwapModel() {
		string[] names = {"capsule1", "teapot1"};
		GameObject trackableGameObject = theTrackable.gameObject;
		//disable any pre-existing augmentation
		for (int i = 0; i < trackableGameObject.transform.GetChildCount(); i++)
		{
			Transform child = trackableGameObject.transform.GetChild(i);
			child.gameObject.SetActive(false);
		}
//		var renderers =  trackableGameObject.GetComponentsInChildren<Renderer>();
//		count = (count + 1) % names.Length;
//		foreach (Renderer r in renderers) {
//			Debug.Log("r name" + r.name);
//			if (r.name == names[count]) {
//				Debug.Log ("r " + r.name + " enabled");
//				r.enabled = true;
//			} else {
//				Debug.Log ("r " + r.name + " diabled");
//				r.enabled = false;
//			}
//		}


		// Create a simple cube object
//		trackableGameObject.transform.GetChild(count).gameObject.SetActive(true);
//		Debug.Log("count is" + count + "tag:" + names[count]);
//		GameObject obj = GameObject.FindGameObjectWithTag(names[count]);
//		if (obj != null) {
//			obj.SetActive (true);
//		} else {
//			GameObject obj2 = GameObject.Find ("UserTarget");
//			Debug.Log ("obj2 is " + obj2);
//			Debug.Log ("obj null!!!!");
//		}
//		// Re-parent the cube as child of the trackable gameObject
//		capsule.transform.parent = theTrackable.transform;
//		// Adjust the position and scale
//		// so that it fits nicely on the target
//		capsule.transform.localPosition = new Vector3(0,0.2f,0);
//		capsule.transform.localRotation = Quaternion.identity;
//		capsule.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
//		// Make sure it is active

	}
}