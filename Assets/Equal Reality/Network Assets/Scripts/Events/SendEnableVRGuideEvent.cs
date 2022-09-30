using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EqualReality.Networking.Events
{
	
	public class SendEnableVRGuideEvent : MonoBehaviour
	{
		public void enableVRGuide(bool _bool)
		{
			Debug.Log("enable VR Guide button pressed");
			SendEventManager.SendEnableVRGuideEvent(_bool);
		}
	}

}
