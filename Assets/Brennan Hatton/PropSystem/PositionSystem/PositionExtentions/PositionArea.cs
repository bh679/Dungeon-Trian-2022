using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools;

namespace BrennanHatton.Positions
{
	public class PositionArea : Position
	{
		public Vector3 center = Vector3.zero, size = Vector3.one;
		public bool canBeTaken = false;
		public bool checkSpaceIsFree = true;
		
		public bool Debugging = false;
		const int _checkFreeSpaceLimit = 500;
	    
		/// <summary>
		/// Places object and saves reference
		/// </summary>
		/// <param name="objectToPlace"></param>
		public override TransformData GetFreeTransformData(Transform objectToPlace)
		{
			
			//set position
			Vector3 relativePosition = new Vector3(Random.Range(0,size.x)-size.x/2f,
				Random.Range(0,size.y)-size.y/2f,
				Random.Range(0,size.z)-size.z/2f);
			
			int count = _checkFreeSpaceLimit;
			
			//check is space is free
			if(checkSpaceIsFree)
			{
				BoxCollider[] boxs = objectToPlace.GetComponentsInChildren<BoxCollider>();
				while (
					IsSpaceFree(relativePosition, boxs, objectToPlace) == false
					&& count > 0
				) {
					relativePosition = new Vector3(Random.Range(0,size.x)-size.x/2f,
						Random.Range(0,size.y)-size.y/2f,
						Random.Range(0,size.z)-size.z/2f);
					//Debug.Log("Repositining: " + relativePosition);
					count--;
					if(count <= 0)
						Debug.LogError("Couldnt find free space");
				}
			}
			
			TransformData data = new TransformData(this.transform.position + center + this.transform.rotation*relativePosition, GetEulerRotation());
			
			Debug.Log(data.ToDebugString());
			
			_isTaken = canBeTaken;
			
			return data;
		}
		
		bool IsSpaceFree(Vector3 relativePosition, BoxCollider[] boxs, Transform original)
		{
			for(int i = 0; i < boxs.Length; i++)
			{
				if(Physics.CheckBox(relativePosition+ boxs[i].center + (boxs[i].transform.position-original.position),boxs[i].size/2,Quaternion.identity,LayerMasks.Instance.placerColliders))
					return false;
			}
			
			return true;
		}
		
		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(transform.position + center, size);
		}
	}
}