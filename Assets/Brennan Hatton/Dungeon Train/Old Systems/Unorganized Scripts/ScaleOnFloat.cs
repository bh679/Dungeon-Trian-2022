using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Scoring
{
	public class ScaleOnFloat : MonoBehaviour
	{
		public Transform target;
		public ScoringFloat floatScore;
		public Vector3 multiplier = Vector3.one;
		
		void Reset()
		{
			target = this.transform;
			floatScore = this.gameObject.GetComponent<ScoringFloat>();
			multiplier = target.localScale;
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
		    target.localScale = multiplier*floatScore.GetScore();
	    }
	}
}