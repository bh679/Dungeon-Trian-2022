using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BrennanHatton.ComponentStates;
using BrennanHatton.Utilities;

namespace BrennanHatton.Props.Old
{

	[DisallowMultipleComponent]
	///
	/// This class handles the placement of props from a pool.
	///
	public class Prop : MonoBehaviour
	{
		//Default State - List of components and their default values
		public iState[] getStartStates
		{
			get{
				if(startStates == null) // when would this be null? Surely this needs to be solved differently.
				{
					Debug.LogError("Oh no, states is null. I commented out a line here. Should I be calling StateSetup. I am worried this may set the parent wrong for the TransformState");
					//StateSetup();
				}
				
				return startStates;
			}
		}
		public iState[] startStates;
		
		public Rigidbody rigibody;
		
		//context which this object lies, and where it is kept track of when in game.
		PropPlacer placer;
			
		//list of placers triggered by this prop
		PropPlacer[] subPlacers;
		
		//External event on place
		public UnityEvent OnPlace = new UnityEvent();
		public bool Debugging;

		//Unity Editor Function -- this should only be called by the editor. Check that is the case.
		#if UNITY_EDITOR
		protected virtual void Reset()
		{
			rigibody = this.GetComponent<Rigidbody>();
				
			//add startstate for transform, rigibody
			Initialization();
			//add them to seriazable list - check google first
			
			//check if needs rename
			if(gameObject.name.Contains("GameObject"))
			{
				MeshRenderer renderer = this.GetComponentInChildren<MeshRenderer>();
				if(renderer != null)
					this.gameObject.name = renderer.gameObject.name;
			}
			
			//remove mesh colliders
			MeshCollider[] meshColliders = this.GetComponentsInChildren<MeshCollider>();
			for(int i = 0; i < meshColliders.Length; i++)
			{
				meshColliders[i].gameObject.AddComponent<BoxCollider>();
				DestroyImmediate(meshColliders[i]);
			}
		}
		#endif
		
		//this should only be called once per object per game
		public void Initialization()
		{
			StateSetup();
			
			InitializePreMadeSubPlacers();
		}
		
		void StateSetup()
		{
			//Debug.LogError("StateSetup");
			
			//initialize StartState if not already set
			if(startStates == null)
				startStates = this.GetComponents<iState>();
				
			//add missing states
			List<iState> states = new List<iState>();
			
			TransformState iTrans = this.GetComponent<TransformState>();
			if(iTrans == null)
				iTrans = this.gameObject.AddComponent<TransformState>();
			
			states.Add(iTrans);
			
			if(rigibody == null)
				rigibody = this.GetComponent<Rigidbody>();
				
			if(rigibody != null)
			{
				RigibodyState iRigb = this.GetComponent<RigibodyState>();
				if(iRigb == null)
					iRigb = this.gameObject.AddComponent<RigibodyState>();
				states.Add(iRigb);
			}
			
			//if there are exisitng start states
			if(startStates != null)
			{
				//for all the start states
				for(int i = 0 ; i < startStates.Length; i++)
				{
					//check if it is not null, and not already referenced
					if(startStates[i]!= null && states.Contains(startStates[i]) == false)
					{
						//add it to the new states
						states.Add(startStates[i]);
					}
				}
			}
			
			
			//setup new startstates
			for(int i = 0;i < states.Count; i++)
			{
				//set-up
				states[i].SetState(false);
			}
			
			//set start states
			startStates = states.ToArray();
			
		}
		
		public void Awake()
		{
				
			if(rigibody == null)
				rigibody = this.GetComponent<Rigidbody>();
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
			
		}
		
		//Places this as object in the scene
		public virtual void Place(PropPlacer newPlacer)
		{
			
			//Check its not placing twice
			if(DoublePlaceCheck())
				//exit
				return;
			
			//set new placer
			ChangePlacer(newPlacer);
			
			if(Debugging)
				Debug.Log("<color = red>Placing " + this.gameObject.name + " and turning on " + this.gameObject.GetInstanceID()+"</color>");
			
			//turn on
			this.gameObject.SetActive(true);
			
			if(Debugging)
				Debug.Log(this.gameObject.name  + " " + this.gameObject.GetInstanceID() + " is active = " + this.gameObject.active + this.gameObject.activeSelf + this.gameObject.activeInHierarchy);
				
			//if subplacers not setup
			if(subPlacers == null)//surely this doesnt need to be run every place? Does it?
			{
				Debug.Log("Missing Subplacers " + gameObject.name);
				InitializePreMadeSubPlacers();
			}
				
			if(Debugging)
				Debug.Log(this.gameObject.name  + " " + this.gameObject.GetInstanceID() + " is active = " + this.gameObject.active);
				
			//place all placers
			for(int i = 0; i < subPlacers.Length; i++)
			{
				subPlacers[i].Place();
			}
			
			if(Debugging)
				Debug.Log(this.gameObject.name  + " " + this.gameObject.GetInstanceID() + " is active = " + this.gameObject.active);
				
			//run external events
			OnPlace.Invoke();
			
			
			if(Debugging)
				Debug.Log(this.gameObject.name  + " " + this.gameObject.GetInstanceID() + " is active = " + this.gameObject.active);
		}
		
