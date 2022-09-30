using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using BNG;
using EqualReality.UserModes;
using EqualReality.UserModes.Offline;//this could be removed by having two player spawn positions classes. One for online, one for offline, and they are enabled / disabiled by an isolated system that checks for offlinemode
using UnityEngine.SceneManagement;

namespace EqualReality.Networking
{

	public class PlayerSpawnPosition : MonoBehaviourPunCallbacks
	{
		public Transform[] spawnPoints;
		public PlayerTeleport Player;
		public Transform FacilitatorPoint;
		
		void Reset()
		{
			Player = GameObject.FindObjectOfType<PlayerTeleport>();//
		}
		
		
		// called first
		void OnEnable()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
	
		// called second
		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if(Player == null) Player = GameObject.FindObjectOfType<PlayerTeleport>();
			
			if(PhotonNetwork.InRoom)
				SetPosition();
		}
		
		public override void OnJoinedRoom()
		{
			SetPosition();
			base.OnJoinedRoom();
		} 
		
		public void SetPosition()
		{
			if(PhotonNetwork.LocalPlayer.ActorNumber == -1)
				return;
			
			
			if(!OfflineMode.isEnabled) 
			{
				
				if(Facilitator.mode && FacilitatorPoint != null)
					Player.TeleportPlayerToTransform(FacilitatorPoint);
				else{
					Player.TeleportPlayerToTransform(spawnPoints[(PhotonNetwork.LocalPlayer.ActorNumber - 1) % spawnPoints.Length]);
					spawnPoints[(PhotonNetwork.LocalPlayer.ActorNumber - 1) % spawnPoints.Length].gameObject.SetActive(true);
				}
			}
			
			else
			{
				Player.TeleportPlayerToTransform(spawnPoints[0]);
			}
		}
		
		public Vector3 GetPosition()
		{
			return GetPosition(PhotonNetwork.LocalPlayer.ActorNumber);
		}
		
		public Quaternion GetRotation()
		{
			return GetRotation(PhotonNetwork.LocalPlayer.ActorNumber);
		}
		
		
		
		public Vector3 GetPosition(int ActorNumber)
		{
			if(ActorNumber == 0)
				ActorNumber = PhotonNetwork.PlayerList.Length+1;
			
			return spawnPoints[(ActorNumber - 1) % spawnPoints.Length].position;
		}
		
		public Quaternion GetRotation(int ActorNumber)
		{
			if(ActorNumber == 0)
				ActorNumber = PhotonNetwork.PlayerList.Length+1;
				
			return spawnPoints[(ActorNumber - 1) % spawnPoints.Length].rotation;
		}
	}

}