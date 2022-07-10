#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Editor;
using BrennanHatton.Props;
using BrennanHatton.Positions;

namespace BrennanHatton.Props.Old.Editor
{
		
	[RequireComponent(typeof(TransformPositionGizmo))]
	[RequireComponent(typeof(PropPlacer))]
	public class PlacerGizmo : MonoBehaviour
	{
		
		public PropPlacer placer;
		
		[HideInInspector]
		[SerializeField]
		TransformPositionGizmo transformPositionGizmo;
		
		public enum NumberOfProps
		{
			Normal = 0,
			WithoutChanceOfZero = 1,
			Minimum = 2,
			Maximum = 3
		} 
		public NumberOfProps numberOfProps = NumberOfProps.WithoutChanceOfZero;
		
		public int numberToPlace = 0;
	    
		[System.Serializable]
		public class PropData
		{
			public int typeId;
			public int propId = -1;
			public Position objectPosition;
			public bool place = true;
			
			public Vector3 GetLocalPosition(Vector3 position)
			{
				if(!objectPosition)
					return Vector3.zero;
					
				return objectPosition.transform.position - position;
			}
			
			public Quaternion GetLocalRotation(Quaternion rotation)
			{
				if(!objectPosition)
					return Quaternion.identity;
					
				return objectPosition.transform.rotation * rotation;
			}
		}
		public PropData[] props;
		
		[SerializeField]
		PropTypeGizmo[] proptypes;
			
		void Reset()
		{		
			Setup();
		}
		
		public void SetupPositionsGizmo()
		{
			transformPositionGizmo = this.GetComponent<TransformPositionGizmo>();
			if(!transformPositionGizmo)
				transformPositionGizmo = this.gameObject.AddComponent<TransformPositionGizmo>();
			
			
			transformPositionGizmo.transforms = new Transform[placer.positionGroup.positions.Length];
			
			for(int i = 0; i < placer.positionGroup.positions.Length; i++)
			{
				if(placer.positionGroup.positions[i] != null)
					transformPositionGizmo.transforms[i] = placer.positionGroup.positions[i].transform;
			}
		}
		
		public int missingProps = 0;
		public void Setup()
		{
			placer = this.gameObject.GetComponent<PropPlacer>();
					
			proptypes = new PropTypeGizmo[placer.propData.Length];
			missingProps = 0;
			for(int p  = 0; p+missingProps < placer.propData.Length; p++)
			{
				
				if(placer.propData[p].propType != null)
				{
					proptypes[p] = placer.propData[p].propType.GetComponent<PropTypeGizmo>();
					
					if(proptypes[p] == null)
					{
						proptypes[p] = placer.propData[p].propType.gameObject.AddComponent<PropTypeGizmo>();
					}
					proptypes[p].Setup();
				}else
				{
					if(placer.propData[p].name != null && placer.propData[p].name[0] != 'X')
					{
						GameObject go = GameObject.Find(placer.propData[p].name);
						
						if(go != null)
						{
							placer.propData[p].propType = go.GetComponent<PropType>();
						}
						
						if(placer.propData[p].propType == null)
						{
							placer.propData[p].name = "X"+placer.propData[p].name;
						}
					}
					Debug.LogError(placer.name + " has missing Prop Reference: " + placer.propData[p].name);
					p--;
					missingProps++;
				}
			}
			
			GetProps();
			SetupPositionsGizmo();
		}
		
		public void GetProps()
		{
			switch((int)numberOfProps)
			{
			case (int)NumberOfProps.Normal:
				numberToPlace = placer.CalRandomNumberOfProps();
				break;
			case (int)NumberOfProps.WithoutChanceOfZero:
				numberToPlace = placer.CalRandomNumberOfProps(false);
				break;
			case (int)NumberOfProps.Minimum:
				numberToPlace = (int)placer.range.x;
				break;
			case (int)NumberOfProps.Maximum:
				numberToPlace = (int)placer.range.y;
				break;
			}
			
			props = new PropData[numberToPlace];
			
			for(int i = 0; i < numberToPlace; i++)
			{
				props[i] = new PropData();
				props[i].typeId =  Random.Range(0,proptypes.Length-1);//placer.GetPropID();
				props[i].propId =  Random.Range(0,100);
				props[i].objectPosition = placer.positionGroup.positions[i % placer.positionGroup.positions.Length];
			}
		}
		
		public void DrawGizmos(Vector3 position, Quaternion rotation, bool color)
		{
			if(proptypes == null || proptypes.Length - missingProps == 0 || props == null || props.Length != numberToPlace)
			{
				if(proptypes == null)
					Debug.Log("proptypes == null");
				else if(proptypes.Length - missingProps == 0)
					Debug.Log("proptypes.Length - missingProps == 0");
				else if(props == null)
					Debug.Log("props == null");
				else if(props.Length != numberToPlace)
					Debug.Log("props.Length != numberToPlace");
				Setup();
				return;
			}
					
			for(int p = 0; p < numberToPlace; p++)
			{
				if(props[p] != null)
				{
					if(props[p].place)
					{
						/*Debug.Log(proptypes);
						Debug.Log(props[p].typeId);
						Debug.Log(proptypes.Length - missingProps);
						Debug.Log(proptypes[props[p].typeId % (proptypes.Length - missingProps)]);*/
						
						if(proptypes[props[p].typeId % (proptypes.Length - missingProps)] != null)
						{
							proptypes[props[p].typeId % (proptypes.Length - missingProps)].DrawGizmos(
							rotation*props[p].GetLocalPosition(transform.position) + position, 
							props[p].GetLocalRotation(transform.rotation) * rotation, 
							color,
								props[p].propId);
						}
					}
				}
			}
		}
 			
		bool selected = false;
		void OnDrawGizmos()
		{
			if(Application.isPlaying)
				return;
			if(!selected)
				DrawGizmos(transform.position, transform.rotation, false);
			
			selected = false;
		}
		
		void OnDrawGizmosSelected()
		{
			if(Application.isPlaying)
				return;
			selected = true;
			DrawGizmos(transform.position, transform.rotation, true);
		}
		
	}
}
#endif