		//this is for when it is cloned
		//gets all subplacers
		public void InitializePreMadeSubPlacers()
		{
			if(subPlacers != null)
				return;
			
			//get subplacers
			subPlacers = GetPlacersInChildren(this.transform);
		}
		
		/*public void AddSubPlacer(PropPlacer newSubPlacer)
		{
			PropPlacer[] newSubPlacerArray = new PropPlacer[subPlacers.Length + 1];
			
			if(subPlacers == null)
				InitializePreMadeSubPlacers();
				
				
			newSubPlacerArray[0] = newSubPlacer;
			for(int i = 0; i < subPlacers.Length; i++)
			{
				if(subPlacers[i] == newSubPlacer)
					return;
					
				newSubPlacerArray[i+1] = subPlacers[i];
			}
			
			subPlacers = newSubPlacerArray;
		}*/
		
		//finds the placers in the children, but not the subplacers within those placer's props
		PropPlacer[] GetPlacersInChildren(Transform transformTarget)
		{
			//list of placers found to return 
			List<PropPlacer> placersFound = new List<PropPlacer>();
			
			//place on current trasnform
			PropPlacer placerTmp = transformTarget.GetComponent<PropPlacer>();
			
			//if placers is not found
			if(placerTmp == null)
			{
				//for all direct children
				for(int i  = 0; i < transformTarget.childCount; i++)
				{
					//search children
					PropPlacer[] placerArray = GetPlacersInChildren(transformTarget.GetChild(i));
					//add them all to the list
					for(int a = 0; a < placerArray.Length; a++)
					{
						if(placerArray[a].gameObject.activeSelf)
							placersFound.Add(placerArray[a]);
							
						//initlize them (this was not being done before, if this creates errors add why)
						placerArray[a].InitializePreMadePropChildren();
					}
				}
			}
			else //if placer is found
			{
				
				placerTmp.FindCorruptData(true);
				
				//add placer to list
				if(placerTmp.gameObject.activeSelf)
					placersFound.Add(placerTmp);
				placerTmp.InitializePreMadePropChildren();
			}
			
			//return an array of placers found
			return placersFound.ToArray();	
		}
		
	    
		//Objects are reset when duplicated
		public void RestoreStartState() 
		{
			if(startStates == null)//this is a hack becuase of order of operation. This should never need to be called
			{
				Debug.LogError("Missing start states on: " + this.gameObject.name);
				StateSetup();//setup start states. - This timing could mean wrong values are stored
				return;
			}
			
			// restores default state
			for(int i = 0;i < startStates.Length; i++)
			{
				startStates[i].RevertToState();
			}
		}
		
		
		//tells all placers to return props
		public void DeleteSubProps()
		{
			//if this prop has subplaces defined
			if(subPlacers != null)
			{
				//return app props for subplacers
				for(int i = 0 ; i < subPlacers.Length; i++)
					subPlacers[i].DeleteProps();
			}
		}
		
		public void ChangePlacer(PropPlacer newPlacer)
		{
			if(placer != null)
				//place should stop tracking
				placer.LosePropReference(this);
				
			placer = newPlacer;
		}
		
		
		float placedThisUpdateTime = 0;
		PropPlacer previousPlacer;
		bool placedThisUpdate = false;
		
		/// <summary>
		/// Check its not placing twice
		/// </summary>
		/// <returns>true is bad, false is good</returns>
		bool DoublePlaceCheck()
		{
			//if it was placed last turn, and it is turned on 
			//(if it is not turned on, it wont be update to update the bool, so this function breaks. We check its activeSelf to address this. So we can trust this error when presented
			if(placedThisUpdate && gameObject.activeSelf == true)
			{
				string debugLocation = gameObject.name + "   " + TransformUtils.HierarchyPath(this.transform,10);
				
				//send error to dev
				Debug.LogError("This prop has been placed. Place was already called this udpate. Time difference: "+(placedThisUpdateTime-Time.time).ToString()+". Disregarding. " + debugLocation + "\n\n Last time it was placed by " + ((previousPlacer == placer)? " the same placer, " : " a different placer, ") + placer.name); 
				
				
				
				//oh no, we got a true
				return true;
			}
			
			previousPlacer = placer;
			//Debug.Log("Placing " + gameObject.name + "   " + TransformUtils.HierarchyPath(this.transform,10) +  "  at " + Time.time);
			
			placedThisUpdate = true;
			placedThisUpdateTime = Time.time;
			return false;
		}
		//let the class know a frame has passed, for the double placer check - maybe redo this based on time.
		public void LateUpdate()
		{
			placedThisUpdate = false;
		}
	}

}
