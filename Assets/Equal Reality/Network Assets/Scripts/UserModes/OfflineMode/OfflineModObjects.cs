using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EqualReality.UserModes.Offline
{

	public class OfflineModObjects : MonoBehaviour
	{
		public List<GameObject> gameObjects = new List<GameObject>();
		public List<GameObject> gameObjectsToTurnOff = new List<GameObject>(); 
	
		bool lastMode;
		
		void Start()
		{
			lastMode = OfflineMode.isEnabled;
				
			for(int i = 0; i < gameObjects.Count; i++)
				gameObjects[i].SetActive(OfflineMode.isEnabled);
			
			for(int i = 0; i < gameObjectsToTurnOff.Count; i++)
				gameObjectsToTurnOff[i].SetActive(!OfflineMode.isEnabled);
		}
	
		// Update is called once per frame
		void Update()
		{
			if(lastMode != OfflineMode.isEnabled)
			{
				for(int i = 0; i < gameObjects.Count; i++)
					gameObjects[i].SetActive(OfflineMode.isEnabled);
				
				for(int i = 0; i < gameObjectsToTurnOff.Count; i++)
					gameObjectsToTurnOff[i].SetActive(!OfflineMode.isEnabled);
			}
			
			lastMode = OfflineMode.isEnabled;
		}
	}

}