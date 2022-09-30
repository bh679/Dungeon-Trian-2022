using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EqualReality.UserModes
{

	public class Facilitator : MonoBehaviour
	{
		public static bool mode = false;
		
		
		public void SetFacilitatorFromToggle(Toggle toggle)
		{
			mode = toggle.isOn;
		}		
		
		public void SetToggleFromFacilitator(Toggle toggle)
		{
			toggle.isOn = mode;
		}
		
		public bool SwitchMode()
		{
			mode = !mode;
			
			return mode;
		}
	
		public void SetMode(bool newModeStatus)
		{
			mode = newModeStatus;
		}
	}

}