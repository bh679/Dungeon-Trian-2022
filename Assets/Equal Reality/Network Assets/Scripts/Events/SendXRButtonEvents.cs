using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace BrennanHatton.LibraryOfBabel.Networking.Events
{
		
	public class SendXRButtonEvents : MonoBehaviour
	{
		public enum ButtonType
		{
			GripLeft = 0,
			TriggerLeft = 1,
			GripRight = 2,
			TriggerRight = 3,
			GripBoth = 4,
			TriggerBoth = 5,
			A = 6,
			B = 7,
			X = 8,
			Y = 9,
			LeftJoyStick = 10,
			RightJoyStick = 11,
			HomeLeft = 12,
			HomeRight = 13,
			HomeBoth = 12
			
		}
		
		public InputActionReference a,b,x,y,leftTrigger,rightTrigger,leftGrip,rightGrip,homeLeft,homeRight,leftJoy,rightJoy;
		
		private void OnEnable() {
	    	
			a.action.performed += PressA;
			b.action.performed += PressB;
			x.action.performed += PressX;
			y.action.performed += PressY;
			leftTrigger.action.performed += PressTriggerLeft;
			rightTrigger.action.performed += PressTriggerRight;
			leftGrip.action.performed += PressGripLeft;
			rightGrip.action.performed += PressGripRight;
			leftJoy.action.performed += PressJoyLeft;
			rightJoy.action.performed += PressJoyRight;
			homeLeft.action.performed += PressHomeLeft;
			homeRight.action.performed += PressHomeRight;
				
		}
		
		public void PressA(InputAction.CallbackContext context) {
			SendEventManager.SendControllerButtonPress((int)ButtonType.A);
		}
		
		public void PressB(InputAction.CallbackContext context) {
			SendEventManager.SendControllerButtonPress((int)ButtonType.B);
		}
		
		public void PressX(InputAction.CallbackContext context) {
			SendEventManager.SendControllerButtonPress((int)ButtonType.X);
		}
		
		public void PressY(InputAction.CallbackContext context) {
			SendEventManager.SendControllerButtonPress((int)ButtonType.Y);
		}
		
		public void PressTriggerLeft(InputAction.CallbackContext context) {
			SendEventManager.SendControllerButtonPress((int)ButtonType.TriggerLeft);
			SendEventManager.SendControllerButtonPress((int)ButtonType.TriggerBoth);
		}
		
		public void PressTriggerRight(InputAction.CallbackContext context) {
			SendEventManager.SendControllerButtonPress((int)ButtonType.TriggerBoth);
			SendEventManager.SendControllerButtonPress((int)ButtonType.TriggerRight);
		}
		
		public void PressGripLeft(InputAction.CallbackContext context) {
			SendEventManager.SendControllerButtonPress((int)ButtonType.GripLeft);
			SendEventManager.SendControllerButtonPress((int)ButtonType.GripBoth);
		}
		
		public void PressGripRight(InputAction.CallbackContext context) {
			SendEventManager.SendControllerButtonPress((int)ButtonType.GripRight);
			SendEventManager.SendControllerButtonPress((int)ButtonType.GripBoth);
		}
		
		public void PressJoyLeft(InputAction.CallbackContext context) {
			SendEventManager.SendControllerButtonPress((int)ButtonType.LeftJoyStick);
		}
		
		public void PressJoyRight(InputAction.CallbackContext context) {
			SendEventManager.SendControllerButtonPress((int)ButtonType.RightJoyStick);
		}
		
		public void PressHomeLeft(InputAction.CallbackContext context) {
			SendEventManager.SendControllerButtonPress((int)ButtonType.HomeLeft);
			SendEventManager.SendControllerButtonPress((int)ButtonType.HomeBoth);
		}
		
		public void PressHomeRight(InputAction.CallbackContext context) {
			SendEventManager.SendControllerButtonPress((int)ButtonType.HomeRight);
			SendEventManager.SendControllerButtonPress((int)ButtonType.HomeBoth);
		}
	
		private void OnDisable() {
	    	
			a.action.performed -= PressA;
			b.action.performed -= PressB;
			x.action.performed -= PressX;
			y.action.performed -= PressY;
			leftTrigger.action.performed -= PressTriggerLeft;
			rightTrigger.action.performed -= PressTriggerRight;
			leftGrip.action.performed -= PressGripLeft;
			rightGrip.action.performed -= PressGripRight;
			leftJoy.action.performed -= PressJoyLeft;
			rightJoy.action.performed -= PressJoyRight;
			homeLeft.action.performed -= PressHomeLeft;
			homeRight.action.performed -= PressHomeRight;
				
		}
		
	}
}