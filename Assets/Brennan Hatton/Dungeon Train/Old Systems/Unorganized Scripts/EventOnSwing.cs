using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if BNG
using BNG;

public class EventOnSwing : MonoBehaviour
{
	public Rigidbody rigibody;
	public Grabbable grabbable;
	public Transform phsyicsBody;
		
	public float minSwingSqrMag = 7;
	public float degreesNeeded = 45;
	
	public UnityEvent onSwingStart = new UnityEvent();
	public UnityEvent onSwinging = new UnityEvent();
	public UnityEvent onSwingEnd = new UnityEvent();
		
	void Reset()
	{
		rigibody = this.GetComponent<Rigidbody>();
			
		if(rigibody == null)
			rigibody = transform.parent.gameObject.GetComponent<Rigidbody>();
			
		if(rigibody == null)
			rigibody = transform.parent.parent.gameObject.GetComponent<Rigidbody>();
			
		if(grabbable == null)
			grabbable = transform.parent.gameObject.GetComponent<Grabbable>();
			
		if(grabbable == null)
			grabbable = transform.parent.parent.gameObject.GetComponent<Grabbable>();
	
		if(rigibody != null)
			phsyicsBody = rigibody.transform;
			
		if(phsyicsBody == null)
			phsyicsBody = this.transform;
	}
	
	void Start()
	{
		
		if(rigibody == null)
			rigibody = this.GetComponent<Rigidbody>();
			
		if(rigibody == null)
			rigibody = transform.parent.gameObject.GetComponent<Rigidbody>();
			
		if(rigibody == null)
			rigibody = transform.parent.parent.gameObject.GetComponent<Rigidbody>();
			
		if(grabbable == null)
			grabbable = transform.parent.gameObject.GetComponent<Grabbable>();
			
		if(grabbable == null)
			grabbable = transform.parent.parent.gameObject.GetComponent<Grabbable>();
	
		if(phsyicsBody == null && rigibody != null)
			phsyicsBody = rigibody.transform;
			
		if(phsyicsBody == null)
			phsyicsBody = this.transform;
	}
		
	public float GetAngularVelocitySqrMagnitude()
	{
		if (grabbable != null && grabbable.BeingHeld)
			return grabbable.GetPrimaryGrabber().GetGrabberAveragedAngularVelocity().sqrMagnitude;
				
		if(rigibody != null)
			return rigibody.angularVelocity.sqrMagnitude;
				
		return 0;
		  
	}
    
	bool hasSwingStarted = false;
	Quaternion startSwingAngle;
	
	bool hasCalledOnSwingStart = false;
	

    // Update is called once per frame
    void Update()
	{
		if(IsSwinging())
		{
			if(!hasCalledOnSwingStart)
			{
				hasCalledOnSwingStart = true;
				onSwingStart.Invoke();
			}
			onSwinging.Invoke();
		}else  if(hasCalledOnSwingStart)
		{
			hasCalledOnSwingStart = false;
			onSwingEnd.Invoke();
		}
    }
    
	public bool IsSwinging()
	{
		//is it going fast enough?
		if(GetAngularVelocitySqrMagnitude() < minSwingSqrMag)
		{
			//this is no swing
			hasSwingStarted = false;
			
			return false;
		}
		
		//is this first frame of a new swing?
		if(hasSwingStarted == false)
		{
			//skip this next time
			hasSwingStarted = true;
			
			//save start angle
			startSwingAngle = phsyicsBody.rotation;
			
			return false;
		}
		
		
		//distance travelled is 45 deg
		if(Quaternion.Angle(startSwingAngle, phsyicsBody.rotation) < degreesNeeded)
			return false;
		
		return true;
		
	}
}
#endif
