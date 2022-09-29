using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelBookcase : MonoBehaviour
	{
		public BabelShelf[] shelves;
		public BookPosition position;
		public bool setup = false;
		public Populator populator;
		public int numberOfShelves = 5;
		
		public void Setup(BookPosition newPosition)
		{
			position = new BookPosition(newPosition);
			
			StartCoroutine(_setup(position));
		}
		
		IEnumerator _setup(BookPosition newPosition)
		{
			//while(!populator.finished)
			//	yield return new WaitForEndOfFrame();
			
			//shelves = populator.GetComponentsInChildren<BabelShelf>();
			shelves = new BabelShelf[numberOfShelves];
			for(int i = 0; i < numberOfShelves; i++)
			{
				while(populator.books[i] == null)
					yield return new WaitForEndOfFrame();
				
				shelves[i] = populator.books[i].GetComponentInChildren<BabelShelf>();
				newPosition.shelf = i+1;
				shelves[i].Setup(newPosition);
			}
			
			setup = true;
			
			yield return null;
		}
	}
}