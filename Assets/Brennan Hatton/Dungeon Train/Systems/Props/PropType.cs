using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BrennanHatton.Props
{

	public class PropType : MonoBehaviour
	{
		//Prop data class
		[System.Serializable]
		public class PropData : ChanceMultiplier
		{
			public GameObject prop;
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
		
		#if UNITY_EDITOR
		void Reset()
		{
			//create a new list of PropData
			propData = new List<PropData>();	
			
			// We need to tell Unity we're changing the component object too.
			Undo.RecordObject(this, "Populating PropData");
			
			//get alist of props in children
			PropData[] children = new PropData[transform.childCount];
			for(int i =0; i < transform.childCount; i++)
			{
				children[i] = new PropData();
				children[i].prop = transform.GetChild(i).gameObject;
			}
			propData.AddRange(children);
			
		}
		#endif
		
		void Awake()
		{
			this.gameObject.SetActive(false);
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
		
		public GameObject GetProp()
		{
			return propData[GetRandomPropId()].prop;
		}
	}

}