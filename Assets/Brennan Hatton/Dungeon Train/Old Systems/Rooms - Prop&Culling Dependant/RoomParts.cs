using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.DungeonTrain.Rooms
{
	
	public class RoomParts : MonoBehaviour
	{
		public GameObject[] walls, doors, floors, roofs;
		public GameObject teleAreaTile;
		
		public GameObject GetFloor()
		{
			if(floors == null || floors.Length == 0)
				return null;
				
			return floors[(int)(Random.value*floors.Length % floors.Length)];
			
		}
		
		public GameObject GetRoof()
		{
			if(roofs == null || roofs.Length == 0)
				return null;
				
			return roofs[(int)(Random.value*roofs.Length % roofs.Length)];
			
		}
		
		public GameObject GetWall()
		{
			if(walls == null || walls.Length == 0)
				return null;
				
			return walls[(int)(Random.value*walls.Length % walls.Length)];
			
		}
		
		public GameObject GetDoor()
		{
			if(doors == null || doors.Length == 0)
				return null;
				
			return doors[(int)(Random.value*doors.Length % doors.Length)];
			
		}
		
	}
}