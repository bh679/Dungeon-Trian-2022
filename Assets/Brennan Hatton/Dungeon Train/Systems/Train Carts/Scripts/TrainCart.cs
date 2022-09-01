using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.TrainCarts
{
	[RequireComponent(typeof(TrainCartArchitecture))]
	public class TrainCart : MonoBehaviour
	{
		//Tracks if the user is inside the cart
		public bool playerInside = false;
		
		TrainCartArchitecture architecture;
		
		
		public ArchitectureTheme theme;
		
		public float tilesLength = 1;
		public float length
		{
			get{
				return ArchitectureTheme.tileSize * tilesLength;
			}
		}
	
		public void SetTheme(ArchitectureTheme newTheme)
		{
			theme = newTheme;
			
			if(architecture == null)
				architecture = this.gameObject.GetComponent<TrainCartArchitecture>();
				
			architecture.SetTheme(newTheme);
		}
		
		public void SetPlayerInside()
		{
			playerInside = true;
		}
	}

}