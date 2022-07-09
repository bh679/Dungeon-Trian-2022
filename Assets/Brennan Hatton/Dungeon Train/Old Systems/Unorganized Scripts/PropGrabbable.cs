#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using BrennanHatton.Props;

public class PropGrabbable : GrabbableEvent
{
	//public Prop prop;
	
	void Reset()
	{
		//prop = this.GetComponent<Prop>();
	}
	
	
	/*public override void OnGrab(Grabber grabber) {
		prop.detached = true;
		
		base.OnGrab();
	}*/
}
#endif