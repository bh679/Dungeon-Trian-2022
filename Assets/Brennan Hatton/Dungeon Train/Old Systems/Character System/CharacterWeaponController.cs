using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Characters
{
	[RequireComponent(typeof(BodyController))]
	public class CharacterWeaponController : MonoBehaviour
	{
		public BodyController body;
		public Weapon weapon;
		public float Range{
			get { return weapon.Range;}
		}
		
		void Reset()
		{
			body = this.GetComponent<BodyController>();
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        
		    //body.MeleeAttackStop();
	    }
	    
		public void Attack()
		{
			if(weapon.Type == WeaponType.Melee)
				body.MeleeAttackStart(weapon.MeleeType);
			else
				body.Shoot();
		}
	}
}