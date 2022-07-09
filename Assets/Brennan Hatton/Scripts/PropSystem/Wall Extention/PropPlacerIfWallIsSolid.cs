using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;
//using BrennanHatton.DungeonTrain.Rooms;


namespace BrennanHatton.Props.Extentions
{
	/// <summary>
	/// This class was designed for placing objects inside rooms without windows or open carraiges
	/// It requires PropPlacers, and Walls
	/// </summary>
	public class PropPlacerIfWallIsSolid : PropPlacer
	{
		public PropPlacer[] wallPlacers;
		public bool AllAreSolid = false;
		//public Room room;
		
		/// <summary>
		/// Place objects only if provided placers have only solid walls
		/// </summary>
		/// <returns></returns>
		public override Prop[] Place(bool addToExisting)
		{
			//for all wall placers
			for(int w = 0; w < wallPlacers.Length; w++)
			{
				//for all props placed
				for(int i = 0; i < wallPlacers[w].propsPlaced.Count; i++)
				{
					if(Debugging) Debug.Log(wallPlacers[w].propsPlaced[i]);
					
					//if it is not wa wall
					if(!(wallPlacers[w].propsPlaced[i] is Wall))
						//return empty array
						return new Prop[0];
				
					if(Debugging) Debug.Log(((Wall)wallPlacers[w].propsPlaced[i].prop).isSolid);
					
					//if it is not solid
					if(((Wall)wallPlacers[w].propsPlaced[i].prop).isSolid != AllAreSolid)
						//return empty array
						return new Prop[0];
				}
			}
			
			//place and return array
			return base.Place(addToExisting);
		}
	}
}