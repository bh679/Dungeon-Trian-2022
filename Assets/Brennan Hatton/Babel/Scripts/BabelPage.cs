﻿using System.Collections;
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
		public UnityEvent onPageTurn, onPageLoad;
		
		public void Setup(BookPosition newPosition)
		{
			position = new BookPosition(newPosition);
			
			if(position.page == 0)
				position.page = 1;
			//Debug.Log(position.Debug);
			//Debug.Log(position.page);
			
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
			Debug.Log(position.page);
			GetPage(position);
		}
	    
	    
		
		public void TurnPage(bool forward)
		{
			if(forward && position.page < 410)
				position.page++;
			else if(position.page > 1)
				position.page--;
			
			onPageTurn.Invoke();
			
			GetPage(position);
			Debug.Log(position.page);
			
		}
	
	    
		protected override void OnPage (string page)
		{
			Debug.Log ("On Page:" + page);
			text.text = page.Remove(0,61);
			onPageLoad.Invoke();
		}
	
		protected override void OnTitle (string title)
		{
			Debug.Log ("On Title:" + title);
		}
	}
}