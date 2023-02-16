using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Positions;
using BrennanHatton.UnityTools;
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
		
		[Range(0,100)]
		public float mixDataChange = 75;
		bool mixData = true;
		
		//Placers have a list of positions.
		[SerializeField]
		public PositionGroup positionGroup;
		
		RandomNumberRange range;
		
		//how far back up the parent chain do the props get placed?
		public int parentLevel = 0;
		
		
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
			
		}
		#endif
		
		/// <summary>
		/// check references for positions groups, and give relevant errors
		/// </summary>
		public void Awake()
		{
			range = this.GetComponent<RandomNumberRange>();
			
			//if still missing
			if(positionGroup == null)
				//let the dev know it is missing
				Debug.LogError("Positions Pool is missing for this placer. " + gameObject.name);
			
		}
		
		
		//
		public virtual void Place()
		{
				
			//get random number to place from range
			int numberOfProps = range.CalRandomNumberOfProps();
			
			//Consider chance of zero
			if(numberOfProps == 0)
				return;
			
			int id = 0;
			CheckMixingData();
			if(mixData == false)
				id = GetPropID();
			
			//for number of objects to place
			for(int i = 0 ; i < numberOfProps; i++)
			{
				//if there is no room for this object
				if(!positionGroup.HasFreeSpace())
				{
					//send messages to developer
					Debug.LogError("No Space for object - " + gameObject.name + "    " + TransformTools.HierarchyPath(transform,5)+". Perhaps you are sharing positions with another Placer. This is advised against");
					positionGroup.DebugData();
					
					//exit, no need to place more and risk objects getting clipped
					return;
				}
				
				//	get random prop id
				if(mixData)
					id = GetPropID();
				
				GameObject prop = propData[id].propType.GetProp();
				
				TransformDataDelegate transCallback = GetPosition;
				
				if(Instantiator.Instance == null)
					Debug.LogError("Need Instantiator object in scene");
				
				//tell instantiator to create
				Instantiator.Instance.CreateObject(prop,transCallback);
			}
			
			//exit
			return;
		}
		
		TransformData GetPosition(GameObject prop)
		{
			TransformData transformData = positionGroup.GetFreeTransformData(prop.transform);
			transformData.SetParent(this.transform);
					
			return transformData;
		}
		
		void CheckMixingData()
		{
			mixData = (Random.Range(0,100) < mixDataChange);
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
		
		
	}

}