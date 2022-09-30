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
		    if(TrainCartManager.Instance.cartChanged || TrainCartManager.Instance.teleported)//.carts[1].playerInside)
		    {
		    	launcher.SetRoomFromSeed(TrainCartManager.Instance.roomId+1, TrainCartManager.Instance.seedBase);
		    }
	    }
	}

}