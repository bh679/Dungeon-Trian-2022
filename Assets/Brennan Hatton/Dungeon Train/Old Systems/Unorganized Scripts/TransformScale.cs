using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformScale : MonoBehaviour
{
	public Vector2 randomScale;
	
	public bool onEnable = false;
	
	public bool uniform = true;
	
	void OnEnable()
	{
		if(onEnable)
		{
			if(uniform)
				TransformSetRandomUniformScale();
			else
				TransformSetRandomUniformScale();
		}
	}
    
	public void ScaleAllRandom()
	{
		Vector3 newScale = this.transform.localScale.normalized;
		
		newScale.x *= Random.Range(randomScale.x,randomScale.y);
		newScale.y *= Random.Range(randomScale.x,randomScale.y);
		newScale.z *= Random.Range(randomScale.x,randomScale.y);
		
		this.transform.localScale = newScale;
	}
    
	public void TransformSetUniformScale(float scale)
	{
		this.transform.localScale = this.transform.localScale.normalized*scale;
	}
    
	public void TransformSetRandomUniformScale()
	{
		float scale = Random.Range(randomScale.x,randomScale.y);
		
		TransformSetUniformScale(scale);
	}
}
