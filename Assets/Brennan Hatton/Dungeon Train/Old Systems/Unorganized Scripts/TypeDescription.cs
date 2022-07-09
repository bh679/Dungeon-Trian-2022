using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;
using BrennanHatton.Scanner;

namespace BrennanHatton.Scanner.Props
{
	
	public class TypeDescription : MonoBehaviour
	{
		public PropType type;
		public List<Description> sharedDescriptions = new List<Description>();
		ItemDescription[] descriptions;
		
		void Reset()
		{
			type = this.GetComponent<PropType>();
			
			Description defaultDescriptions = new Description();
			defaultDescriptions.lines = new Line[1];
			defaultDescriptions.lines[0] = new Line();
			defaultDescriptions.lines[0].textLine = "A " + gameObject.name;
			if(defaultDescriptions.lines[0].textLine[defaultDescriptions.lines[0].textLine.Length-1] == 's')
				defaultDescriptions.lines[0].textLine = defaultDescriptions.lines[0].textLine.Remove(defaultDescriptions.lines[0].textLine.Length-1);
			
			sharedDescriptions.Add(defaultDescriptions);
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    AddDescriptions();
	    }
	    
		void AddDescriptions()
		{
			//creates array of item descriptions components
			descriptions = new ItemDescription[type.propData.Count+type.unconvertedProps.Count];
			
			//for all props in type
			//adds item descirption comonents to all props in type 
			for(int i = 0; i < type.propData.Count; i++)
			{
				//try get existing prop description
				descriptions[i] = type.propData[i].prop.gameObject.GetComponent<ItemDescription>();
				
				//check if prop has description
				if(descriptions[i] == null)
					//if not, create new description
					descriptions[i] = type.propData[i].prop.gameObject.AddComponent<ItemDescription>();
				
				//add description lines
				for(int d = 0; d< sharedDescriptions.Count; d++)
					descriptions[i].descriptions.Add(sharedDescriptions[d]);
			}
			
			
			//props
			for(int i = 0; i < type.unconvertedProps.Count; i++)
			{
				//try get existing prop description
				descriptions[i+type.propData.Count] = type.unconvertedProps[i].gameObject.GetComponent<ItemDescription>();
				
				//check if prop has description
				if(descriptions[i+type.propData.Count] == null)
					//if not, create new description
					descriptions[i+type.propData.Count] = type.unconvertedProps[i].gameObject.AddComponent<ItemDescription>();
				
				//add description lines
				descriptions[i+type.propData.Count].descriptions.AddRange(sharedDescriptions.ToArray());
			}
			
		}
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	}

}
