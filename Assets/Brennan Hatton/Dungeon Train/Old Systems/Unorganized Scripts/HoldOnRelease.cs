//Extention by Brennan Hatton, writen for DungeonTrain.com. 20200413
//Inherenting from BNG's code
#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class HoldOnRelease : GrabbableEvents
{
	public float timeBeforeReset = 1f;
	public HoldType switchTo = HoldType.Toggle;
	
	bool grabbedAgain = false;
	/// <summary>
	/// Item has been grabbed by a Grabber
	/// </summary>
	/// <param name="grabber"></param>
	public override void OnGrab(Grabber grabber) {
		
		grabbedAgain = true;
		base.OnGrab(grabber);
	}
	
	/// <summary>
	/// Has been dropped from the Grabber
	/// </summary>
	public override void OnRelease() {

		//change hold type
		grab.Grabtype = switchTo;
		
		grabbedAgain = false;
		//start timer for reset
		StartCoroutine(resetAfterTime());
		
		base.OnRelease();
	
	}
	
	/// <summary>
	/// Waits given time and resets to opposite hold type
	/// </summary>
	/// <returns></returns>
	IEnumerator resetAfterTime()
	{
		yield return new WaitForSeconds(timeBeforeReset);
		
		if(!grabbedAgain)
		{
			if(switchTo == HoldType.Toggle)
				grab.Grabtype = HoldType.HoldDown;
			else
				grab.Grabtype = HoldType.Toggle;
		}else
		{
			if(switchTo == HoldType.Toggle)
				switchTo = HoldType.HoldDown;
			else
				switchTo = HoldType.Toggle;
		}
	}
	
}
#endif