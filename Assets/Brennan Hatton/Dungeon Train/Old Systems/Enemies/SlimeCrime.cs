using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Characters;
using BrennanHatton.Props;
using BrennanHatton.Scoring;

[RequireComponent(typeof(TweenMoveTowardsTarget))]
[RequireComponent(typeof(TweenScale))]
[RequireComponent(typeof(TweenLookAt))]
[RequireComponent(typeof(CharacterAIColliderTargetFinder))]
[RequireComponent(typeof(IdleWalkTarget))]
//[RequireComponent(typeof(MeleeDamage))]
public class SlimeCrime : MonoBehaviour
{
	public Transform targetOffset;
	Transform target;
	
	public CharacterAIColliderTargetFinder targetFinder;
	public TweenLookAt rotator;
	public TweenScale scaler;
	public TweenMoveTowardsTarget mover;
	public IdleWalkTarget idleTargetter;
	public AttackTimer attackTimer;
	//MeleeDamage melee;
	//RangeAttack range;
	
	void Reset()
	{
		if(mover == null)
			mover = this.GetComponentInChildren<TweenMoveTowardsTarget>();
		if(mover != null)
		{
			mover.ease = TweenCurvesType.EaseInOut;
			mover.objectToMove = this.transform;
			mover.duration = 1;
			mover.delay = 0.1f;
			mover.distance = 0.2f;
			mover.loop = true;
		}
		
		if(scaler == null)
			scaler = this.GetComponentInChildren<TweenScale>();
		if(scaler != null)
		{
			scaler.objectToScale = this.transform;
			scaler.scale = new Vector3(1.1f, 1.1f, 1.5f);
			scaler.ease = TweenCurvesType.EaseInOut;
			scaler.duration = 1f;
			scaler.delay = 0.1f;
		}
		
		if(rotator == null)
			rotator = this.GetComponentInChildren<TweenLookAt>();
		if(rotator != null)
		{
			rotator.objectToLook = this.transform;
			rotator.ease = TweenCurvesType.EaseLinear;
			rotator.duration = 0.1f;
			rotator.delay = 0f;
			rotator.contiunous = true;
			rotator.up = Vector3.up;
		}
		
		if(idleTargetter == null)
			idleTargetter = this.GetComponentInChildren<IdleWalkTarget>();
		
		if(targetFinder == null)
			targetFinder = this.GetComponentInChildren<CharacterAIColliderTargetFinder>();
		if(targetFinder != null)
		{
			targetFinder.colliderLayers = 1 << LayerMask.NameToLayer("aiVisibility");
		}
		
		if(attackTimer == null)
			attackTimer = this.GetComponentInChildren<AttackTimer>();
		//melee = this.GetComponent<MeleeDamage>();
		//range = this.GetComponent<RangeAttack>(); 
		
	}
	
	public void Place()
	{
		if(idleTargetter == null)
			idleTargetter = this.GetComponent<IdleWalkTarget>();
		idleTargetter.Place();
	}
	
    // Start is called before the first frame update
    void Start()
	{
		Prop prop = this.GetComponent<Prop>();
		prop.OnPlace.AddListener(() => {Place();});
		
		if(mover == null)
			mover = this.GetComponentInChildren<TweenMoveTowardsTarget>();
		
		if(scaler == null)
			scaler = this.GetComponentInChildren<TweenScale>();
		
		if(rotator == null)
			rotator = this.GetComponentInChildren<TweenLookAt>();
		
		if(targetFinder == null)
			targetFinder = this.GetComponentInChildren<CharacterAIColliderTargetFinder>();
		
		if(idleTargetter == null)
			idleTargetter = this.GetComponentInChildren<IdleWalkTarget>();
		
		if(attackTimer == null)
			attackTimer = this.GetComponentInChildren<AttackTimer>();
		//melee = this.GetComponent<MeleeDamage>();
		//range = this.GetComponent<RangeAttack>();
        
		mover.target = targetOffset;
		rotator.target = targetOffset;
		if(attackTimer != null)
		{
			attackTimer.target = target;
			attackTimer.targetFeet = targetOffset;
		}
		/*if(melee != null)
			melee.targetFeet = targetOffset;
		if(range != null)
		{
			range.target = target;
			range.targetFeet = targetOffset;
		}*/
	}

    // Update is called once per frame
    void Update()
    {
	    target = targetFinder.GetTarget();
	    
	    if(target == null)
	    {
		    //do idle
		    targetOffset.position = idleTargetter.GetTarget();
	    }
	    else
	    {
		    targetOffset.position = new Vector3(target.position.x, targetOffset.position.y, target.position.z);
			
	    }
	    if(attackTimer != null)
		    attackTimer.target = target;
	    /*if(melee != null)
	    	melee.target = target;
	    if(range != null)
	    range.target = target;*/
    }
    
    
	/*public override void Remove()
	{
		DestroyObject(this.gameObject);
	}*/
	
	public void JumpBack()
	{
		transform.position = this.transform.localPosition - Vector3.forward*attackTimer.range;
	}
	
}
