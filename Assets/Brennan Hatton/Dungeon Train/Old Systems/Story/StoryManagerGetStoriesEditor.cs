using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BrennanHatton.Props.Story
{
	[RequireComponent(typeof(StoryImporter))]
	public class StoryManagerGetStoriesEditor : MonoBehaviour
	{
	    // Start is called before the first frame update
		void Reset()
	    {
		    StoryImporter storyImporter = this.GetComponent<StoryImporter>();
		    storyImporter.GetStories();
	    }
	}
}