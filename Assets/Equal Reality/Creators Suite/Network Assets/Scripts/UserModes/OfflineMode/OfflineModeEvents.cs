using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EqualReality.UserModes.Offline
{
	public class OfflineModeEvents : MonoBehaviour
	{
		//	public List<GameObject> gameObjects = new List<GameObject>();
		public UnityEvent onOffline, onOnline;
	
		bool lastMode;
		
		public bool onStart = true;
		public bool update = false;
		
		void Start()
		{
			lastMode = OfflineMode.isEnabled;
			
			if(onStart){
				checkStatus();
			}
			
			/*if(OfflineMode.isEnabled){
				onOffline.Invoke();
			}else
			onOnline.Invoke();*/
		}
		
		public void checkStatus()
		{
			if(OfflineMode.isEnabled){
				onOffline.Invoke();
			}else
				onOnline.Invoke();
		}
	
		// Update is called once per frame
		void Update()
		{
			if(update)
			{
				if(lastMode != OfflineMode.isEnabled){
					onOffline.Invoke();
				}else
					onOnline.Invoke();
				
				lastMode = OfflineMode.isEnabled;
			}
		}
	}
}
