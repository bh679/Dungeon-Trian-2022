using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace EqualReality.Networking.Events
{
	
	public class SendEventManager : MonoBehaviourPunCallbacks
	{
		// If you have multiple custom events, it is recommended to define them in the used class
		public const byte AskClassQuestionEventCode = 100,
		//StartExperienceEventCode = 101,
		ChangeAvatarEventCode = 102,
		ChangeSceneEventCode = 103,
		NetworkedButtonCode = 105,
		TriggerDataEvent = 104,
		ChangeMenuSelectionEventCode = 105,
		ControllerButtonPressEventCode = 106,
		EnableClassTeleportingEventCode = 107,
		BecomeFacilitatorEventCode = 108,
		teleportPlayerEventCode = 109,
		SendToRoomEventCode = 110,
		SendPrivateModeKeyEventCode = 111,
		EnableVRGuideEventCode = 112,
		PlayLineEventCode = 113,
		ChangeDataReviewEventCode = 114,
		ChangeScriptEventCode = 115,
		NameChangeEventCode = 116,
		ChangeScriptPageEventCode = 117,
		SendFacilitatorBellCode = 118,
		SendRequestSceneTransitionCode = 119,
		SendConfirmSceneTransitionCode = 120,
		SendCancelSceneTransitionCode = 121;
		
		
		
		public static void SendCancelSceneTransitionEvent()
		{
			Debug.Log("public static void SendConfirmSceneTransitionEvent ");
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(SendCancelSceneTransitionCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		
		public static void SendConfirmSceneTransitionEvent()
		{
			Debug.Log("public static void SendConfirmSceneTransitionEvent ");
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(SendConfirmSceneTransitionCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		public static void SendRequestSceneTransitionEvent()
		{
			Debug.Log("public static void SendRequestSceneTransitionEvent ");
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(SendRequestSceneTransitionCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		public static void SendFacilitatorBellEvent()
		{
			Debug.Log("public static void SendFacilitatorBellEvent ");
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(SendFacilitatorBellCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
	
		public static void SendNameChangeEvent()
		{
			Debug.Log("public static void SendChangeScriptEvent ");
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(NameChangeEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
	
		public static void SendChangeScriptEvent(int scriptId)
		{
			Debug.Log("public static void SendChangeScriptEvent " + scriptId);
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, scriptId };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(ChangeScriptEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		public static void SendChangeScriptPageEvent(int pageId)
		{
			Debug.Log("public static void SendChangeScriptPageEvent " + pageId);
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, pageId };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(ChangeScriptPageEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
	
		public static void SendChangeDataReviewEvent(int dataViewId)
		{
			Debug.Log("public static void SendChangeDataReviewEvent " + dataViewId);
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, dataViewId };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(ChangeDataReviewEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		public static void SendPlayLineEvent(int lineId)
		{
			Debug.Log("public static void SendPlayLineEvent");
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, lineId };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(PlayLineEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
	
		public static void SendPrivateModeKeyEvent(string privateRoomKey, bool privateMode)
		{
			Debug.Log("public static void SendPrivateModeKeyEvent(string privateRoomKey, string privateMode)");
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, privateRoomKey, privateMode };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(SendPrivateModeKeyEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
	
		public static void SendSendToRoomEvent(string roomName, byte voiceChannel = 0, int[] actorIds = null)
		{
			Debug.Log("public static void SendSendToRoomEvent(string roomName " + roomName +", string voiceChannel = "+voiceChannel+", int[] actorIds = null " +actorIds+")");
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, roomName, voiceChannel, actorIds, voiceChannel};
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(SendToRoomEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		public static void SendTeleportPlayerEvent(Transform destinationId)
		{
			SendTeleportPlayerEvent(new Vector2(destinationId.position.x,destinationId.position.z), destinationId.rotation);
		}
		
		public static void SendTeleportPlayerEvent(Vector2 destination, Quaternion rotation, int spread = 1, int[] targetPlayers = null)
		{
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, destination, rotation, spread, targetPlayers};
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(teleportPlayerEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
	
		public static void SendBecomeFacilitatorEvent(bool isEnabled)
		{
			PlayerCustomProperties.SetFacilitator(isEnabled);
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, isEnabled };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(BecomeFacilitatorEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
	
		public static void SendEnableClassTeleportingEvent(bool isEnabled)
		{
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, isEnabled };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(EnableClassTeleportingEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		public static void SendEnableVRGuideEvent(bool isEnabled)
		{
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, isEnabled };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(EnableVRGuideEventCode, content, raiseEventOptions, SendOptions.SendReliable);
			
			Debug.Log("sending EnableVRGuideEvent");
		}
	
		public static void SendAskClassQuestionEvent(TextList textList)
		{
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, textList.i }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(AskClassQuestionEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		/*public static void SendStartExperienceEvent(int id)
		{
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, id }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(StartExperienceEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}*/
		
		public static void SendChangeAvatarEvent(int id)
		{
			object[] content = new object[] { id }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(ChangeAvatarEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		public static void SendChangeScene(int sceneBuildIndex, int ERSceneId, int knotId, bool facilitator)
		{
			//App Id (build Index), scene, knot, playerId
			//Debug.Log(sceneBuildIndex + " " +ERSceneId + " " + knotId);
			
			PlayerCustomProperties.SetActiveScene();
			
			//App Id (build Index), scene, knot, playerId
			Debug.Log(sceneBuildIndex + " " +ERSceneId + " " + knotId);
			
			object[] content = new object[]{PhotonNetwork.LocalPlayer.ActorNumber, sceneBuildIndex, ERSceneId, knotId, facilitator};
			
			//object[] content = new object[] { id }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(ChangeSceneEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		public static void SendRoomInvite(string name)
		{
			Debug.Log("SendRoomInvite " + name);
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, name }; 
			//object[] content = new object[] { id }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(ChangeSceneEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		public static void SendTriggerData(float numberOfTriggers)
		{
			Debug.Log("SendTriggerData " + numberOfTriggers);
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, numberOfTriggers }; 
			
			//object[] content = new object[] { id }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(TriggerDataEvent, content, raiseEventOptions, SendOptions.SendReliable);
			
		}
		
		public static void SendNetworkedButton(int buttonId, int sceneBuildId, int eventType)
		{
			Debug.Log("SendNetworkedButton " + buttonId + sceneBuildId);
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, buttonId, sceneBuildId, eventType }; 
			
			//object[] content = new object[] { id }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(NetworkedButtonCode, content, raiseEventOptions, SendOptions.SendReliable);
			
		}
		
		public static void SendControllerButtonPress(int buttonId)
		{
			Debug.Log("SendNetworkedButton " + buttonId);
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, buttonId}; 
			
			//object[] content = new object[] { id }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(ControllerButtonPressEventCode, content, raiseEventOptions, SendOptions.SendReliable);
			
		}
		
		
		bool inivitedAndReturn;
		string returnRoomName;
		
		public void SendEventToInviteRooomHere(string name)
		{
			returnRoomName = PhotonNetwork.CurrentRoom.Name;
			
			//leave room
			PhotonNetwork.LeaveRoom();
			
			inivitedAndReturn = true;
			//join main menu
			PhotonNetwork.JoinRoom(name);
		}
		
		public static void SendEventChangeMenuSelect(int id)
		{
			Debug.Log("SendEventChangeMenuSelect " + id);
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, id}; 
			
			//object[] content = new object[] { id }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(ChangeMenuSelectionEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		
		
		
		public override void OnJoinedRoom()
		{
			if(inivitedAndReturn)
			{
				SendRoomInvite(returnRoomName);
				PhotonNetwork.LeaveRoom();
				PhotonNetwork.JoinRoom(returnRoomName);
			}
		}
		
		
		public override void 	OnJoinRoomFailed (short returnCode, string message)
		{
			if(inivitedAndReturn)
			{
				PhotonNetwork.JoinRoom(returnRoomName);
			}
		}
	}

}