using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using ExitGames.Client.Photon;
using BrennanHatton.LibraryOfBabel.Networking.Events;
using EqualReality.Networking;

public class PlayerSetActive : MonoBehaviourPunCallbacks
{
	public PhotonView photonView;
	
	static bool playersEnabled = true;
	static bool localPlayerEnabled = true;
	
	public bool sameSceneOnLoad = true;
	public bool active = true;
	
	public GameObject[] playerObjects;
	
	public int appId; //we could update this value from the event recived, but Im not yet
	
	public static List<PlayerSetActive> players = new List<PlayerSetActive>();
	
	public static void SetPlayersEnabledAll(bool active)
	{
		playersEnabled = active;
		
		for(int i = 0; i < players.Count; i++)
		{
			Debug.Log(players[i].gameObject.name +" "+active);
			Debug.Log(players[i].gameObject);
			
			Debug.Log("players[i].active" +" "+players[i].active);
			
			players[i].SetPlayerActive(active);//players[i].active);
		}
	}
	
	public static void SetPlayersEnabledInScene(bool active)
	{
		playersEnabled = active;
		
		for(int i = 0; i < players.Count; i++)
		{
			Debug.Log(players[i].gameObject.name +" "+active);
			Debug.Log(players[i].gameObject);
			if(players[i].photonView.Owner.CustomProperties.ContainsKey(PlayerCustomProperties.ActiveScene))
			{
				Debug.Log((int)players[i].photonView.Owner.CustomProperties[PlayerCustomProperties.ActiveScene]);
				Debug.Log(SceneManager.GetActiveScene().buildIndex);
				active = ((int)players[i].photonView.Owner.CustomProperties[PlayerCustomProperties.ActiveScene] == SceneManager.GetActiveScene().buildIndex);
			
			}
				else
			active = true;
			
			Debug.Log("players[i].active" +" "+players[i].active);
			
			players[i].SetPlayerActive(active);
		}
	}
	
	public static void SetLocalPlayerEnabled(bool active)
	{
		localPlayerEnabled = active;
		
		for(int i = 0; i < players.Count; i++)
		{
			if(players[i].photonView.IsMine)
				players[i].SetPlayerActive(localPlayerEnabled);
		}
	}
	
	void Reset()
	{
		playerObjects = new GameObject[transform.childCount];
		
		for(int i = 0 ;i < playerObjects.Length; i++)
		{
			playerObjects[i] = transform.GetChild(i).gameObject;
		}
		
		photonView = this.GetComponent<PhotonView>();
	}
	// called first
	void OnEnable()
	{
		base.OnEnable();
		
		if(photonView.IsMine)
		{
			appId = SceneManager.GetActiveScene().buildIndex;
			//SendEventManager.SendChangeCartEvent(); -- put this back in when you figure out how to send it without the whole cart number
		}
		
		EnableIfInSceneFromProperties();
		
		players.Add(this);
		//Debug.Log("OnEnable called");
		SceneManager.sceneLoaded += OnSceneLoaded;
		PhotonNetwork.AddCallbackTarget(this);
		
	}

	// called second
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("OnSceneLoaded " + photonView.Owner.NickName);
		
		//if(sameSceneOnLoad)
		//	EnableIfInSceneFromProperties();
			
		if(photonView.IsMine)
		{
			appId = SceneManager.GetActiveScene().buildIndex;
			//SendEventManager.SendChangeCartEvent(appId, -1, -1, false); -- put this back in when you figure out how to send it without the whole cart number
		}
		else
			EnableIfInSceneFromProperties();
			
		//Debug.Log("OnSceneLoaded: " + scene.name);
		Debug.Log(mode);
	}
	
	public void SetPlayerActive(bool value)
	{
		active = value && playersEnabled;
		Debug.Log("value " + value + "    playersEnabled " + playersEnabled);
		active = value && playersEnabled;
		Debug.Log("active " + active);
		Debug.Log("localPlayerEnabled " + localPlayerEnabled);
		if(!localPlayerEnabled && photonView.IsMine)
			active = false;
		
		for(int i =0 ;i < playerObjects.Length; i++)
		{
			playerObjects[i].SetActive(active);
		}
	}
	
	//make this check what art they are in
	public void EnableIfInSceneFromProperties()
	{
		if(photonView.Owner == null)
		{
			Debug.Log("Owner reference missing. photonView.Owner = null");
			return;
		}
		
		if(photonView.Owner.CustomProperties.ContainsKey(PlayerCustomProperties.ActiveScene))
		{
			if(SceneManager.GetActiveScene().buildIndex == -1) 
				SetPlayerActive(0 == (int)photonView.Owner.CustomProperties[PlayerCustomProperties.ActiveScene]);
			else
				SetPlayerActive(SceneManager.GetActiveScene().buildIndex == (int)photonView.Owner.CustomProperties[PlayerCustomProperties.ActiveScene]);
		}else
		{
			//loop wait check
			StartCoroutine(_checkPropertiesUntilSetPlayerActive());
		}
	}
	
	IEnumerator _checkPropertiesUntilSetPlayerActive()
	{
		float seconds = 1f;
		
		while(photonView.Owner.CustomProperties.ContainsKey(PlayerCustomProperties.ActiveScene) == false)
		{
			Debug.Log("Checking for actor " +photonView.Owner.ActorNumber+" properties in " + seconds + " seconds.");
			yield return new WaitForSeconds(seconds);
			
			seconds = seconds+1+seconds/2f;
		}
		
		SetPlayerActive(0 == (int)photonView.Owner.CustomProperties[PlayerCustomProperties.ActiveScene]);
		
		yield return null;
	}

	// called third
	/*void Start()
	{
	}*/

	// called when the game is terminated
	void OnDisable()
	{
		base.OnDisable();
		
		//Debug.Log("OnDisable");
		SceneManager.sceneLoaded -= OnSceneLoaded;
		
		players.Remove(this);
		PhotonNetwork.RemoveCallbackTarget(this);
	}
	
	
	public void OnEvent(EventData photonEvent)
	{		
		
		byte eventCode = photonEvent.Code;
		if (eventCode == SendEventManager.ChangeCartEventCode)
		{
			Debug.Log("eventCode == SendEventManager.ChangeSceneEventCode" + " " + photonView.Owner.NickName);
			object[] data = (object[])photonEvent.CustomData;
				
			int senderId = (int)data[0];
			string _room = (string)data[1];
			
			//if(senderId == photonView.Owner.ActorNumber) - TODO make it check the cart the player is in and turn them on
				//	SetPlayerActive(_room == SceneManager.GetActiveScene().buildIndex);
		}
	}
	
	public static void UpdatePlayerInScene(int actor, int appId)
	{
		//for all players
		for(int i = 0; i < players.Count; i++)
		{
			//if correct player
			if(players[i].photonView.Owner.ActorNumber == actor)
			{
				//turn on based on same scene
				players[i].SetPlayerActive(appId == SceneManager.GetActiveScene().buildIndex);
			}
		}
	}
}
