using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BrennanHatton.Utilities
{
	public class TransformData{
		Vector3 position, localPosition, localScale;
		Quaternion rotation, localRotation;
		Transform parent;
		
		public TransformData(Transform transform)
		{
			//local data
			localPosition = transform.localPosition;
			localScale = transform.localScale;
			localRotation = transform.localRotation;
			
			//global data
			position = transform.position;
			rotation = transform.rotation;
			
			//parent
			parent = transform.parent;
		}
		
		public void ApplyLocalData(Transform transform, bool includeParent)
		{
			transform.localPosition = localPosition;
			transform.localScale = localScale;
			transform.localRotation = localRotation;
			
			if(includeParent)
			{
				transform.SetParent(parent);
			}
		}
		
		public void ApplyGlobalData(Transform transform, bool includeParent)
		{
			transform.position = position;
			transform.rotation = rotation;
			
			if(includeParent)
			{
				transform.SetParent(parent);
			}
		}
		
	}
}