using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Positions.Extentions
{

	public class PositionScale : Position
	{
	    
		public Vector3 relativeScale = Vector3.one;
	    
		//called by ObjectPositionPool.PlaceInFreePosition
		public override void Place(Transform objectToPlace)
		{
			base.Place(objectToPlace);
			
			objectToPlace.localScale = new Vector3(objectToPlace.localScale.x * relativeScale.x, objectToPlace.localScale.y * relativeScale.y, objectToPlace.localScale.z * relativeScale.z );//this will need to change back at some point
		}
	}

}