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
			Debug.Log("SetupBabelBookcase");
			chamberId = _chamberId;
			wallId = _wall;
			for(int i = 0; i < shelves.Length; i++)
			{
				shelves[i].Setup(chamberId, wallId, i);
			}
			
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	}
}