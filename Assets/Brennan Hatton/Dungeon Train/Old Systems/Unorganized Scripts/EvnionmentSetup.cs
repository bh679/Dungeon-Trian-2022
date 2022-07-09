using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.DungeonTrain.Rooms;

public class EvnionmentSetup : MonoBehaviour
{
	public DuplicateAndFlip[] leftSides;
	public RoomCreator roomCreator;
	
	void Reset()
	{
		roomCreator = FindObjectOfType<RoomCreator>();
		leftSides = this.GetComponentsInChildren<DuplicateAndFlip>(true);
	}
	
    // Start is called before the first frame update
	void Awake()
	{

		Transform[] rightTerrain = new Transform[leftSides.Length], leftTerrain = new Transform[leftSides.Length];
		
		for(int i =0; i < leftSides.Length; i++)
		{
			leftTerrain[i] = leftSides[i].transform;
			leftTerrain[i].parent.gameObject.SetActive(true);
			leftTerrain[i].gameObject.SetActive(false);
			rightTerrain[i] = leftSides[i].duplicatedAndFlip().transform;
			rightTerrain[i].gameObject.SetActive(false);
		}
		
		roomCreator.rightTerrain = rightTerrain;
		roomCreator.leftTerrain = leftTerrain;
	}
}
