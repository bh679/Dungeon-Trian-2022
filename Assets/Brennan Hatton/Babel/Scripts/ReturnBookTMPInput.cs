using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BrennanHatton.LibraryOfBabel.Networking.Events;

namespace BrennanHatton.LibraryOfBabel
{
	public class ReturnBookTMPInput : MonoBehaviour
	{
		public bool networked = true;
		    
		public void Return(TMP_Text text)
		{
			Return(text.text);
		}
		    
		public void Return(string url)
		{
			Return(new BookData(url));
		}
		public void Return(BookPosition position)
		{
			Return(new BookData(position));
		}
		
		
		public void Return(BookData book)
		{
			if(networked)
				SendEventManager.ReturnBookEvent((BookPosition)book);
			else
				BookReturnsManager.Instance.Return(book);
		}
	}
}