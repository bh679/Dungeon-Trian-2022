using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BrennanHatton.Scoring
{
	
	public class DamageDealer : MonoBehaviour
	{
		public float damage = 1;
		public bool DamageOff = false;
		public float mutliplier = 1;
		
		public virtual float GetDamage()
		{
			if(DamageOff)
				return 0;
				
			return damage * mutliplier;
		}
		
		public void TurnOffDamage()
		{
			DamageOff = true;
		}
		
		public void TurnOnDamage()
		{
			DamageOff = false;
		}
		
		public void SetMutliplier(float newVal)
		{
			mutliplier = newVal;
		}
	}

}