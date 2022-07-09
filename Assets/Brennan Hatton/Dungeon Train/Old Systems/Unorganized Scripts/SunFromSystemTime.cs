using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFromSystemTime : MonoBehaviour
{
	public Transform sun, midnight;
	
	void OnEnable()
	{
		Quaternion midnightAngle = midnight.rotation;
		Debug.Log("Hour: "+System.DateTime.Now.Hour);
		Debug.Log("Angle: "+System.DateTime.Now.Hour/24f*360);
		
		midnight.RotateAround(new Vector3(0,0,1),System.DateTime.Now.Hour/24f*360);
		
		sun.rotation = midnight.rotation;
		
		midnight.rotation = midnightAngle;
		
	}
}
