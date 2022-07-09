using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
	public Transform target;
	
	public bool onUpdate = true, onStart;
	
    // Start is called before the first frame update
    void Start()
	{
		if(onStart)
			LookAt();
    }

    // Update is called once per frame
    void Update()
    {
	    if(onUpdate)
		    LookAt();
    }
    
	public void LookAt()
	{
		LookAt(target);
	}
	
	public void LookAt(Transform _target)
	{
		transform.LookAt(target);
	}
	
}
