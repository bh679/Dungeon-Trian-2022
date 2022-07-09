using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Rendering;

namespace BrennanHatton.Rendering.Editor
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(UVMover))]
	public class TestUVMover : MonoBehaviour
	{
		UVMover mover;
		
	    // Start is called before the first frame update
		void Reset()
	    {
		    mover = this.GetComponent<UVMover>();
		    
		    if(mover != null)
		    {
		    	mover._replaceUV();
		    }
	    }
	}

}