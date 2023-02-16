using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools;

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
		
		public virtual TransformData GetFreeTransformData(Transform objectToPlace)
		{
			TransformData data = null;
			
			if(!MutliUse)
			{
				//let developer know this is being used when it shouldnt be
				if(_isTaken)
					Debug.LogError("Position being used when it is already taken by: " + TransformTools.HierarchyPath(transform,5));
				
				//this position is now taken
				_isTaken = true;
			}
			
			Collider[] colliders = objectToPlace.GetComponentsInChildren<Collider>();
			
			if(IsSpaceFree(Vector3.zero,colliders,objectToPlace))
				data = new TransformData(this.transform.position, GetEulerRotation());
			else
				Debug.LogError("No space on position " + this.gameObject.name);
			
			return data;
		}
		
		bool IsSpaceFree(Vector3 relativePosition, Collider[] colliders, Transform original)
		{
			for(int i = 0; i < colliders.Length; i++)
			{
				if(colliders[i].GetType () == typeof(BoxCollider))
				{
					BoxCollider box = (BoxCollider)colliders[i];
					
					if(Physics.CheckBox(relativePosition+ box.center + (box.transform.position-original.position),box.size/2,Quaternion.identity,LayerMasks.Instance.placerColliders))
						return false;
						
				}else if(colliders[i].GetType () == typeof(SphereCollider))
				{
					SphereCollider sphere = (SphereCollider)colliders[i];
					
					if(Physics.CheckSphere(relativePosition+ sphere.center + (sphere.transform.position-original.position),sphere.radius,LayerMasks.Instance.placerColliders))
						return false;
				}else
				{
					CapsuleCollider cap = (CapsuleCollider)colliders[i];
					
					if(Physics.CheckCapsule(relativePosition+ cap.transform.up*cap.height/2 + (cap.transform.position-original.position),relativePosition - cap.transform.up*cap.height/2 + (cap.transform.position-original.position),cap.radius,LayerMasks.Instance.placerColliders))
						return false;
				}
				
			}
			
			return true;
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
		
		
		
	}
}