
#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using BrennanHatton.Props;
using RootMotion;//for comments
using UnityEditor;

[ExecuteInEditMode]
public class AxeSetup : WeaponSetup
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
		target.AddComponent<AxeSetup>();
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
		CapsuleCollider coverCollider = this.GetComponent<CapsuleCollider>();
		
		if(coverCollider == null)
			coverCollider = this.gameObject.AddComponent<CapsuleCollider>();
		if(meshFilter != null)
		{
			
			Vector3 size = new Vector3((meshFilter.sharedMesh.bounds.size.x/2)*meshFilter.transform.localScale.x,
				meshFilter.sharedMesh.bounds.size.y*meshFilter.transform.localScale.y,
				meshFilter.sharedMesh.bounds.size.z*meshFilter.transform.localScale.z);
			
			//size.z = 0.007f;
			Vector3 meshCenter = new Vector3(meshFilter.sharedMesh.bounds.center.x*meshFilter.transform.localScale.x,
				meshFilter.sharedMesh.bounds.center.y*meshFilter.transform.localScale.y,
				meshFilter.sharedMesh.bounds.center.z*meshFilter.transform.localScale.z);
			
			Vector3 coverColcenter = new Vector3(0,
				meshCenter.y,
				0)+meshOffset;
				
			if(Quaternion.Angle(meshFilter.transform.rotation, transform.rotation) == 90)
			{
				float tmp = size.x;
				size.x = size.z;
				size.z = tmp;
				
				tmp = coverColcenter.x;
				coverColcenter.x = coverColcenter.z;
				coverColcenter.z = tmp;
			}
				
			coverCollider.height = size.y;
			coverCollider.radius = 0.02f;
			coverCollider.center = coverColcenter;// - meshFilter.transform.position + transform.position;
		
			
			if(SwingController)
			{
				BoxCollider sliceBoxcollider = SwingController.GetComponentInChildren<BoxCollider>();
			
				Vector3 sliceSize = sliceBoxcollider.size;
				Vector3 sliceCenter = sliceBoxcollider.center;
				
				float bottom = size.y/3;
				float top = size.y-size.y/3;
				
				sliceSize.y = (top-bottom);
				sliceSize.z = size.z;//size.z;
				sliceSize.x = 0.001f;
				sliceCenter.y = (top + bottom)/2f;
				sliceCenter.z = size.z/2f;
				
				sliceBoxcollider.size = sliceSize;
				sliceBoxcollider.center = sliceCenter;
		
			}
			
		}
	}
	
	#endif
}
#endif