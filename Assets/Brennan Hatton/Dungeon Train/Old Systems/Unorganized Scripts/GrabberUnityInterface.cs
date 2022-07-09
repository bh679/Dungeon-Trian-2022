using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if BNG
using BNG;
#endif

#if BNG
[RequireComponent(typeof(Grabber))]
#endif
public class GrabberUnityInterface : MonoBehaviour
{
	
#if BNG
	public Grabber grabber;
	
	void Reset()
	{
		grabber = this.GetComponent<Grabber>();
	}
	
	public void TryGrab()
	{
		grabber.TryGrab();
	}
#endif
}
