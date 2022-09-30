using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EqualReality.VoteBars
{
		
	public class VoteIconManager : MonoBehaviour
	{
		public Transform voteIconParent;
		public Image voteObjectPrefab;
		List<Image> voteIcons = new List<Image>();
		
		void Reset()
		{
			voteIconParent = this.transform;
	    }
	
		float numberOfVotesLast;
	
	    
		public void UpdateVoteIcons(int votersCount)
		{
			while(voteIcons.Count < votersCount)
			{
				AddVoteIcon();
			}
			
			while(voteIcons.Count > votersCount)
			{
				Destroy(voteIcons[0]);
				voteIcons.RemoveAt(0);
			}
				
			for(int i = 0; i < voteIcons.Count; i++)
			{
				voteIcons[i].color = Random.ColorHSV();
			}
		}
		
		void AddVoteIcon()
		{
			Image newIcon = Instantiate(voteObjectPrefab,voteIconParent);
			
			//position icon
			
			voteIcons.Add(newIcon);
		}
	}

}
