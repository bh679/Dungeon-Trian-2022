/*The AnimationController layer lets me call methods like Crouch() or Reload() that play the appropriate animations without me having to worry about how they're implemented in the Mecanim Animator Controller. I can swap out different AnimationControllers to control different underlying Animator Controllers or even Legacy animation. The AnimationController receives animation commands (such as Reload) one-way from the BodyControllers and sends events (such as ReloadGrabbedAmmo) one-way to the higher layer.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Characters
{
	/*Shoot_b - Activates shoot

	FullAuto_b - Enables full auto if avaliable (AssultRiffle01,02 and subMachineGun)

	Reload_b - Reloads the current gun

	Jump_b - will make the character jump 

	Death_b - will kill the character

	DeathType_int - Will select the type of death animation

	Static_b - Will turn root motion on and off

	Grounded_b - will play a falling animation when not grounded

	Animation_int - selects an idle animation to play

	WeaponType_int - Sets the type of weapon animation to play
	
	Head_Horizontal_f and Head_Vertical_f - Control the head direction
	
	*/
	
	public enum IdleType
	{
		NormalIdle = 0, 
		CrossedArms = 1,
		HandsOnHips = 2,
		CheckWatch = 3,
		SexyDance = 4,
		Smoking = 5,
		Salute = 6,
		WipeMount = 7,
		LeaningAgainstWall = 8,
		SittingOnGround = 9
	}
	
	
	public enum DeathType
	{
		Back = 1,
		Front = 2
	}
	
	public enum WeaponType
	{
		NoWeapon = 0, 
		Pistol = 1, 
		AssultRifle01 = 2, 
		AssultRifle02 = 3, 
		Shotgun = 4,
		SniperRifle = 5,
		Rifle = 6,
		SubMachineGun = 7,
		RPG = 8,
		MiniGun = 9,
		Grenades = 10,
		Bow = 11,
		Melee = 12
	}
	
	public enum MeleeType
	{
		Stab = 0, 
		OneHanded = 1, 
		TwoHanded = 2
	}
	
	public enum Hand
	{
		Left = 0, 
		Right = 1
	}

	public class CharacterAnimationController : MonoBehaviour
	{
		
		public Animator animator;
		public float movementSpeedMultiplier = 0.1f;
		
		void Reset()
		{
			animator = this.GetComponent<Animator>();
		}
		
		
		// Start is called before the first frame update
		void Start()
		{
			
		}
		
		//Shoot_b - Activates shoot
		public void SetShoot(bool isOn)
		{
			animator.SetBool("Shoot_b",isOn);
		}
		
		
		//FullAuto_b - Enables full auto if avaliable (AssultRiffle01,02 and subMachineGun)
		public void SetFullAutomatic(bool isOn)
		{
			animator.SetBool("FullAuto_b",isOn);
		}
		
		//Reload_b - Reloads the current gun
		public void SetReload(bool isOn)
		{
			animator.SetBool("Reload_b",isOn);
		}
		
		//Jump_b - will make the character jump 
		public void SetJump(bool isOn)
		{
			animator.SetBool("Jump_b",isOn);
		}
		
		//Death_b - will kill the character
		public void SetDeath(bool isOn)
		{
			animator.SetBool("Death_b",isOn);
		}
		
		//DeathType_int - Will select the type of death animation
		public void SetDeathType(DeathType deathType)
		{
			animator.SetInteger("DeathType_int", (int)deathType);
		}
		
		///Static_b - Will turn root motion on and off
		public void SetRootMotion(bool isOn)
		{
			animator.SetBool("Static_b",isOn);
		}
		
		//	Grounded_b - will play a falling animation when not grounded
		public void SetGrouded(bool isOn)
		{
			animator.SetBool("Grounded_b",isOn);
		}
		
		
		public void SetCrouch(bool isOn)
		{
			animator.SetBool("Crouch_b",isOn);
		}
		
		//Animation_int - selects an idle animation to play
		public void Idle(IdleType idleType)
		{
			animator.SetInteger("Animation_int", (int)idleType);
		}
		
		
		public void SetSpeed(float speed)
		{
			animator.SetFloat("Speed_f", speed*movementSpeedMultiplier);
		}
		
		//WeaponType_int - Sets the type of weapon animation to play
		public void SetWeapon(WeaponType weaponType)
		{
			animator.SetInteger("WeaponType_int", (int)weaponType);
		}
		
		//meleeType_int - Sets the type of melee animation to play
		public void SetMelee(MeleeType meleeType)
		{
			animator.SetInteger("MeleeType_int", (int)meleeType);
		}
		
		//Head_Horizontal_f and Head_Vertical_f - Control the head direction
		public void SetHeadHorizontal(float angle)
		{
			animator.SetFloat("Head_Horizontal_f", angle);
		}
	
		//Head_Horizontal_f and Head_Vertical_f - Control the head direction
		public void SetHeadVertical(float angle)
		{
			animator.SetFloat("Head_Vertical_f", angle);
		}
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	}

}