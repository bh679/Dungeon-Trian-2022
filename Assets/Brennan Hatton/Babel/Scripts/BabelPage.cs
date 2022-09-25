using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelPage : TextDownload
	{
		public BookPosition position;
		public TMP_Text text;
		public UnityEvent onPageTurn;
		
		public void Setup(string _chamberId, int _wallId, int _shelfId, int _volume, int pageId){
			position = new BookPosition();
			
			position.room = _chamberId;
			position.wall = _wallId+1;
			position.shelf = _shelfId+1;
			position.volume = _volume+1;
			position.page = pageId+1;
			
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
			GetPage(position);
		}
	    
	    
		
		public void TurnPage(bool forward)
		{
			if(forward && position.page < 410)
				position.page++;
			else if(position.page > 0)
				position.page--;
			
			onPageTurn.Invoke();
			
			GetPage(position);
			
		}
	
	    
		protected override void OnPage (string page)
		{
			Debug.Log ("On Page:" + page);
			text.text = page.Remove(0,61);//removes "<div class = "bookrealign" id = "real"><PRE id = "textblock">"
		}
	
		protected override void OnTitle (string title)
		{
			Debug.Log ("On Title:" + title);
		}
	}
}