using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Podcasting
{
	[System.Serializable]
	public class PodCastEp
	{
		public AudioClip[] clips;
		public string name;
		
		public IEnumerator Play(AudioSource source)
		{
			for(int i = 0; i < clips.Length; i++)
			{
				source.PlayOneShot(clips[i]);
				
				yield return new WaitForSeconds(clips[i].length);
			}
			
			yield return null;
		}
		
	}
	
	public class PodcastManager : MonoBehaviour
	{
		
		public List<PodCastEp> episodes;
		
		public AudioSource source;
		
		int id = -1;
		
		public void PlayRandom()
		{
			id = Random.Range(0,episodes.Count);
			
			Play();
		}
		
		public void PlayNext(){
			
			id++;
			
			Play();
			
		}
		
		public void Play()
		{
			StopAllCoroutines();
			source.Stop();
			StartCoroutine(episodes[id].Play(source));
		}
	}
}
