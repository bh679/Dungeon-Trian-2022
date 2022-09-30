using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{

	public class ReturnBook : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			BabelBook book = other.GetComponent<BabelBook>();
			
			if(book == null)
				return;
			
			BookReturnsManager.Instance.Return(book.data);
				
			Destroy(book.gameObject);
		}
	}
}