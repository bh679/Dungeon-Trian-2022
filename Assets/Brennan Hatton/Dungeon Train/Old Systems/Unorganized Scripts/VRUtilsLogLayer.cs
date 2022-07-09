#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class VRUtilsLogLayer : MonoBehaviour
{
	void Awake()
	{
		VRUtils.Instance.Log("Awake: " + LayerMask.LayerToName(this.gameObject.layer) + " " + gameObject.name);
	}
	// Start is called before the first frame update
	void Start()
	{
		VRUtils.Instance.Log("Start: " + LayerMask.LayerToName(this.gameObject.layer) + " " + gameObject.name);
	}
	// Start is called before the first frame update
	void OnEnable()
	{
		VRUtils.Instance.Log("OnEnable: " + LayerMask.LayerToName(this.gameObject.layer) + " " + gameObject.name);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
#endif