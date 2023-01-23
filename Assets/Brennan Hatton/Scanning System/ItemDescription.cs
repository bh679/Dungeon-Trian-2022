using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrennanHatton.Scanner
{
	
	[System.Serializable]
	public class Line
	{
		public string textLine;
		public AudioClip audioLine;
		public float delayafter = 0.5f;
		public SharedLines sharedLines;
		
		public void SetSharedLines()
		{
			if(sharedLines != null)
				textLine = sharedLines.GetSharedLine();
		}

		//this should be moved to display
		/*public void Play(Text textDisplay, AudioSource audioSource)
		{
			/*if(audioLine != null)
				Debug.Log("Play " + " " + audioLine.name);
			else
			//Debug.Log("Play " + " " + audioLine);-//*-/
				
			if(textLine != null)
				textDisplay.text = textLine;
			else
				textDisplay.text = "";
		
			if(audioSource != null && audioLine != null)
			{
				audioSource.PlayOneShot(audioLine);
				//Debug.Log("audioSource.PlayOneShot(audioLine);");
			}
		}*/
		
		public float Length(bool plusDelay = false)
		{
			
			if(audioLine != null)
				return audioLine.length + (plusDelay? delayafter : 0) ;
				
			else return textLine.Length * 0.045f + (plusDelay? delayafter : 0);
		}
	}


	[System.Serializable]
	public class Description
	{
		public Line[] lines;
	
		int nextLineNumber = 0;
		
		public void StartFromStart()
		{
			nextLineNumber = 0;
		}
		
		public bool NoMoreLines()
		{
			return (nextLineNumber == 0);
			
		}
	
		//this should be moved to display
		public Line GetNextLine()
		{
			
			int retVal = nextLineNumber;
			//float delay = lines[nextLineNumber].delayafter + lines[nextLineNumber].Length();
			
			//lines[nextLineNumber].Play(textDisplay, audioSource); // right now it only plays one line
			nextLineNumber++;

			nextLineNumber = nextLineNumber % lines.Length;
			
			lines[retVal].SetSharedLines();
			
			return lines[retVal];
			
		}
		
		public float TotalLength()
		{
			float total = 0;
			
			for(int i = 0; i < lines.Length; i++)
			{
				total += lines[i].delayafter + lines[i].Length();
			}
			
			return total;
		}
		
		
	}

	public class ItemDescription : MonoBehaviour
	{
		
		
		public List<Description> descriptions = new List<Description>();
		static int i = 0;
		
		void Reset()
		{
			Description defaultDescription = new Description();
			defaultDescription.lines = new Line[1];
			defaultDescription.lines[0] = new Line();
			defaultDescription.lines[0].textLine = "A " + this.gameObject.name;
			descriptions.Add(defaultDescription);
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
		public Description Scan()
		{
			i++;
			
			if(i > descriptions.Count -1)
			 i = 0;
			
			
			if(descriptions.Count <= 1)
				return descriptions[0];
			
			return descriptions[i];
			
		}
	    
	    
		/*/this should be moved to display
		public float Scan(TextMesh textDisplay, AudioSource audioSource)
		{
			i++;
			
			if(i > descriptions.Length -1)
			 i = descriptions.Length -1;
			
			
			if(descriptions.Length > 1)
				StartCoroutine(PlayAllLines(textDisplay, audioSource, i));
			else
				StartCoroutine(PlayAllLines(textDisplay, audioSource, 0));
			
			return descriptions[i].TotalLength();
			
		}
		
		//this should be moved to display
		IEnumerator PlayAllLines(TextMesh textDisplay, AudioSource audioSource, int _i)
		{
			descriptions[_i].StartFromStart();
			float delay;
			
			do
			{
				delay = descriptions[_i].PlayNextLine(textDisplay, audioSource);
				
				yield return new WaitForSeconds(delay);
				
			}while(!descriptions[_i].NoMoreLines());
			
			yield return null;
		}*/
	}

}
