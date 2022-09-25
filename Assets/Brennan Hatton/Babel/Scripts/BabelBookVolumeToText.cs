using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelBookVolumeToText : MonoBehaviour
	{
		public BabelBook book;
		public TMP_Text text;
		bool loaded = false;
		
	    // Start is called before the first frame update
	    void Start()
	    {
	    }
	
		
	
	    // Update is called once per frame
	    void Update()
		{
			if(book.setup && !loaded)
			{
				text.text = book.volumeId.ToString();
				loaded = true;
			}
	    }
	}
}