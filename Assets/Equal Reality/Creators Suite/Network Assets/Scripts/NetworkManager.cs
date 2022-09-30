using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using Photon.Voice.Unity;
//using Photon.Voice.Unity.UtilityScripts;
namespace EqualReality.Networking
{
	public class NetworkManager : MonoBehaviourPunCallbacks
	{
		public UnityEvent onConnectedToMaster = new UnityEvent(),onJoinedRoom = new UnityEvent(), onPlayerEnteredRoom = new UnityEvent();
		
		//public Recorder voiceRecorder;
		//public VoiceConnection voiceConnection;
		
		static RoomOptions roomOptions = null;
		public static int roomSize = 10;
		public static bool visible = false, open = true;
		public bool autoConnect = false;
		public static string roomName = "Public";
		
		
		static string roomPassword;
		public static string RoomPassword
		{	
			set{
				Debug.Log("set " + roomPassword);
				
				//if the password isnt changing
				if(string.Compare(roomPassword,value) == 0)
					//exit
					return;
					
				roomPassword = value;
				Debug.Log("set " + roomPassword);
					
				if(usePassword)
				{
					PhotonNetwork.LeaveRoom();
					
					
				}
			}
		}
		
		public static bool usePassword;
		
		
		public Text statusText;
		
		void Reset()
		{
			roomName = SceneManager.GetActiveScene().name;
			//	voiceRecorder = GameObject.FindObjectOfType<Recorder>();
			//	voiceConnection = GameObject.FindObjectOfType<VoiceConnection>();
		}
		
		public void Start()
		{
			/*if(voiceRecorder == null)
				voiceRecorder = GameObject.FindObjectOfType<Recorder>();
				
			if(voiceConnection == null)
			voiceConnection = GameObject.FindObjectOfType<VoiceConnection>();*/
			
			if(!PhotonNetwork.IsConnectedAndReady)
			{
				GetRoomOptions();
				ConnectToServer();
			}
			
		}
		
		public static RoomOptions GetRoomOptions()
		{
			if(roomOptions == null)	
			{
				roomOptions = new RoomOptions();
			}
			
			roomOptions.MaxPlayers = (byte)roomSize;
			roomOptions.IsVisible = visible;
			roomOptions.IsOpen = open;
			
			return roomOptions;
		}
		
		public void ConnectToRoom()
		{
			
			
			PhotonNetwork.JoinOrCreateRoom(roomName+roomPassword, GetRoomOptions(), TypedLobby.Default);
		}
		
		public override void OnJoinedRoom()
		{
			
			if(statusText != null)
				statusText.text = "You joined a classroom.\nYour name is " + PhotonNetwork.NickName + ".\nThere are " + PhotonNetwork.PlayerList.Length + " classmates already here.";
			//Debug.Log("You joined a classroom.\nYour name is " + PhotonNetwork.NickName + ".\nThere are " + PhotonNetwork.PlayerList.Length + " classmates already here.");
			base.OnJoinedRoom();
			
			onJoinedRoom.Invoke();
		}
		
		public override void OnPlayerEnteredRoom(Player newPlayer)
		{
			
			if(statusText != null)
				statusText.text = "You are live with " + PhotonNetwork.PlayerList.Length + " classmates in the room.\nYour name is " + PhotonNetwork.NickName + ".";
			Debug.Log("A new player joined the room");
			base.OnPlayerLeftRoom(newPlayer);
			
			onPlayerEnteredRoom.Invoke();
		}
		
		// called second
		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			
			Debug.Log("OnSceneLoaded: " + scene.name);
			Debug.Log(mode);
			
			if(PhotonNetwork.IsConnectedAndReady)
			{
				PhotonNetwork.LeaveRoom();
			}
			
			
			if(autoConnect)
				ConnectToRoom();
	
		}
	
		void ConnectToServer()
		{
			PhotonNetwork.ConnectUsingSettings();
			//PhotonNetwork.ConnectToRegion("us");
			//Debug.Log("Trying to connect to server");
			if(statusText != null)
				statusText.text = "Trying to connect to server";
		}
		public override void OnConnectedToMaster()
		{
			if(statusText != null)
				statusText.text = "Connected to server";
			//Debug.Log("ConnectedToServer,");
			base.OnConnectedToMaster();
			/*RoomOptions roomOptions = new RoomOptions();
			roomOptions.MaxPlayers = 10;
			roomOptions.IsVisible = true;
			roomOptions.IsOpen = true;*/
			
			ConnectToRoom();
			
			onConnectedToMaster.Invoke();
		}
		
		public override void OnDisconnected (DisconnectCause cause)
		{
			if(statusText != null)
				statusText.text = "You have been disconnected. Reason: " + cause.ToString() + "\nTrying to reconnect...";
			ConnectToServer();
		}
		
	}
}
