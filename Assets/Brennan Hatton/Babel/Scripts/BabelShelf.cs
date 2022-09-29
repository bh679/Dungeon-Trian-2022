using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelShelf : MonoBehaviour
	{
		public BabelBook[] books;
		BookPosition position;
		public bool setup = false;
		public Populator populator;
		
		public void Setup(BookPosition newPosition)
		{
			position = new BookPosition(newPosition);
			
			StartCoroutine(_setup(newPosition));
		}
		
		IEnumerator _setup(BookPosition newPosition)
		{
			while(!populator.finished)
				yield return new WaitForEndOfFrame();
			
			books = populator.GetComponentsInChildren<BabelBook>();
			for(int i = 0; i < books.Length; i++)
			{
				newPosition.volume = i+1;
				books[i].Setup(newPosition);
			}
			
			setup = true;
			
			yield return null;
		}
	}
}