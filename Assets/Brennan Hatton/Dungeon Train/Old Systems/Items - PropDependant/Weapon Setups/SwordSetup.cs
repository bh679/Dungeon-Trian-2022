#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using BrennanHatton.Props;
using RootMotion;//for comments
using UnityEditor;

[ExecuteInEditMode]
public class SwordSetup : WeaponSetup
{
	#if UNITY_EDITOR
	
	protected override void Reset()
	{
		handleDefaultPosition = new Vector3(0.012f, 0.112f, -0.01f);
		swingControllerName = "SwingController";
		base.Reset();
	}
	
	protected override void AddSelfTo(GameObject target)
	{
		target.AddComponent<SwordSetup>();
	}
	
	protected override void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawLine(transform.position-transform.up*0.25f,transform.position+transform.up*2f);
		Gizmos.DrawLine(transform.position+transform.up * 0.25f - transform.forward*0.2f,transform.position+transform.up * 0.25f + transform.forward*0.2f);
	
		base.OnDrawGizmosSelected();
	}
	
	protected override void ColliderSetup()
	{
		BoxCollider boxCollider = this.GetComponent<BoxCollider>();
		
		if(boxCollider == null)
			boxCollider = this.gameObject.AddComponent<BoxCollider>();
		if(meshFilter != null)
		{
			
			Vector3 size = new Vector3((meshFilter.sharedMesh.bounds.size.x/2)*meshFilter.transform.localScale.x,
				meshFilter.sharedMesh.bounds.size.y*meshFilter.transform.localScale.y,
				meshFilter.sharedMesh.bounds.size.z*meshFilter.transform.localScale.z);
			
			Vector3 center = new Vector3(meshFilter.sharedMesh.bounds.center.x*meshFilter.transform.localScale.x,
				meshFilter.sharedMesh.bounds.center.y*meshFilter.transform.localScale.y,
				meshFilter.sharedMesh.bounds.center.z*meshFilter.transform.localScale.z)+meshOffset;
				
			if(Quaternion.Angle(meshFilter.transform.rotation, transform.rotation) == 90)
			{
				float tmp = size.x;
				size.x = size.z;
				size.z = tmp;
				
				tmp = center.x;
				center.x = center.z;
				center.z = tmp;
			}
			
			
			
			size.x = size.x/4f;
			size.z = size.z/3f;
				
			boxCollider.size = size;
			boxCollider.center = center;// - meshFilter.transform.position + transform.position;
		
			
			if(SwingController)
			{
				BoxCollider sliceBoxcollider = SwingController.GetComponentInChildren<BoxCollider>();
			
				Vector3 sliceSize = sliceBoxcollider.size;
				Vector3 sliceCenter = sliceBoxcollider.center;
				
				float bottom = 0.25f;
				float top = center.y+size.y/2;
				
				if(meshFilter.transform.localScale.y < 0)
				{
					top = center.y-size.y/2;
				}
				
				sliceSize.y = (top-bottom);
				sliceCenter.y = (top + bottom)/2f;
				sliceSize.x = sliceSize.x/2;
				sliceSize.z = sliceSize.z/2;
				
				sliceBoxcollider.size = sliceSize;
				sliceBoxcollider.center = sliceCenter;
		
			}
			
		}
	}
	
	#endif
}
#endif