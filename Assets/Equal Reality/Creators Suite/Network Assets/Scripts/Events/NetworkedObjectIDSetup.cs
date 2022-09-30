using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EqualReality.Networking.Events
{
	
	public class NetworkedObjectIDSetup : MonoBehaviour
	{
		public bool onStart= false;
		
		void Reset()
		{
			ResetAllButtons();
		}
		
		void ResetAllButtons()
		{
			SendNetworkedButtonEvent[] buttons = GameObject.FindObjectsOfType<SendNetworkedButtonEvent>(true);
			
			for(int i = 0; i < buttons.Length; i++)
			{	
				
#if UNITY_EDITOR		
				// We need to tell Unity we're changing the component object too.
				Undo.RecordObject(buttons[i], "Resetting object id to " + i);
#endif
				
				buttons[i].id = i;
			}
		}
		
		void Start()
		{
			if(onStart)
				ResetAllButtons();
		}
	}
}