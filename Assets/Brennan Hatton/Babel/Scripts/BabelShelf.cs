using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelShelf : MonoBehaviour
	{
		public BabelBook[] books;
		
		BookPosition position;
		
		public void Setup(BookPosition newPosition)//string _chamberId, int _wall, int _shelfId)
		{
			position = new BookPosition(newPosition);
			books = this.GetComponentsInChildren<BabelBook>();
			for(int i = 0; i < books.Length; i++)
			{
				newPosition.volume = i+1;
				books[i].Setup(newPosition);
			}
			
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	}
}