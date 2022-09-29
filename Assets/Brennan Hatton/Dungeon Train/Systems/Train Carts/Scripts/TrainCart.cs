using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.TrainCarts.Seeds;

namespace BrennanHatton.TrainCarts
{

	public class TrainCart : MonoBehaviour
	{
		//Tracks if the user is inside the cart
		public bool playerInside = false;
		
		public ArchitectureTheme[] themesPrefabs;
		public TrainCartStructure[] structurePrefabs;
		
		public string seed;
		
		TrainCartArchitecture architecture;
		TrainCartStructure trainCartStructure;
		int themeId ,
			structureId;
		
		public float tilesLength = 1;
		public float length
		{
			get{
				return trainCartStructure.length;//ArchitectureTheme.tileSize * tilesLength;
			}
		}
		
		public void Create(string _seed)
		{
			seed = _seed;
			Random.seed = seed.GetHashCode();
			
			themeId  = Random.RandomRange(0,themesPrefabs.Length-1);
			structureId = Random.RandomRange(0,structurePrefabs.Length-1);
			
			SetThemeAndStructure();
			
		}
		
		public void PopulateContents()
		{
			architecture.PopulateContents();
		}
	
		public void SetThemeAndStructure()
		{
			
			if(architecture == null)
				architecture = this.gameObject.AddComponent<TrainCartArchitecture>();
				
			trainCartStructure = Instantiate(structurePrefabs[structureId], this.transform.position, this.transform.rotation, this.transform);
				
			architecture.SetThemeAndStructure(themesPrefabs[themeId], trainCartStructure);
		}
		
		public void SetPlayerInside()
		{
			playerInside = true;
			trainCartStructure.SetPlayerInside();
		}
	}

}