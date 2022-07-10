/*
by Brennan Hatton, 20200424
//Placers
//Placers have a data set of objects to place.
//Items in dataset of objectpools have the a weight for chance of being placed
//Placers have a list of positions.
//Placers keep track of objects and their subplacers.
//Placers can randomly selected by mutliplier (default), by type or by object (no mutliplier)
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Utilities;
using BrennanHatton.Positions;
using BrennanHatton.Positions.Extentions;

namespace BrennanHatton.Props.Old
{

	[DisallowMultipleComponent]
	public class PropPlacer : MonoBehaviour
	{
		
		//a data set of props to place.
		[System.Serializable]
		public class PropData : ChanceMultiplier
		{
			public string name;
			public PropType propType; 
			
			public PropType PropType {
				get { 
					if(propType == null)
					{
						Debug.Log("Searching for propType " + name);
						
						GameObject go = GameObject.Find(name);
						
						if(go != null)
						{
							Debug.Log("Found");
							propType = go.GetComponent<PropType>();
						}else
							Debug.LogError("No Found");
					}
					return propType; 
				}
				set{
					propType = value;
				}
			}
			//public float chanceMultiplier = 10;//this data needs to be moved elsewhere.
			
			public PropData()
			{
				chanceMultiplier = 10;
			}
			
		
		}
		public PropData[] propData = new PropData[0];
		
		//Placers have a list of positions.
		[SerializeField]
		PositionGroup _positionGroup;
		public PositionGroup positionGroup{
			get{
				
				//check if it is missing
				if(_positionGroup == null)
				{
					//let dev know it shouldnt be missing
					Debug.LogWarning("Positions Group Reference Found. Should be assigned in inspector.");
				
					//find it in children
					_positionGroup = this.GetComponentInChildren<PositionGroup>();
					
					//if it is not found in children
					if(_positionGroup == null)
					{
						Debug.LogError("Positions Group Reference not found in Children. Looking in parent, this is risky. " + gameObject.name);
						_positionGroup = this.transform.parent.gameObject.GetComponentInChildren<PositionGroup>();
					}
					
				}
				
				return _positionGroup;
			}
			
			set{
				_positionGroup = value;
			}
		}
		
		//Places have a range of min and max objects to place
		public Vector2 range = new Vector2(1,2);//TODO - make a matrix to make higher numbers rare, and an enum to define how the matrixis made (x=y, x=2*y, x=length-i*y, x=y*y, x=y*y*y, etc. This will be applied in CalRandomNumberOfProps
		
		//Places have a value for chance of not placing anything.
		[Range(0,100)]
		public float ChangeOfZero = 50;
		public bool ChanceofZeroForEach = false;
		
		//how far back up the parent chain do the props get placed?
		public int parentLevel = 0;
		
		public bool Debugging = false;
		
		//We need a reference to the proptype with the propsplaced so when they are returned, the proptype can be informed the prop is no longer in use.
		[System.Serializable]//this data might need to be just copied over
		public class PropAndType
		{
			public Prop prop;
			public PropType type;
			
			public PropAndType(Prop _prop, PropType _type)
			{
				prop = _prop;
				type = _type;
			}
		}
		
		//list of props - made public so it is seriazable and copied when object is cloned. But should be private read only.
		public List<PropAndType> _propsPlaced;
		
		//public interface for list of props
		public List<PropAndType> propsPlaced { get { 
			if(_propsPlaced == null)
				_propsPlaced = new List<PropAndType>();	
			return _propsPlaced; 
		} }
		
		//this is used to catch errors where place is called twice when it shouldnt be.
		bool placedThisUpdate;
		
		
		//matrix for calculating chane based on mutliplier.
		int[] _propChanceMatrix;
		int[] propChanceMatrix{
			get{
				
				//if matrix is no set
				if(_propChanceMatrix == null
				#if UNITY_EDITOR
					//or running in editor, not in playmode
					|| Application.isPlaying == false
				#endif
				)
				{
					//create new matrix
					_propChanceMatrix = ChanceMultiplier.CreateChanceMatrix(propData);
					
					//Check matrix is valid
					if(_propChanceMatrix.Length == 0)
						//send message to dev
						Debug.Log("No prop data");
				}
				
				//return matrix
				return _propChanceMatrix;
			}
		}
		
		
		/// <summary>
		/// check references for positions groups, and give relevant errors
		/// </summary>
		public void Awake()
		{
			
			//if still missing
			if(positionGroup == null)
				//let the dev know it is missing
				Debug.LogError("Positions Pool is missing for this placer. " + gameObject.name);
			else // it is not missing
			{
				//if there are not enough positions
				if(range.y > positionGroup.positions.Length)
				{
					//if it is not using an object area
					if(!(positionGroup.positions.Length > 1 && positionGroup.positions[0] != null && positionGroup.positions[0] is PositionArea))
						//let the dev know there might be an issue
						Debug.LogWarning("Dont have enough room to place maxmium obejcts - "+gameObject.name + ". This can be ignore if using PositionArea not in array id 0");
				
				}
			}
			
			if(_propsPlaced == null)
				_propsPlaced =  new List<PropAndType>();
		}
		
		//gets all palced props
		public void InitializePreMadePropChildren()
		{
			//get subplacers
			//	if(_propsPlaced == null)
			//	_propsPlaced = new List<PropAndType>();
			
			//	Prop[] propsArray = GetPropsInChildren(this.transform);
			
			//for all props found
			for(int a = 0; a < propsPlaced.Count; a++)
			{
				//initlize
				propsPlaced[a].prop.Initialization();//InitializePreMadeSubPlacers();
				
				//add to list
				//_propsPlaced.Add(new PropAndType(propsArray[a], );
			}
		}
		
		Prop[] GetPropsInChildren(Transform transformTarget)
		{
			//list of props found to return 
			List<Prop> propsFound = new List<Prop>();
			
			//prop on current trasnform
			Prop tmpProp;
			
			//for all direct children
			for(int i  = 0; i < transformTarget.childCount; i++)
			{
				//find prop
				tmpProp = transformTarget.GetChild(i).GetComponent<Prop>();
				
				//if prop is not found
				if(tmpProp == null)
				{
					//search children
					Prop[] placerArray = GetPropsInChildren(transformTarget.GetChild(i));
					
					//add them all to the list
					for(int a = 0; a < placerArray.Length; a++)
					{
						propsFound.Add(placerArray[a]);
					}
					
				}
				else //if prop is found
				{
					//add prop to list
					propsFound.Add(tmpProp);
				}
			}
			
			//return an array of placers found
			return propsFound.ToArray();	
		}
		
		
		public void DestorySubProps()
		{
			Debug.Log("DestorySubProps of " + gameObject.name);
			//For all props managed by this placer
			for(int i = 0; i < propsPlaced.Count; i++)
			{
				//free the position
				positionGroup.FreePositionsFrom(propsPlaced[i].prop.transform);
				
				//Destory the prop
				DestroyObject(propsPlaced[i].prop.gameObject);
			}
			
			_propsPlaced =  new List<PropAndType>();
		}
		
		public virtual void PlacePlz(bool addToExisting)
		{
			Debug.Log("public virtual void PlacePlz(bool addToExisting)");
			Place(addToExisting);
		}
		
		//
		public virtual Prop[] Place(bool addToExisting = false)
		{
			//Check its not placing twice
			if(DoublePlaceCheck())
				//exit
				return null;
				
			//Do we need to reset props?
			if(addToExisting == false)
				//remove existing props
				DeleteProps();
			
			//create list to be returned
			List<Prop> newPropsPlaced = new List<Prop>();
			
			//check prop data isnt corrupt		--Should this be done elsewhere, perhaps at start?
			if(FindCorruptData())
				return newPropsPlaced.ToArray(); 
				
			//get random number to place from range
			int numberOfProps = CalRandomNumberOfProps();
			
			//Consider chance of zero
			if(numberOfProps == 0)
				return newPropsPlaced.ToArray();
			
			//for number of objects to place
			for(int i = 0 ; i < numberOfProps; i++)
			{
				//if there is no room for this object
				if(!positionGroup.HasFreeSpace())
				{
					//send messages to developer
					Debug.LogError("No Space for object - " + gameObject.name + "    " + TransformUtils.HierarchyPath(transform,5)+". Perhaps you are sharing positions with another Placer. This is advised against");
					positionGroup.DebugData();
					
					//exit, no need to place more and risk objects getting clipped
					return newPropsPlaced.ToArray();
				}
				
				//	get random prop
				PropAndType newPropAndType = GetProp();
				
				//place object in position
				PlaceInPosition(newPropAndType.prop.transform);
				
				//let prop know it has been placed
				newPropAndType.prop.Place(this);
				
				//add to list of props being placed this update
				newPropsPlaced.Add(newPropAndType.prop);
				
				//	add to list of all placed objects
				propsPlaced.Add(newPropAndType);
				
			}
			
			//return the newly placed props
			return newPropsPlaced.ToArray();
		}
		
		public void DeleteProps()
		{
			
			//For all props managed by this placer
			for(int i = 0; i < propsPlaced.Count; i++)
			{
				propsPlaced[i].prop.DeleteSubProps();
				Destroy(propsPlaced[i].prop.gameObject);
			}
		}
		
		/// <summary>
		/// Loses track of prop, but does not restore prop to original state
		/// </summary>
		/// <param name="prop">Prop to lose</param>
		public void LosePropReference(Prop prop)
		{
			//For all props managed by this placer
			for(int i = 0; i < propsPlaced.Count; i++)
			{
				if(propsPlaced[i].prop == prop)
				{
					//free the position
					positionGroup.FreePositionsFrom(propsPlaced[i].prop.transform);
				
					//delete reference.
					propsPlaced.RemoveAt(i);
					
					return;
				}
			}
			
			Debug.LogError("Prop '"+prop.name+"' is not tracked by placer '"+gameObject.name+"' Reference is lost. It should never be lost");
		}
		
		
		
		//########################old code
		public virtual void PlaceOverTime(float time)
		{
			Debug.LogError("PlaceOverTime() is Deprecated. However, perhaps this code should exist if it is in use");
			
			//if(Debugging)
				//	Debug.Log("Place");
			//PlaceRet(false,time);
		}
		
		float placedThisUpdateTime = 0;
		string placerPosHolderDebugMsg = "";
		
		/// <summary>
		/// Check its not placing twice
		/// </summary>
		/// <returns>true is bad, false is good</returns>
		bool DoublePlaceCheck()
		{
			//if it was placed last turn, and it is turned on 
			//(if it is not turned on, it wont be update to update the bool, so this function breaks. We check its activeSelf to address this. So we can trust this error when presented
			if(placedThisUpdate && gameObject.activeSelf == true && placedThisUpdateTime-Time.time == 0)
			{
				string debugLocation = gameObject.name + "   " + TransformUtils.HierarchyPath(this.transform,10);
				
				//send error to dev
				Debug.LogError("Place was already called this udpate. Time difference: "+(placedThisUpdateTime-Time.time).ToString()+". Disregarding. " + debugLocation + "\n\n Last time it was placed " + ((placerPosHolderDebugMsg == debugLocation)? "in the same place" : "at: " + placerPosHolderDebugMsg)); 
				
				
				
				//oh no, we got a true
				return true;
			}
			
			placerPosHolderDebugMsg = gameObject.name + "   " + TransformUtils.HierarchyPath(this.transform,10);
			if(Debugging) Debug.Log("Placing " + placerPosHolderDebugMsg +  "  at " + Time.time);
			
			placedThisUpdate = true;
			placedThisUpdateTime = Time.time;
			return false;
		}
		
		//check prop data isnt corrupt
		//this can happen from objects being deleted, misstakes, or changes in dependancies
		public bool FindCorruptData(bool fix = false)
		{
			//for all prop data
			for(int t = 0; t < propData.Length; t++)
			{
				//if data elememnt is corrupt
				if(propData[t] == null)
				{
					
					if(fix)
					{
						Debug.LogWarning(this.gameObject.name + " is missing prop " + propData[t].name + ". Is being fixed.");
						GameObject go = GameObject.Find(propData[t].name);
						
						if(go != null)
						{
							propData[t].PropType = go.GetComponent<PropType>();
						}
					}
					else
					{
					//let dev know
						Debug.LogError(this.gameObject.name + " is missing prop " + propData[t].name);
						//return true for data being corrupt
						return true;
					}
				}
			}
			
			//data is not corrupt, we check it all
			return false;
		}
		
		/// <summary>
		/// Calulate how many props to place based on range and chance of zero
		/// </summary>
		/// <param name="runChanceOfZero">Does it use the chance of zero? Defualt is yes/true</param>
		/// <returns>number of props to place</returns>
		public int CalRandomNumberOfProps(bool runChanceOfZero = true)
		{
			//hold the return value here
			int numberOfProps;
			
			//run chance of zero
			if(runChanceOfZero)
				if(Random.Range(0,100) < ChangeOfZero)
					return 0;
				
			if(ChanceofZeroForEach)
			{
				numberOfProps = (int)range.x;
				for(int i = (int)range.x; i < range.y; i++)
				{
					if(Random.Range(0,100) >= ChangeOfZero)
						numberOfProps++;
				}
				
				return numberOfProps;
				
			}
			
			//calculate how many props to pick
			if(range.y == range.x)
				numberOfProps = Mathf.CeilToInt(range.y);
			else	
				numberOfProps = Random.Range(Mathf.CeilToInt(range.x),Mathf.CeilToInt(range.y)+1);
				
			return numberOfProps;
		}
		
		/// <summary>
		/// Gets random prop from prop data and chance matrix
		/// </summary>
		/// <returns>Random Prop</returns>
		protected PropAndType GetProp()
		{
			//if propdata is empty
			if(propData == null || propData.Length == 0)
			{
				Debug.LogError("No prop data " + TransformUtils.HierarchyPath(transform));
				return null;
			}
			
			//get a prop type from random id
			int id = GetPropID();
			
			if(propData[id] == null)
				Debug.LogError("Corrupt PropData type on " + this.gameObject.name);
			else if(propData[id].PropType == null)
				Debug.LogError("Missing prop type on " + this.gameObject.name + ". For " + propData[id].name);
				
			if(propData[id].PropType == null)
				Debug.LogError(gameObject.name  + " " + TransformUtils.HierarchyPath(transform,5));
			
			//save type
			PropType type = propData[id].PropType;
			
			//gets a prop that is not in use
			Prop prop = type.GetProp();
			
			return new PropAndType(prop, type);
			
		}
		
		/// <summary>
		/// get id of prop type using chance matrix
		/// </summary>
		/// <returns>id of prop type</returns>
		protected int GetPropID()
		{
			//gets random place in matrix
			int p = Random.Range(0, propChanceMatrix.Length-1);
				
			//gets id from matrix
			return propChanceMatrix[p];
		}
		
		/// <summary>
		/// Place object in position, and set parent relative to placer
		/// </summary>
		/// <param name="transformToPlace"></param>
		void PlaceInPosition(Transform transformToPlace)
		{
			//parent to placer
			transformToPlace.SetParent(this.transform);
			
			//offset parent level from place
			TransformUtils.MoveToParentUp(transformToPlace,parentLevel);
			
			//get random position
			//get transform data from position
			positionGroup.PlaceInFreePosition(transformToPlace);
		}
		
		
		//let the class know a frame has passed, for the double placer check - maybe redo this based on time.
		public void LateUpdate()
		{
			placedThisUpdate = false;
		}
			
			
		
	}

}