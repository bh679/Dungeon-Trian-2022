using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BrennanHatton.LibraryOfBabel
{
	public class BookTitle : TextDownload
	{
		public string title;
		public TMP_Text[] titles;
		BookPosition position;
		public int pos;
		
		static List<BookTitle> titleRequests = new List<BookTitle>();
		
		public void GetBookTitle(BookPosition newPosition)
		{
			for(int i = 0; i < titles.Length; i++)
			{
				titles[i].text = "";
			}
			
			position = newPosition;
			
			titleRequests.Add(this);
			
			StartCoroutine(waitMyTurn());
		}
	
		bool CheckMyTurn()
		{
			for(int i = 0; i < Mathf.Min(titleRequests.Count,7); i++)
			{
				if(titleRequests[i] != this)
					return true;
			}
			
			return false;
		}
		IEnumerator waitMyTurn()
		{
			while(CheckMyTurn() == false)
			{
				yield return new WaitForSeconds(0.01f*titleRequests.IndexOf(this));
			}
			
			GetTitle(position);
			
			yield return null;
		}
	    
		protected override void OnPage (string page)
		{
			Debug.Log ("On Page:" + page);
			//text.text = page.Remove(0,61);//removes "<div class = "bookrealign" id = "real"><PRE id = "textblock">"
		}
	
		protected override void OnTitle (string title)
		{
			titleRequests.Remove(this);
			
			for(int i = 0; i < titles.Length; i++)
			{
				titles[i].text = title;
			}
			
			Debug.Log ("On Title:" + title);
		}
		
	}
}