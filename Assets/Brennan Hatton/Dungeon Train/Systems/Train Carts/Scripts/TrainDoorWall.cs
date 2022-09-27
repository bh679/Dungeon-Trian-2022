using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.TrainCarts
{

	public class TrainDoorWall : MonoBehaviour
	{
		
		public GameObject WindowShutter, Lock, Door;
		
		//public Door door;
		
		public void LockDoor()
		{
			
		}
		
		public void OpenDoor()
		{
			Door.gameObject.SetActive(false);
			//	door.Open();
		}
		
		public void CloseDoor()
		{
			Door.gameObject.SetActive(true);
			//	door.Close();
		}
		
		public void OpenWindow()
		{
			
		}
		
		void Start()
		{
			//CloseDoor();
			//CloseWindow();
		}
	}

}