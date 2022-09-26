using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelBook : MonoBehaviour
	{
		public bool setup = false;
		
		public BabelPage page;
		public BookPosition position;
		
		/*public void Setup(string _chamberId, int _wallId, int _shelfId, int _volume)
		{
			
		}*/
		
		public void Setup(BookPosition newPosition)
		{
			position = newPosition;
			//page = this.GetComponentInChildren<BabelPage>();
			
			/*chamberId = _chamberId;
			wallId = _wallId;
			shelfId = _shelfId;
			volumeId = _volume;*/
			newPosition.page = 1;
			page.Setup(newPosition);
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