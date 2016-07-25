using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeProductMenuScript : MonoBehaviour {

	public GameObject productButtonPrefab;
	public GameObject menuBody;

	// Use this for initialization
	private void Start () {
//		Sprite[] thumbnails = Resources.LoadAll<Sprite> ("Products");
//		foreach (Sprite thumbnail in thumbnails) {
//			GameObject container = Instantiate (productButtonPrefab) as GameObject;
//			container.GetComponent<Image> ().sprite = thumbnail;
//			container.transform.SetParent (menuBody.transform, false);
//
//			string sceneName = thumbnail.name;
//			container.GetComponent<Button> ().onClick.AddListener (() => LoadModel(sceneName));
//
//			container.AddComponent<Vuforia.DefaultTrackableEventHandler> ().Swap ();
//		}
	}

	private void LoadModel(string sceneName) {
		Debug.Log (sceneName);

//		Vuforia.DefaultTrackableEventHandler handler = new Vuforia.DefaultTrackableEventHandler();
//		Vuforia.DefaultTrackableEventHandler container2 = Instantiate (handler) as Vuforia.DefaultTrackableEventHandler;
//		handler.Swap();
	}

}
