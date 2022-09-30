using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ServerStatusToText : MonoBehaviourPunCallbacks // IConnectionCallbacks, IMatchmakingCallbacks, IInRoomCallbacks, ILobbyCallbacks, IWebRpcCallback, and IErrorInfoCallback.
{
	public Text text;
	public GameObject[] inRoom, inLobby, connectedAndReady, connected, offOnceInRoom, offlineMessagesComplete;
	public float timeTillConectionIssus = 15;
	public static bool connectedChecked = false;

	public string inRoomMsg = "In class", 
		//inLobbyMsg = "In Lobby ", 
		onConnectedMsg = "Connecting to server.", 
		onConnectedToMasterMsg = "Joining Class.", 
		offlineMsg;
	public string[] 
		offlineMsgs = {"Trying to connect.",  "Checking connection issues.","Please check your internet Connection"};

	void Reset()
	{
		text = this.GetComponent<Text>();
	}
	
	void Start()
	{
		if(text != null)
		{
			if(PhotonNetwork.InRoom)
				text.text = inRoomMsg + PhotonNetwork.CurrentRoom.Name;
			//else if(PhotonNetwork.InLobby)
			//	text.text = inLobbyMsg + PhotonNetwork.CurrentLobby.Name;
			else if(PhotonNetwork.IsConnectedAndReady)
				text.text = onConnectedToMasterMsg;
			else if(PhotonNetwork.IsConnected)
				text.text = onConnectedMsg;
			else
				text.text = offlineMsg;
		}
			
			
		SetActiveGameObjects(inLobby, PhotonNetwork.InLobby);
		SetActiveGameObjects(connectedAndReady, PhotonNetwork.IsConnectedAndReady);
		SetActiveGameObjects(connected, PhotonNetwork.IsConnected);
		SetActiveGameObjects(inRoom, PhotonNetwork.InRoom);
		
		if(connectedChecked)
			SetActiveGameObjects(offlineMessagesComplete, !connectedChecked);
		
		StartCoroutine(CheckInternetMessage());
	}
	
	IEnumerator CheckInternetMessage(){
		
		if(connectedChecked || offlineMsgs.Length == 0)
			yield return null;
		else
		{
			yield return new WaitForSeconds(timeTillConectionIssus);
			offlineMsg = offlineMsgs[1];
			if(text != null) text.text = offlineMsg;
			
			if(connectedChecked || offlineMsgs.Length == 1)
				yield return null;
			else
			{
				yield return new WaitForSeconds(timeTillConectionIssus);
				offlineMsg = offlineMsgs[2];
				if(text != null) text.text = offlineMsg;
			
				if(connectedChecked || offlineMsgs.Length == 2)
					yield return null;
				else
				{
					yield return new WaitForSeconds(timeTillConectionIssus);
					offlineMsg = offlineMsgs[3];
					
					if(text != null) text.text = offlineMsg;
			
					Debug.Log("connectedChecked");
					SetActiveGameObjects(offlineMessagesComplete,true);
					
					yield return null;
				}
			}
		}
	}
	
	void SetActiveGameObjects(GameObject[] gameObjects, bool on)
	{
		for( int i =0; i < gameObjects.Length; i++)
		{
			gameObjects[i].SetActive(on);
		}
	}
	
	//Called to signal that the raw connection got established but before the client can call operation on the server. More...
	public override void 	OnConnected ()
	{
		
		base.OnConnected();
		
		SetActiveGameObjects(inLobby, PhotonNetwork.InLobby);
		SetActiveGameObjects(connectedAndReady, PhotonNetwork.IsConnectedAndReady);
		SetActiveGameObjects(connected, PhotonNetwork.IsConnected);
		SetActiveGameObjects(inRoom, PhotonNetwork.InRoom);
		SetActiveGameObjects(offOnceInRoom, !PhotonNetwork.InRoom);
		if(text)
			text.text = onConnectedMsg;
			
		connectedChecked = true;
		SetActiveGameObjects(offlineMessagesComplete,false);
	}
	
 
	//Called when the local user/client left a room, so the game's logic can clean up it's internal state. More...irtual void 	OnLeftRoom ()
	public override void OnLeftRoom ()
	{
		
		base.OnConnected();
		
		
		SetActiveGameObjects(inRoom, PhotonNetwork.InRoom);
		SetActiveGameObjects(offOnceInRoom, !PhotonNetwork.InRoom);
		
		if(text)
			text.text = "Room left";
	}
 
	public override void 	OnMasterClientSwitched (Player newMasterClient)
	{
		
		base.OnMasterClientSwitched(newMasterClient);
		
		
		SetActiveGameObjects(inLobby, PhotonNetwork.InLobby);
		SetActiveGameObjects(connectedAndReady, PhotonNetwork.IsConnectedAndReady);
		SetActiveGameObjects(connected, PhotonNetwork.IsConnected);
		SetActiveGameObjects(inRoom, PhotonNetwork.InRoom);
		SetActiveGameObjects(offOnceInRoom, !PhotonNetwork.InRoom);
		
		if(text)
			text.text = "Master Client Switched to " + newMasterClient.NickName;
	}
	//Called after switching to a new MasterClient when the current one leaves. More...
 
	public override void 	OnCreateRoomFailed (short returnCode, string message)
	{
		
		base.OnCreateRoomFailed(returnCode, message);
		
		
		if(text != null) text.text = "Create Room Failed";
	}
	//Called when the server couldn't create a room (OpCreateRoom failed). More...
 
	public override void 	OnJoinRoomFailed (short returnCode, string message)
	{
		
		base.OnJoinRoomFailed(returnCode, message);
		
		
		if(text != null) text.text = "Room Joined Failed. " + returnCode.ToString() + " " + message;
		SetActiveGameObjects(inRoom, PhotonNetwork.InRoom);
		SetActiveGameObjects(offOnceInRoom, !PhotonNetwork.InRoom);
	}
	//Called when a previous OpJoinRoom call failed on the server. More...
 
	public override void 	OnCreatedRoom ()
	{
		
		base.OnCreatedRoom();
		
		
		if(text != null) text.text = "Room Created";
	}
	//Called when this client created a room and entered it. OnJoinedRoom() will be called as well. More...
 
	public override void 	OnJoinedLobby ()
	{
		
		base.OnJoinedLobby();
		
		
		SetActiveGameObjects(inLobby, PhotonNetwork.InLobby);
		if(text != null) text.text = "Joined Lobby " + PhotonNetwork.CurrentLobby.Name;
	}
	//Called on entering a lobby on the Master Server. The actual room-list updates will call OnRoomListUpdate. More...
 
	public override void 	OnLeftLobby ()
	{
		
		base.OnLeftLobby();
		
		
		SetActiveGameObjects(inLobby, PhotonNetwork.InLobby);
		if(text != null) text.text = "Lobby Left";
	}
	//Called after leaving a lobby. More...
 
	public override void 	OnDisconnected (DisconnectCause cause)
	{
		
		base.OnDisconnected(cause);
		
		
		SetActiveGameObjects(inLobby, PhotonNetwork.InLobby);
		SetActiveGameObjects(connectedAndReady, PhotonNetwork.IsConnectedAndReady);
		SetActiveGameObjects(connected, PhotonNetwork.IsConnected);
		SetActiveGameObjects(inRoom, PhotonNetwork.InRoom);
		SetActiveGameObjects(offOnceInRoom, !PhotonNetwork.InRoom);
		
		if(connectedChecked && offlineMsgs.Length > 0)
			if(text != null) text.text = offlineMsgs[0] + cause.ToString();
		else
			if(text != null) text.text = offlineMsg ;
	}
	//Called after disconnecting from the Photon server. It could be a failure or intentional More...
 
	public override void 	OnRegionListReceived (RegionHandler regionHandler)
	{
		
		base.OnRegionListReceived(regionHandler);
		
		
		if(text != null) text.text = "Region List Received.";
	}
	//Called when the Name Server provided a list of regions for your title. More...
 
	public override void 	OnRoomListUpdate (List< RoomInfo > roomList)
	{
		
		base.OnRoomListUpdate(roomList);
		
		
		if(text != null) text.text = "Room List Updated";
	}
	//Called for any update of the room-listing while in a lobby (InLobby) on the Master Server. More...
 

	public override void OnJoinedRoom()
	{
		
		base.OnJoinedRoom();
		
		
		if(text != null) text.text = inRoomMsg + PhotonNetwork.CurrentRoom.Name;
		SetActiveGameObjects(inRoom, PhotonNetwork.InRoom);
		SetActiveGameObjects(offOnceInRoom, !PhotonNetwork.InRoom);
	}
	
 
	public override void 	OnPlayerEnteredRoom (Player newPlayer)
	{
		
		base.OnPlayerEnteredRoom(newPlayer);
		
		
		if(text != null) text.text = "Player entered room " + newPlayer.NickName;
	}
	//Called when a remote player entered the room. This Player is already added to the playerlist. More...
 
	public override void 	OnPlayerLeftRoom (Player otherPlayer)
	{
		
		base.OnPlayerLeftRoom(otherPlayer);
		
		
		if(text != null) text.text = "Player left room " + otherPlayer.NickName;
	}
	//Called when a remote player left the room or became inactive. Check otherPlayer.IsInactive. More...
 
	public override void 	OnJoinRandomFailed (short returnCode, string message)
	{
		
		base.OnJoinRandomFailed(returnCode, message);
		
		
		if(text != null) text.text = "Join Random Failed " + returnCode.ToString() + " " + message;
	}
	//Called when a previous OpJoinRandom call failed on the server. More...
 
	public override void 	OnConnectedToMaster ()
	{
		
		base.OnConnectedToMaster();
		
		
		SetActiveGameObjects(inLobby, PhotonNetwork.InLobby);
		SetActiveGameObjects(connectedAndReady, PhotonNetwork.IsConnectedAndReady);
		SetActiveGameObjects(connected, PhotonNetwork.IsConnected);
		SetActiveGameObjects(inRoom, PhotonNetwork.InRoom);
		if(text != null) text.text = onConnectedToMasterMsg;
	}
	//Called when the client is connected to the Master Server and ready for matchmaking and other tasks. More...
 
	/*public override void 	OnRoomPropertiesUpdate (Hashtable propertiesThatChanged)
	{
		text.text = "Room Properties updated";
	}*/
	//Called when a room's custom properties changed. The propertiesThatChanged contains all that was set via Room.SetCustomProperties. More...
 
	/*public override void 	OnPlayerPropertiesUpdate (Player targetPlayer, Hashtable changedProps)
	{
		text.text = "Player properties updated for " + targetPlayer.NickName;
	}*/
	//Called when custom player-properties are changed. Player and the changed properties are passed as object[]. More...
 
	public override void 	OnFriendListUpdate (List< FriendInfo > friendList)
	{
		
		base.OnFriendListUpdate(friendList);
		
		
		if(text != null) text.text = "Friend list updated.";
	}
	//Called when the server sent the response to a FindFriends request. More...
 
	public override void 	OnCustomAuthenticationResponse (Dictionary< string, object > data)
	{
		
		base.OnCustomAuthenticationResponse(data);
		
		
		if(text != null) text.text = "Custom Authentication Response";
	}
	//Called when your Custom Authentication service responds with additional data. More...
 
	public override void 	OnCustomAuthenticationFailed (string debugMessage)
	{
		
		base.OnCustomAuthenticationFailed(debugMessage);
		
		
		if(text != null) text.text = "Custom Authentication Failed. " + debugMessage;
	}
	//Called when the custom authentication failed. Followed by disconnect! More...
 
	/*public override void 	OnWebRpcResponse (OperationResponse response)
	{
		text.text = "Web Rpc Response" + PhotonNetwork.CurrentRoom.Name;
	}*/
	//Called when the response to a WebRPC is available. See LoadBalancingClient.OpWebRpc. More...
 
	public override void 	OnLobbyStatisticsUpdate (List< TypedLobbyInfo > lobbyStatistics)
	{
		
		base.OnLobbyStatisticsUpdate(lobbyStatistics);
		
		
		if(text != null) text.text = "Lobb yStatistics Update";
	}
	//Called when the Master Server sent an update for the Lobby Statistics. More...
 
 //	Called when the client receives an event from the server indicating that an error happened there. More...
	public override void 	OnErrorInfo (ErrorInfo errorInfo)
	{
		
		base.OnErrorInfo(errorInfo);
		
		
		if(text != null) text.text = "Error info recieved: " + errorInfo.Info;
	}
}
