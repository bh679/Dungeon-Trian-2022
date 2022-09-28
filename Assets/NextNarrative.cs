using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel.Narrative
{

public class NextNarrative : MonoBehaviour
{
	
	public void Next()
	{
		NarrativeManager.Instance.FeedNextBook();
	}
}
}