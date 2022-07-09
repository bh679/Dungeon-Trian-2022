using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if BNG
using BNG;
#endif

public class MainHandAssigner : MonoBehaviour
{
	public Transform LeftHand, RightHand;
	public CopyTransform mainHandTracker, otherHandTracker;
	
	public Transform LeftWrist, RightWrist;
	public CopyTransform mainWristTracker, otherWristTracker;
	
	public Transform LeftElbow, RightElbow;
	public CopyTransform mainElbowTracker, otherElbowTracker;
	
	#if BNG
	public PlayerTeleport teleportController;
	#endif
	//public HandedOVRPlayerController smoothLocomotionController;
	
	public UnityEvent onSwitchImediate, onSwitchNextFrame;
	
	public enum MainHand{
		Left = 1,Right = 0
	}
	
	public MainHand mainHand = MainHand.Left;
	
	public void SetMainHandLeft()
	{
		mainHand = MainHand.Left;
		
		mainHandTracker.target = LeftHand;
		otherHandTracker.target = RightHand;
		
		mainWristTracker.target = LeftWrist;
		otherWristTracker.target = RightWrist;
		
		mainElbowTracker.target = LeftElbow;
		otherElbowTracker.target = RightElbow;
		
		//teleportController.hand = ControllerHand.Left;
		//smoothLocomotionController.handedness = HandedOVRPlayerController.Handedness.LeftHanded;
		
		mainHandTracker.transform.localScale = new Vector3(1,1,1);
		
		otherHandTracker.transform.localScale = new Vector3(1,1,1);
		
		
		onSwitchImediate.Invoke();
		StartCoroutine(invokeNextFrame());
	}
	
	public void SetMainHandRight()
	{
		mainHand = MainHand.Right;
		mainHandTracker.target = RightHand;
		otherHandTracker.target = LeftHand;
		
		mainWristTracker.target = RightWrist;
		otherWristTracker.target = LeftWrist;
		
		mainElbowTracker.target = RightElbow;
		otherElbowTracker.target = LeftElbow;
		
		//teleportController.hand = ControllerHand.Right;
		//smoothLocomotionController.handedness = HandedOVRPlayerController.Handedness.RightHanded;
		
		mainHandTracker.transform.localScale = new Vector3(-1,1,1);
		
		otherHandTracker.transform.localScale = new Vector3(-1,1,1);
		
		
		onSwitchImediate.Invoke();
		StartCoroutine(invokeNextFrame());
	}
	
	IEnumerator invokeNextFrame()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		
		onSwitchNextFrame.Invoke();
		
	}
}