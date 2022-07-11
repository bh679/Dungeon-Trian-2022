#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;

namespace BrennanHatton.Props.Old.Editor
{
	
	[DisallowMultipleComponent]
	[RequireComponent(typeof(PropType))]
	public class PropTypeGizmo : MonoBehaviour
	{
		public int propIdDefault;
			
		void Reset()
		{
			Setup();
		}
			
		[SerializeField]
		PropGizmo[] props; 
		
		public int missingProps = 0;
		public void Setup()
		{
			if(this.gameObject.GetComponent<ArrangeChildInGrid>() == null)
				this.gameObject.AddComponent<ArrangeChildInGrid>();
				
			PropType type;
			type = this.gameObject.GetComponent<PropType>();
			
			//get the props that are not convereted to data yet
			Prop[] unconvertedProps = type.unconvertedProps.ToArray();
			
			//setup the prop gizmo array
			props = new PropGizmo[type.propData.Count + unconvertedProps.Length];
			
			//counter for prop gizmo array
			int x  = 0;
			
			//pass through unconverted props, discarding corrupt or duplicates, and saving the relevant ones
			for(int i = 0; i < unconvertedProps.Length; i++)
			{
				//check if it is alread existing in prop data
				if(unconvertedProps[i] == null || type.DoesPropDataContain(unconvertedProps[i]))
				{
					//do nothing
				}else
				{
					props[x] = GetPropGizmo(unconvertedProps[x]);
					x++;
				}
			}
					
			missingProps = 0;
			for(int p = 0; p + missingProps < type.propData.Count; p++)
			{
				
				if(type.propData[p] != null)
				{
					props[p+x] = GetPropGizmo(type.propData[p].prop);
					
					props[p+x].Setup();
				}else
				{
					Debug.LogError("Missing Prop Ref " + type.name);
					p--;
					missingProps++;
				}
			}
		}
		
		PropGizmo GetPropGizmo(Prop prop)
		{
			PropGizmo returnvalue = prop.GetComponent<PropGizmo>();
					
			if(returnvalue == null)
			{
				returnvalue = prop.gameObject.AddComponent<PropGizmo>();
			}
			
			return returnvalue;
		}
		
		public void DrawGizmos(Vector3 position, Quaternion rotation, bool color, int propId = -1)
		{
			if(props == null || props.Length - missingProps == 0)
				return;
				
			if(propId == -1)
				propId = propIdDefault;
					
			props[propId % (props.Length - missingProps)].DrawGizmos(position, rotation, color);
		}
		
			
	}
}
#endif