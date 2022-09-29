using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.TrainCarts
{

	public class TrainCartGenerator : MonoBehaviour
	{
		public TrainCart[] trainCarts;
		
		int s = 0;
	    
		public TrainCart CreateCart(string seed)
		{
			TrainCart cart = Instantiate(trainCarts[s], this.transform.position, this.transform.rotation, this.transform);
			
			cart.Create(seed);
			
			//int id = Random.RandomRange(0,themes.Length-1);
			//cart.SetThemeAndStructure(themes[id],structurePrefabs[s]);
			s = (s + 1) % trainCarts.Length;
			
			return cart;
		}
	}

}