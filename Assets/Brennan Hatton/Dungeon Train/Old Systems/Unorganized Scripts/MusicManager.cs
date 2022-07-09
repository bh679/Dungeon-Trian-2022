using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public AudioSource source;
	public AudioClip[] soundTracks;
	
	void Reset()
	{
		source = this.GetComponent<AudioSource>();
	}
	
	public void PlayTrack(int id)
	{
		source.Stop();
		source.clip = soundTracks[id];
		source.Play();
	}
	
	public void PlayClip(int id)
	{
		source.Stop();
		source.clip = soundTracks[id];
		source.Play();
	}
	
	public void PlayRandomTrack()
	{
		PlayTrack(Random.Range(0,soundTracks.Length - 1));
	}
	

}
