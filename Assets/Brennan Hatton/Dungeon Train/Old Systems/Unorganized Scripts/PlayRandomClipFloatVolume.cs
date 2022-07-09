using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Scoring;

namespace BrennanHatton.Audio
{

	public class PlayRandomClipFloatVolume : MonoBehaviour
	{
		public PlayRandomClip randomClips;
		public float mutltiplier = 1;
		public float baseValue = 0;
		
		void Reset()
		{
			randomClips = this.GetComponent<PlayRandomClip>();
		}
		
		public void PlayRandomClipWithFloatVolume(ScoringFloat scoringFloat)
		{
			randomClips.playRandomClipVolumeScale(baseValue + scoringFloat.GetScore()*mutltiplier);
		}
	}
	
}