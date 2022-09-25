using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BrennanHatton.TrainCarts;

namespace BrennanHatton.LibraryOfBabel
{
	public class AddBookcasesToWalls : MonoBehaviour
	{
		
		public TrainCartStructure trainCartStructure;
		public GameObject prefabToAdd;
		public List<GameObject> bookcases = new List<GameObject>();
		public BabelChamber chamber;
		
		bool[] WallsToAdd;
		
	    // Start is called before the first frame update
	    void Start()
		{
			WallsToAdd = new bool[trainCartStructure.walls.Length];
			
			StartCoroutine(AddToWallWhenReady());
	    }
	    
		IEnumerator AddToWallWhenReady()
		{
			int i = 0;
			while(i < trainCartStructure.walls.Length)
			{
				if(trainCartStructure.walls[i].done)
				{
					AddToWall(trainCartStructure.walls[i].position);
					yield return new WaitForEndOfFrame();
					i++;
				}
				yield return new WaitForEndOfFrame();
			}
			
			chamber.SetupBookShelves();
			
			yield return null;
		}
	    
	
		public void AddToWall(Transform position)
		{
			bookcases.Add(Instantiate(prefabToAdd,position.position, position.rotation, position));	
			chamber.bookcases.Add(bookcases[bookcases.Count-1].GetComponentInChildren<BabelBookcase>());
		}
	}
}