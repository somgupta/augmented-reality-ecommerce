/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;
		private bool mSwapModel = false;
		private bool inserted = false;
		private int count = 1;
		string[] names = {"Printer", "capsule1", "cube1", "FlatScreenTV", "FridgeNew", "teapot1", "dutchdragon3", "Clock", "sofa"};
		//string[] names = {"capsule1", "cube1"};

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

		void Update () {
			if (mSwapModel) {
				SwapModel();
				mSwapModel = false;
			}
		}

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

		public void ChangeModelTo(int modelNumber){
			count = modelNumber;
			Debug.Log ("hello");
			mSwapModel = true;
			inserted = true;
		}

		public void Swap(){
			Debug.Log ("hello");
			mSwapModel = true;
			inserted = true;
		}

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS

		private void SwapModel() {
			
			//disable any pre-existing augmentation
//			for (int i = 0; i < trackableGameObject.transform.GetChildCount(); i++)
//			{
//				Transform child = trackableGameObject.transform.GetChild(i);
//				child.gameObject.SetActive(false);
//			}
			var renderers = GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers) {
				Debug.Log ("r " + r.name + " disabled");
				r.enabled = false;
			}
			// count = (count + 1) % names.Length;
			foreach (Renderer r in renderers) {
				if (r.name == names[count]) {
					Debug.Log ("r " + r.name + " enabled");
					r.enabled = true;
				}
			}


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

        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
            Collider[] colliderComponents = GetComponentsInChildren<Collider>();

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
				if (component.name == names[count] && inserted) {
					component.enabled = true;
				}
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
            Collider[] colliderComponents = GetComponentsInChildren<Collider>();

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
				Debug.Log ("Disabling renderer " + component.name);
				component.enabled = false;
            }
				
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}
