
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if BNG
using BNG;
#endif
using BrennanHatton.Positions.Extentions;

namespace BrennanHatton.Props.Story
{
	public class StoryPopulator : MonoBehaviour
	{
		public class LetterPage
		{
			public Paper paper;
			#if BNG
			public GrabbableUnityEvents paperEvents;
			public Grabbable paperGrabbable;
			
			public void Shred()
			{
				paper.Shred();
				
				if(paperEvents != null)
					Destroy(paperEvents);
			}
			#endif
		}
		
		public StoryManager storyManager;
		public PropPlacer paperPlacer;
		public Prop myProp;
		bool check = false;
		bool stop = false;
		public float letterHeight = 0.0015f;
		public Transform paperStack;
		public float paperStackObjectheight = 0.002969325f;
		public int storySeed = 0;
		
		bool createdOnce = false;
		string[] pagesWritenContent;
		
		LetterPage[] pagesPlacedInScene;
		int nextPageId = 0;
		
		
		void Reset()
		{
			storyManager = transform.root.GetChild(0).gameObject.GetComponentInChildren<StoryManager>();
			
			myProp = this.transform.parent.gameObject.GetComponent<Prop>();
		}
		
#if BNG
		// Start is called before the first frame update
		void Start()
		{
			myProp.OnPlace.AddListener(OnPlace);
			
			OnPlace();//becuase the above call is missed due to it already being placed.
		}
		
		void OnPlace()
		{
			
			//get all the letter pages
			pagesWritenContent = storyManager.GetLetter();
			storySeed = storyManager.storyTimeSeed;
			
			//reset old paper still laying around in children
			Prop[] oldPapers = this.GetComponentsInChildren<Paper>();
			for(int i = 0 ;i < oldPapers.Length; i++)
			{
				oldPapers[i].Remove();
			}
			
			//remove all old paper
			paperPlacer.ReturnProps();
			
			//setup stack
			((PositionMoveWhenPlaced)paperPlacer.positionGroup.positions[0]).Reposition(Vector3.up*letterHeight*pagesWritenContent.Length);
			
			paperStack.gameObject.SetActive(true);
			paperStack.localScale = new Vector3(1,pagesWritenContent.Length*letterHeight/paperStackObjectheight,1);
			
			pagesPlacedInScene = new LetterPage[2];
			
			nextPageId = 0;
			
			//place a paper
			PlacePaper();
			PlacePaper();
		}
		
		
		//do this next
		//int pageId;
		int id = 0;
		public void PlacePaper()
		{
			if(pagesPlacedInScene[id] != null)
				pagesPlacedInScene[id].paperGrabbable.enabled = true;
			
			id = (nextPageId) % pagesPlacedInScene.Length;
			
			//do I shred the last one?
			if(pagesPlacedInScene[(id) % pagesPlacedInScene.Length] != null)
				pagesPlacedInScene[(id) % pagesPlacedInScene.Length].Shred();
			if(nextPageId >= pagesWritenContent.Length)
			{
				
				nextPageId++;
				return;
			}
			//create a new paper
			Prop[] newPage;
			paperPlacer.range = new Vector2(1,1);
			newPage = paperPlacer.Place(true);
			
			pagesPlacedInScene[id] = new LetterPage();
			pagesPlacedInScene[id].paper = (Paper)newPage[0];
				
			pagesPlacedInScene[id].paper.SetText(pagesWritenContent[nextPageId]);
				
			if(pagesPlacedInScene[id].paperEvents == null)
			{
				pagesPlacedInScene[id].paperEvents = pagesPlacedInScene[id].paper.gameObject.GetComponent<GrabbableUnityEvents>();
				if(pagesPlacedInScene[id].paperEvents == null)
					pagesPlacedInScene[id].paperEvents = pagesPlacedInScene[id].paper.gameObject.AddComponent<GrabbableUnityEvents>();
			}
			
			pagesPlacedInScene[id].paperEvents.onRelease = new UnityEngine.Events.UnityEvent();
			pagesPlacedInScene[id].paperEvents.onRelease.AddListener(PlacePaper);
			
				
			pagesPlacedInScene[id].paperGrabbable = pagesPlacedInScene[id].paperEvents.gameObject.GetComponent<Grabbable>();
			pagesPlacedInScene[id].paperGrabbable.enabled = false;
			
			paperStack.localScale = new Vector3(1,(pagesWritenContent.Length-nextPageId)*letterHeight/paperStackObjectheight,1);
			
			nextPageId++;
			
			if(nextPageId >= pagesWritenContent.Length)
				paperStack.gameObject.SetActive(false);
			
		}
		
		
		/*public void ShredPrevious(){
			
			if(nextGrabbable == 1)
				return;
				
			((Paper)letterPage[letterPage.Length-nextGrabbable+1].paper).Shred();
		}*/
		
		/*public void NextIsGrabbable(){
			//Debug.Log("NextIsGrabbale " + nextGrabbable);
			
			if(nextGrabbable > 0)
			((Paper)letterPage[letterPage.Length-nextGrabbable].paper).Shred();
			
			if(nextGrabbable >= letterPage.Length)
				return;
				
			
			nextGrabbable++;
			
			letterPage[letterPage.Length-nextGrabbable].paperGrabbable.enabled = true;
			
			
		}*/
		
		void OnDisable()
		{
			if(pagesPlacedInScene != null)
			{
				for(int e = 0; e < pagesPlacedInScene.Length; e++)
				{
					if(pagesPlacedInScene[e].paperEvents != null)
						Destroy(pagesPlacedInScene[e].paperEvents);
				}
			}
			//paperPlacer.ReturnProps();
		}
		
	
#endif
	}
}