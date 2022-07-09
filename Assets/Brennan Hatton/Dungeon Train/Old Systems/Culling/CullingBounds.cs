/*
This object represents the bounds for group objects together when culling
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Utilities;

namespace BrennanHatton.Culling
{

	public class CullingObject
	{
		public MeshRenderer[] meshRenderers;
		public Collider[] colliders;
		public bool[] collisionStatus;
		
		public void EnableRenderers(bool isEnabled)
		{
			if(meshRenderers == null)
			{
				Debug.Log("No Renderers");
				return;
			}
			
			for(int i = 0 ; i < meshRenderers.Length; i++)
			{
				if(meshRenderers[i] != null)
					meshRenderers[i].enabled = isEnabled;
			}
		}
	}

	public class CullingBounds : MonoBehaviour
	{
		public LayerMask boundsLayer;
		public LayerMask ignoreLayer;
		List<CullingObject> cullingObjects = new List<CullingObject>();
		Collider[] myColliders;
		
		[HideInInspector]
		public List<CullingTrigger> cullingTriggers = new List<CullingTrigger>();
		
		public Transform hasParent;
		public int parentDepthCheck = 5;
		
		public bool priorityCuller = false;
		public CullingManager manager;
		
		public bool Debugging = false;
		
		//fall back system to take object sout of culling - for some reason on trigger exit isnt working.
		public float breakDistanceMutliplier = 1;
		float[] breakDistance;// = 3;
		
		void Start()
		{
			myColliders = this.GetComponentsInChildren<Collider>();
			
			if(myColliders != null)
			{
				breakDistance = new float[myColliders.Length];
				for(int i = 0; i < myColliders.Length; i++)
				{
					breakDistance[i] = Mathf.Sqrt(Mathf.Pow(myColliders[i].bounds.size.x,2) + Mathf.Pow(myColliders[i].bounds.size.z,2))/2;
					
					breakDistance[i] *= breakDistanceMutliplier;
				}
			}
				
				
		}
		
		public void ResetCulling()
		{
			if(Debugging) Debug.Log("<color=red>Reseting</color> Culling ");
			
			TurnOnMeshes(true);
			cullingObjects = new List<CullingObject>();
		}
		
		bool LayerIsInMask(int layer)
		{
			return (((boundsLayer.value & 1<< layer) != 0));
		}
		
		CullingObject getCullingObjectByCollider(Collider collider)
		{
			for(int i = 0; i < cullingObjects.Count; i++)
			{
				for(int c = 0; c < cullingObjects[i].colliders.Length; c++)
				{
					if(cullingObjects[i].colliders[c] == collider)
					{
						return cullingObjects[i];
					}
				}
			}
			
			return null;
		}
	    
		private void OnTriggerEnter(Collider other)
		{
			//if object is to be culled
			if(LayerIsInMask(other.gameObject.layer))//((boundsLayer.value & 1<< other.gameObject.layer) != 0))
			{
				if(hasParent != null)
					if(!TransformUtils.HasObjectInParents(other.transform, hasParent, parentDepthCheck))
						return;
				
				MeshRenderer[] newMeshes = other.GetComponentsInChildren<MeshRenderer>();
				List<Collider> subColliders = new List<Collider>();
				subColliders.AddRange(other.GetComponentsInChildren<Collider>());
				bool[] collisionStatus = new bool[subColliders.Count]; 
				
				
				//for all sub-colliders
				for(int i = 0; i < subColliders.Count; i++)
				{
					
					//remove colliders on wrong layer
					if(LayerIsInMask(subColliders[i].gameObject.layer) == false)
					{
						subColliders.RemoveAt(i);
						i--;
					}
					
					//check if colliders already registered
					CullingObject existingObject = getCullingObjectByCollider(subColliders[i]);
					if(existingObject != null)
					{
						//check which has more colliders, and there is the parent
						if(existingObject.colliders.Length < subColliders.Count)
						{//existing is the child
							
							//copy collision status bool array
							//for every collider in existing
							for(int e = 0; e < existingObject.colliders.Length; e++)
							{
								//for every subcollider here
								for(int s = 0; s < subColliders.Count; s++)
								{
									//see if they match
									if(existingObject.colliders[e] == subColliders[s])
									{
										//copy collision statys
										collisionStatus[s] = existingObject.collisionStatus[e];
									}
								}
							}
							
							//destory preivious object
							cullingObjects.Remove(existingObject);
						}
						else
						{
							//update bool array of existing
							for(int e = 0; e < existingObject.colliders.Length; e++)
							{
								//see if they match
								if(existingObject.colliders[e] == other)
								{
									//copy collision statys
									existingObject.collisionStatus[e] = true;
								}
							}
							
							//exit loop
							return;
						}
					}
				}
				
				
				/*if(Debugging) Debug.Log("Adding " + other.gameObject.name + "'s " + newMeshes.Length + " child meshes to culling");
				if(!PlayerIsInATrigger())
				{
					if(Debugging) Debug.Log("Cullign now");
					for(int i = 0 ;i < newMeshes.Length; i++)
					{
						if(((ignoreLayer.value & 1<< newMeshes[i].gameObject.layer) == 0))
							newMeshes[i].enabled = false;
					};
				}*/
				
				CullingObject newCullingObject = new CullingObject ();
				newCullingObject.meshRenderers = newMeshes;
				newCullingObject.colliders = subColliders.ToArray();	
				newCullingObject.collisionStatus = collisionStatus;	
				
				
				if(other.transform.parent == null)
				{
					//it is being grabbed
				}
				else if((other.transform.parent != transform.root) && (((boundsLayer.value & 1<< other.transform.parent.gameObject.layer) != 0)))
				{
					MeshRenderer newMesh = other.transform.parent.gameObject.GetComponent<MeshRenderer>();//what if we are getting the meshes of a child collider, whos parent has already listed this meshes?
					
					if(newMesh != null)
					{
						if(Debugging) Debug.Log("Adding " + other.gameObject.name + "'s parent mesh to culling");
						
						
						MeshRenderer[] renderers = new MeshRenderer[newCullingObject.meshRenderers.Length+1];
						renderers[0] = newMesh;
						
						for(int i = 0; i < newCullingObject.meshRenderers.Length; i++)
						{
							renderers[i+1] = newCullingObject.meshRenderers[i];
						}
						
						newCullingObject.meshRenderers = renderers;
					}
				}
				
				//turn on collision status for main collider
				for(int i = 0; i < subColliders.Count; i++)
				{
					//if its the main collider
					if(subColliders[i] == other)
						//set entered
						collisionStatus[i] = true;
				}
				
				//did the collider have renderers?
				if(newCullingObject.meshRenderers.Length > 0)
				{
					
					//is the player not starting in this one?
					if(!PlayerIsInATrigger())
					{
						//turn them all off
						newCullingObject.EnableRenderers(false);
					}
					
					//add them to list
					cullingObjects.Add(newCullingObject);
					
				}
				
				if(priorityCuller && manager != null)
				{
					StopAllCoroutines();
					StartCoroutine(RemoveFromOthers());
				}
			}
			
		}
	    
		IEnumerator RemoveFromOthers()
		{
			yield return new WaitForSeconds(0.1f);
			
			/*List<MeshRenderer> meshesCulling = new List<MeshRenderer>();
			for(int i = 0 ;i < cullingObjects.Count; i++){
				meshesCulling.AddRange(cullingObjects[i].meshRenderers);
			}*/
			
			manager.RemoveCullingObjects(cullingObjects.ToArray(), PlayerIsInATrigger());
		}
		
		int uIndex = 0;
		void Update()
		{
			
			uIndex++;
			
			if(cullingObjects.Count <= uIndex)
				uIndex = 0;
				
			if(
				//values exists
				cullingObjects != null && 
				cullingObjects.Count != 0 &&
				// and it is not close enough to a collider of this bounders
				IsCloestEnoughToACollider() == false
			)
			{
				
				//turn them back on
				cullingObjects[uIndex].EnableRenderers(true);
						
				//remove from list
				cullingObjects.RemoveAt(uIndex);
			}
		}
		
		bool IsCloestEnoughToACollider()
		{
			
			
			if(cullingObjects[uIndex].colliders.Length == 0 || cullingObjects[uIndex].colliders[0] == null)
			{
				#if UNITY_ENGINE
				Debug.Log(cullingObjects[uIndex].colliders.Length + "  " + cullingObjects[uIndex].meshRenderers.Length);
				Debug.LogError("missing collider " + TransformUtils.HierarchyPath(cullingObjects[uIndex].meshRenderers[0].transform,7));
				#endif
				return false;
			}
			
			//for all colliders of this bounds
			for(int i = 0; i < myColliders.Length; i++)
			{
				for(int c = 0;  c < cullingObjects[uIndex].colliders.Length; c++)
				{
					if(cullingObjects[uIndex].colliders[c] == null)
					{
					
					}else if(Vector3.Distance(cullingObjects[uIndex].colliders[c].bounds.center+cullingObjects[uIndex].colliders[c].transform.position, myColliders[i].bounds.center
						+ myColliders[i].transform.position) > breakDistance[i])
					{
						return true;
					}
				}
			}
			
			if(cullingObjects.Count > 0 && cullingObjects[uIndex].colliders.Length > 0)
				Debug.Log("Broken Culling Distance. Removing " + cullingObjects[uIndex].colliders[0].name + "   from " + TransformUtils.HierarchyPath(gameObject.transform,5));
			return false;
		}
		
	    
		private void OnTriggerExit(Collider other)
		{
			//if object is to be culled
			if(((boundsLayer.value & 1<< other.gameObject.layer) != 0))
			{
				//get the objects
				CullingObject cullingObject = getCullingObjectByCollider(other);
				
				//check if the other collders are still inside?
				//	do nothing
				//else
				//	remove from list
				
				//check if it has moved out of all colliders?
				
				/*//check if colliders already registered
				CullingObject existingObject = getCullingObjectByCollider(other);
					
				//does the collider exist?
				if(existingObject != null)
				{
					//turn them back on
					existingObject.EnableRenderers(true);
						
					//remove from list
					cullingObjects.Remove(existingObject);
				}*/
				
				/*/check all culling Objets
				for(int i = 0; i < cullingObjects.Count; i++)
				{
					//does the collider match?
					if(cullingObjects[i].collider == other)
					{
						//turn them back on
						cullingObjects[i].EnableRenderers(true);
						
						//remove from list
						cullingObjects.RemoveAt(i);
						
						//dont skip the next one, now that the list is shorter
						i--;
					}
				}*/
					
				//MeshRenderer[] newMeshes = other.GetComponentsInChildren<MeshRenderer>();
			
				/*for(int i = 0 ;i < newMeshes.Length; i++)
				{
					if(((ignoreLayer.value & 1<< newMeshes[i].gameObject.layer) == 0))
						newMeshes[i].enabled = true;
					meshesCulling.Remove(newMeshes[i]);
					//if(manager)
					//	manager.RemoveMeshes(new MeshRenderer[1] {newMeshes[i]});
				}*/
				//if(Debugging) Debug.Log("Removing " + other.gameObject.name + "'s " + newMeshes.Length + " meshes from culling");
			
			}
			
		}
		
		public bool PlayerIsInATrigger()
		{
			for(int i = 0 ; i < cullingTriggers.Count; i++)
			{
				if(cullingTriggers[i].PlayerIsIn)
					return true;
			}
			return false;
		}
		
		public void TurnOnMeshes(bool isOn)
		{
			if(Debugging) Debug.Log("turning " + cullingObjects.Count + " meshes " + (isOn?"on":"off"));
			for(int i = 0 ;i < cullingObjects.Count; i++)
			{
				while(cullingObjects[i] == null)
				{
					cullingObjects.RemoveAt(i);
					
					if(i == cullingObjects.Count)
						return;
				}
				
				cullingObjects[i].EnableRenderers(isOn);
			}
		}
		
		public void RemoveCullingObject(CullingObject cullingObject)
		{
			if(cullingObjects.Contains(cullingObject))
				cullingObjects.Remove(cullingObject);
			
		}
		
	}
	
}
