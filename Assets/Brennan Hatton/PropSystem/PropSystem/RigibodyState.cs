using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.ComponentStates
{
	public class RigibodyState : MonoBehaviour, iState
	{
		public Rigidbody rb;
		[HideInInspector]
		public bool isKinematic, useGravity;
		[HideInInspector]
		public Vector3 velocity, angularVelocity;
		
		public bool setPreviously = false;
		
		public bool Debugging = false;
		
		void Reset()
		{
			rb = this.GetComponent<Rigidbody>();
		}
		
		void Awake()
		{
			if(!rb)
				rb = this.GetComponent<Rigidbody>();
		}
		
		
		
		public void SetState(bool reset)
		{
			if(Debugging)
				Debug.Log("SetState: " + this.transform.parent + ".  Go head : " + (reset || !setPreviously));
				
			if(reset || !setPreviously)
			{
				if(!rb)
					rb = this.GetComponent<Rigidbody>();
				
				isKinematic = rb.isKinematic;
				useGravity = rb.useGravity;
				velocity = rb.velocity;
				angularVelocity = rb.angularVelocity;
				setPreviously = true;
			}
		}
		
		public void RevertToState()
		{
			rb.isKinematic = isKinematic;
			rb.useGravity = useGravity;
			rb.velocity = velocity;
			rb.angularVelocity = angularVelocity;
		}
		
		
		public void CopyStartState(iState stateToCopy)
		{
			if(!rb)
				rb = this.GetComponent<Rigidbody>();
				
			isKinematic = ((RigibodyState)stateToCopy).isKinematic;
			useGravity = ((RigibodyState)stateToCopy).useGravity;
			velocity = ((RigibodyState)stateToCopy).velocity;
			angularVelocity = ((RigibodyState)stateToCopy).angularVelocity;
		}
	}
}