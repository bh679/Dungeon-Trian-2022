using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EqualReality.Networking.Events
{
	
	public class SendChangeScriptEvent : MonoBehaviour
	{
		public void ChangeScriptEvent(int id)
		{
			SendEventManager.SendChangeScriptEvent(id);
		}
	}

}