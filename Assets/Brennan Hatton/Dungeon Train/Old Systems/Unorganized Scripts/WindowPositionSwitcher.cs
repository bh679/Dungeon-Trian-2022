using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Positions;

namespace BrennanHatton.Props.Extentions
{
	/// <summary>
	/// Allows control over where the windows are
	/// </summary>
	public class WindowPositionSwitcher : MonoBehaviour
	{
		//placer for solid walls
		public PropPlacer SolidWallPlacer, 
			//placer for other wall pieces
			AllWindowWallPlacer;
		
		//positions on the left and right
		public PositionGroup LeftPositions, RightPositions;
		
		//which side is the window on
		public bool WindowIsLeft = true;
		
		//setup, gets references 
		void Reset()
		{
			//get all placers
			PropPlacer[] placers = this.GetComponentsInChildren<PropPlacer>();
			
			if(placers.Length == 2)
			{
				//find solid placer
				if(placers[0].gameObject.name.ToLower().Contains("solid"))
				{
					SolidWallPlacer = placers[0];
					AllWindowWallPlacer = placers[1];
				}else
				{
					SolidWallPlacer = placers[1];
					AllWindowWallPlacer = placers[0];
				}
			}
			
			//get all positions
			PositionGroup[] pools = this.GetComponentsInChildren<PositionGroup>();
			
			if(pools.Length == 2)
			{
				//find right position
				if(pools[0].gameObject.name.ToLower().Contains("right"))
				{
					RightPositions = pools[0];
					LeftPositions = pools[1];
				}else
				{
					RightPositions = pools[1];
					LeftPositions = pools[0];
				}
			}
			
			//start on the right
			SetWindowRight();
		}
		
		public void SetWindowRight()
		{
			WindowIsLeft = false;
			
			AllWindowWallPlacer.ReturnProps();
			SolidWallPlacer.ReturnProps();
			
			AllWindowWallPlacer.positionGroup = RightPositions;
			SolidWallPlacer.positionGroup = LeftPositions;
		}
		
		public void SetWindowLeft()
		{
			WindowIsLeft = true;
			
			AllWindowWallPlacer.ReturnProps();
			SolidWallPlacer.ReturnProps();
			
			AllWindowWallPlacer.positionGroup = LeftPositions;
			SolidWallPlacer.positionGroup = RightPositions;
		}
		
	}

}
