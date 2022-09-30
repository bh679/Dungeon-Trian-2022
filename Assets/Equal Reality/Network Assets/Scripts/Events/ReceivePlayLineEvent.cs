using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

namespace EqualReality.Networking.Events
{
	
	public class ReceivePlayLineEvent : MonoBehaviour, IOnEventCallback
	{
		public AudioClip[] lines;
		public AudioSource player;
		
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
			if (eventCode == SendEventManager.PlayLineEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
					
				int senderId = (int)data[0];
				int lineId = (int)data[1] - 1;
				
				if(player.isPlaying)
				{
					player.Stop();
					
					if(player.clip == lines[lineId])
					{
						return;
					}
				}
				player.clip = lines[lineId];
				player.Play();
				
			}
		}
	}
	
}
