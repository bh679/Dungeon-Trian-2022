using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if BNG
using BNG;
using UnityEngine.Events;

public class ControllerInputUnityEvents : MonoBehaviour
{
	public InputBridge input;
	
	public UnityEvent AnyButtonDown, GripDown, ThumbStickDown, TriggerDown, AXButtonDown, YBButtonDown;
	
	void Reset()
	{
		input = FindObjectOfType<InputBridge>();
	}

    // Update is called once per frame
    void Update()
	{
		if(input.RightGripDown || input.LeftGripDown)
		{
			AnyButtonDown.Invoke();
			GripDown.Invoke();
		}
			
		if(input.RightThumbstickDown || input.LeftThumbstickDown)
		{
			AnyButtonDown.Invoke();
			ThumbStickDown.Invoke();
		}
		
		if(input.RightTriggerDown || input.LeftTriggerDown)
		{
			AnyButtonDown.Invoke();
			TriggerDown.Invoke();
		}
		
		if(input.AButtonDown || input.XButtonDown)
		{
			AnyButtonDown.Invoke();
			AXButtonDown.Invoke();
		}
		
		if(input.YButtonDown || input.BButtonDown)
		{
			AnyButtonDown.Invoke();
			YBButtonDown.Invoke();
		}
	    
    }
}
#endif