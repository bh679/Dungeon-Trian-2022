using System;
 using UnityEngine;
 
namespace BrennanHatton
{
 
	public class MouseLook : MonoBehaviour
	{
 		public Transform target;
	 	
		public float mouseSensitivity = 100.0f;
		public float clampAngle = 80.0f;
		
		private float rotY = 0.0f; // rotation around the up/y axis
		private float rotX = 0.0f; // rotation around the right/x axis
		
		public bool x = true, y = true;
		
		void Start ()
		{
			Vector3 rot = target.localRotation.eulerAngles;
			rotY = rot.y;
			rotX = rot.x;
		}
		
		void Update ()
		{
			
			if(y)
			{
				float mouseX = Input.GetAxis("Mouse X");
				rotY += mouseX * mouseSensitivity * Time.deltaTime;
			}
			
			if(x)
			{
				float mouseY = -Input.GetAxis("Mouse Y");
				rotX += mouseY * mouseSensitivity * Time.deltaTime;
				rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
			}
			
			Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
			target.rotation = localRotation;
		}
	}
}