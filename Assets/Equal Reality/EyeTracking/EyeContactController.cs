using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools;

namespace EqualReality.EyeTracking
{
	[RequireComponent(typeof(CopyTransform))]
	public class EyeContactController : MonoBehaviour
	{
		public Transform leftEye, rightEye;
		public float minTime = 1f, maxTime = 10f;
		float time;
		CopyTransform copyTransform;
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    time = Random.Range(minTime,maxTime);
		    copyTransform = this.GetComponent<CopyTransform>();
		    copyTransform.target = leftEye;
		    
	    }
	
	    // Update is called once per frame
	    void Update()
		{
			time -= Time.deltaTime;
			
			if(time < 0)
			{
				//reset timer
				time = Random.Range(minTime,maxTime);
				
				if(copyTransform.target == leftEye)
					copyTransform.target = rightEye;
				else
					copyTransform.target = leftEye;
			}
			
	    	
	    }
	}
}
