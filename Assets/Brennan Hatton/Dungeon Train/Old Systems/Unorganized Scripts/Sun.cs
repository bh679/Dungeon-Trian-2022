using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.DayCycle
{

	public class Sun : MonoBehaviour
	{
		public Light light;
		public RotateAroundAxis rotator;
		public float MinutesADay = 5f, rotationDelay = 5f;
		
		public float SunSetStart = 0.01f;
		public float darknessMultiplyer = 1f, fogStartDistance = 10f, fogEndDistance = 150f;
		
		
		public Color sunSetColor;
		public float nightSpeedMulitplier = 1;
		float defaultSpeed;
		float nightSpeed;
		Color defaultColor;
		float colorChangeStart;
		float distanceEachRotation;
		
		float fogDensity,
			dayClippingDistance;
		
	    // Start is called before the first frame update
		void Reset()
	    {
	    	light = this.GetComponent<Light>();
		    sunSetColor = light.color;   
	    }
	    
		void Start()
		{
			dayClippingDistance = Camera.main.farClipPlane;
			defaultColor = light.color;
			colorChangeStart = -SunSetStart*2;
			RenderSettings.fogDensity = 1f;
			defaultSpeed = rotator.speed;
			nightSpeed = defaultSpeed*nightSpeedMulitplier;
			//StartCoroutine(UpdateSun());
			
			distanceEachRotation = (360f/MinutesADay)*rotationDelay/60;
			RenderSettings.fog = false;
		}
	    
	    // Update is called once per frame
		IEnumerator UpdateSun()
		{
			while(true)
			{
				yield return new WaitForSeconds(rotationDelay - rotationDelay/10f);
				rotator.Rotate(distanceEachRotation);
				UpdateLight();
				
				yield return new WaitForSeconds(rotationDelay/10f);
			}
		}
		
		void Update()
		{
			UpdateLight();
		}
		
		float y;
		void UpdateLight()
		{
			y = light.transform.forward.y+0.01f;
			
			if(y >= colorChangeStart)
			{
				light.color = Color.Lerp(sunSetColor,defaultColor,Mathf.Max(0,Mathf.Min(1,y/colorChangeStart)));
			
				if(y >= -SunSetStart)
				{
					light.intensity = Mathf.Max(0,Mathf.Min(1,y/-SunSetStart));
					
					UpdateFog();
				}
			}
		}
		
		void UpdateFog()
		{
			if(IsNight())
			{
				if(NightDarkest())
				{
					if(rotator.speed < nightSpeed && rotator.speed != 0)
						rotator.speed = nightSpeed;
				}
				else if(rotator.speed == nightSpeed)
					rotator.speed = defaultSpeed;
					
				RenderSettings.fog = true;
				RenderSettings.fogMode = FogMode.Linear;
				fogDensity = Mathf.Max(0,Mathf.Min(1,y/darknessMultiplyer));
				float fogDensity1 = Mathf.Pow(1-Mathf.Min(fogDensity*1.042f,1),10);
						
				RenderSettings.fogStartDistance = fogStartDistance + dayClippingDistance*fogDensity1;
						
				float fogDensity2 = Mathf.Pow(1-Mathf.Min(fogDensity*1.042f,1),6);
				RenderSettings.fogEndDistance = fogEndDistance + dayClippingDistance*fogDensity2*2;
						
			}else
			{
				RenderSettings.fog = false;
				rotator.speed = defaultSpeed;
			}
		}
		
		public bool IsNight()
		{
			return (light.intensity <= 0);
		}
		
		public bool NightDarkest()
		{
			return y >= 0.45f;
		}
		
		public float SunSetValue()
		{
			return y-0.01f;
		}
	}

}