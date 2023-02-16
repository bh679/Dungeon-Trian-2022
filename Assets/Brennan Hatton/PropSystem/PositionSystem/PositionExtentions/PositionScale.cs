using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools;

namespace BrennanHatton.Positions
{

	public class PositionScale : Position
	{
	    
		public Vector3 relativeScale = Vector3.one;
		
		
		public override TransformData GetFreeTransformData(Transform objectToPlace)
		{
			//place object into position
			TransformData data = base.GetFreeTransformData(objectToPlace);
			
			objectToPlace.localScale = new Vector3(objectToPlace.localScale.x * relativeScale.x, objectToPlace.localScale.y * relativeScale.y, objectToPlace.localScale.z * relativeScale.z );
			
			return data;
			
		}
	}

}