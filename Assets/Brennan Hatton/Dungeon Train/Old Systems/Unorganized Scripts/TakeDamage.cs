using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BrennanHatton.Scoring
{

	[RequireComponent(typeof(Rigidbody))]
	public class TakeDamage : MonoBehaviour
	{
		
		public bool collision = true, trigger= true;
		public UnityEvent onTakeDamage = new UnityEvent();
		public LayerMask layer;
		public Health health;
		
		
		void Reset()
		{
			health = this.GetComponent<Health>();
			
			if(!health)
				health = transform.parent.gameObject.GetComponent<Health>();
			
		}
		
		private void OnCollisionEnter(Collision col)
		{
			TakeTheDamage(col.collider.gameObject);
		}
	
		private void OnTriggerEnter(Collider col)
		{
			TakeTheDamage(col.gameObject);
		}
		
		void TakeTheDamage(GameObject dealerObj)
		{
			if(health == null)
				return;
		
			if(!collision)
				return;
			
			if((layer.value & 1<< dealerObj.layer) == 0)
				return;
		
			DamageDealer dealer = dealerObj.GetComponentInChildren<DamageDealer>();
		
			if(dealer != null)
			{
				float damage = dealer.GetDamage();
				if(damage != 0)
				{
					health.DamageHealth(damage);
					onTakeDamage.Invoke();
				}
			}
		}
	}

}