using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemStop : MonoBehaviour
{
	public ParticleSystem PS;
	
	void Reset()
	{
		PS = this.GetComponent<ParticleSystem>();
	}
	
	public void StopEmitting()
	{
		PS.Stop(true,ParticleSystemStopBehavior.StopEmitting);
	}
}
