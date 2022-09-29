using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{
	
	public class Bookmark : MonoBehaviour
	{
		BookPosition position;
		
		public void SetBookmark(BookPosition newPosition)
		{
			position = new BookPosition(newPosition);
			
			
		}
	}
}
