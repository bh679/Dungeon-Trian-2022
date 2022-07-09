using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrennanHatton.DungeonTrain.Book
{
	public class BookManager : MonoBehaviour
	{
		public BookOpenner openner;
		public Transform contentsParent;
		
		void Reset()
		{
			openner = this.GetComponent<BookOpenner>();
			contentsParent = this.transform;
		}
		
		public void TurnNextPageContents(Transform pageContents)
		{
			//turn off current contents
			contentsParent.GetChild(0).gameObject.SetActive(false);
			
			//get tune
			float time = openner.TurnPage(true);
			
			StartCoroutine(setContentsAfterTime(pageContents,time));			
		}
		
		public void FindPageContents(Transform pageContents)
		{
			//turn off current contents
			contentsParent.GetChild(0).gameObject.SetActive(false);
			
			//turn page
			float time = openner.TurnPage();
			
			//turn on next contents after time
			StartCoroutine(setContentsAfterTime(pageContents,time));
		}
		
		IEnumerator setContentsAfterTime(Transform contents, float time)
		{
			yield return new WaitForSeconds(time);
			
			//parent to book
			contents.SetParent(contentsParent);
			
			//places as child 0
			contents.SetAsFirstSibling();
		}
	}
}