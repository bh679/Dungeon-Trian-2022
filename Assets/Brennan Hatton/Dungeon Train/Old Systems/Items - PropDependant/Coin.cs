#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;
using BNG;

[RequireComponent(typeof(Prop))]
[RequireComponent(typeof(Rigidbody))]
public class Coin : MonoBehaviour
{
	public int value = 1;
	public LayerMask playerLayer;
	bool held, inBody;
	public GameObject storedEffect;
	Prop prop;
	public CoinPouch wallet;
	public Transform targetPointerPivot;
	public LineRenderer pointerLine;
	public Grabbable grabbable;
	
	void Reset()
	{
		wallet = FindObjectOfType<CoinPouch>();
		pointerLine = this.GetComponentInChildren<LineRenderer>();
		if(pointerLine.transform.parent != this.transform)
			targetPointerPivot = pointerLine.transform.parent;
		else
			targetPointerPivot = pointerLine.transform;
		grabbable = this.GetComponent<Grabbable>();
	}
	
	void Start()
	{
		prop = this.GetComponent<Prop>();
		
		if(wallet == null)
		{
			wallet = FindObjectOfType<CoinPouch>();
		}
		
		if(pointerLine == null)
		{
			
			pointerLine = this.GetComponentInChildren<LineRenderer>();
			if(pointerLine.transform.parent != this.transform)
				targetPointerPivot = pointerLine.transform.parent;
			else
				targetPointerPivot = pointerLine.transform;
		}
		
		if(grabbable == null)
			grabbable = this.GetComponent<Grabbable>();
	}
	
	void OnEnable()
	{
		pointerLine.gameObject.SetActive(false);
	}
	
	public void setHeld(bool _isHeld)
	{
		held = _isHeld;
	}
    
	public void Store()
	{
		//grabbable release
		if(grabbable != null)
			grabbable.DropItem(true, true);
		
		storedEffect.transform.position = this.transform.position;
		storedEffect.SetActive(true);
		
		if(wallet != null)
			wallet.CollectCoins(value);
		
		prop.DestoryProp();
		
		
	}
	
	void Update()
	{
		if(held)
		{
			float distanceToCollision = Vector3.Distance(this.transform.position,wallet.transform.position) - wallet.collisionRadius;
			
			if(pointerLine != null)
			{
				targetPointerPivot.transform.LookAt(wallet.transform);
				
				if(distanceToCollision > 0)
				{
					pointerLine.gameObject.SetActive(true);
					pointerLine.SetPosition(1,new Vector3(0,0,distanceToCollision - pointerLine.transform.localPosition.z));
				}
				else
					pointerLine.gameObject.SetActive(false);
				
			}
				
			
			
			
			if(distanceToCollision <= 0)
			{
				pointerLine.gameObject.SetActive(false);
				Store();
			}
		}else
			pointerLine.gameObject.SetActive(false);
	}
}

#endif