using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using EqualReality.Networking.Voice;

namespace EqualReality.Networking.Events
{
	
	public class ReceiveJoinRoomEvent : MonoBehaviour, IOnEventCallback
	{
		
		private void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
			//base.OnEnable();
		}
	
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
			//base.OnDisable();
		}
	
		public void OnEvent(EventData photonEvent)
		{
#if PUNVOICE
			byte eventCode = photonEvent.Code;
			if (eventCode == SendEventManager.SendToRoomEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				
				int senderId;
				string roomName;
				byte voiceChannel;
				int[] actorsInvited;
				
				senderId = (int)data[0];
				roomName = (string)data[1];
				voiceChannel = (byte)data[2];
				actorsInvited = (int[])data[3];
				
				Debug.Log("ReceiveEvent SendEventManager.SendToRoomEventCode" + senderId);
				
				if(actorsInvited == null || actorsInvited.Contains(PhotonNetwork.LocalPlayer.ActorNumber))
				{
					NetworkManager.roomName = roomName;
					NetworkVoiceManager.instance.SetVoiceChannel(voiceChannel);
					//roomOptions = NetworkManager.GetRoomOptions();
					
					PhotonNetwork.LeaveRoom();
					
				}
			}
#endif
		}
		
	}

}