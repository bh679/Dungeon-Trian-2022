//makes character body attack target
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Characters
{
	[RequireComponent(typeof(BodyController))]
	[RequireComponent(typeof(Targetter))]
	[RequireComponent(typeof(CharacterWeaponController))]
	public class CharacterAIAttack : MonoBehaviour
	{
		public BodyController body;
		public Targetter targetter;
		public CharacterWeaponController weapon;
		Transform target;
		public float backStepDistance = 0;
		
		float speed;
		
		public bool ignoreTarget;
		
		public bool Debugging = false;
		void Reset()
		{
			body = this.GetComponent<BodyController>();
			targetter = this.GetComponent<Targetter>();
			weapon = this.GetComponent<CharacterWeaponController>();
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
			if(body == null)
				body = this.GetComponent<BodyController>();
			if(targetter == null)
				targetter = this.GetComponent<Targetter>();
			if(weapon == null)
				weapon = this.GetComponent<CharacterWeaponController>();
	    }
	
	    // Update is called once per frame
	    void Update()
		{
			MoveOrAttackUpdate(); // perhaps move this to another class
		}
		
		public void MoveOrAttackUpdate()
		{
			
			target = targetter.GetTarget();
			
			if(target == null)
				return;
				
			if(Debugging) Debug.Log("Target Found - Attacking " + target.gameObject.name);
			
			float distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);
		    	
			if(distanceToTarget < weapon.Range)
			{
				if(Debugging) Debug.Log("withing range of " + target.gameObject.name);
				weapon.Attack();//.MeleeAttackStart(MeleeType.OneHanded);
				
				//		is it too close
				if(distanceToTarget < backStepDistance)
				{
					if(Debugging) Debug.Log("Step back from " + target.gameObject.name);
					//			move back
					Vector3 backDirection = this.transform.position - target.position;
					body.Move(this.transform.position-backDirection.normalized, true);
				}else
					body.StopMoving();
				
			}else 
			{
				
				if(Debugging) Debug.Log("move towards " + target.gameObject.name);
				//			move towards the target
				body.Move(target.position, true);
			}
		}
		
	    
	    
	}
}