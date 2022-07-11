using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Props.Old
{

	public class PropType : MonoBehaviour
	{
		//Prop data class
		[System.Serializable]
		public class PropData : ChanceMultiplier
		{
			public Prop prop;
			bool Debugging;
			PropType type;
			int propDataId = 0;
			static int propDataCounter = 0;
			
			
			public PropData(Prop _prop, bool _debugging, PropType _type)
			{
				prop = _prop;
				Debugging = _debugging;
				
				propDataId = propDataCounter;
				propDataCounter++;
				type = _type;
			}
		}
		//A list of props and data
		public List<PropData> propData = new List<PropData>();
		
		
		//matrix for calculating chane based on mutliplier.
		int[] _propChanceMatrix;
		int[] propChanceMatrix{
			get{
				
				if(_propChanceMatrix == null
				#if UNITY_EDITOR
					|| Application.isPlaying == false
				#endif
				)
				{
					_propChanceMatrix = ChanceMultiplier.CreateChanceMatrix(propData.ToArray());
				}
				
				
				if(_propChanceMatrix.Length == 0)
					Debug.Log("No prop data");
				
				return _propChanceMatrix;
			}
		}
		
		//props found as children
		[SerializeField]
		List<Prop> props;
		public List<Prop> unconvertedProps
		{
			get{
				return props;
			}
		}
		
		public PropType parentType;
		public PropType[] subTypes;
		
		
		public bool Debugging;
		
		//Prop[] originalClones;
		
		#if UNITY_EDITOR
		void Reset()
		{
			//create a new list of PropData
			propData = new List<PropData>();	
			
			//get alist of props in children
			props = new List<Prop>();
			props.AddRange(this.GetComponentsInChildren<Prop>());
		}
		#endif
		
		/// <summary>
		/// Repopulate data from children
		/// </summary>
		void RefreshList()
		{
			
			//create a new list of PropData
			propData = new List<PropData>();	
			
			//get alist of props in children
			props = new List<Prop>();
			props.AddRange(this.GetComponentsInChildren<Prop>());
			
			ConvertPropsToPropData();
			
			parentType = GetTypeParent(this.transform.parent);
			subTypes = GetSubTypes(this.transform).ToArray();
			
		}
		
		//returns first proptype found in parent hierarchy
		PropType GetTypeParent(Transform testSubject)
		{
			//check if we are at the root of the parent tree
			if(testSubject == transform.root)
				//Looks like there is no parent propType
				return null;
			
			//Look for propType on testsubject
			PropType retVal = testSubject.gameObject.GetComponent<PropType>();
			
			//if has a propType
			if(retVal != null)
				//this is it! We found it.
				return retVal;
				
			//look higher!
			return GetTypeParent(testSubject.parent);
		}
		
		//finds all subtypes in children, but not in the children of those subtypes
		//will explore children until first proptype or end of child hierarchy
		List<PropType> GetSubTypes(Transform parent)
		{
			PropType propTypeFound;
			List<PropType> propTypesFound = new List<PropType>();
			
			//for all children
			for(int i = 0; i < parent.childCount; i++)
			{
				//look for prop type on child
				propTypeFound = parent.GetChild(i).gameObject.GetComponent<PropType>();
				
				//if propType found
				if(propTypeFound != null)
				{
					//add to list
					propTypesFound.Add(propTypeFound);
				}
				else
				{
					//see if there are propTypes in its children
					propTypesFound.AddRange(GetSubTypes(parent.GetChild(i)));
				}
			}
			
			return propTypesFound;
		}
		
		void ConvertPropsToPropData()
		{
			//add props list to prop data
			//for all props from children
			for(int i = 0; i < props.Count; i++)
			{
				if(props[i] == null)
				{
					Debug.LogError("Corrupt Prop Data: " + this.gameObject.name);
				}
				//check if it is not alread existing in prop data
				else if(DoesPropDataContain(props[i]) == false)
				{
					//convert prop to prop data
					PropData newPropData = new PropData(props[i], Debugging, this);
					
					//add to propdata list
					propData.Add(newPropData);
				}
			}
			
			if(props.Count > 0)
				props = new List<Prop>();
		}
		
		/// <summary>
		/// Does the prop data contain the provided prop
		/// </summary>
		/// <param name="prop">Prop in question</param>
		/// <returns>true if that prop is in the prop data</returns>
		public bool DoesPropDataContain(Prop prop)
		{
			//for all of prop data
			for(int i = 0; i < propData.Count; i++)
			{
				if(propData[i].prop == prop)
					return true;
			}
			
			return false;
		}
		
		void Awake()
		{
			//check for missing references
			for(int i = 0; i < propData.Count; i++)
			{
				
				if(propData[i] == null)
				{
					Debug.LogWarning("Missing reference in list " + this.gameObject.name + ". Check out your propType list");
					RefreshList();
					i = propData.Count;
				}
			}
			
			//add all editor values to data
			ConvertPropsToPropData();
			
			//turn off all propData
			for(int i = 0; i < propData.Count; i++)
			{
				propData[i].prop.gameObject.SetActive(false);
				propData[i].prop.Initialization();// This should probably be called elsewhere, but for some reasn this makes the most sense for now
			}
			
			if(parentType == null && this.transform.parent != null)
				parentType = GetTypeParent(this.transform.parent);
			if(subTypes == null || subTypes.Length == 0)
				subTypes = GetSubTypes(this.transform).ToArray();
		}
		
		public Prop GetProp()
		{
			//conver data over from old data types
			ConvertPropsToPropData();//this should probably be run as an editor tool, so its not needed at runtime. Do this n the future. //TODO
			
			//create a new one and exit.
			return CreateNewProp();
			
		}
		
		int idToCopy = 0;
		public Prop CreateNewProp()
		{
			//ToDO: Objects are reset when duplicated, copy over start state - I think I did this
			
			//create new prop
			Prop newProp = Instantiate(propData[idToCopy % propData.Count].prop, this.transform);
			
			
			//rename to name of old prop
			newProp.name = propData[idToCopy % propData.Count].prop.name;
			
			
			//subplacers setup and states
			newProp.Initialization();
			
			
			
			//next time, copy a different object - if implmenting the above advice, this could only be chanced when the chance value has reached 1.
			idToCopy++;
			
			return newProp;
		}
		
		/// <summary>
		/// Gets random prop id using chance matrix
		/// </summary>
		/// <returns>id of random prop</returns>
		public int GetRandomPropId()
		{
			//get random number in chance matrix
			int p = Random.Range(0, propChanceMatrix.Length-1);
				
			//return id from chance matrix
			return propChanceMatrix[p];
		}
		
	}
	
}