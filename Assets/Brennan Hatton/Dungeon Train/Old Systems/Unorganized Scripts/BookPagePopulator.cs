//Extention by Brennan Hatton, writen for DungeonTrain.com. 20210324
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if BNG
using BNG;
#endif
using BrennanHatton.Props;
using BrennanHatton.Props.Story;
using BrennanHatton.Positions.Extentions;

namespace BrennanHatton.Scanner
{
		
	class LetterContent
	{
		public int idOffset;
		public string[] pagesWritenContent;
		public int nextPageId;
			
		public LetterContent(int _idOffset, string[] _pagesWritenContent, int _nextPageId, string segwayPhrase)
		{
			idOffset = _idOffset;
			nextPageId = Mathf.Max(_nextPageId - 2,0);
				
				
			pagesWritenContent = new string[_pagesWritenContent.Length + 1];
				
			for(int i= 0; i < pagesWritenContent.Length; i++)
			{
				Debug.Log(i + "<=>?" + nextPageId);
				if(i < nextPageId)
				{
					pagesWritenContent[i] = _pagesWritenContent[i];
				}else if (i == nextPageId)
				{
					pagesWritenContent[i] = segwayPhrase;
				}/*else if(nextPageId < 0)
				{
					if(i < _pagesWritenContent.Length)
						pagesWritenContent[i] = _pagesWritenContent[i];
					else
						pagesWritenContent[i] = "";
				}*/else 
				{
					
					if(i-1 < _pagesWritenContent.Length)
						pagesWritenContent[i] = _pagesWritenContent[i-1];
					else
						pagesWritenContent[i] = "";
				}
				Debug.Log(pagesWritenContent[i]);
			}
				
			Debug.Log("second last page: " + pagesWritenContent[pagesWritenContent.Length-2]);
			Debug.Log("Last page: " + pagesWritenContent[pagesWritenContent.Length-1]);
				
		}
	}
	
	public class BookPagePopulator : MonoBehaviour
	{
		[System.Serializable]
		public class LetterPage
		{
			public Paper paper;
#if BNG
			public GrabbableUnityEvents paperEvents;
			public Grabbable paperGrabbable;
			public bool originalRemoteSettings;
#endif
			public Collider collider;
			
			public void Shred()
			{
				paper.Shred();
				
#if BNG
				if(paperEvents != null)
				Destroy(paperEvents);
#endif
			}
		}
		
		public ScanDataDisplay dataManager;
		public PropPlacer paperPlacer;
		//public Prop myProp;
		bool check = false;
		bool stop = false;
		public float letterHeight = 0.0015f;
		public Transform paperStack;
		public float paperStackObjectheight = 0.002969325f;
		
		public SharedLines segwayPhrase;
		
		bool createdOnce = false;
		string[] pagesWritenContent;
		
		LetterPage[] pagesPlacedInScene;
		int nextPageId = 0;
		
		
		void Reset()
		{
			dataManager = transform.root.GetChild(0).gameObject.GetComponentInChildren<ScanDataDisplay>();
			
			//myProp = this.transform.parent.gameObject.GetComponent<Prop>();
		}
		
		// Start is called before the first frame update
		void Start()
		{
			//myProp.OnPlace.AddListener(OnPlace);
			
			OnPlace();//becuase the above call is missed due to it already being placed.
		}
		
		void OnPlace()
		{
			//get all the letter pages
			pagesWritenContent = dataManager.GetLetter();
			
			//reset old paper still laying around in children
			Prop[] oldPapers = this.GetComponentsInChildren<Paper>();
			for(int i = 0 ;i < oldPapers.Length; i++)
			{
				oldPapers[i].Remove();
			}
			
			//remove all old paper
			paperPlacer.ReturnProps();
			
			//setup stack
			//((PositionMoveWhenPlaced)paperPlacer.positionGroup.positions[0]).Reposition(Vector3.up*letterHeight*pagesWritenContent.Length);
			
			paperStack.gameObject.SetActive(true);
			//paperStack.localScale = new Vector3(1,pagesWritenContent.Length*letterHeight/paperStackObjectheight,1);
			
			pagesPlacedInScene = new LetterPage[2];
			
			nextPageId = 0;
			
			//place a paper
			PlacePaper();
			PlacePaper();
		}
		
		LetterContent pausedStory = null;
		
