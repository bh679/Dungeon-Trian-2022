#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class GrabberForceGrab : MonoBehaviour
{
	public Grabber grabber;
	
	/// <summary>
	/// Gets the grabber from the same gameobject when added to an gameobject
	/// </summary>
	void Reset()
	{
		grabber = this.GetComponent<Grabber>();
	}
	
	public void ForceGrab()
	{
		grabber.ForceGrab = true;
		grabber.ForceRelease = false;
	}
	
	public void ForceRelease()
	{
		grabber.ForceGrab = false;
		grabber.ForceRelease = true;
	}
}
#endif