using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickActive : MonoBehaviour
{
	public GameObject target;
	
	public float length = 5;
	public float rate = 1;
	
    // Start is called before the first frame update
	void OnEnable()
	{
		StopAllCoroutines();
		target.SetActive(true);
	    StartCoroutine(Flicker(length));
    }
    
	IEnumerator Flicker(float time)
	{
		float endTime = Time.time + time;
		
		
		while(Time.time < endTime)
		{
			yield return new WaitForSeconds((endTime - Time.time)/(time*rate)*2);
			target.SetActive(false);
			yield return new WaitForSeconds((endTime - Time.time)/(time*rate*2));
			target.SetActive(true);
		}
		
		
		
		
		
	}
}
