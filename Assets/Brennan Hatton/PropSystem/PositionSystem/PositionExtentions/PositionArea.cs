using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Positions.Extentions
{
	public class PositionArea : Position
	{
		public Vector3 center = Vector3.zero, size = Vector3.one;
		
		public bool Debugging = false;
	    
		/// <summary>
		/// Places object and saves reference
		/// </summary>
		/// <param name="objectToPlace"></param>
		public override void Place(Transform objectToPlace)
		{
			//track objects in this position
			objectsInPosition.Add(objectToPlace);
			
			//set position
			Vector3 relativePosition = new Vector3(Random.Range(0,size.x)-size.x/2f,
				Random.Range(0,size.y)-size.y/2f,
				Random.Range(0,size.z)-size.z/2f);
			
			objectToPlace.position = this.transform.position + center + this.transform.rotation*relativePosition;
			
			//set rotation
			objectToPlace.eulerAngles = GetEulerRotation();
			
			_isTaken = false;
		}
		
		/*public override void RemoveAllObjects()
		{
			
			if(objectToRemove != null)
			{
				return;
				
				int i = objectsInArea.IndexOf(objectToRemove);
				if(i == null || i < 0 || i >= objectsInArea.Count)
					return;
				objectsInArea[i].position = previousPositions[i];
				objectsInArea.RemoveAt(i);
				objectsInArea.RemoveAt(i);
				return;
			}
			
			//Debug.Log("RemoveObject");
			_isTaken = false;
			
			if(objectsInArea == null || previousPositions == null)
				return;
				
			for(int i = 0; i < objectsInArea.Count; i++)
			{
				objectsInArea[i].position = previousPositions[i];
			
				if(Debugging)	Debug.Log("Removing " + objectsInArea[i].name);
			}
			objectsInArea = new List<Transform>();
			previousPositions = new List<Vector3>();
		}*/
		
		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(transform.position + center, size);
		}
	}
}