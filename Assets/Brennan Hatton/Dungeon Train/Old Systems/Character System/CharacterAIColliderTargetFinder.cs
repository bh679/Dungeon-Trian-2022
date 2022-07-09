using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Characters
{
	[RequireComponent(typeof(Rigidbody))]
	public class CharacterAIColliderTargetFinder : MonoBehaviour, Targetter
	{
		List<VisibilityCollider> visibilityColliders = new List<VisibilityCollider>(); 
		public LayerMask colliderLayers;
		
		Collider[] colliders;
		
		void Start()
		{
			colliders = this.GetComponentsInChildren<Collider>();
			
			if(colliders.Length == 0)
				Debug.LogError("Blind Character Error. " + this.gameObject.name+ " missing collider. It is blind.");
		}
		
		
		private void OnTriggerEnter(Collider other)
		{
			//if this is layred as a visibleFeild
			if((colliderLayers.value & 1<< other.gameObject.layer) != 0)
			{
				//if it has the right component
				VisibilityCollider visibilityCollider = other.gameObject.GetComponent<VisibilityCollider>();
				if(visibilityCollider != null)
					visibilityColliders.Add(visibilityCollider);
			}
		}
	    
		private void OnTriggerExit(Collider other)
		{
			
			//if this is layred as a visibleFeild
			if((colliderLayers.value & 1<< other.gameObject.layer) != 0)
			{
				//if it has the right component
				VisibilityCollider visibilityCollider = other.gameObject.GetComponent<VisibilityCollider>();
				if(visibilityCollider != null)
					if(visibilityColliders.Contains(visibilityCollider))
						visibilityColliders.Remove(visibilityCollider);
			}
		}
		
	    
		public Transform GetTarget()
		{
			if(visibilityColliders.Count == 0)
				return null;
			
			Transform closestVisibleTarget = GetClosestTargetInVisCollider(visibilityColliders[0]);
			float closestDistance = Mathf.Infinity;//Vector3.Distance(this.transform.position, closestVisibleTarget.position);
			
			for(int i = 0; i < visibilityColliders.Count; i++)
			{
				float distance = Mathf.Infinity;
				Transform closestColliderTarget = GetClosestTargetInVisCollider(visibilityColliders[i]);
				
				if(closestColliderTarget != null)
					distance = Vector3.Distance(this.transform.position, closestColliderTarget.position);
				
				if(distance < closestDistance)
				{
					closestDistance = distance;
					closestVisibleTarget = closestColliderTarget;
				}
				
			}
			
			return closestVisibleTarget;
		}
		
		
		Transform GetClosestTargetInVisCollider(VisibilityCollider collider)
		{
			if(collider.VisibleTargets.Count == 0)
				return null;
			
			float closestDistance = Mathf.Infinity;
			int closestDistanceId = 0;
			for(int i = 0; i < collider.VisibleTargets.Count; i++)
			{
				float distance = Vector3.Distance(this.transform.position, collider.VisibleTargets[i].position);
				
				if(distance < closestDistance)
				{
					closestDistance = distance;
					closestDistanceId = i;
				}
				
			}
			
			return collider.VisibleTargets[closestDistanceId];
		}
		    
	}
}