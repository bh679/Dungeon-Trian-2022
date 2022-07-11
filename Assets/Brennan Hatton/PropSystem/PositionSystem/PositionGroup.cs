using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Utilities;

namespace BrennanHatton.Positions
{
	
	public class PositionGroup : MonoBehaviour
	{
		//list of position
		public Position[] positions;
		
		
		//Is the position randomly selected, or linarily incremented
		public bool randomPosition = true;
		
		//id of next position to select if not random
		int idIncrementor = 0;
		
		#if UNITY_EDITOR
		/// <summary>
		/// Edtior code, used for setup
		/// </summary>
		public void Reset()
		{
			positions = this.GetComponentsInChildren<Position>();
		}
		#endif
		
		/// <summary>
		/// Checks data is corrupt on start
		/// </summary>
		void Start()
		{
			//CheckForCorruptData
			if(DataIsCorrupt())
			{
				//send messages to dev
				Debug.LogWarning("(refreshing list from children) Missing position reference on" + gameObject.name + "/" + this.transform.parent.parent.parent.name + "/" +this.transform.parent.parent.name + "/" +this.transform.parent.name);
				
				//refresh data
				positions = this.GetComponentsInChildren<Position>();
			}
		}
		
		//Positions can be freed externally
		public void FreePositionsFrom(Transform transform)
		{
			// --			this could be optamized with a reverse lookup dictionary.
			
			//for all positions
			for(int i = 0; i < positions.Length; i++)
			{
				//if it is taken
				if(positions[i].isTaken)
				{
					//free from this object
					positions[i].FreeFromObject(transform);
				}
			}
		}
		
		//deprecated
		public void ResetAll(bool fixing = false)
		{
			Debug.LogError("PositionGroup.ResetAll() is Deprecated. Please remove this call");
		}
		
		//Position Groups can find a free position.
		public virtual Position PlaceInFreePosition(Transform objectToPlace = null)
		{
			//if there is no free space
			if(!HasFreeSpace())
				return null;
				
			//get the id for a free space
			int id = GetPositionId();
			
			//if it is in invalid id
			if(id == -1)
			{
				//let the dev know
				Debug.LogError("Couldnt find avalible position for '"+TransformUtils.HierarchyPath(this.transform));
				return null;
			}
			
			//repositions object
			if(objectToPlace != null)
				positions[id].Place(objectToPlace);
			
			//returns the position of the object
			return positions[id];
		}
		
		/// <summary>
		/// Checks if there free space left
		/// </summary>
		/// <returns>true if there is free space</returns>
		public bool HasFreeSpace()
		{
			//for all positions
			for(int i = 0; i < positions.Length; i++)
			{
				//if the data is corrupt
				if(positions[i] == null)
				{
					#if UNITY_EDITOR
					//let the developer know
					Debug.LogError("Corrupt data found when checking for free space at" + TransformUtils.HierarchyPath(this.transform,5));
					#endif
				}//if the position is free
				else if(!positions[i].isTaken)
					//yes there is free space left
					return true;
			}
			
			//there is no space left, we went through all spaces and checked.
			return false;
		}
		
		/// <summary>
		/// Returns the position id of the position to be used for placing
		/// </summary>
		/// <returns>id of position</returns>
		int GetPositionId()
		{
			
			//if position data does not exist
			if(PositionDataExists() == false)
				
				//return invalid code
				return -1;
			
			//starting id to being search for free position
			int offset;
			
			//if random position
			if(randomPosition)
				//start at a random spot
				offset = Random.Range(0,positions.Length-1);
			else//not a random position
			{
				//start at position by incrementor
				offset = idIncrementor;
				
				//move incrementor
				idIncrementor++;
			}
			
			//id current testing, starts at offset
			int id = offset % positions.Length;
			
			//to stop loop going forever
			int counter = 0;
			
			//while the position data is corrupt
			while(positions[id] == null)
			{
				#if UNITY_EDITOR
				//let dev know
				Debug.LogError("Missing reference to position. id:" + id + ". isActive: <color=red>" + gameObject.activeInHierarchy + "</color> from " + TransformUtils.HierarchyPath(this.transform, 10));
				#endif
				
				//find the next position
				counter++;
				id = (offset + counter) % positions.Length;
				
				//if positions are too long
				if(counter > positions.Length*2)//---			test this with counter > positions.Length+1
					//return error code
					return -1;
			
			}
			
			//while position is taken
			while(positions[id].isTaken)
			{
				//find next position
				counter++;
				id = (offset + counter) % positions.Length;
				
				//if positions are too long
				if(counter > positions.Length*2)//---			test this with counter > positions.Length+1
					//return error code
					return -1;
					
				//if position data is corrupt
				if(positions[id] == null)
				{
					#if UNITY_EDITOR
					//let dev know
					Debug.LogError("Missing reference to position. id:" + id + ". isActive:" + gameObject.activeInHierarchy);
					#endif
					
					//skip this id
					counter++;
					id = (offset + counter) % positions.Length;
				}
			}
			
			//return id found
			return id;
		}
		
		
		//##################################
		//##		Data checks
		//##################################
		
		
		/// <summary>
		/// Checks if position data is corrupt
		/// </summary>
		/// <returns>true if data is corrupt</returns>
		bool DataIsCorrupt()
		{
			//for all positions
			for(int i = 0 ; i < positions.Length; i++)
			{
				//check if data is corrupt
				if(positions[i] == null)
				{
					//corrupt data found
					return true;
				}
			}
			
			//no corrupt data found
			return false;
		}
		
		/// <summary>
		/// Does the position data exist
		/// </summary>
		/// <returns></returns>
		bool PositionDataExists()
		{
			//if position data does not exist
			if(positions == null)
			{
				//let developer know
				Debug.LogError("Positions list missing from "+TransformUtils.HierarchyPath(this.transform));
				
				//return invalid code
				return false;
			}
			
			//if position data still does not exist
			if(positions.Length == 0)
			{
				//let developer know
				Debug.LogError("Positions list is 0 from "+TransformUtils.HierarchyPath(this.transform));
				
				//return invalid code
				return false;
			}
			
			return true;
		}
		
		/// 
		///output data
		/// ///
		/// 
		
		public void DebugData()
		{
			//for all positions
			for(int i = 0; i < positions.Length; i++)
			{
				if(positions[i] == null)
					Debug.Log("Missing Positoin in group: " + TransformUtils.HierarchyPath(this.transform));
				
				//
				positions[i].DebugObjectsInPosition();

			}
		}
		
	}
}