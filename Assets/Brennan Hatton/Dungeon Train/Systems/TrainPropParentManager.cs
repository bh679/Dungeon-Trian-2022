using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.TrainCarts
{
	public class TrainPropParentManager : MonoBehaviour
	{
		public Transform parentWhenDropped;
		
		public void Reparent()
		{
			if(parentWhenDropped == null)
			{
				TrainCart cart = TrainCartManager.Instance.carts[1];
				
				parentWhenDropped = cart.transform;
			}
			
			this.transform.SetParent(parentWhenDropped);
			
		}
	}
}