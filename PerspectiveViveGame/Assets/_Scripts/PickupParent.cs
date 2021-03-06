﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickupParent : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;
	public bool touchpadDown = false;
	public bool triggerUp = false;

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void FixedUpdate () {
		device = SteamVR_Controller.Input ((int)trackedObj.index);

		if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log ("Trigger held");
		}
		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			touchpadDown = true;
		}

		if (device.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			touchpadDown = false;
		}

		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			triggerUp = true;
		}
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			triggerUp = false;
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "key") {
			if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
				Debug.Log ("Object grabbed");
				other.attachedRigidbody.isKinematic = true;
				other.gameObject.transform.SetParent (gameObject.transform);
			}
			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
					
				Debug.Log ("Object dropped");
				other.attachedRigidbody.isKinematic = false;
				other.gameObject.transform.SetParent (null);
			}
		}
	}
}
