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
		
		TrainCartArchitecture architecture;
		TrainCartStructure trainCartStructure;
		public string seed;
		
		
		public ArchitectureTheme theme;
		
		public float tilesLength = 1;
		public float length
		{
			get{
				return trainCartStructure.length;//ArchitectureTheme.tileSize * tilesLength;
			}
		}
	
		public void SetThemeAndStructure(ArchitectureTheme newTheme, TrainCartStructure structurePrefab)
		{
			theme = newTheme;
			
			if(architecture == null)
				architecture = this.gameObject.AddComponent<TrainCartArchitecture>();
				
			trainCartStructure = Instantiate(structurePrefab, this.transform.position, this.transform.rotation, this.transform);
				
			architecture.SetThemeAndStructure(newTheme, trainCartStructure);
		}
		
		public void SetPlayerInside()
		{
			playerInside = true;
			trainCartStructure.SetPlayerInside();
		}
	}

}