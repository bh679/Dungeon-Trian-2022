using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.TrainCarts
{

	public class TrainCartGenerator : MonoBehaviour
	{
		public TrainCart trainCartPrefab;
		
		public ArchitectureTheme[] themes;
		public TrainCartStructure[] structurePrefabs;
		int s = 0;
	    
		public TrainCart CreateCart()
		{
			TrainCart cart = Instantiate(trainCartPrefab, this.transform.position, this.transform.rotation, this.transform);
			
			int id = Random.RandomRange(0,themes.Length-1);
			cart.SetThemeAndStructure(themes[id],structurePrefabs[s]);
			s = (s + 1) % structurePrefabs.Length;
			
			return cart;
		}
	}

}