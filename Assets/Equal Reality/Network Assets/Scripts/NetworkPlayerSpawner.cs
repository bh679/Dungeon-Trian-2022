using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
	
	public string PlayerPrebName = "Network Player";
	public static List<PhotonView> spawnedPlayerPrefabs = new List<PhotonView>();
	
	/*public void OnEnable()
	{
		
		if(PhotonNetwork.InRoom)
		{
			spawnedPlayerPrefabs.Add(PhotonNetwork.Instantiate(PlayerPrebName, transform.position, transform.rotation).GetComponent<PhotonView>());
		}
		
		base.OnEnable();
	}*/
	
	public override void OnJoinedRoom()
	{
		//Debug.Log("public override void OnJoinedRoom() - PhotonNetwork.Instantiate(PlayerPrebName");
		
		base.OnJoinedRoom();
		spawnedPlayerPrefabs.Add(PhotonNetwork.Instantiate(PlayerPrebName, transform.position, transform.rotation).GetComponent<PhotonView>());
	}
	
	public override void OnLeftRoom()
	{
		base.OnLeftRoom();
		
		for (int i = 0; i < spawnedPlayerPrefabs.Count; i++)
		{
			if(spawnedPlayerPrefabs[i] != null)
				PhotonNetwork.Destroy(spawnedPlayerPrefabs[i].gameObject);
		}
	}
}
