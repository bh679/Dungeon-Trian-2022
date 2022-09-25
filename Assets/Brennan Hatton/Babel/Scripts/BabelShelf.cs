using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelShelf : MonoBehaviour
	{
		public BabelBook[] books;
		
		string chamberId;
		int wallId;
		int shelfId;
		
		public void Setup(string _chamberId, int _wall, int _shelfId)
		{
			books = this.GetComponentsInChildren<BabelBook>();
			chamberId = _chamberId;
			wallId = _wall;
			shelfId = _shelfId;
			for(int i = 0; i < books.Length; i++)
			{
				books[i].Setup(chamberId, wallId, shelfId, i);
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