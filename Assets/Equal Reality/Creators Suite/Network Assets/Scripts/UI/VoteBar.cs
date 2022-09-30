using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace EqualReality.VoteBars
{
	
	public class VoteBar : MonoBehaviour
	{
		public Image image;
		
		float length, minRight;
		public float votesbuf, playersbuf;
		
		public Color defaultColor, UserSelectedColor;
		
		void Reset()
		{
			image = this.GetComponent<Image>();
				
			if(image != null)
			{
				defaultColor = image.color;
				UserSelectedColor = image.color;
			}
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
			minRight = ((RectTransform)this.transform).rect.left;
			length = ((RectTransform)this.transform).rect.size.x;
			length = Mathf.Abs(length);
	    }
	
	    // Update is called once per frame
		public void UpdateVotes(bool youVoted, int numberOfVotes, int maxVotes)
		{
			if(youVoted)
				image.color = UserSelectedColor;
			else
				image.color = defaultColor;
			
			float right = -length;
			
			if(numberOfVotes+votesbuf > 0)
				right = ((numberOfVotes+votesbuf) / (maxVotes+playersbuf)) * length - length;
			
			((RectTransform)this.transform).offsetMax = new Vector2(right,((RectTransform)this.transform).offsetMax.y);
	    }
	}
}
