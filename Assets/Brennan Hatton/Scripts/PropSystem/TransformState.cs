using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.ComponentStates
{
	
	public class TransformState : MonoBehaviour, iState
	{
		//[HideInInspector]
		public Vector3 localPosition;
		//[HideInInspector]
		public Quaternion localRotation;
		//[HideInInspector]
		public Transform parent;
		
		public bool setPreviously = false;
		
		public bool Debugging = false;
		
		public void SetState(bool reset)
		{
			if(Debugging)
				Debug.Log("SetState: " + this.transform.parent + ".  Go head : " + (reset || !setPreviously) + ((reset || !setPreviously)?" - this is probably fine":" more info in Warnings"));
				
			if(reset || !setPreviously)//a start state should only be set nce
			{
				localPosition = this.transform.localPosition;
				localRotation = this.transform.localRotation;
				parent = this.transform.parent;
				setPreviously = true;
			}else
				Debug.LogWarning("SetState trying to be reset, when perhaps it has already been set. Ignoring request. " + Utilities.TransformUtils.HierarchyPath(this.transform, 5));
		}
		
		public void RevertToState()
		{
			//Debug.Log(Utilities.TransformUtils.HierarchyPath(transform,5));
			this.transform.SetParent(parent);
			this.transform.localPosition = localPosition;
			this.transform.localRotation = localRotation;
		}
		
		public void CopyStartState(iState stateToCopy)
		{
			localPosition = ((TransformState)stateToCopy).localPosition;
			localRotation = ((TransformState)stateToCopy).localRotation;
			parent = ((TransformState)stateToCopy).parent;
		}
	}

}