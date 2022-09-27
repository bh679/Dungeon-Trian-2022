using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{
	
	public class BookReturnsManager : MonoBehaviour
	{
		public List<BookData> _returns = new List<BookData>();
		
		public int bookCount{
			get{
				return _returns.Count;
			}
		}
		
		public static BookReturnsManager Instance { get; private set; }
		private void Awake() 
		{ 
			// If there is an instance, and it's not me, delete myself.
    
			if (Instance != null && Instance != this) 
			{ 
				Destroy(this); 
			} 
			else 
			{ 
				Instance = this; 
			} 
		}
		
		public void Return(BookData book)
		{
			_returns.Add(book);
		}
		
		public BookData GetBook()
		{
			if(_returns.Count > 0)
			{
				BookData retVal = _returns[0];
				_returns.Remove(retVal);
				
				Debug.Log("Returning " + retVal);
				return retVal;
			}
			
			return new BookData(new BookPosition(true));
		}
	}

}