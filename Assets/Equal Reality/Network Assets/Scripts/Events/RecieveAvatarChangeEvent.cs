using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

namespace BrennanHatton.LibraryOfBabel.Networking.Events
{
	
	public class RecieveAvatarChangeEvent : MonoBehaviour, IOnEventCallback
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
			
			if(eventCode == SendEventManager.ChangeAvatarEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int id = (int)data[0];
				Debug.Log("ReceiveEvent SendEventManager.ChangeAvatarEventCode" + id);
				//Menu.SetActive(false);
				//levelConfirmations[id].SetActive(true);
				for(int i = 0; i < NetworkPlayerSpawner.spawnedPlayerPrefabs.Count; i++)
				{
					if(NetworkPlayerSpawner.spawnedPlayerPrefabs[i].Owner.ActorNumber == id)
					{
						NetworkPlayerSpawner.spawnedPlayerPrefabs[i].GetComponentInChildren<AvatarManager>(true).SetAvatar();
					}
				}
			}
		}
	}
}