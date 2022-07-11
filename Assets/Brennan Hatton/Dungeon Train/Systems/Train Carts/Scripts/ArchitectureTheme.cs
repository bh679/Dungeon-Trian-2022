using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;

namespace BrennanHatton.TrainCarts
{

	public class ArchitectureTheme : MonoBehaviour
	{
		public PropType walls, floors, roofs, windows, doors;
		public const float tileSize = 5f;
		//public TrainDoorWall[] doors;
		
		/*public GameObject GetAWall()
		{
			int id = Random.RandomRange(0,walls.Length-1);
			return walls[id];
		}
		public TrainDoorWall GetADoor()
		{
			int id = Random.RandomRange(0,doors.Length-1);
			return doors[id];
		}
		
		public GameObject GetAFloor()
		{
			int id = Random.RandomRange(0,floors.Length-1);
			return floors[id];
		}
		
		public GameObject GetARoof()
		{
			int id = Random.RandomRange(0,roofs.Length-1);
			return roofs[id];
		}*/
	}

}