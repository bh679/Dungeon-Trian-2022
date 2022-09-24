using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.TrainCarts
{
	public class AddShelvesToWalls : MonoBehaviour
	{
		
		public TrainCartStructure trainCartStructure;
		public GameObject prefabToAdd;
		
		bool[] WallsToAdd;
		
	    // Start is called before the first frame update
	    void Start()
		{
			WallsToAdd = new bool[trainCartStructure.walls.Length];
			
			StartCoroutine(AddToWallWhenReady());
			
			/*for(int i =0 ; i < trainCartStructure.walls.Length; i++)
		    {
		    	trainCartStructure.walls[i].onStructureCreated.Add(AddToWall);
			}*/
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
			
			yield return null;
		}
	    
	
		// Create a method for a delegate.
		public void AddToWall(Transform position)
		{
			GameObject newObject = Instantiate(prefabToAdd,position.position, position.rotation, position);	
		}
	}
}