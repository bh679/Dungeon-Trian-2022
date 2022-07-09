using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Characters
{

	public class IdleWalkTarget : MonoBehaviour
	{
		Vector3 targetPosition;
		Vector3 origin;
		
		public float range = 2f, minWalkDistnace = 0.1f;
		public float waitMinTime = 0f, waitMaxTime = 5f;
		
		float timer, waitTime;
		
		public void Place()
		{
			origin = this.transform.position;
			waitTime = Random.Range(waitMinTime, waitMaxTime);
			
			CaluatleNewTarget();
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    Place();
	    }
	
	    // Update is called once per frame
	    void Update()
		{
			if(Vector3.Distance(targetPosition, this.transform.position) < minWalkDistnace)
			{
				timer += Time.deltaTime;
				
				if(timer > waitTime)
				{
					CaluatleNewTarget();
				}
			}
	    }
	    
		void CaluatleNewTarget()
		{
			timer = 0;
			targetPosition = origin + new Vector3(Random.value*range/2f,0,Random.value*range/2f);
		}
	    
		public Vector3 GetTarget()
		{
			
			return targetPosition;
		}
		
	}

}