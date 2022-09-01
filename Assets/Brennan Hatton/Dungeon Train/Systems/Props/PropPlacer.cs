using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Positions;
using BrennanHatton.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace BrennanHatton.Props
{
	
	
	[RequireComponent(typeof(RandomNumberRange))]
	public class PropPlacer : MonoBehaviour
	{
		//a data set of props to place.
		[System.Serializable]
		public class PropData : BrennanHatton.Props.ChanceMultiplier
		{
			public PropType propType; 
		}
		public PropData[] propData = new PropData[0];
		
		//Placers have a list of positions.
		[SerializeField]
		public PositionGroup positionGroup;
		
		[SerializeField]
		RandomNumberRange range;
		
		//how far back up the parent chain do the props get placed?
		public int parentLevel = 0;
		
		public bool runOnEnable = true;
		
		public List<GameObject> objectsPlaced;
		
		
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
					_propChanceMatrix = BrennanHatton.Props.ChanceMultiplier.CreateChanceMatrix(propData);
					
					//Check matrix is valid
					if(_propChanceMatrix.Length == 0)
						//send message to dev
						Debug.Log("No prop data");
				}
				
				//return matrix
				return _propChanceMatrix;
			}
		}
		
		#if UNITY_EDITOR
		void Reset()
		{
			//create a new list of PropData
			propData = new PropData[0];
			
			// We need to tell Unity we're changing the component object too.
			Undo.RecordObject(this, "Finding Position Group");
			
			positionGroup = this.GetComponentInChildren<PositionGroup>();
			
			range = this.GetComponent<RandomNumberRange>();
			
		}
		#endif
		
		/// <summary>
		/// check references for positions groups, and give relevant errors
		/// </summary>
		public void Awake()
		{
			
			//if still missing
			if(positionGroup == null)
				//let the dev know it is missing
				Debug.LogError("Positions Pool is missing for this placer. " + gameObject.name);
			
		}
		
		public void OnEnable()
		{
			if(runOnEnable)
				Place();
		}
		
		public virtual void Place()
		{
			Place(null);
		}
		
		//
		public virtual void Place(Action<GameObject> externalCallback)
		{
				
			//get random number to place from range
			int numberOfProps = range.CalRandomNumberOfProps();
			
			//Consider chance of zero
			if(numberOfProps == 0)
				return;
			
			//for number of objects to place
			for(int i = 0 ; i < numberOfProps; i++)
			{
				//if there is no room for this object
				if(!positionGroup.HasFreeSpace())
				{
					//send messages to developer
					Debug.LogError("No Space for object - " + gameObject.name + "    " + BrennanHatton.Utilities.TransformUtils.HierarchyPath(transform,5)+". Perhaps you are sharing positions with another Placer. This is advised against");
					positionGroup.DebugData();
					
					//exit, no need to place more and risk objects getting clipped
					return;
				}
				
				//	get random prop id
				int id = GetPropID();
				
				TransformData transformData = new TransformData(positionGroup.PlaceInFreePosition().transform, this.transform);
				
				//tell instantiator to create
				Instantiator.Instance.CreateObject(propData[id].propType.GetProp(),transformData, new Action<GameObject>[2] {PlacedObjectCalledback, externalCallback});
			}
			
			//exit
			return;
		}
		
		/// <summary>
		/// get id of prop type using chance matrix
		/// </summary>
		/// <returns>id of prop type</returns>
		protected int GetPropID()
		{
			//gets random place in matrix
			int p = UnityEngine.Random.Range(0, propChanceMatrix.Length-1);
				
			//gets id from matrix
			return propChanceMatrix[p];
		}
		
		
		protected void PlacedObjectCalledback(GameObject objectPlaced)
		{
			objectsPlaced.Add(objectPlaced);
		}
		
		
	}

}