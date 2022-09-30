using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

public class SendSceneDataTester : MonoBehaviour
{
	public Text text1, text2, text3, text4, eventId;
    
	public void Send()
	{
		
		object[] content = new object[]{int.Parse(text1.text), int.Parse(text2.text), int.Parse(text3.text), int.Parse(text4.text)};
		
		//object[] content = new object[] { id }; // Array contains the target position and the IDs of the selected units
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
		PhotonNetwork.RaiseEvent((byte)int.Parse(eventId.text), content, raiseEventOptions, SendOptions.SendReliable);
		
	}
}
