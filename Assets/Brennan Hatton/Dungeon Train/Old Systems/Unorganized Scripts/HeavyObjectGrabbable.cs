#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNG
{
	
	public class HeavyObjectGrabbable : GrabbableEvents
	{
		public AddForceTowardsTargetScaled forceProvider;
		public CopyTransform selfTransformCopy;
		
		bool originalGravity = false;
		
		void Reset()
		{
			forceProvider = this.transform.parent.GetComponentInChildren<AddForceTowardsTargetScaled>();
			selfTransformCopy = this.GetComponent<CopyTransform>();
			
		}
		
		void OnEnable()
		{
			forceProvider.enabled =false;
			selfTransformCopy.RunOnUpdate(true);
		}
	    
		/// <summary>
		/// Item has been grabbed by a Grabber
		/// </summary>
		/// <param name="grabber"></param>
		public override void OnGrab(Grabber grabber) {
			forceProvider.enabled = true;
			selfTransformCopy.RunOnUpdate(false);
			originalGravity = forceProvider.myRb.useGravity;
			//forceProvider.myRb.useGravity= false;
		}
	        
		/// <summary>
		/// Has been dropped from the Grabber
		/// </summary>
		public override void OnRelease() {
			forceProvider.enabled = false;
			selfTransformCopy.RunOnUpdate(true);
			//forceProvider.myRb.useGravity= originalGravity;
		}
	}
}
#endif