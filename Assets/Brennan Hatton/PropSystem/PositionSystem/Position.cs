using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Utilities;

namespace BrennanHatton.Positions
{
	public class Position : MonoBehaviour
	{
		//[SerializeField]
		//which axis do we modify randomly
		public bool RanRotX, RanRotY, RanRotZ;
		
		//what size segments are rotations randomly added by
		public float roundRandomRotationTo = 90;
		
		//can this position have mutliple obejcts
		public bool MutliUse = false;
	
		//is this position currently taken
		protected bool _isTaken = false;
		public bool isTaken
		{
			get {
				if(MutliUse)
					return false;
				return _isTaken;
			}
		}
		
		//objects currently using this position
		protected List<Transform> objectsInPosition = new List<Transform>();
		
		
		/// <summary>
		/// called by ObjectPositionPool.PlaceInFreePosition
		/// Places object and saves reference
		/// </summary>
		/// <param name="objectToPlace"></param>
		public virtual void Place(Transform objectToPlace)
		{
			//if this is single use
			if(!MutliUse)
			{
				//let developer know this is being used when it shouldnt be
				if(_isTaken)
					Debug.LogError("Position being used when it is already taken by: " + TransformUtils.HierarchyPath(transform,5));
				
				//remove existing objects from position
				RemoveAllObjects();
				
				//this position is now taken
				_isTaken = true;
			}
			
			//track objects in this position
			objectsInPosition.Add(objectToPlace);
			
			//set position
			objectToPlace.position = this.transform.position;
	
			//set rotation
			objectToPlace.eulerAngles = GetEulerRotation();
			
		}
		
		/// <summary>
		/// Get rotation vector 3 based on system settings
		/// </summary>
		/// <returns>Rotation as euler vector 3</returns>
		protected Vector3 GetEulerRotation()
		{
			return new Vector3(
				this.transform.eulerAngles.x + (RanRotX? GetRandomRotation(): 0),
				this.transform.eulerAngles.y + (RanRotY? GetRandomRotation() : 0),
				this.transform.eulerAngles.z + (RanRotZ? GetRandomRotation() : 0)
			);
		}
		
		/// <summary>
		/// get a random rotation offset based on amount to rotate by
		/// </summary>
		/// <returns>a rotation value for one axis</returns>
		public float GetRandomRotation()
		{
			int intVal = Random.Range(0,Mathf.FloorToInt(360 / roundRandomRotationTo));
			
			return  intVal * roundRandomRotationTo;
		}
		
		/// <summary>
		/// Frees the position from this object
		/// </summary>
		/// <param name="objectToRemove"></param>
		/// <returns></returns>
		public void FreeFromObject(Transform objectToRemove)
		{
			//remve the object from the list
			objectsInPosition.Remove(objectToRemove);
			
			//if it has no more objects
			if(objectsInPosition.Count == 0)
				//it is now free
				_isTaken = false;
		}
		
		//deprecated
		public void RemoveReference(Transform objectToRemove)
		{
			Debug.LogError("public void RemoveReference(Transform objectToRemove) has been deprecated, remove this reference");
		}
		
		
		//deprecated
		public void RemoveObjects(Transform objectToRemove)
		{
			Debug.LogError("public void RemoveReference(Transform objectToRemove) has been deprecated, remove this reference");
			RemoveAllObjects();
		}
		
		/// <summary>
		/// Resets 
		/// </summary>
		public virtual void RemoveAllObjects()
		{
			/*#if UNITY_EDITOR
			if(objectToRemove != null && objectToRemove == objectInPosition)
				Debug.LogWarning("Removing wrong objects from position. " + objectToRemove.name);
			#endif*/
				
			_isTaken = false;
			
			objectsInPosition = new List<Transform>();
		}
		
		public void DebugObjectsInPosition()
		{
			string debugLog = gameObject.name + " holds: ";
			for(int i = 0 ; i < objectsInPosition.Count; i++)
				debugLog += objectsInPosition[i] + ", ";
				
				
			//Debug.Log(debugLog + TransformUtils.HierarchyPath(this.transform));
		}
		
	}
}