using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EqualReality.Networking
{
		
	public class NetworkManagerPasswordToggle : MonoBehaviour
	{
		//move this to networkmanager interface class
		public static void SetPrivateModeFromToggle(Toggle toggle)
		{
			NetworkManager.usePassword = toggle.isOn;
		}		
		
		//move this to networkmanager interface class
		public static void SetToggleFromPrivateMode(Toggle toggle)
		{
			toggle.isOn = NetworkManager.usePassword;
		}
	}
}