using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Props.Story
{
	
	
	[System.Serializable]
	public class LetterVersion
	{
		public string[] Pages;
	}
	
	[System.Serializable]
	public class Letter
	{
		public string name;
		public LetterVersion[] versions;
			
		public LetterVersion GetRandomVersion()
		{
			if(versions == null || versions.Length == 0)
			{
				LetterVersion retVal = new LetterVersion();
				retVal.Pages = new string[1] {"404 story version not found"};
				return retVal;
				
			}
			
					
			return versions[Random.Range(0, versions.Length-1)];
		}
	}
	
	public class Story : MonoBehaviour
	{
		public string storyName;
		public string characterName;
		
		
		public Letter[] letters;
		
		public string[] GetLetterPages(int currentLetter)
		{
			return letters[currentLetter % letters.Length].GetRandomVersion().Pages;
		}
	}
}