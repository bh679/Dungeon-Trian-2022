//Extention by Brennan Hatton, writen for DungeonTrain.com. 20200413
//Inherenting from BNG's code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if BNG
using BNG;

public class GrabbableEventOnHand : GrabbableEvents
{
	
	//Set which hand you want to call the events with
	public ControllerHand Hand = ControllerHand.Left;
	
	//for remembering hand 
	bool HeldByCorrectHand = false;
	
	//unity events
	public UnityEvent onGrab = new UnityEvent(), 
		onRelease = new UnityEvent();
    
	/// <summary>
	/// Item has been grabbed by a Grabber
	/// </summary>
	/// <param name="grabber"></param>
	public override void OnGrab(Grabber grabber) {
		
		//is it the correct hand?
		HeldByCorrectHand = (grabber.HandSide == Hand);
			
		//exit if it is not in the correct
		if(!HeldByCorrectHand)
			return;
			
		onGrab.Invoke();
		base.OnGrab(grabber);
	}
        
	/// <summary>
	/// Has been dropped from the Grabber
	/// </summary>
	public override void OnRelease() {
		
		//exit if it is not in the correct
		if(!HeldByCorrectHand)
			return;
		
		onRelease.Invoke();
		base.OnRelease();

	}

}
#endif