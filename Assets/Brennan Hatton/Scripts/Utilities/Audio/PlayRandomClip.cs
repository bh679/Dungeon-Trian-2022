using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Audio
{

	public class PlayRandomClip : MonoBehaviour
	{
		public bool PlayOnEnable = false;
		
		public AudioClip[] clips;
		public AudioSource source;
		
		void Reset()
		{
			source = this.GetComponent<AudioSource>();
		}
		
		
		void OnEnable()
		{
			if(PlayOnEnable)
				playRandomClip();
		}
		
		public void playRandomClip()
		{
			int id = Random.Range(0,clips.Length);
			source.PlayOneShot(clips[id]);
			//Debug.Log(clips[id]);
		}
		
		public void playRandomClipVolumeScale(float volumeScale)
		{
			int id = Random.Range(0,clips.Length);
			source.PlayOneShot(clips[id],volumeScale);
			//Debug.Log(clips[id]);
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	}

}
