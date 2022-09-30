using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

namespace EqualReality.Networking.Events
{

	//update player visible
	//TODO: update lists of players
	public class ReceiveChangeSceneEvent : MonoBehaviour, IOnEventCallback
	{
		
		public UnityEvent beforeLoad;
		public float loadDelay = 0;
		
		private void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
		}
	
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}
	
		public void OnEvent(EventData photonEvent)
		{
			byte eventCode = photonEvent.Code;
			
			if(eventCode == SendEventManager.ChangeSceneEventCode)
			{
				//PhotonNetwork.LocalPlayer.ActorNumber, appId, sceneId, knotId, facilitator
				object[] data = (object[])photonEvent.CustomData;
				
				//PhotonNetwork.LocalPlayer
				int senderActorNumber = (int)data[0];
				int sceneBuildIndex = (int)data[1];
				int ERSceneId = (int)data[2];
				int knotId = (int)data[3];
				bool facilitator = (bool)data[4];
				
				//Debug.LogError("ReceiveEvent SendEventManager.ChangeSceneEventCode " + senderActorNumber + " " + sceneBuildIndex + " " + ERSceneId + " " + knotId + " " + facilitator);
				
				
				PlayerSetActive.UpdatePlayerInScene(senderActorNumber, sceneBuildIndex);
				
				
				if(facilitator && sceneBuildIndex != SceneManager.GetActiveScene().buildIndex)
				{
					beforeLoad.Invoke();
					
					if(loadDelay <= 0)
						SceneManager.LoadScene(sceneBuildIndex);
					else
						StartCoroutine(loadAfterTime(loadDelay,sceneBuildIndex));
				}
				
			}
		}
		
		IEnumerator loadAfterTime(float time, int appId)
		{
			yield return new WaitForSeconds(time);
			
			SceneManager.LoadScene(appId);
			
			yield return null;
		}
	}

}