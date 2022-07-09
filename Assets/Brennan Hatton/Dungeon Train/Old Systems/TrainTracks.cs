using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTracks : MonoBehaviour
{
    
	private void OnCollisionEnter(Collision col)
	{
		col.rigidbody.AddForce(-Vector3.forward*5f);
		
	}
}
