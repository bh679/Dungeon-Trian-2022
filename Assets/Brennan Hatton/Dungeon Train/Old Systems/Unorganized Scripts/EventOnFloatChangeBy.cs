using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BrennanHatton.Scoring
{

	public class EventOnFloatChangeBy : MonoBehaviour
	{
		public ScoringFloat scoringFloat;
		public EventOnFloatChange requireScoreChange;
		public enum Direction
		{
			Increasing,
			Decreasing,
			Both
		}
		public Direction direction = Direction.Both;
		public float size = 1f;
		float lastFrameValue;
		public UnityEvent onScoreChange = new UnityEvent();
		bool scoreChanged = false;
		
		void Reset()
		{
			scoringFloat = this.GetComponent<ScoringFloat>();
		}
		
		// Start is called before the first frame update
		void Start()
		{
			lastFrameValue = scoringFloat.GetScore();
		}
	
		// Update is called once per frame
		void Update()
		{
			if(CheckScoreChanged())
				onScoreChange.Invoke();
		}
		
		public bool HasScoreChanged()
		{
			return scoreChanged;
		}
	    
		bool CheckScoreChanged()
		{
			scoreChanged = false;
			if(requireScoreChange != null)
			{
				if(!requireScoreChange.HasScoreChanged())
					return scoreChanged;
			}
			
			
			if(direction == Direction.Both)
			{
				if(Mathf.Abs(lastFrameValue - scoringFloat.GetScore()) >= size)
				{
					scoreChanged = true;
				}
			}else if(direction == Direction.Increasing)
			{
				if(scoringFloat.GetScore() - lastFrameValue >= size)
				{
					scoreChanged = true;
				}
			}else if(direction == Direction.Decreasing)
			{
				if(lastFrameValue - scoringFloat.GetScore() >= size)
				{
					scoreChanged = true;
				}
			}
		    
			lastFrameValue = scoringFloat.GetScore();
			
			return scoreChanged;
		}
	}

}