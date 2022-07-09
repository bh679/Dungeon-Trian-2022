/*The BodyControllers keep track of higher level state such as the character's place in the environment. For example, if the character is no longer grounded, the base FSM might change state from Idle to Falling. The BodyControllers read input from the VirtualController and tell the AnimationController what animations to play. They also receive events from the AnimationController. The BodyControllers may change change state based on the events or pass them up to a higher level.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Characters
{
	[RequireComponent(typeof(CharacterAnimationController))]
	[RequireComponent(typeof(CharacterEquipper))]
	public class BodyController : MonoBehaviour
	{
		public CharacterAnimationController animationController;
		public CharacterEquipper equipper;
		
		public float walkSpeed = 0.3f, runSpeed = 0.6f;
		
		float speed = 0;
		bool dead = false;
		public bool Dead {get;}
		
		void Reset()
		{
			animationController = this.GetComponent<CharacterAnimationController>();
			equipper = this.GetComponent<CharacterEquipper>();
		}
		
		public void Refresh()
		{
			dead  = false;
		}
	    
		public void Die()
		{
			dead = true;
			animationController.SetDeath(true);
			animationController.SetWeapon(WeaponType.NoWeapon);
		}
	
		public void HoldWeapon(Transform weaponObject, WeaponType type, Hand hand)
		{
			equipper.Hold(weaponObject, hand);
			if(type == WeaponType.Melee)
				animationController.SetWeapon(WeaponType.NoWeapon);
			else
				animationController.SetWeapon(type);
		}
	
		public void Shoot()
		{
			
		}
	
		public void MeleeAttackStart(MeleeType meleeAttackType)
		{
			animationController.SetMelee(meleeAttackType);
			animationController.SetWeapon(WeaponType.Melee);
		}
	
		public void MeleeAttackStop()
		{
			animationController.SetWeapon(WeaponType.NoWeapon);
		}
		
		public void Move(Vector3 targetPosition, bool run)
		{
			if(dead)
				return;
				
			if(run)
				speed = runSpeed;
			else
				speed = walkSpeed;
				
			animationController.SetSpeed(speed);
			
			Vector3 targetPostition = new Vector3( targetPosition.x, 
				this.transform.position.y, 
				targetPosition.z ) ;
			this.transform.LookAt( targetPostition ) ;
			
			this.transform.position += transform.forward * speed*Time.deltaTime;
		}
		
		public void StopMoving()
		{
			animationController.SetSpeed(0);
		}
		
		public void LookAtPoint(Vector3 point)
		{
			
		}
	
	}
}
