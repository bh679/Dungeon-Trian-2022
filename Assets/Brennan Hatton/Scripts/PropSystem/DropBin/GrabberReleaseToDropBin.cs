using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;
using BrennanHatton.DungeonTrain.Rooms;

#if BNG
using BNG;

//this goes on the grabbing hand

//When a player picks up an item, it should be parented to the hand, and disconnected from the spawner
//When the player drops an item. It should go into the dropbin
public class GrabberReleaseToDropBin : MonoBehaviour
{
	public Grabber grabber;
	//public Transform holdBin;
	public DroppedItemCleaner clearner;
	Prop propHeld;
	
	void Reset()
	{
		grabber = this.GetComponent<Grabber>();
	}
	
	void Start()
	{
		if(!grabber)
			grabber = this.GetComponent<Grabber>();
	}
	
	bool heldLastFrame = false;
	
	void Update()
	{
		if (grabber.HoldingItem && heldLastFrame == false) {
			//grabbing  
			OnGrab();
		}else
			if (!grabber.HoldingItem && heldLastFrame == true) {
			//releasing  
			OnRelease();
		}
		
		heldLastFrame = grabber.HoldingItem;
	}
	
	public void OnGrab() {
		propHeld = grabber.HeldGrabbable.GetComponent<Prop>();
		
		if(propHeld != null)
			clearner.GrabProp(propHeld, grabber.HeldGrabbable);
		//grabber.HeldGrabbable.UpdateOriginalParent(dropBin);
	}
	
	public void OnRelease() {
		if(propHeld != null)
		{
			clearner.DropProp(propHeld);
			propHeld = null;
		}
	}
}
#endif