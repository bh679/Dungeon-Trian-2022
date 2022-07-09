using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if BNG
using BNG;

public class EventOnAngularVelocity : MonoBehaviour
{
	public Rigidbody rb;
	public Grabbable grabbale;
	
	public float triggerValue;
	
	public float resetValue;
	
	public UnityEvent OnValueExceeded = new UnityEvent();
	
	bool running = true;
	
	void Reset()
	{
		rb = this.GetComponent<Rigidbody>();
		grabbale = this.GetComponent<Grabbable>();
	}
	
	void Start()
	{
		if(rb == null)
			rb = this.GetComponent<Rigidbody>();
			
		if(grabbale == null)
		grabbale = this.GetComponent<Grabbable>();
	}

    // Update is called once per frame
    void Update()
	{
		//if(grabbale != null && grabbale.BeingHeld)
			//BNG.VRUtils.Instance.Log(this.gameObject.name + " : " + grabbale.GetPrimaryGrabber().GetGrabberAveragedAngularVelocity());
		
		if(running)
		{
			if(
				(grabbale != null && grabbale.BeingHeld && grabbale.GetPrimaryGrabber().GetGrabberAveragedAngularVelocity().sqrMagnitude <= resetValue)
				|| (rb != null && rb.angularVelocity.sqrMagnitude <= resetValue)
				)
				running = false;
		}
		else
		{
			if(
				(grabbale != null && grabbale.BeingHeld && grabbale.GetPrimaryGrabber().GetGrabberAveragedAngularVelocity().sqrMagnitude  >= triggerValue)
			|| (rb != null && rb.angularVelocity.sqrMagnitude >= triggerValue)
				)
			{
				running = true;
				OnValueExceeded.Invoke();
			}
		}
    }
}
#endif