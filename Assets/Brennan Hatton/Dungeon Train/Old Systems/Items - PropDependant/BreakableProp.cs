#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BrennanHatton.Props;
using BNG;

[RequireComponent(typeof(Prop))]
[RequireComponent(typeof(OnCollisionGrabbableEvent))]
public class BreakableProp : MonoBehaviour
{
	public Prop prop;
	public PropType brokenVaseEffect;
	public PropType brokenVaseEffect2;
	public PropType brokenVases;
	public PropPlacer lootPlacer;
	float setupTime = 2f;
	bool setupDone = false;
	bool broken = false;
	public Grabbable grabbable;
	
	void Reset()
	{
		lootPlacer = this.GetComponentInChildren<PropPlacer>(true);
		grabbable = this.GetComponent<Grabbable>();
		prop = this.GetComponent<Prop>();
	}
	
    // Start is called before the first frame update
    void Start()
    {
	    if(!prop) prop = this.GetComponent<Prop>();
	    OnCollisionEvent collisionEvent = this.GetComponent<OnCollisionEvent>();
	    collisionEvent.onCollisionEnterEvent.AddListener(() => Break());
	    
	    if(!grabbable)
	    	grabbable = this.GetComponent<Grabbable>();
    }
    
	void OnEnable()
	{
		broken = false;
		
		setupDone = false;
		
		if(lootPlacer)
			lootPlacer.ReturnProps();
			
		StartCoroutine(SetupTimer());
	}
	
	IEnumerator SetupTimer()
	{
		yield return new WaitForSeconds(setupTime);
		setupDone = true;
	}
    
	public void Break()
	{
		
		if(!setupDone)
			return;
			
		if(broken)
			return;
		
		broken = true;
		
			
		if(grabbable != null)
			grabbable.DropItem(true, true);	
		
		if(brokenVaseEffect != null)
		{
			Prop effect = brokenVaseEffect.GetProp();
			
			
			effect.Place(null);
			effect.transform.position = this.transform.position;
			
			if(grabbable != null && grabbable.BeingHeld)
			{
				effect.transform.SetParent(grabbable.transform.parent);//(grabbable.GetOriginalParnet());
			}
		}
		
		if(brokenVaseEffect2 != null)
		{
			Prop effect = brokenVaseEffect2.GetProp();
			effect.Place(null);
			effect.transform.position = this.transform.position;
			
			if(grabbable != null  && grabbable.BeingHeld)
			{
				effect.transform.SetParent(grabbable.transform.parent);//(grabbable.GetOriginalParnet());
			}
		}
		
		if(brokenVases != null)
		{
			Prop brokenVase = brokenVases.GetProp();
			brokenVase.Place(null);
			brokenVase.transform.position = this.transform.position;
			
			if(grabbable != null  && grabbable.BeingHeld)
			{
				brokenVase.transform.SetParent(grabbable.transform.parent);//(grabbable.GetOriginalParnet());
			}
		}
		
		if(lootPlacer != null)
		{
			lootPlacer.ReturnProps();
			Prop[] props = lootPlacer.Place();
			
			//needs to reassign context here for loot placed
			
			if(grabbable != null  && grabbable.BeingHeld)
			{
				for(int i = 0; i < props.Length; i++)
				{
					props[i].transform.SetParent(grabbable.transform.parent);//(grabbable.GetOriginalParnet());
				}
			}
		}
		
		prop.ReturnToPool();
	}
	
    
}
#endif