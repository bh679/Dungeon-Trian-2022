using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelBookcase : MonoBehaviour
	{
		public BabelShelf[] shelves;
		
		void Reset()
		{
			shelves = this.GetComponentsInChildren<BabelShelf>();
		}
		
		BookPosition position;
		
		public void Setup(BookPosition newPosition)
		{
			position = newPosition;
			
			for(int i = 0; i < shelves.Length; i++)
			{
				newPosition.shelf = i+1;
				shelves[i].Setup(newPosition);
			}
			
		}
	}
}