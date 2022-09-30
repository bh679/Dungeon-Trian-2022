using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CurrentRoomNameToText : MonoBehaviourPunCallbacks
{
	
	public Text text;
    
	void Reset()
	{
		text = this.GetComponent<Text>();
	}
	
	public void OnEnable()
	{
		if(PhotonNetwork.InRoom)
			text.text = PhotonNetwork.CurrentRoom.Name;
		base.OnEnable();
	}
    
	public override void OnJoinedRoom()
	{
		text.text = PhotonNetwork.CurrentRoom.Name;
		
		base.OnJoinedRoom();
	}
}
