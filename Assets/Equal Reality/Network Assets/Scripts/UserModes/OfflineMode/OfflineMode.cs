using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EqualReality.UserModes.Offline
{

	public class OfflineMode : MonoBehaviour
	{
		public static bool isEnabled = false;
		
		public static void Toggle()
		{
			isEnabled = !isEnabled;
		}
		
		public static void SetActive(bool isActive)
		{
			isEnabled = isActive;
		}
	}
	
}