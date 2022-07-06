using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitectureTheme : MonoBehaviour
{
	public GameObject[] walls, floors, roofs, doorframes, windows;
	public const float tileSize = 5f;
	
	public GameObject GetAWall()
	{
		int id = Random.RandomRange(0,walls.Length-1);
		return walls[id];
	}
	public GameObject GetADoor()
	{
		int id = Random.RandomRange(0,doorframes.Length-1);
		return doorframes[id];
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
	}
}
