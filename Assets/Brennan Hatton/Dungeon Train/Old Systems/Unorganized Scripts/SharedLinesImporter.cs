using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Scanner
{
	[RequireComponent(typeof(SharedLines))]
	public class SharedLinesImporter : MonoBehaviour
	{
		public SharedLines sharedLines;
		public TextAsset[] storyFiles;
		
		void Reset()
		{
			sharedLines = this.gameObject.GetComponent<SharedLines>();
		}
		
		public void Import()
		{
			sharedLines.sharedLines = new List<string>();
			for(int i = 0; i < storyFiles.Length; i++)
			{
				String[] lines = storyFiles[i].text.Split(new Char[] { '\n' }, StringSplitOptions.None);
				
				foreach(String line in lines)
				{
					sharedLines.sharedLines.Add(line);
				}
			}
			sharedLines.firstLine = sharedLines.sharedLines[0];
		}
		
	}
}