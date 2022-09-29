using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.TrainCarts.Seeds;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelChamber : MonoBehaviour
	{
		public List<BabelBookcase> bookcases = new List<BabelBookcase>();
		BookPosition position = new BookPosition();
		
		public CartTitle title;
	    
		public void SetupBookShelves(string seed)
		{
			
			position.room  = seed;
			title.text.text = seed;
			
			for(int i = 0; i < bookcases.Count; i++)
			{
				position.wall = i+1;
				bookcases[i].Setup(position);
			}
		}
	}
}