		public void InterruptLetterWihtNew(string[] newLetter)
		{
			//if there is already a story waiting
			if(pausedStory != null)
				//for now we will just stick to the first saved story. 
				return;
			
			//put current story on pause
			//this needs to be developed
			pausedStory = new LetterContent(idOffset, pagesWritenContent, nextPageId, segwayPhrase.GetSharedLine());
			
			//increase offset
			idOffset = id;//nextPageId % pagesPlacedInScene.Length;
			//get new letter
			pagesWritenContent = dataManager.GetLetter();
			//reset page id
			nextPageId = 0;
			
			
			
			//set new story
			pagesWritenContent = newLetter;
			
			//reset page id
			nextPageId = 0;
			
			
		}
		
		
		//do this next
		//int pageId;
		int id = 0;
		int idOffset = 0;
		public void PlacePaper()
		{
			/////////
			//Prep next paper to be interactive
			/////////
			
			//turn on grabbale of existing paper
			if(pagesPlacedInScene[id] != null){
				
				//make this page grabbale
#if BNG
				pagesPlacedInScene[id].paperGrabbable.enabled = true;
				pagesPlacedInScene[id].originalRemoteSettings = pagesPlacedInScene[id].paperGrabbable.RemoteGrabbable;
				pagesPlacedInScene[id].paperGrabbable.RemoteGrabbable = false;
#endif
				pagesPlacedInScene[id].collider.enabled = true;
				
				//make this page the top page
				pagesPlacedInScene[id].paper.transform.localPosition += Vector3.up * 0.001f;
				
				//update datamanager with text
				dataManager.textMeshDisplay = pagesPlacedInScene[id].paper.textMesh;
				
				//move to top of stack
				pagesPlacedInScene[id].paperGrabbable.transform.localPosition += Vector3.up*letterHeight;
			}
			
			
			/////////
			//Place behind paper, so visible when front is picked up
			/////////
			
			//////
			//Setting up data (id, and getting content
			
			//move to next paper being placed
			id = (nextPageId + idOffset) % pagesPlacedInScene.Length;
			
			//If this already exists? 
			if(pagesPlacedInScene[(id) % pagesPlacedInScene.Length] != null)
			{
				pagesPlacedInScene[id].paperGrabbable.RemoteGrabbable = pagesPlacedInScene[id].originalRemoteSettings;
				
				//shred it
				pagesPlacedInScene[(id) % pagesPlacedInScene.Length].Shred();
				pagesPlacedInScene[(id) % pagesPlacedInScene.Length].paperEvents.onRelease.RemoveListener(PlacePaper);
			}
			else
				Debug.Log("Nothing stored to shred " + pagesPlacedInScene);
			
			//if the id is to big
			if(nextPageId >= pagesWritenContent.Length)
			{
				if(pausedStory != null)
				{
					idOffset = pausedStory.idOffset;
					pagesWritenContent = pausedStory.pagesWritenContent;
					nextPageId = pausedStory.nextPageId;
					pausedStory = null;
				}
				else
				{
					//increase offset
					idOffset = id;//nextPageId % pagesPlacedInScene.Length;
					//get new letter
					pagesWritenContent = dataManager.GetLetter();
					//reset page id
					nextPageId = 0;
				}
			}
			
			//////
			//creating new paper game object from placer system
			
			//create a new paper
			Prop[] newPage;
			paperPlacer.range = new Vector2(1,1);
			newPage = paperPlacer.Place(true);
			
			//get data from letter
			pagesPlacedInScene[id] = new LetterPage();
			pagesPlacedInScene[id].paper = (Paper)newPage[0];
				
			//write text on paper
			pagesPlacedInScene[id].paper.SetText(pagesWritenContent[nextPageId]);
			
			//update datamanager with text
			dataManager.textMeshDisplay2 = pagesPlacedInScene[id].paper.textMesh;
				
			//////
			//setup grabbability of new paper created
			
			//Check if GrabbableUnityEvents already exist on object
			GrabbableUnityEvents existingGrabEvents = pagesPlacedInScene[id].paper.gameObject.GetComponent<GrabbableUnityEvents>();
			if(existingGrabEvents != null)
				//just use that!
				pagesPlacedInScene[id].paperEvents = existingGrabEvents;
				
#if BNG
			//add events
			else
				pagesPlacedInScene[id].paperEvents = pagesPlacedInScene[id].paper.gameObject.AddComponent<GrabbableUnityEvents>();
				
			pagesPlacedInScene[id].paperEvents.onRelease = new UnityEngine.Events.UnityEvent();
			pagesPlacedInScene[id].paperEvents.onRelease.AddListener(PlacePaper);
				
			pagesPlacedInScene[id].paperGrabbable = pagesPlacedInScene[id].paperEvents.gameObject.GetComponent<Grabbable>();
			pagesPlacedInScene[id].collider = pagesPlacedInScene[id].paperEvents.gameObject.GetComponent<Collider>();
			pagesPlacedInScene[id].collider.enabled = false;
			
			pagesPlacedInScene[id].paperGrabbable.enabled = false;
#endif
			
			nextPageId++;
			
		}
		
		/*	void OnDisable()
		{
#if BNG
			if(pagesPlacedInScene != null)
			{
				for(int e = 0; e < pagesPlacedInScene.Length; e++)
				{
					if(pagesPlacedInScene[e].paperEvents != null)
			
						pagesPlacedInScene[id].paperEvents.onRelease.RemoveListener(PlacePaper);
						//Destroy(pagesPlacedInScene[e].paperEvents);
				}
			}
#endif
			paperPlacer.ReturnProps();
		}*/
		
	
	}
}