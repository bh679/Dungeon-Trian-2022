using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EqualReality.Networking.Events
{
	
	public class SendNetworkedButtonEvent : MonoBehaviour
	{
		public int id;
		public ReceiveButtonPressEventHighlight reciever;
		
		public enum ButtonEvents
		{
			Hover = 0,
			Pressed = 1,
			Reset = 2
		}
		
		public void PressedIfUp()
		{
			if(reciever.Down)
				return;
				
			ButtonPressed(ButtonEvents.Pressed);
		}
	
		public void Pressed()
		{
			ButtonPressed(ButtonEvents.Pressed);
		}
	    
		//let everyone know this button has been pressed
		void ButtonPressed(ButtonEvents buttonEvents)
		{
			SendEventManager.SendNetworkedButton(
				id, 
				SceneManager.GetActiveScene().buildIndex,
				(int)buttonEvents
			);
		}
		
		
	}

}