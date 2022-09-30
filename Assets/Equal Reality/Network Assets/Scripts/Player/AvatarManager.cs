using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using BrennanHatton.LibraryOfBabel.Networking.Events;
using EqualReality.Networking;
using BrennanHatton.Utilities;

public class AvatarManager : MonoBehaviour
{
	public PhotonView photonView;
	public enableRandomGameobject eyes, mouth, head, body;
	public setMaterial materialSetter;
	int activeId = 0;
	
	public void SetAvatar()
	{
		int previousId = activeId;
		
		if(photonView.Owner.CustomProperties.ContainsKey(PlayerCustomProperties.AvatarId))
			activeId = (int)photonView.Owner.CustomProperties[PlayerCustomProperties.AvatarId];
		
		if(previousId != activeId)
			EnableFromSeed();
	}
	
	void EnableFromSeed()
	{
		int seed = Random.seed;
		
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
				children[i].gameObject.layer = LayerMask.NameToLayer("PlayerInvisible");
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
