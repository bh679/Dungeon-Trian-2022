using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using BrennanHatton.Utilities;

namespace EqualReality.Networking.Events
{
	public class ReceiveChoseGameObjectEvent : MonoBehaviour,IOnEventCallback
	{
		public chooseGameobject chooser;
		
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
			if (eventCode == SendEventManager.ChangeDataReviewEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
					
				int senderId = (int)data[0];
				int id = (int)data[1];
				
				chooser.chooseObject(id);
				
			}
		}
	}
}