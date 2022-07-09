using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BzKovSoft.ObjectSlicer.EventHandlers
{
	[DisallowMultipleComponent]
	public class SliceableReapplyColliderTrigger : MonoBehaviour, IBzObjectSlicedEvent
	{
		
		
		//public bool parentUp = false;
		public void ObjectSliced(GameObject original, GameObject resultNeg, GameObject resultPos)
		{
			// we need to wait one fram to allow destroyed component to be destroyed.
			StartCoroutine(NextFrame(original, resultNeg, resultPos));
		}

		private IEnumerator NextFrame(GameObject original, GameObject resultNeg, GameObject resultPos)
		{
			yield return null;
			
			

			var oCollider = original.GetComponent<Collider>();
			var aCollider = resultNeg.GetComponent<Collider>();
			var bCollider = resultPos.GetComponent<Collider>();

			if (oCollider == null)
			{
				yield break;
			}
			
			aCollider.isTrigger = oCollider.isTrigger;
			bCollider.isTrigger = oCollider.isTrigger;
		}
	}
}
