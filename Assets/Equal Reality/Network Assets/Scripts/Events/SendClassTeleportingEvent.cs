using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EqualReality.Networking.Events
{
	
	public class SendClassTeleportingEvent : MonoBehaviour
	{
		public Toggle toggle;
		
		void Reset()
		{
			toggle = this.GetComponent<Toggle>();
		}
		
		public void ToggleTeleporting(Toggle toggle)
		{
			SendEventManager.SendEnableClassTeleportingEvent(toggle.isOn);
		}
	}

}