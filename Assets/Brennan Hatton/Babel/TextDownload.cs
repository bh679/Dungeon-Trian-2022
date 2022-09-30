//code from https://github.com/game-prototypes/3d-babel
//I will be using this as a reference, and changing this code later.
//I probably shouldnt have put it under /BrennanHatton folder. If I dont end up changing it I will move it

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

namespace BrennanHatton.LibraryOfBabel
{
	public abstract class TextDownload : MonoBehaviour 
	{
		public static string url="https://libraryofbabel.info/"; //url of web
		public static string bookWebPage = "book.cgi";
		public static string anglishizeWebPage = "anglishize.cgi";
	
		//regexp to parse html document
		private static string regexp="<div class = \"bookrealign\" id = \"real\"><PRE id = \"textblock\">[a-z.,\\s]*<\\/PRE><\\/div>";
		private static string titleregex="<\\/form><H3>[a-z,]*<\\/H3>";
	
	
		private IEnumerator GetPage2 (BookPosition book, bool anglishized = false) {
			string url = generateUrl(book, anglishized);
			WWW www = new WWW(url);
			yield return www;
			string text=TextDownload.ParsePage(www.text);
			OnPage (text);
		}
	
		private IEnumerator GetTitle2 (BookPosition _book) {
			BookPosition book = new BookPosition(_book);
			book.page = 1;
			string url = TextDownload.generateUrl(book);
			WWW www = new WWW(url);
			yield return www;
			string text=TextDownload.ParseTitle(www.text);
			OnTitle (text);
		}
		protected void GetPage(BookPosition book, bool anglishized = false){
			StartCoroutine(GetPage2 (book, anglishized));
		}
		protected void GetTitle(BookPosition book){
			StartCoroutine(GetTitle2 (book));
		}
	
		protected abstract void OnPage (string page);
		protected abstract void OnTitle (string title);
	
	
	
		private static string ParsePage(string html){
			string text;
			Regex regex = new Regex (regexp);
			Match res = regex.Match (html);
			text = res.Groups [0].Value;
			text=Regex.Replace(text,"<div class = \"bookrealign\" id = \"real\"><PRE id = \"textblock\">\n","");
			text=Regex.Replace(text,"</PRE></div>","");
			return text;
		}
		private static string ParseTitle(string html){
			Regex regex = new Regex (titleregex);
			Match res = regex.Match (html);
			string title = res.Groups [0].Value;
			title = Regex.Replace (title, "</form><H3>", "");
			title = Regex.Replace (title, "</H3>", "");
			return title;
		}
		private static string generateUrl(BookPosition book, bool anglishized = false){
			//Debug.Log(book.log());
			string volume = book.volume.ToString ();
			if (book.volume < 10)
				volume = "0" + volume;
			string fullUrl = url + (anglishized?anglishizeWebPage:bookWebPage) + "?" + book.room + "-w" + book.wall + "-s" + book.shelf + "-v" + volume + ":" + book.page;
			//Debug.Log (fullUrl);
			return fullUrl;
		}
	
	}
	
	[System.Serializable]
	public class BookPosition{
		public string room;
		public int wall;
		public int shelf;
		public int volume;
		public int page;
		
		public string Debug{
			get {
				return "p:"+page + " v:" + volume + " s:" + shelf + " w:" + wall + " r:" + room;
			}
		}
		
		public BookPosition(string url)
		{
			url = Helper.GetAfterOrAll(url,"?");
			
			room =  Helper.GetUntilOrEmpty(url,"-").Remove(0,1);
			
			url =  Helper.GetAfterOrAll(url,"-").Remove(0,2);
			
			wall =  int.Parse(Helper.GetUntilOrEmpty(url,"-"));
			
			url =  Helper.GetAfterOrAll(url,"-").Remove(0,2);
			
			shelf =  int.Parse(Helper.GetUntilOrEmpty(url,"-"));
			
			url =  Helper.GetAfterOrAll(url,"-").Remove(0,2);
			
			volume =  int.Parse(Helper.GetUntilOrEmpty(url,":"));
			
			url =  Helper.GetAfterOrAll(url,":").Remove(0,1);
			
			if(int.TryParse(url,out page) == false)
				page =  int.Parse(url.Remove(url.Length-1,1));
		}
		
		public BookPosition(BookPosition bookToCopy)
		{
			CopyBookPosition(bookToCopy);
		}
		
		public BookPosition(string _room, int _wall, int _shelf,int _volume, int _page)
		{
			room = _room;
			wall = _wall;
			shelf = _shelf;
			volume = _volume;
			page = _page;
		}
		
		//Creates a new book of null position or random
		public BookPosition(bool isRandom = false)
		{
			if(isRandom)
			{
				room = Random.value.ToString();
				wall = Random.Range(1,4);
				shelf = Random.Range(1,5);
				volume = Random.Range(1,32);
				page = Random.Range(1,410);
				
				return;
			}
			
			room = null;
			wall = 0;
			shelf = 0;
			volume = 0;
			page = 0;
		}
		
		public string log()
		{
			return room + " " + wall + " " + shelf + " " + volume + " " + page;
		}
		
		
		public void CopyBookPosition(BookPosition position)
		{
			room = position.room;
			wall = position.wall;
			shelf = position.shelf;
			volume = position.volume;
			page = position.page;
		}
	}
}
