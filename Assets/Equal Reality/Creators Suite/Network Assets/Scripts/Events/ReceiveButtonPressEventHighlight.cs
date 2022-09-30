using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using EqualReality.UserModes;

namespace EqualReality.Networking.Events
{
	
	public class ReceiveButtonPressEventHighlight : MonoBehaviour, IOnEventCallback
	{
		public SendNetworkedButtonEvent senderButton;
		public Image image;
		public bool toggle = true;
		public bool Down { get{return down;} } 
		bool down = false;
		public UnityEvent onNetworkedPressed;
		public bool onlyFromFacilitator = true;
	    
		
		void Reset()
		{
			senderButton = this.GetComponent<SendNetworkedButtonEvent>();
			
			if(transform.GetChild(0) != null)
				image = transform.GetChild(0).GetComponent<Image>();
		}
		
		private void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
		}
	
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}
		
		public void OnEvent(EventData photonEvent)
		{
			byte eventCode = photonEvent.Code;
			if (eventCode == SendEventManager.NetworkedButtonCode)
			{
				object[] data = (object[])photonEvent.CustomData;
					
				int senderId = (int)data[0];
				int id = (int)data[1];
				
				//check which button sent the event
				if(id == senderButton.id)
				{
					
					int sceneId = (int)data[2];
					
					//if you got a message from another scene, exit
					if(sceneId != SceneManager.GetActiveScene().buildIndex)// why is this needed?
						return;
					
					//Are you the facilitator?
					if(Facilitator.mode || onlyFromFacilitator == false)
					{
						//Do the highlighting
						HighlightingOnNetworkedButtonEvent((SendNetworkedButtonEvent.ButtonEvents)((int)data[3]));
					}
					//NO? youre not the facilitator
					//Do a counter / Graph
				}
				
			}
		}
		
		//this is the reciever function
		void HighlightingOnNetworkedButtonEvent(SendNetworkedButtonEvent.ButtonEvents buttonEvent)
		{
			switch(buttonEvent)
			{
			case SendNetworkedButtonEvent.ButtonEvents.Pressed:
			
				if(toggle)
				{
					down = !down;
				
					image.enabled = down;
				}
				
				onNetworkedPressed.Invoke();
			
				break;
			}
		}
	}
}
