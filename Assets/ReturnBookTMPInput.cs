using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BrennanHatton.LibraryOfBabel
{
	public class ReturnBookTMPInput : MonoBehaviour
	{
		    
		public void Return(TMP_Text text)
		{
			BookReturnsManager.Instance.Return(new BookData(text.text));
		}
		    
		public void Return(string url)
		{
			BookReturnsManager.Instance.Return(new BookData(url));
		}
		public void Return(BookPosition position)
		{
			BookReturnsManager.Instance.Return(new BookData(position));
		}
	}
}