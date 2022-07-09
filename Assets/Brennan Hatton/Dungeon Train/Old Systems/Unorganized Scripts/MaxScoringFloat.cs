using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Scoring
{
	
public class MaxScoringFloat : MonoBehaviour
{
	public ScoringFloat scoringFloat;
	public float max = 10;
	
	void Reset()
	{
		scoringFloat = this.GetComponent<ScoringFloat>();
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if(scoringFloat.GetScore() > max)
		    scoringFloat.SetScore(max);
    }
}

}