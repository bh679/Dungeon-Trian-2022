using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomOptionsToText : MonoBehaviourPunCallbacks
{
	
	public Text Visible;
	public Text Open;
	public Text Limit;

    // Update is called once per frame
	void OnEnable()
	{
		if(PhotonNetwork.InRoom)
		{
			if(PhotonNetwork.CurrentRoom.IsVisible)
				Visible.text = "True";
			else
				Visible.text = "False";
	
			if(PhotonNetwork.CurrentRoom.IsOpen)
				Open.text = "True";
			else
				Open.text = "False";
				
			Limit.text = ((int)PhotonNetwork.CurrentRoom.MaxPlayers).ToString();
		}
		
		base.OnEnable();
	}
	
	
	public override void OnJoinedRoom()
	{
			if(PhotonNetwork.CurrentRoom.IsVisible)
				Visible.text = "True";
			else
				Visible.text = "False";
	
			if(PhotonNetwork.CurrentRoom.IsOpen)
				Open.text = "True";
			else
				Open.text = "False";
				
		Debug.Log("public override void OnJoinedRoom(");
			Limit.text = ((int)PhotonNetwork.CurrentRoom.MaxPlayers).ToString();
		
		base.OnJoinedRoom();
	}
}
