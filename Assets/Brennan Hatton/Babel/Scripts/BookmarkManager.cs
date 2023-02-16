using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools;

namespace BrennanHatton.LibraryOfBabel
{

	public class BookmarkManager : MonoBehaviour
	{
		public BabelChamber chamber;
		public Bookmark bookmarkPrefab;
		public List<Bookmark> bookmarks = new List<Bookmark>();
		
		public void HighlightBook(BookPosition bookPos)
		{
			StartCoroutine(_highlightBook(bookPos));
		}
		
		IEnumerator _highlightBook(BookPosition bookPos)
		{
			while(chamber.bookcases == null || chamber.bookcases.Count <= bookPos.wall || !chamber.bookcases[bookPos.wall-1].setup)
				yield return new WaitForEndOfFrame();
				
			while(!chamber.bookcases[bookPos.wall-1].shelves[bookPos.shelf-1].setup)
				yield return new WaitForEndOfFrame();
				
			while(!chamber.bookcases[bookPos.wall-1].shelves[bookPos.shelf-1].books[bookPos.volume-1].setup)
				yield return new WaitForEndOfFrame();
				
				
			int id = InstantiateList.Instance.GetLine();
					
			while(InstantiateList.Instance.IsMyTurn(id) == false)
			{
				yield return new WaitForEndOfFrame();
			}
			
			BabelBook book = chamber.bookcases[bookPos.wall-1].shelves[bookPos.shelf-1].books[bookPos.volume-1];
			bookmarks.Add(Instantiate(bookmarkPrefab, book.transform.position, book.transform.rotation));
			book.data.page = bookPos.page;
			book.page.position.page = bookPos.page;
			bookmarks[bookmarks.Count-1].SetBookmark(bookPos);
			
			yield return null;
		}
		
	}

}