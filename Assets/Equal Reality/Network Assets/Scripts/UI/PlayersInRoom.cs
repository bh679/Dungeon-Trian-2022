using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace EqualReality.Networking
{

	public class PlayersInRoom : MonoBehaviour
	{
		[System.Serializable]
		public class RoomToText
		{
			public List<Text> text = new List<Text>();
			public int roomId;
			public int number = 0;
			
			public void NumberOfPlayersToText()
			{
				for(int i = 0; i < text.Count; i++)
				{
					text[i].text = number.ToString();;
				}
			}
		}
		
		int numebrOfRooms;
		RoomToText[] rooms = new RoomToText[0];
		
		Dictionary<int, RoomToText> RoomLookUp = new Dictionary<int, RoomToText>();
		
		public static PlayersInRoom Instance { get; private set; }
		private void Awake() 
		{ 
			// If there is an instance, and it's not me, delete myself.
	    
			if (Instance != null && Instance != this) 
			{ 
				DestroyImmediate(this); 
			} 
			else 
			{ 
				Instance = this;
				DontDestroyOnLoad(this);
			} 
	    	
			/*for(int i = 0; i < rooms.Length; i++)
			RoomLookUp.Add(rooms[i].roomId, rooms[i]);*/
		}

		
		private void OnEnable()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
	
		// called second
		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			//Debug.LogError("void OnSceneLoaded(Scene scene, LoadSceneMode mode)");
			numebrOfRooms = SceneManager.sceneCountInBuildSettings;
			
			RoomLookUp = new Dictionary<int, RoomToText>();
			rooms = new RoomToText[numebrOfRooms];
			for(int i = 0; i < numebrOfRooms; i++)
			{
				rooms[i] = new RoomToText();
				rooms[i].roomId = i;
				RoomLookUp.Add(rooms[i].roomId, rooms[i]);
			}
		}
				
		public void AddText(Text text, int roomId)
		{
			rooms[roomId].text.Add(text);
		}
		
		public void CountPlayersInRooms()
		{
			for(int i = 0; i < rooms.Length; i++)
			{
				rooms[i].number = 0;
			}
				
			int id;
			for(int i = 0;i < PhotonNetwork.PlayerList.Length; i++)
			{
				//if(PhotonNetwork.PlayerList[i] != null && PhotonNetwork.PlayerList[i].CustomProperties != null)
				//{
					//Debug.Log(PhotonNetwork.PlayerList[i].CustomProperties);
					if(PhotonNetwork.PlayerList[i].CustomProperties.ContainsKey(PlayerCustomProperties.ActiveScene))
					{
						id = (int)PhotonNetwork.PlayerList[i].CustomProperties[PlayerCustomProperties.ActiveScene];
					
						RoomLookUp[id].number++;
					}
				//}
				//else
				//	Debug.Log("Owner reference missing. PhotonNetwork.PlayerList[i] = null. i=" + i + " or PhotonNetwork.PlayerList[i].CustomProperties == null");
				
			}
		}
	
	    // Update is called once per frame
	    void Update()
	    {
		    CountPlayersInRooms();//this should idelly only run when a player joins or leaves the room. 
		    
		    
		    for(int i = 0; i < rooms.Length; i++)
			    rooms[i].NumberOfPlayersToText();
	    }
	}
}