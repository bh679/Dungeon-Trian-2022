using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EqualReality.Networking.Events
{

	public class SendTeleportPlayerEvent : MonoBehaviour
	{
		Transform player;
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    player = Camera.main.transform.parent.parent.parent;
	    }
	
		public void TeleportAllToPlayer()
		{
			SendEventManager.SendTeleportPlayerEvent(player);
		}
	}

}