using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools;

namespace EqualReality.EyeTracking
{
	
	[RequireComponent(typeof(CopyTransform))]
	public class NearestEyeTarget : MonoBehaviour
	{
		public Transform myHead;
		public List<EyeTarget> eyeTargets = new List<EyeTarget>();
		CopyTransform copyTransform;
		
		public List<EyeTarget> eyeTargetsToIgnore;
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    EyeTarget[] eyes = FindObjectsOfType<EyeTarget>();
		    
		    
		    for(int i = 0; i < eyes.Length; i++)
		    {
		    	AddEyeTarget(eyes[i]);
		    }
		    
		    
		    copyTransform = this.GetComponent<CopyTransform>();
		    
		    target = GetNearestEyeTarget();
		    
		    if(target != null)
			    copyTransform.target = target.transform;
	    }
	    
		public void AddEyeTarget(EyeTarget eyeToAdd)
		{
			if(eyeTargetsToIgnore.Contains(eyeToAdd) == false)
				eyeTargets.Add(eyeToAdd);
		}
	
		EyeTarget target;
	    // Update is called once per frame
	    void Update()
	    {
		    target = GetNearestEyeTarget();
		    
		    if(target != null)
			    copyTransform.target = target.transform;
	    }
	    
		public EyeTarget GetNearestEyeTarget()
		{
			EyeTarget nearest = null;
			float distanceToNearest = 0, distanceToi = 0;
			for(int i = 0; i < eyeTargets.Count; i++)
			{
				
				while(eyeTargets[i] == null && i < eyeTargets.Count)
				{
					eyeTargets.RemoveAt(i);
					
					if(i == eyeTargets.Count)
						return nearest;
				}
				
					
				
				//check distance
				if(nearest == null)
				{
					nearest = eyeTargets[i];
					distanceToNearest = GetFOV(nearest.transform);
				}
				else 
				{
					distanceToi = GetFOV(eyeTargets[i].transform);
					if (distanceToi < distanceToNearest)
					{
						nearest = eyeTargets[i];
						distanceToNearest= distanceToi;
						
					}
				}
				
			}
			
			return nearest;
		}
	    
		/*public EyeTarget GetNearestEyeTargetDistance()
		{
			EyeTarget nearest = null;
			float distanceToNearest = 0, distanceToi = 0;
			for(int i = 0; i < eyeTargets.Count; i++)
			{
				
				//check if in front
				if(FrontTest(eyeTargets[i].transform, eyeTargets[i].FOV))
				{
					//check distance
					if(nearest == null)
					{
						nearest = eyeTargets[i];
						distanceToNearest = Vector3.Distance(myHead.position,nearest.transform.position);
					}
					else 
					{
						distanceToi = Vector3.Distance(myHead.position,nearest.transform.position);
						if (distanceToi < distanceToNearest)
						{
							nearest = eyeTargets[i];
							distanceToNearest= distanceToi;
							
						}
					}
				}
			}
			
			return eyeTargets[0];
		}*/
		
		float GetFOV(Transform otherTransform)
		{
			Vector3 fwd = myHead.forward;
			Vector3 vec = otherTransform.position - myHead.position;
			vec.Normalize();
 
			float ang = Mathf.Acos(Vector3.Dot(fwd, vec)) * Mathf.Rad2Deg;
 
			bool isFront = false;
 
			return ang;
		}
		
		bool FrontTest(Transform otherTransform, float FOV)
		{
			Vector3 fwd = myHead.forward;
			Vector3 vec = otherTransform.position - myHead.position;
			vec.Normalize();
 
			float ang = Mathf.Acos(Vector3.Dot(fwd, vec)) * Mathf.Rad2Deg;
 
			bool isFront = false;
 
			if (ang <= FOV)
				return true;
 
			return false;
		}
	}
}
