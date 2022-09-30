using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EqualReality.EyeTracking
{
	
	public class EyeTarget : MonoBehaviour
	{
		//public float FOV = 45;
		
	    // Start is called before the first frame update
	    void Start()
		{
			NearestEyeTarget[] eyesFacers = FindObjectsOfType<NearestEyeTarget>();
			
			for(int i = 0; i < eyesFacers.Length; i++)
			{
				eyesFacers[i].AddEyeTarget(this);
			}
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	}
}