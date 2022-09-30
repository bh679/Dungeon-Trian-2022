using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace EqualReality.Networking
{

	/// <summary>
	/// Networks Player tracking points
	///  - Headset, Left controller, Right controller
	/// </summary>
	public class NetworkPlayer : MonoBehaviour
	{
		public Transform head, leftHand, rightHand, liveHead = null, liveRHand, liveLHand;
		public int[] liveRoomIds;
		
		PhotonView photoView;
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    photoView = GetComponent<PhotonView>();
		    
	    }
	
	
	    // Update is called once per frame
	    void Update()
		{
			//if mine
		    if(photoView.IsMine)
		    {
		    	
			    //if live local headset is not assigned
			    if(liveHead == null)
			    {
			    	//find headset
				    liveHead = GameObject.Find("CenterEyeAnchor").transform;
				    //find conntrollers
				    liveLHand = GameObject.Find("LeftController").transform;
				    liveRHand = GameObject.Find("RightController").transform;
				    
			    }	
			    
			    //update netowrked transforms position and rotation from live XR system
		    	head.transform.position = liveHead.transform.position;
		    	head.transform.rotation = liveHead.transform.rotation;
		    	leftHand.transform.position = liveLHand.transform.position;
		    	leftHand.transform.rotation = liveLHand.transform.rotation;
		    	rightHand.transform.position = liveRHand.transform.position;
		    	rightHand.transform.rotation = liveRHand.transform.rotation;
		    }
	    }
	}
}
