using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EqualReality.Networking.Events
{
	
	public class SendChangeNameEvent : MonoBehaviour
	{
		public void SendNameChangeEvent()
		{
			SendEventManager.SendNameChangeEvent();
		}
	}

}