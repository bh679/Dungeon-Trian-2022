using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeBetween : MonoBehaviour
{
	
	public AudioSourceExt currentManager;
	public float transitionSpeed = 3f;
	float lastFadeTime = 0;
	
	public void FadeTo(AudioSourceExt newSource)
	{
		if(newSource == currentManager)
			return;
		
		StartCoroutine(FadeWhenReady(newSource));
	}
	
	IEnumerator FadeWhenReady(AudioSourceExt newSource)
	{
		if(lastFadeTime > Time.time - transitionSpeed)
			yield return new WaitForSeconds(Time.time - lastFadeTime);
			
		currentManager.VolumeFadeToZeroAndPause(transitionSpeed);
		newSource.VolumeFadeToFull(transitionSpeed);
		
		newSource.audioSource.Play();
		
		currentManager = newSource;
	}
    
}
