using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Networking.PUN;

namespace BrennanHatton.TrainCarts.Networking
{

	public class TrainCartNetworking : MonoBehaviour
	{
		public Launcher launcher;
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
		    if(TrainCartManager.Instance.carts[1].playerInside)
		    {
		    	string roomName = TrainCartManager.Instance.carts[1].seed;
		    	launcher.SetRoomName(roomName);
		    }
	    }
	}

}