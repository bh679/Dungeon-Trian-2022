using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EqualReality.UserModes;

namespace EqualReality.Networking.Events
{

	public class SendFacilitatorEvent : MonoBehaviour
	{
		bool modeLastFrame;
	
	    // Update is called once per frame
	    void Update()
		{
			//has the faciltator mode changed?
			if(modeLastFrame != Facilitator.mode)
				//if yes, send current mode
				SendEventManager.SendBecomeFacilitatorEvent(Facilitator.mode);
	    	
			//save current mode, to check against nextime
		    modeLastFrame = Facilitator.mode;
	    }
	}

}