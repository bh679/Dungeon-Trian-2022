using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

namespace EqualReality.Networking.Events
{
	
	public class RecieveBecomeFacilitatorEvent : MonoBehaviour, IOnEventCallback
	{
		
		public PhotonView photonView;
		public GameObject facilitatorHat;
		public GameObject facilitatorSash;
		public UnityEvent onReceive; 
		
		void Reset()
		{
			photonView = this.GetComponent<PhotonView>();
		}
		
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
			
			if(eventCode == SendEventManager.BecomeFacilitatorEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int id = (int)data[0];
				bool isFacilitator = (bool)data[1];
				
				if(photonView!= null && id == photonView.Owner.ActorNumber)
				{
					Debug.Log("ReceiveEvent SendEventManager.BecomeFacilitatorEventCode" + id);
					
					facilitatorHat.SetActive(isFacilitator);
					facilitatorSash.SetActive(isFacilitator);
				}
				
				onReceive.Invoke();
			}
		}
	}

}
