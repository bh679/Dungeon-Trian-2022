using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceTowardsTargetScaled : MonoBehaviour
{
	public Rigidbody myRb;
	public Transform target;
	
	public float force = 2f;
	public float minDistance = 0.25f, maxDistance = 1f;
	public bool closerIsMore = false;
	
	void Reset()
	{
		myRb = this.GetComponent<Rigidbody>();
	}

	float distance;
    // Update is called once per frame
    void Update()
	{
		distance = Vector3.Distance(target.position, this.transform.position);
		
		if(distance < maxDistance)
	    {
	    	Quaternion originalAngle = target.rotation;
	    	
	    	target.LookAt(this.transform);
	    	
			float scale = Mathf.Max(0,(distance-minDistance) / maxDistance);
	    	
	    	if(!closerIsMore)
		    	scale = 1-scale;
	    	
	    	myRb.velocity = -target.forward*force*scale;
	    	
	    	target.rotation = originalAngle;
	    	
	    }
    }
}
