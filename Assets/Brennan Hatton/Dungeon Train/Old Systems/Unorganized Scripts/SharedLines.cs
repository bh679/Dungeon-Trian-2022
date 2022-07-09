using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Scanner
{
		
	[System.Serializable]
	public class SharedLineWeight
	{
		public SharedLines sharedLine;
		
		[Range(0,1)]
		public float weight = 1f;
	}
		
	[System.Serializable]
	public class FollowingLine
	{
		public SharedLineWeight[] sharedLines = new SharedLineWeight[0];
		[Range(0,1)]
		public float chanceOfAdding = 1;
		public string connection;
		public bool newLine;
		
		public string GetLine()
		{
			if(sharedLines.Length > 0 && Random.value < chanceOfAdding)
			{
				
				
				return connection + (newLine?"\n":"")+ PickSharedLine().GetSharedLine();
			}
			return null;
		}
		
		public SharedLines PickSharedLine() 
		{
			float total = 0;
			for(int i = 0; i < sharedLines.Length; i++)
			{
				total += sharedLines[i].weight;
			}
			
			float rand = Random.Range(0, total);
			
			total = 0;
			for(int i = 0; i < sharedLines.Length; i++)
			{
				if(rand < total)
					return sharedLines[i-1].sharedLine;
				total += sharedLines[i].weight;
			}
			
			return sharedLines[sharedLines.Length-1].sharedLine;
			
		}
	}
		
	public class SharedLines : MonoBehaviour
	{
		public string firstLine;
		public List<string> sharedLines = new List<string>();
		
		public FollowingLine[] followingLinesInOrder;
	    
		public string GetSharedLine()
		{
			string returnval = "";
			if(sharedLines.Count > 0)
				returnval = sharedLines[Random.Range(0,sharedLines.Count-1)];
			
			for(int i = 0; i < followingLinesInOrder.Length; i++)
			{
				returnval += followingLinesInOrder[i].GetLine();
			}
			
			return returnval;
		}
	}

}