using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelBook : MonoBehaviour
	{
		//public BookPosition position = new BookPosition();
		
		string chamberId;
		public int wallId,  shelfId, volumeId;
		public bool setup = false;
		
		public BabelPage page;
		
		public void Setup(string _chamberId, int _wallId, int _shelfId, int _volume)
		{
			//page = this.GetComponentInChildren<BabelPage>();
			
			chamberId = _chamberId;
			wallId = _wallId;
			shelfId = _shelfId;
			volumeId = _volume;
			
			page.Setup(chamberId,wallId,shelfId,volumeId,0);
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