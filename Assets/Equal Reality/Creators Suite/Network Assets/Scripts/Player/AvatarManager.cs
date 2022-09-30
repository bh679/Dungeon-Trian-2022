using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using EqualReality.Networking.Events;//the need for this should be removed into a seperate class, that references this class to get what it needs
using EqualReality.Networking;
using BrennanHatton.Utilities;

public class AvatarManager : MonoBehaviour
{
	public PhotonView photonView;
	public enableRandomGameobject eyes, mouth, head, body;
	public setMaterial materialSetter;
	int activeId = 0;
	
	/*public static List<AvatarManager> avatars = new List<AvatarManager>();
	
	void Awake(){
		avatars.Add(this);
	}*/
	
	public void SetAvatar()
	{
		//Debug.Log(photonView.Owner.CustomProperties + " " + (photonView.Owner.CustomProperties.ContainsKey(PlayerCustomProperties.AvatarId)).ToString());
		int previousId = activeId;
		
		if(photonView.Owner.CustomProperties.ContainsKey(PlayerCustomProperties.AvatarId))
			activeId = (int)photonView.Owner.CustomProperties[PlayerCustomProperties.AvatarId];
		
		if(previousId != activeId)
			EnableFromSeed();
	}
	
	void EnableFromSeed()
	{
		int seed = Random.seed;
		//Debug.LogError(activeId);
		Random.seed = activeId;
		eyes.enableRandom();
		mouth.enableRandom();
		head.enableRandom();
		body.enableRandom();
		materialSetter.SetRandomMaterial();
		
		Random.seed = seed;
	}
	
    // Start is called before the first frame update
	void Start()
	{
		activeId = (int)System.DateTime.Now.TimeOfDay.TotalMilliseconds;
	
		if(photonView.Owner.CustomProperties.ContainsKey(PlayerCustomProperties.AvatarId))
			activeId = (int)photonView.Owner.CustomProperties[PlayerCustomProperties.AvatarId];
		else if(photonView.Owner.IsLocal)
		{
			PlayerCustomProperties.SetAvatarId(activeId);
			SendEventManager.SendChangeAvatarEvent(PhotonNetwork.LocalPlayer.ActorNumber);
		}
		
		EnableFromSeed();
		
		if(photonView.Owner.IsLocal)
		{
			Transform[] children = head.GetComponentsInChildren<Transform>(true);
			
			for(int i =0 ; i < children.Length; i++)
			{
				children[i].gameObject.layer = LayerMask.NameToLayer("Player");
			}
		}
	}
	
    
	void TurnOffGroup(List<GameObject> objectsToTurnOff)
	{
		for(int i = 0; i < objectsToTurnOff.Count; i++)
		{
			objectsToTurnOff[i].SetActive(false);
		}
	}
	
	/*void OnDestory()
	{
		avatars.Remove(this);
	}
	
	public static Color GetColorOfAvatar(int actorNumber)
	{
		for(int i = 0; i < avatars.Count; i++)
		{
			if(avatars[i].photonView.Owner.ActorNumber == actorNumber)
				return avatars[i].materialSetter.meshRenderers[0].material.GetColor("_Highlight Color");
		}
		
		return Color.white;
	}*/
}
