using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EyeTextureResolutionScale : MonoBehaviour
{
	public float scale = 2.0f;
	
    // Start is called before the first frame update
	void Awake()
    {
	    XRSettings.eyeTextureResolutionScale = scale;
    }
}
