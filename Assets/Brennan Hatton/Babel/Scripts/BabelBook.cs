 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{
	
	public class BookMark
	{
		public int page,
		characterStart,
		length;
	}
	
	[System.Serializable]
	public class BookData : BookPosition 
	{
		List<BookMark> bookmark;
		
		public BookData(bool random = false)
		{
			CopyBookPosition(new BookPosition(random));
		}
		
		public BookData(BookPosition position)
		{
			CopyBookPosition(position);
		}
	}
	
	public class BabelBook : MonoBehaviour
	{
		public bool setup = false;
		
		public BabelPage page;
		public BookData data;
		public BookTitle title;
		
		
		public void Setup(BookPosition newPosition)
		{
			data = new BookData(newPosition);
			page.Setup(newPosition);
			if(title != null)
				title.GetBookTitle(newPosition);
			setup = true;
		}
	}
}