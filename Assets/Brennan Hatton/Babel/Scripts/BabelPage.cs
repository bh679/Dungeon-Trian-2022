using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelPage : TextDownload
	{
		public BookPosition position;
		public TMP_Text text;
		
		public void Setup(string _chamberId, int _wallId, int _shelfId, int _volume, int pageId){
			position = new BookPosition();
			
			position.room = _chamberId;
			position.wall = _wallId;
			position.shelf = _shelfId;
			position.volume = _volume;
			position.page = pageId;
			
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
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