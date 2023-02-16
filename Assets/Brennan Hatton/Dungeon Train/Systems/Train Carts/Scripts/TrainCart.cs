using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.TrainCarts.Seeds;
using BrennanHatton.UnityTools;

namespace BrennanHatton.TrainCarts
{

	public class TrainCart : MonoBehaviour
	{
		//Tracks if the user is inside the cart
		public bool playerInside = false;
		
		public TrainCartStructure[] structurePrefabs;
		
		public string seed;
		
		TrainCartArchitecture architecture;
		TrainCartStructure trainCartStructure;
		int structureId;
		
		public float tilesLength = 1;
		public float length
		{
			get{
				return trainCartStructure.length;//ArchitectureTheme.tileSize * tilesLength;
			}
		}
		
		public void Create(string _seed, ArchitectureTheme theme)
		{
			seed = _seed;
			if(seed != "")
			{
				Random.seed = seed.GetHashCode();
			}
			structureId = Random.RandomRange(0,structurePrefabs.Length-1);
			
			SetThemeAndStructure(theme);
			
		}
		
		public void PopulateContents()
		{
			architecture.PopulateContents();
		}
	
		public void SetThemeAndStructure(ArchitectureTheme theme)
		{
			
			if(architecture == null)
				architecture = this.gameObject.AddComponent<TrainCartArchitecture>();
				
			trainCartStructure = Instantiate(structurePrefabs[structureId], this.transform.position, this.transform.rotation, this.transform);
				
			architecture.SetThemeAndStructure(theme, trainCartStructure);
		}
		
		public void SetPlayerInside()
		{
			playerInside = true;
			trainCartStructure.SetPlayerInside();
			InstantiateList.Instance.Clear();
		}
	}

}