using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Props.Story
{
	public class StoryImporter : MonoBehaviour
	{
		public StoryManager storyManger;
		public TextAsset[] storyFiles;
	    
		void Reset()
		{
			storyManger = this.GetComponentInChildren<StoryManager>();
		}
	    
		public void GetStories()
		{
			if(storyManger == null)
				return;
			
			//do something about existing stories
			for(int i = 0; i < storyManger.stories.Length; i++)
			{
				//for now reset them
				if(storyManger.stories[i]!= null)
					DestroyImmediate(storyManger.stories[i].gameObject);
			}
				
			storyManger.stories = new Story[storyFiles.Length];
			for(int i = 0; i < storyFiles.Length; i++)
			{
				storyManger.stories[i] = GetStory(storyFiles[i]);
			}
		}
			
		Story GetStory(TextAsset textFile)
		{
				
			Story newStory = new GameObject().AddComponent<Story>();
			newStory.transform.SetParent(this.transform);
			
			List<Letter> letterList = new List<Letter>();
			List<List<string>> letterAltTextList = new List<List<string>>();
			letterAltTextList.Add(new List<string>());
			string lastname = "";
				
			newStory.gameObject.name = "";
			String[] lines = textFile.text.Split(new Char[] { '\n' }, StringSplitOptions.None);
		
			bool storyFound = false;
			bool addNextLine = false;
			Debug.Log(lines.Length + " lines");
			bool addFirstLineToName = false;
				
			int lineNumber = 0;
			foreach(String line in lines)
			{
				if(!storyFound)
				{
					
					//check for character name
					if(string.IsNullOrEmpty(newStory.characterName))
					{
						newStory.characterName = GetLineAfter(line, 0, "Character:");
						Debug.Log("newStory.characterName " + newStory.characterName);
					}
							
					//check for story name
					if(string.IsNullOrEmpty(newStory.storyName))
					{
						newStory.storyName = GetLineAfter(line, 0, "Story:");
						Debug.Log("newStory.storyName " + newStory.storyName);
							
					}
						
					//check if first letter found
					if(AtNewLetter(line))
					{
						Debug.Log("storyFound");
						storyFound = true;
						lastname = line;
						addNextLine = false;
					}
				}else
				{
					if(AtNewLetter(line))
					{
						addNextLine = false;
						
						letterList.Add(new Letter());
						
						//make sure name isnt blank
						letterList[letterList.Count-1].name = lastname;
						lastname = line;
						
						addFirstLineToName = (lastname.ToLower() == "letter -" || lastname.ToLower() == "letter-" || lastname.ToLower() == "letter" || lastname.ToLower() == "letter - ");
						
							
						if(letterAltTextList.Count <= 0)
							Debug.LogError("Missing letter contents on "  + textFile.name + " at line " + lineNumber);
						
						//create new letter version
						letterList[letterList.Count-1].versions = new LetterVersion[letterAltTextList.Count];
						
						//copy over versions recorded
						for(int l = 0; l < letterAltTextList.Count; l++)
						{
							
							
							LetterVersion alt = new LetterVersion();
							alt.Pages = letterAltTextList[l].ToArray();
							
							letterList[letterList.Count-1].versions[l] = alt;
							if(addFirstLineToName)
							{
								addFirstLineToName = false;
								letterList[letterList.Count-1].name += alt.Pages[0];
							}
							//Debug.Log(letterList[letterList.Count-1].versions[l].Pages);
						}
						
							
						letterAltTextList = new List<List<string>>();
						letterAltTextList.Add(new List<string>());
							
					}else if(NextWordIs(line,0,"Alt"))
					{
						letterAltTextList.Add(new List<string>());
						addNextLine = false;
					}else if(line.Length != 0 && line[0] == '[' && line[line.Length-1] == ']')
					{
						Debug.Log("Comment found and skipping: " + line);
					}else if(line.Length == 0)
					{
						//ignore empty lines
					}else
					{
						//Debug.Log(line + "  " + thisAddNextLine.ToString());
						string lineToSave = line;
						
						lineToSave = lineToSave.Replace("\\n","");
						
						if(addNextLine)
						{
							List<string> vpages = letterAltTextList[letterAltTextList.Count-1];
							
							//Debug.Log(vpages.Count);
							vpages[vpages.Count-1] += '\n' + lineToSave;
							
						
						}
							else 
							letterAltTextList[letterAltTextList.Count-1].Add(lineToSave);
						
						
						
						addNextLine = nextLineIsSamePage(line);
						
						
					}
						
				}
				
			}
			letterList.Add(new Letter());
							
			letterList[letterList.Count-1].versions = new LetterVersion[letterAltTextList.Count];
						
			for(int l = 0; l < letterAltTextList.Count; l++)
			{
							
				LetterVersion alt = new LetterVersion();
				alt.Pages = letterAltTextList[l].ToArray();
							
				letterList[letterList.Count-1].versions[l] = alt;
							
				Debug.Log(letterList[letterList.Count-1].versions[l].Pages);
			}
					
			if(string.IsNullOrEmpty(newStory.storyName))
				newStory.gameObject.name = "New Story";
			else
				newStory.gameObject.name = newStory.storyName;
								
			if(!string.IsNullOrEmpty(newStory.characterName))
				newStory.gameObject.name += " -of- " + newStory.characterName;
					
			newStory.letters = letterList.ToArray();
				
				
			return newStory;
		}
		
		bool AtNewLetter(string line)
		{
			return (
				
				NextWordIs(line,0,"Letter") 
				
				&& 
				
				(
				line[line.Length-1] == '-' || line[line.Length-1] == '–'
				||
				line[line.Length-2] == '-' || line[line.Length-2] == '–'
				)
				
				
			
			);
		}
		
		bool nextLineIsSamePage(string line)
		{
			if(line.Length < 2)
				return false;
			if(line.Length < 3)
				return(line[line.Length-2] == '\\' && line[line.Length-1] == 'n');
				
			return 
			(
				(line[line.Length-2] == '\\' && line[line.Length-1] == 'n')||
				(line[line.Length-3] == '\\' && line[line.Length-2] == 'n')
			);
		}
			
		string GetLineAfter(string text, int index, string prelim)
		{
			string output = "";
				
			//is it first line?
			int plus = 1;
			if (index == 0)
				plus = 0;
			else if (text[index] != '\n')
				return null;
				
			index += plus;
				
			if(NextWordIs(text,index,prelim))
			{
					
				index += prelim.Length;
				bool foundStart = false;
				for(int i = index; i < text.Length; i++)
				{
					if (text[index] == '\n')
						return output;
						
					if(foundStart || char.IsWhiteSpace(text[i]) == false)
					{
						output += text[i];
						foundStart = true;
					}
				}
			}
				
			return output;
		}
			
		bool NextWordIs(string source, int index, string word)
		{
			if(source.Length < index + word.Length-1)
				return false;
					
			source = source.ToLower();
			word = word.ToLower();
			for(int i = 0; i < word.Length; i++)
			{
					
				if(source[index+i] != word[i])
					return false;
			}
				
			return true;
		}
	}
}
