using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using BzKovSoft.ObjectSlicer.EventHandlers;

[DisallowMultipleComponent]
public class SliceMeleeRange : MonoBehaviour, IBzObjectSlicedEvent
	{
		public AttackTimer attack;
		public float rangeScaleFactor = 1;
		
		void Reset()
		{
			attack = this.GetComponent<AttackTimer>();
				
		}
		
		//public bool parentUp = false;
		public void ObjectSliced(GameObject original, GameObject resultNeg, GameObject resultPos)
		{
			// we need to wait one fram to allow destroyed component to be destroyed.
			StartCoroutine(NextFrame(original, resultNeg, resultPos));
		}

		private IEnumerator NextFrame(GameObject original, GameObject resultNeg, GameObject resultPos)
		{
			yield return null;
			
			
			var oRigid = original.GetComponent<Rigidbody>();
			var nRigid = resultNeg.GetComponent<Rigidbody>();
			var pRigid = resultPos.GetComponent<Rigidbody>();
			
			float negMass = nRigid.mass, posMass = pRigid.mass;
			
			nRigid.SetDensity(1);
			pRigid.SetDensity(1);
			
			float nVol = nRigid.mass, pVol = pRigid.mass;
			
			nRigid.mass = negMass;
			pRigid.mass = posMass;
			
			if(attack)
				attack.range = nVol * rangeScaleFactor;
		}
	}