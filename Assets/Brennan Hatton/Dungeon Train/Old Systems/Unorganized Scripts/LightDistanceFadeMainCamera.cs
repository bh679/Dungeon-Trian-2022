using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LightDistanceFade))]
public class LightDistanceFadeMainCamera : MonoBehaviour
{
	LightDistanceFade fader;
	
	void Reset()
	{
		fader = this.GetComponent<LightDistanceFade>();
		fader.target = Camera.main.transform;
	}
	
    // Start is called before the first frame update
    void Start()
    {
	    fader = this.GetComponent<LightDistanceFade>();
	    fader.target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
