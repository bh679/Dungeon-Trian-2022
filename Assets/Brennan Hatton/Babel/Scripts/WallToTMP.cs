using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BrennanHatton.LibraryOfBabel
{
	public class WallToTMP : MonoBehaviour
	{
		public TMP_Text text;
		public BabelBookcase bookcase;
	
	    // Update is called once per frame
	    void Update()
	    {
		    if(bookcase.position != null)// && bookcase.position.shelf != 0)
		    {
		    	text.text = bookcase.position.wall.ToString();
		    }
	    }
	}
}