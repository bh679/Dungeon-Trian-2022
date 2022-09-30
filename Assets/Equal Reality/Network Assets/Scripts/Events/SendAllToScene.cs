using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EqualReality.UserModes;

namespace EqualReality.Networking.Events
{
	
	public class SendAllToScene : MonoBehaviour
	{
		public static void SendAllToScenePlz(int ERSceneId)
		{
			SendEventManager.SendChangeScene(SceneManager.GetActiveScene().buildIndex, ERSceneId, 0, Facilitator.mode);
		}
		
		public static void SendAllToAppScenePlz(int sceneBuildIndex)
		{
			SendEventManager.SendChangeScene(sceneBuildIndex, 0, 0, Facilitator.mode);
		}
	}

}