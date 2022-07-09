
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Props.Story
{

	public class StoryManager : MonoBehaviour
	{
		public Story[] stories;
		public bool newStoryAtEnd = false;//when set, this will mean stories will pick a new sotry when the last is over
		int currentStory = 0;
		int currentLetter = 0;
		public int storyTimeSeed;
		static int storyId = 0;
		
		void OnEnable()
		{
			RestartStory();
		}
		
		public void NextLetter()
		{
			currentLetter++;
				
		}
		
		
		public string[] GetLetter()
		{
			if(newStoryAtEnd)
			{
				if(currentLetter >= stories[currentStory].letters.Length)
				{
					RestartStory();
				}
			}
			
			if(currentLetter == stories[currentStory].letters.Length)
				return new string[0];
				
			string[] retVal = stories[currentStory].GetLetterPages(currentLetter);
			
			NextLetter();
			
			return retVal;
		}
		
		public void RestartStory()
		{
			currentStory = Random.Range(0,stories.Length) % stories.Length;
			currentLetter = 0;
			storyTimeSeed = (int)System.DateTime.Now.Ticks + storyId++;
		}
	}
}