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
		
		/*public void Setup(string _chamberId, int _wallId, int _shelfId, int _volume)
		{
			
		}*/
		
		public void Setup(BookPosition newPosition)
		{
			data = new BookData(newPosition);
			//page = this.GetComponentInChildren<BabelPage>();
			
			/*chamberId = _chamberId;
			wallId = _wallId;
			shelfId = _shelfId;
			volumeId = _volume;*/
			newPosition.page = 1;
			page.Setup(newPosition);
			if(title != null)
				title.GetBookTitle(newPosition);
			setup = true;
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