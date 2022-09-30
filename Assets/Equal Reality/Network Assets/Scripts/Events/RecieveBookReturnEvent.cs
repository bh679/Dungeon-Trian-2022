using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

namespace BrennanHatton.LibraryOfBabel.Networking.Events
{
	
	public class RecieveBookReturnEvent : MonoBehaviour, IOnEventCallback
	{
		
		//public NetworkPlacerSpawner PlayerManager;
		
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
			
			if(eventCode == SendEventManager.ReturnBookEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int id = (int)data[0];
				BookPosition book = new BookPosition((string)data[1],(int)data[2],(int)data[3],(int)data[4],(int)data[5]);
				Debug.Log("ReceiveEvent SendEventManager.ReturnBookEventCode" + id);
				
				BookReturnsManager.Instance.Return(new BookData(book));
				
			}
		}
	}
}