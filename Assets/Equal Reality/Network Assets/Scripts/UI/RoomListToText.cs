using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListToText : MonoBehaviourPunCallbacks
{
	public Text playerListText, countText;
	
    
	void OnEnable()
	{
		OnLeftLobby();
	}
	
	string roomName = null;
	public void RefreshRooms()
	{
		roomName = PhotonNetwork.CurrentRoom.Name;
		
		if(PhotonNetwork.InRoom)
			PhotonNetwork.LeaveRoom();
	}
	
	public override void OnLeftRoom ()
	{
		base.OnLeftRoom();
		PhotonNetwork.JoinLobby();
	}
	
	public override void 	OnJoinedLobby ()
	{
		base.OnJoinedLobby();
		if(roomName != null)
			PhotonNetwork.JoinRoom(roomName);
	}
    

	public override void OnJoinedRoom()
	{
		
		countText.text = PhotonNetwork.CountOfRooms.ToString();
		Debug.LogError(PhotonNetwork.CountOfRooms);
		
		if(PhotonNetwork.CountOfRooms == 0)
		{
			RefreshRooms();
		}else roomName = null;
		
		base.OnJoinedRoom();
	}
    
	public override void OnRoomListUpdate (List< RoomInfo > roomList)
	{
		base.OnRoomListUpdate(roomList);
		UpdateList(roomList);
	}
	
	void UpdateList(List< RoomInfo > roomList)
	{
		Debug.LogError("void UpdateList(List< RoomInfo > roomList)");
		countText.text = roomList.Count.ToString();
		
		playerListText.text = "";
		
		for(int i = 0;i < roomList.Count; i++)
		{
			string you = roomList[i].Name == PhotonNetwork.CurrentRoom.Name? " (You are here)": "";
					
			
			if(roomList[i].Name != null)
				playerListText.text += "• "+roomList[i].Name + " " + roomList[i].PlayerCount + "/" + roomList[i].MaxPlayers +you + "\n";
			else
				playerListText.text += "• "+roomList[i].masterClientId + " " + roomList[i].PlayerCount + "/" + roomList[i].MaxPlayers +you + "\n";
		}
	}
}
