//This class is for turning on and off all culling triggers to test performance ofthe system

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Culling;

public class CullingTestController : MonoBehaviour
{
	public bool culling = true;
	bool currentlyingCulling = true;
	
	public CullingTrigger[] triggers;
	
	void Reset()
	{
		
		triggers = this.transform.root.GetComponentsInChildren<CullingTrigger>(true);
	
	}
	
	void SwitchCulling()
	{
		Debug.Log("Switching");
		currentlyingCulling = culling;
		
		
		Debug.Log("triggers.Length " + triggers.Length);
		for(int i = 0; i < triggers.Length; i++)
		{
			for(int c = 0; c < triggers[i].cullingBounds.Length; c++)
			{
				Debug.Log("cullingBounds.Length " + triggers[i].cullingBounds.Length);
				if(currentlyingCulling)
					triggers[i].cullingBounds[c].TurnOnMeshes(triggers[i].PlayerIsIn);
				else
					triggers[i].cullingBounds[c].TurnOnMeshes(true);
			}
					
		}
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if(currentlyingCulling != culling)
		    SwitchCulling();
    }
}
