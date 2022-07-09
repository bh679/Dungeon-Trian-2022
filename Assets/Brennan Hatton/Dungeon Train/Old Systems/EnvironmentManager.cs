using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Utilities;

namespace BrennanHatton.DungeonTrain.Envionments
{

	public class EnvironmentManager : MonoBehaviour
	{
		public Background[] allBackgrounds;
		
		public float visibleLength = 100;
		Background[] activeBg;
		public int speed = 5;
		int totalPassed = 0, frontId = 0;
		
		public bool createMinimum = false;
		
	    // Start is called before the first frame update
	    void Start()
		{
			//turn all off
			for(int i = 0; i < allBackgrounds.Length; i++)
			{
				allBackgrounds[i].gameObject.SetActive(false);
			}
	    	
			activeBg = new Background[(int)(visibleLength*2/allBackgrounds[0].length)]; 
			
			if(createMinimum)
			{
				CreateBackGroundsNeeded();
			}
			
		    
			float nextPos = -visibleLength;
			for(int i =0 ; i < activeBg.Length; i++)
		    {
				activeBg[i] = allBackgrounds[GetIDNotActive()];//SetNextBackground();
				activeBg[i].transform.localPosition = Vector3.forward*nextPos;
				activeBg[i].gameObject.SetActive(true);
				nextPos += activeBg[i].length;
		    }
			//frontId = activeBg.Length-1;
		}
	    
		int GetIDNotActive()
		{
			int id = Random.Range(0,allBackgrounds.Length);
			int counter = 0;
			while(IsActive(id))
			{
				id = (id + 1) % allBackgrounds.Length;
				counter++;
				
				if(counter > allBackgrounds.Length*2)
				{
					Debug.LogError("Not Enough Terrain Sections. " + gameObject.name + " " + TransformUtils.HierarchyPath(this.transform,3));
					
					return -1;;
				}
			}
			
			return id;
		}
		
		void CreateBackGroundsNeeded()
		{
			List <Background> backgroundsList = new List<Background>();
			for(int i = 0 ;i < allBackgrounds.Length; i++)
				backgroundsList.Add(allBackgrounds[i]);
			
			int x = 0;
			while(backgroundsList.Count < activeBg.Length+1){
				
				Background newBG = Instantiate(backgroundsList[x],backgroundsList[x].transform.parent);
				backgroundsList.Add(newBG);
				
				x++;
				
			}
			
			allBackgrounds = backgroundsList.ToArray();
		}
		
		bool IsActive(int id)
		{
			for(int i = 0; i < activeBg.Length; i++)
			{
				if(activeBg[i] == allBackgrounds[id])
					return true;
			}
			
			return false;
		}
	
	    // Update is called once per frame
		void Update()
		{
			if(activeBg == null || activeBg.Length == 0)
				return;
			
			//move all forward
		    for(int i =0 ; i < activeBg.Length; i++)
		    {
		    	activeBg[i].transform.localPosition -= Vector3.forward*speed*Time.deltaTime;
		    }
		    
			//if ready for next one
			if(activeBg[frontId].transform.localPosition.z/* + activeBg[frontId].length*/ < -visibleLength)
			{
				AddNext();
			}
		}
	    
	    
		public void AddNext()
		{
			//move away last
			activeBg[frontId].gameObject.SetActive(false);
			
			activeBg[frontId] = SetNextBackground();
			totalPassed++;
			frontId = totalPassed % activeBg.Length;
		}
		
		
		Background SetNextBackground()
		{
			Background bg = allBackgrounds[GetIDNotActive()];
			int nextId = (frontId+activeBg.Length-1) % activeBg.Length;
			bg.transform.localPosition = activeBg[nextId].transform.localPosition + activeBg[nextId].length*Vector3.forward;
				//Vector3.forward*bg.length*(activeBg.Length-1);
			bg.gameObject.SetActive(true);
			
			return bg;
		}
	}

}
