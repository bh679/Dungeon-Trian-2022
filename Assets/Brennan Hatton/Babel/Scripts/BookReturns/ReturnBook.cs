﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.LibraryOfBabel.Networking.Events;

namespace BrennanHatton.LibraryOfBabel
{

	public class ReturnBook : MonoBehaviour
	{
		public bool onTrigger = true;
		public static bool networked = true;
		
		private void OnTriggerEnter(Collider other)
		{
			if(!onTrigger)
				return;
			
			BabelBook book = other.GetComponent<BabelBook>();
			
			if(book == null)
				return;
			
			Return(book.data);
				
			Destroy(book.gameObject);
		}
		
		public void Return(BookData book)
		{
			if(networked)
				SendEventManager.ReturnBookEvent((BookPosition)book);
			else
				BookReturnsManager.Instance.Return(book);
		}
	}
}