using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EqualReality.Networking.Events
{

	public class SendChangeDataReviewEvent : MonoBehaviour
	{
		public void ChangeDataReviewEventCodePlz(int id)
		{
			SendEventManager.SendChangeDataReviewEvent(id);
		}
	}
}