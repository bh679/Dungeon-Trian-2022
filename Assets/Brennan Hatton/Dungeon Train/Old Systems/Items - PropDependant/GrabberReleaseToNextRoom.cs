#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;
using BrennanHatton.DungeonTrain.Rooms;
using BNG;

public class GrabberReleaseToNextRoom : MonoBehaviour
{
	public Grabber grabber;
	public RoomCreator roomCreator;
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
			//deattach from current placer (although this seems to already happen for some things
			propHeld.ChangePlacer(null);
	}
	
	public void OnRelease() {
		if(propHeld != null)
		{
			//propHeld.ChangePlacer(roomCreator.activeRoom[roomCreator.GetCurrentRoomRotationID(1)].placer);
		}
	}
}
#endif