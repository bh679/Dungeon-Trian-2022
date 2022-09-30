using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using BNG;
using EqualReality.UserModes;
using UnityEngine.SceneManagement;

namespace EqualReality.Networking.Events
{
	
	public class ReceiveClassTeleportEvent : MonoBehaviour, IOnEventCallback
	{
		
		public PlayerTeleport playerTeleport;
		public PlayerSpawnPosition spawnPoints;
		
		void Reset()
		{
			playerTeleport = GameObject.FindObjectOfType<PlayerTeleport>();
		}

		
		private void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
			
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
	
		// called second
		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if(playerTeleport == null) playerTeleport = GameObject.FindObjectOfType<PlayerTeleport>();
		
		}
		
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}
	
		public void OnEvent(EventData photonEvent)
		{
			byte eventCode = photonEvent.Code;
			
			if(eventCode == SendEventManager.EnableClassTeleportingEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int id = (int)data[0];
				bool enableTeleporting = (bool)data[1];
				//Debug.Log("ReceiveEvent SendEventManager.EnableClassTeleportingEventCode" + id + " as " + enableTeleporting.ToString());
				
				if(Facilitator.mode == false)
					playerTeleport.enabled = enableTeleporting;
			}
			
			if(eventCode == SendEventManager.teleportPlayerEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int senderId = (int)data[0];
				Vector2 destination = (Vector2)data[1]; 
				Quaternion rotation= (Quaternion)data[2]; 
				int spread = (int)data[3];  
				int[] targetPlayers = (int[])data[4]; 
				
				//Debug.Log("ReceiveEvent SendEventManager.teleportPlayerEventCode" + senderId );
				
				if(targetPlayers == null)
				{
					spawnPoints.transform.position = new Vector3(destination.x, 3, destination.y);
					spawnPoints.transform.rotation = rotation;
					
					Vector3 pos;
					Quaternion rot;
					
					if(senderId == PhotonNetwork.LocalPlayer.ActorNumber)
					{
						pos = spawnPoints.GetPosition(1);
						rot = spawnPoints.GetRotation(1);
					}
					else if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
					{
							pos = spawnPoints.GetPosition(0);
							rot = spawnPoints.GetRotation(0);
						
					}else
					
					{
						pos = spawnPoints.GetPosition();
						rot = spawnPoints.GetRotation();
					}
					
					//Debug.Log("pos " + pos);
					
					playerTeleport.TeleportPlayer(pos, rot);
				}
				else if(targetPlayers.Contains(PhotonNetwork.LocalPlayer.ActorNumber))
				{
					
					playerTeleport.TeleportPlayer(destination,rotation);
				}
					
			}
		}
	}
}
