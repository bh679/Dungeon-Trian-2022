/*


This class represents the visal feild/distance for culling objects in a Culling Bounds

Culling Trigger is used to decide if the player is close enough to rendera Culling Bounds

Culling Trigger is placed on a box collider, which is used to check if the player is inside that box, in order to render particular objects inside a Culling Bounds
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Culling
{

	public class CullingTrigger : MonoBehaviour
	{
		bool playerIsIn;
		public bool PlayerIsIn{
			get{return playerIsIn;}
		}
		public LayerMask playerLayer;
		public GameObject[] gameObjects = new GameObject[0];
		public CullingBounds[] cullingBounds;
		
		public bool Debugging = false;
		
		void Reset()
		{
			cullingBounds = this.GetComponentsInChildren<CullingBounds>();
		}
		
		void Start()
		{
			playerIsIn = false;
			
			for(int i = 0 ; i < cullingBounds.Length; i++)
			{
				cullingBounds[i].cullingTriggers.Add(this);
			}
		}
	    
		private void OnTriggerEnter(Collider other)
		{
			if(playerIsIn)
				return;
				
			//if object is player
			if(((playerLayer.value & 1<< other.gameObject.layer) != 0))
			{
				TurnOnMeshes(true);
				playerIsIn = true;
				if(Debugging) Debug.Log("Enabling culled meshes");
			}
			
		}
		
		
		private void OnTriggerStay(Collider other)
		{
			
			if(playerIsIn)
				return;
				
			//if object is player
			if(((playerLayer.value & 1<< other.gameObject.layer) != 0))
			{
				TurnOnMeshes(true);
				playerIsIn = true;
				if(Debugging) Debug.Log("Enabling culled meshes");
			}
		}
		
		private void OnTriggerExit(Collider other)
		{
			if(!playerIsIn)
				return;
				
			if(((playerLayer.value & 1<< other.gameObject.layer) != 0))
			{
				TurnOnMeshes(false);
				playerIsIn = false;
				if(Debugging) Debug.Log("Culling meshes");
			}
			
		}
		
		public void TurnOnMeshes(bool isOn)
		{
			for(int i = 0 ;i < cullingBounds.Length; i++)
			{
				cullingBounds[i].TurnOnMeshes(isOn);
			}
		}
		
		public void ResetCulling()
		{
			playerIsIn = false;
			
			for(int i = 0 ;i < cullingBounds.Length; i++)
			{
				cullingBounds[i].ResetCulling();
			}
		}
	}
}