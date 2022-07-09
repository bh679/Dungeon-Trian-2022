using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.DayCycle
{
	
	public class NightShader : MonoBehaviour
	{
		public Sun sun;
		public string propertyName;
		public Material materialWithShader;
		public float delay = 0.1f;
		public float nightFadeRange = 0.1f;
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
		    if(sun.IsNight())
		    {
		    	float value = nightFadeRange-sun.SunSetValue()*3+0.7f;
		    	if(value > 0)
		    	{
		    		SetFloat(propertyName,value);
		    	}else
			    	SetFloat(propertyName,0);
		    }else
		    	SetFloat(propertyName,1);
	    }
	    
		public void SetFloat(string _propertyName, float _value)
		{
			materialWithShader.SetFloat(_propertyName, _value);
		}
	}

}