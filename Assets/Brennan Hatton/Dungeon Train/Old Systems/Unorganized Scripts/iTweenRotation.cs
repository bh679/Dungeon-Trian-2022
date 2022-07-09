using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iTweenRotation : MonoBehaviour
{
	public GameObject target;
	
	public Vector3 scale = Vector3.one * 2;
	public iTween.EaseType easyType = iTween.EaseType.easeInOutBack;
	public iTween.LoopType loopType = iTween.LoopType.pingPong;
	public float delay = 0.1f;
	
	
	void Reset()
	{
		target = this.gameObject;
	}
	
	
	void Start(){
		
		if(target == null)
			target = this.gameObject;
		
		iTween.ScaleBy(target, iTween.Hash("x", scale.x,"y", scale.y,"z", scale.z, "easeType", easyType.ToString(), "loopType", loopType.ToString(), "delay", delay));
	}
}
