#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class GrabbableOriginalParentSetter : MonoBehaviour
{
	public Transform parent;
	public LayerMask layer;
	
	public float delayFromEnable = 2f;
	
	private bool ready = false;
	
	void OnEnable()
	{
		StartCoroutine(SetReadyAfterDelay());
	}
	
	IEnumerator SetReadyAfterDelay()
	{
		yield return new WaitForSeconds(delayFromEnable);
		
		ready = true;
		
		yield return null;
	}
	
	void Reset()
	{
		parent = this.transform;
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(!ready)
			return;
		
		if((layer.value & 1<< col.gameObject.layer) != 0)
		{
			Grabbable grabbale = col.gameObject.GetComponent<Grabbable>();
			if(grabbale != null)
			{
				grabbale.UpdateOriginalParent(parent);
			}
		}
	}
}
#endif