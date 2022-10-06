using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{

public class EnableOnBooksFinished : MonoBehaviour
{
	public BabelShelf shelf;
	
	public GameObject[] target;
	
	bool done = false;

    // Update is called once per frame
    void Update()
    {
	    if(shelf.setup && !done)
	    {
	    	for(int i = 0 ;i < target.Length; i++)
		    	target[i].SetActive(true);
		    	
		    done = true;
	    }
    }
}
}