using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDoorWall : MonoBehaviour
{
	
	public GameObject WindowShutter, Lock;
	
	public Door door;
	
	public void LockDoor()
	{
		
	}
	
	public void OpenDoor()
	{
		door.Open();
	}
	
	public void CloseDoor()
	{
		door.Close();
	}
	
	public void OpenWindow()
	{
		
	}
	
	void Start()
	{
		CloseDoor();
		//CloseWindow();
	}
}
