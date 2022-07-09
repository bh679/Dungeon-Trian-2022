using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.DungeonTrain.Rooms
{
	public class SetActiveOnRoomWidth : MonoBehaviour
	{
		public RoomCreator roomCreator;
		
		public GameObject[] objectsToActivate = new GameObject[0];
		
		public bool runOnEnbale = true;
		
		public int minWidth, maxWidth;
		
		void Reset()
		{
			roomCreator = FindObjectOfType<RoomCreator>();
		}
		
		
		void OnEnable()
		{
			//if not running automatically
			if(!runOnEnbale)
				return; // exit
				
			
			SetActiveObjectsBasedOnWidth();
			
		}
		
		public void SetActiveObjectsBasedOnWidth()
		{
			//get width of train
			int width = roomCreator.TrainWidth();

			//calculate out if acctive
			bool isActive = width >= minWidth && width <= maxWidth;
			
			//set active based on width
			SetObjectsActive(isActive);
		}
		
		//sets all obejcts to be active or inactive
		void SetObjectsActive(bool isActive)
		{
			//for all objects
			for(int i = 0; i < objectsToActivate.Length; i++)
			{
				//set active or inactive
				objectsToActivate[i].SetActive(isActive);
			}
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	}
}