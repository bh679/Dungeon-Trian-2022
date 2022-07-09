using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Scanner
{
	[RequireComponent(typeof(SharedLinesImporter))]
	public class ImportSharedLines : MonoBehaviour
	{
	    
		SharedLinesImporter sharedLines;
			
		void Reset()
		{
			sharedLines = this.gameObject.GetComponent<SharedLinesImporter>();
			sharedLines.Import();
		}
	}
}