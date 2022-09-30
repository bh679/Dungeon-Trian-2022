using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EqualReality.Networking.Events
{
	
	//becuase SendEventManager ha some non-static functions, I made a wrapper for the one funciton we need
	public class SendPlayLineEvent : MonoBehaviour
	{
		public Text lineIdText;
		
		public void SendPlayLineEventPlz()
		{
			SendPlayLineEventPlz(int.Parse(lineIdText.text));
		}
		
		public static void SendPlayLineEventPlz(int lineId)
		{
			SendEventManager.SendPlayLineEvent(lineId);
		}
	}

}