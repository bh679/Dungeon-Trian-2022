using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemColor : MonoBehaviour
{
	public ParticleSystem PS;
	public Color color;
	
	void Reset()
	{
		PS = this.GetComponent<ParticleSystem>();
	}
	
	public void SetColor()
	{
		PS.startColor = color;
	}
	
	public void SetRed()
	{
		PS.startColor = Color.red;
	}
	
	public void SetWhite()
	{
		PS.startColor = Color.white;
	}
}
