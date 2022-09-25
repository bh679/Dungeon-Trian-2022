using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelBookcase : MonoBehaviour
	{
		public BabelShelf[] shelves;
		
		string chamberId;
		int wallId;
		
		void Reset()
		{
			shelves = this.GetComponentsInChildren<BabelShelf>();
		}
		
		public void Setup(string _chamberId, int _wall)
		{
			chamberId = _chamberId;
			wallId = _wall;
			for(int i = 0; i < shelves.Length; i++)
			{
				shelves[i].Setup(chamberId, wallId, i);
			}
			
		}
	}
}