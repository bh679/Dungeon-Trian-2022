using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateAndFlip : MonoBehaviour
{
	public bool onStart = true;
	public Transform parent;
	
	void Reset()
	{
		parent = this.transform.parent;
	}
	
    // Start is called before the first frame update
    void Start()
	{
	     if(!onStart)
		     return;
		     
	    //duplicatedAndFlip();
		     
    }

    // Update is called once per frame
	public GameObject duplicatedAndFlip()
	{
		if(parent == null)
			parent = this.transform.parent;
	     
		GameObject copy = Instantiate(this.gameObject) as GameObject;
		copy.name = copy.name.Replace("Left","Right");
		copy.transform.SetParent(parent);
		copy.transform.localScale = new Vector3(-1,1,1);
		copy.transform.localPosition = new Vector3 (-1.5f,this.transform.localPosition.y,this.transform.localPosition.z);
		copy.GetComponent<DuplicateAndFlip>().onStart = false;
		
		return copy;
	}
}
