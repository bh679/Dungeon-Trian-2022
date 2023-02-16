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
		
		protected bool IsSpaceFree(Vector3 relativePosition, Collider[] colliders, Transform original)
		{
			//boxesToDraw = new List<DrawBox>();
			for(int i = 0; i < colliders.Length; i++)
			{
				Vector3 position = this.transform.position + (colliders[i].transform.position-original.position) + relativePosition;
				
				if(colliders[i].GetType () == typeof(BoxCollider))
				{
					BoxCollider box = (BoxCollider)colliders[i];
					
					position += box.center;
					
					#if UNITY_EDITOR
						DrawBox box2Draw = new DrawBox(5); 
					box2Draw.position = position;
						box2Draw.size = box.size;
					#endif
					if(Physics.CheckBox(position,box.size/2,Quaternion.identity,LayerMasks.Instance.placerColliders))
					{
						
						#if UNITY_EDITOR
						boxesToDraw.Add(box2Draw);
						
						
						box2Draw.color = Color.red;
						boxesToDraw.Add(box2Draw);
						#endif
						return false;
					}
					
					#if UNITY_EDITOR
						boxesToDraw.Add(box2Draw);
					#endif
						
				}else if(colliders[i].GetType () == typeof(SphereCollider))
				{
					SphereCollider sphere = (SphereCollider)colliders[i];
					
					position += sphere.center;
					
					if(Physics.CheckSphere(position,sphere.radius,LayerMasks.Instance.placerColliders))
						return false;
				}else if(colliders[i].GetType () == typeof(CapsuleCollider))
				{
					CapsuleCollider cap = (CapsuleCollider)colliders[i];
					
					if(Physics.CheckCapsule(position +cap.transform.up*cap.height/2,
						position -cap.transform.up*cap.height/2,
						cap.radius,LayerMasks.Instance.placerColliders))
						return false;
				}else
					Debug.Log("<color=red>Unsupported Collider</color> for finding empty space.");
				
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
		
		class DrawBox
		{
			public Vector3 position;
			public Vector3 size;
			public Color color = Color.green;
			float time, startTime;
			
			public DrawBox(float _time)
			{
				startTime = _time;
				time = startTime;
			}
			
			public bool isAlive(float deltaTime)
			{
				time -= deltaTime;
				
				color = new Color(color.r,color.g, color.b,time/startTime);
				
				return (time > 0);
					
			}
		}
		
		#if UNITY_EDITOR
		void Update()
		{
			for(int i = 0; i < boxesToDraw.Count; i++)
			{
				if(!boxesToDraw[i].isAlive(Time.deltaTime))
				{
					boxesToDraw.RemoveAt(i);
					i--;
				}
			}
		}
		
		List<DrawBox> boxesToDraw = new List<DrawBox>();
		void OnDrawGizmos()
		{
			for(int i = 0; i < boxesToDraw.Count; i++)
			{
				
				Gizmos.color = boxesToDraw[i].color;
				Gizmos.DrawWireCube(boxesToDraw[i].position
					,boxesToDraw[i].size);
			}
			
		}
		#endif
		
	}
}