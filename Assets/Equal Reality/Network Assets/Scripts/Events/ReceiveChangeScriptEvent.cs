using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using BrennanHatton.Utilities;

namespace EqualReality.Networking.Events
{
	
	public class ReceiveChangeScriptEvent : MonoBehaviour,IOnEventCallback
	{
		public chooseGameobject[] choosers;
		
		private void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
		}
	
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}
		
		public void OnEvent(EventData photonEvent)
		{
			byte eventCode = photonEvent.Code;
			if (eventCode == SendEventManager.ChangeScriptEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
					
				int senderId = (int)data[0];
				int id = (int)data[1];
				
				for(int i = 0; i < choosers.Length; i++)
					choosers[i].chooseObject(id);
				
			}
		}
	}
}