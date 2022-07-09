using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BrennanHatton.Scoring;

public class CollisionDamageHealthAngular : MonoBehaviour
{
	public float damage = 1;
	public LayerMask layer;
	public bool trigger = true, collision = true;
	public UnityEvent onDealDamage = new UnityEvent();
	
	private void OnCollisionEnter(Collision col)
	{
		
		if(!collision)
			return;
			
		if((layer.value & 1<< col.gameObject.layer) == 0)
			return;
		
		Health health = col.gameObject.GetComponentInChildren<Health>();
		
		if(health != null)
		{
			health.DamageHealth(damage);
		}
	}
	
	private void OnTriggerEnter(Collider col)
	{
		//Debug.Log("OnTriggerEnter " + col.gameObject.name);
		if(!trigger)
			return;
			
		if((layer.value & 1<< col.gameObject.layer) == 0)
			return;
		
		Health health = col.gameObject.GetComponentInChildren<Health>();
		//Debug.Log(health);
		if(health != null)
		{
			health.DamageHealth(damage);
			onDealDamage.Invoke();
		}
	}
}
