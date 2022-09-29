using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BrennanHatton.TrainCarts
{
	public class InputToSeed : MonoBehaviour
	{
		public TMP_InputField input;
		public TrainCartManager manager;
		
		public void Go()
		{
			manager.TeleportToCart(input.text);
		}
	}
}