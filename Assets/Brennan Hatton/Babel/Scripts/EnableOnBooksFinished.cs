using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{

public class EnableOnBooksFinished : MonoBehaviour
{
	public BabelShelf shelf;
	
	public GameObject target;

    // Update is called once per frame
    void Update()
    {
	    if(shelf.setup)
		    target.SetActive(true);
    }
}
}