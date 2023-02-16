using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using BrennanHatton.UnityTools;

namespace EqualReality.Networking
{

	public class AvatarUI : MonoBehaviour
	{
		public int AvatarId = 0;
		
		public PlayerCustomProperties customPropSetter;
		
		public EnableRandomGameobject eyes, mouth, head, body;
		public SetMaterial materialSetter;
	    
		void Start()
		{
			if(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey(PlayerCustomProperties.AvatarId))
				SetFromId((int)PhotonNetwork.LocalPlayer.CustomProperties[PlayerCustomProperties.AvatarId]);
			
		}
	    
		public void SetFromId(int id)
		{
			//Debug.LogError(id);
			Random.seed = id;
			eyes.enableRandom();
			mouth.enableRandom();
			head.enableRandom();
			body.enableRandom();
			materialSetter.SetRandomMaterial();
		}
	}
}