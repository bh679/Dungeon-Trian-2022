using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackTimer : MonoBehaviour
{
	
	public UnityEvent attack = new UnityEvent();
	public delegate void AttackDelegate (Transform target);
	public AttackDelegate Attack = null;// = new AttackDelegate();
	
	public float range = 5.6f;
	public float delayBetweenAttacks = 1;
	
	public Transform target;
	public Transform targetFeet;
	
	float timer = 0;
    
    
	// Update is called once per frame
	void Update()
	{
		
		if(timer > 0)
			timer -= Time.deltaTime;
		
		if(target == null)
			return;
		
		float _distance = Vector3.Distance(this.transform.position,targetFeet.transform.position);
		
		if(range > _distance & timer <= 0) // move this to a co-routine
		{
			timer = delayBetweenAttacks;
			attack.Invoke();
			//Debug.Log(Attack);
			
			if(Attack != null)
				Attack(target);
		}
		
	}
	
	#if UNITY_EDITOR
	
	private void OnDrawGizmosSelected()
	{
		UnityEditor.Handles.color = Color.yellow;
		
		UnityEditor.Handles.DrawWireDisc(this.transform.position ,Vector3.up,range);
			
	}
	
	#endif
}
