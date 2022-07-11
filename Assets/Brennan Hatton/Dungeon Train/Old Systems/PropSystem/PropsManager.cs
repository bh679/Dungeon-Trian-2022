#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Props.Old.Editor
{

	[DisallowMultipleComponent]
	public class PropsManager : MonoBehaviour
	{
		
		public PropType[] propTypes;
		public PropPlacer[] placers;
		public Prop[] props;
		
		void Reset()
		{
			PropPlacersAssignReferences();
			
			PropsRest();
		}
		
		void PropsRest()
		{
			props = this.transform.root.GetComponentsInChildren<Prop>(true);
			
			/*for(int i = 0; i < props.Length; i++)
			{
				//props[i].originalParent = this.transform.parent;
				//props[i].subProps = props[i].GetComponentsInChildren<PropPlacer>();
			}*/
			
			
			if(Application.isPlaying == false)
			{
				PropGizmo[] propGizmos = this.transform.root.GetComponentsInChildren<PropGizmo>(true);
				for(int i = 0; i < propGizmos.Length; i++)
				{
						propGizmos[i].Setup();
				}
				
				PlacerGizmo[] placerGizmo = this.transform.root.GetComponentsInChildren<PlacerGizmo>(true);
				for(int i = 0; i < placerGizmo.Length; i++)
				{
					placerGizmo[i].Setup();
				}
				
				PropTypeGizmo[] propTypeGizmos = this.transform.root.GetComponentsInChildren<PropTypeGizmo>(true);
				for(int i = 0; i < propTypeGizmos.Length; i++)
				{
					propTypeGizmos[i].Setup();
				}
			}
		}
		
		void PropPlacersAssignReferences()
		{
			
			propTypes = this.transform.root.GetComponentsInChildren<PropType>(true);
			//find all propPlacers
			placers = this.transform.root.GetComponentsInChildren<PropPlacer>(true);
			
			//if missing types
			for(int i = 0; i < placers.Length; i++)
			{
				
				for(int t = 0; t < placers[i].propData.Length; t++)
				{
					if(placers[i].propData[t].propType == null || placers[i].propData[t].propType.gameObject == null || string.IsNullOrEmpty(placers[i].propData[t].propType.gameObject.name))
					{
						Debug.LogWarning(placers[i].name + " has no prop type - attempting to fix");
						//find by name
						if(string.IsNullOrEmpty(placers[i].propData[t].name))
						{
							//-name was empty
							Debug.LogError(placers[i].gameObject.name + "has no type, and name is empty");
						}else
						{
							PropType propType = FindTypeByName(placers[i].propData[t].name);
				
							//-error could not find type	
							if(propType == null)
								Debug.LogError("'" + placers[i].propData[t].name + "' is an incorrect name for item, searched on " + placers[i].gameObject.name);
							else
								Debug.LogWarning(placers[i].name + " now has a proptype of " + propType.name);
							//-add type
							placers[i].propData[t].propType = propType;
						}
					}else if(string.IsNullOrEmpty(placers[i].propData[t].name))
					{
						Debug.LogWarning("Missing name of '"+placers[i].propData[t].propType.name+"' added for PropPlacer " + placers[i].gameObject.name);
						
						placers[i].propData[t].name = placers[i].propData[t].propType.name;
					}/*else if(placers[i].propTypeName != placers[i].propType.gameObject.name)
					{
					
					}*/
				}
				
				/*if(placers[i].propData == null || (placers[i].propData.Length == 0 && placers[i].propTypeName != null))
				{
					placers[i].propData = new PropPlacer.PropData[1];
					placers[i].propData[0] = new PropPlacer.PropData();
					placers[i].propData[0].name = placers[i].propTypeName;
					placers[i].propData[0].propType = placers[i].propType;
					
				}*/
				
			}
		}
		
		public PropType FindTypeByName(string name)
		{
			
			for(int i = 0 ;i < propTypes.Length; i++)
			{
				if(propTypes[i].name == name)
				{
					return propTypes[i];
				}
			}
			
			Debug.LogWarning("No type was found by the name '" + name + "' out of " + propTypes.Length + " types");
			
			return null;
			
		}
		
	    // Start is called before the first frame update
		/*void Awake()
	    {
		    PropPlacersAssignReferences();
		    
		    PropsRest();
		}
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }*/
	    
	}
	
}
#endif