
#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class GrabberDebugger : MonoBehaviour
{
	public Grabber grabber;
	
	public string prefix = "";
	public bool AngularVelocitySqr;
	
	void Reset()
	{
		grabber = this.GetComponent<Grabber>();
		prefix = this.gameObject.name;
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if(AngularVelocitySqr && grabber != null)
		    VRUtils.Instance.Log(prefix + " AngularVelocitySqr: " + grabber.velocityTracker.GetAngularVelocity().sqrMagnitude);
    }
}
#endif