using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitectureTheme : MonoBehaviour
{
	public GameObject[] walls, floors, roofs;
	
	public GameObject GetAWall()
	{
		int id = Random.Range(0,walls.Length-1);
		return walls[id];
	}
	
	public GameObject GetAFloor()
	{
		int id = Random.Range(0,floors.Length-1);
		return floors[id];
	}
	
	public GameObject GetARoof()
	{
		int id = Random.Range(0,roofs.Length-1);
		return roofs[id];
	}
}
