using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayersInCurrentRoom : MonoBehaviour
{
	public Text text;
	
	void Reset()
	{
		text = this.GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
	{
		if(PhotonNetwork.InRoom)
	    	text.text = ((int)PhotonNetwork.CurrentRoom.PlayerCount).ToString();
    }
}
