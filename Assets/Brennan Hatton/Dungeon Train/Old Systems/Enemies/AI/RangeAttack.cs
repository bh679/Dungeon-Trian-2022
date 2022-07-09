using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;

public class RangeAttack : MonoBehaviour
{
	
	public PropPlacer projectilePlacer;
	public Transform projectileSpawn;
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
    
	public void Attack(Transform 
		_target)
	{
		projectileSpawn.LookAt(_target);
		
		projectilePlacer.Place();
	}
	
	
}
