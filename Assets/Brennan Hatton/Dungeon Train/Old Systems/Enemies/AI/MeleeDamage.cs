using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BrennanHatton.Scoring;

public class MeleeDamage : MonoBehaviour
{
	public float damage = 1;
	public UnityEvent OnAttack = new UnityEvent();
	public AttackTimer timer;
	
	void Reset()
	{
		timer = this.GetComponent<AttackTimer>();
	}
	
	void Start()
	{
		if(timer == null)
			timer = this.GetComponent<AttackTimer>();
		
		if(timer != null)
			timer.Attack = new AttackTimer.AttackDelegate(Attack);
	}
	
    
	public void Attack(Transform target)
	{
		Health health = target.GetComponentInChildren<Health>();
		
		if(health != null)
		{
			health.DamageHealth(damage);
			OnAttack.Invoke();
		}
	}
	
}
