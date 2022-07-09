using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;

namespace BrennanHatton.Props.Story
{

	public class PaperGrabber : MonoBehaviour
	{
		Paper paper;
		public PropPlacer paperPlacer;
		[TextArea(15,20)]
		public string[] messages;
		int messageId = 0;
		int messageCount = 0;
		public bool loop = false;
		
		[Tooltip("this will cause repeats")]
		public bool random = false;
		
	    // Start is called before the first frame update
	    void Start()
		{
			
	    }
	
	    // Update is called once per frame
	    void Update()
		{
			if(!loop)
			{
				if(messageCount >= messages.Length)
					return;
			}
			
			if(paper == null)
			{
				paper = this.GetComponentInChildren<Paper>();
			}
	    	
			if(paper != null && Vector3.Distance(paper.transform.position,this.transform.position) > 0.3f)
		    {
		    	CreatePaper();
		    }
	    }
	    
		public void CreatePaper()
		{
			if(messageCount >= messages.Length)
				messageId = 0;
				
			Prop[] papersPlaced = paperPlacer.Place(true); // this needs to not reset the existing props
			
			paper = papersPlaced[0] as Paper;
			paper.SetText(messages[messageId]);
			
			if(papersPlaced.Length > 0)
			{
				if(random)
				{
					messageId = Random.Range(0,messages.Length - 1);//make this not cause repeats
				}
				else
				{
					messageId++;
					
					if(messageCount >= messages.Length)
						messageId = 0;
				}
				messageCount++;
			}
		}
		
	    
		
		
	}

}
