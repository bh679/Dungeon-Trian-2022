#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using BrennanHatton.Props;
using RootMotion;//for comments
using UnityEditor;

[ExecuteInEditMode]
public class WeaponSetup : MonoBehaviour
{
	#if UNITY_EDITOR
	public Transform Handle, SwingController;
	public Vector3 meshOffset = Vector3.zero;
	public Vector3 handleOffset = Vector3.zero;
	public MeshFilter meshFilter;
	
	protected Vector3 handleDefaultPosition = new Vector3(0.012f, 0.112f, -0.01f);
	protected string swingControllerName = "SwingController";
	protected string swingControllerLocation = "Prefabs/Weapons/";
	
	
	protected virtual void Reset()
	{
		Setup();
	}
	
	protected void Setup()
	{
		//if mesh found on this obj
		if(this.GetComponent<MeshRenderer>() != null)
		{
			GameObject parent = new GameObject();
			parent.transform.SetParent(this.transform.parent);
			parent.name = this.name;
			parent.transform.position = this.transform.position;
			parent.transform.localRotation = Quaternion.identity;
			
			this.transform.SetParent(parent.transform);
			
			AddSelfTo(parent);
			
			
			DestroyImmediate(this);
			return;
		}
		
		//[RequireComponent(typeof(Grabbable))]
		Grabbable grabbable = this.GetComponent<Grabbable>();
		if(!grabbable)
			grabbable = this.gameObject.AddComponent<Grabbable>();
			
		//[RequireComponent(typeof(GrabbableRingHelper))]
		GrabbableRingHelper ringHelper = this.GetComponent<GrabbableRingHelper>();
		if(!ringHelper)
			ringHelper = this.gameObject.AddComponent<GrabbableRingHelper>();
			
		
		//[RequireComponent(typeof(Prop))]
		Prop prop = this.GetComponent<Prop>();
		if(!prop)
			prop = this.gameObject.AddComponent<Prop>();
			
		
		//[RequireComponent(typeof(Rigidbody))]
		Rigidbody rigidbody = this.GetComponent<Rigidbody>();
		if(!rigidbody)
			rigidbody = this.gameObject.AddComponent<Rigidbody>();
			
		
		//[RequireComponent(typeof(Comments))]
		Comments comments = this.GetComponent<Comments>();
		if(!comments)
			comments = this.gameObject.AddComponent<Comments>();
			
		
	
		
		//child objects
		SetupHandle(grabbable);
		
		//check for Swing Controller
		SwingController = transform.Find(swingControllerName);
		
		//-- Add swing controller.
		if(!SwingController)
		{
			Object Prefab = Resources.Load(swingControllerLocation+swingControllerName);
			
			SwingController = (PrefabUtility.InstantiatePrefab(Prefab) as GameObject).transform;
			
			SwingController.SetParent(this.transform);
			SwingController.localPosition = Vector3.zero;
			SwingController.localRotation = Quaternion.identity;
			SwingController.name = swingControllerName;
		}
		
		if(!SwingController)
		{
			Debug.LogError("Could not find "+swingControllerName+" Prefab");
		}
		
		grabbable.Grabtype = HoldType.Toggle;
		grabbable.GrabSpeed = 25f;
		grabbable.CustomHandPose = HandPoseId.Generic;
		grabbable.GrabPoints = new List<Transform>();
		grabbable.GrabPoints.Add(Handle);
		grabbable.BreakDistance = 1000;
		grabbable.RemoteGrabbable = true;
		grabbable.RemoteGrabDistance = 1f;
		grabbable.ParentHandModel = true;
		
		meshFilter = this.GetComponentInChildren<MeshFilter>();
		meshOffset = meshFilter.transform.localPosition;
		
		ColliderSetup();
		
		
		
		
		comments.text = "Box Collider for Slicer should cover blade only.\n Change color & position of particles.";
	}
	
	public void SetupHandle(Grabbable grabbable = null)
	{
		
		if(grabbable == null)
			grabbable = this.GetComponent<Grabbable>();
			
		if(grabbable == null)
			Debug.LogError("No Grabbable Componet on weapon");
		
		//check for handle
		if(grabbable.GrabPoints != null && grabbable.GrabPoints.Count == 1)
			Handle = grabbable.GrabPoints[0];
		
		if(!Handle)
		{
			Handle = transform.Find("Handle");
		}
			
		//  -- make handle
		if(!Handle)
		{
			Handle = (new GameObject()).transform;
			Handle.SetParent(this.transform);
			Handle.name = "Handle";
			//postiion handle
			Handle.localPosition = handleDefaultPosition;
		
			//rotate handle
			Handle.localEulerAngles = new Vector3(34f, 0f, 0f);
		}
		
		handleOffset = Handle.localPosition;
	}
	
	
	protected virtual void AddSelfTo(GameObject target)
	{
		Debug.LogError("'protected virtual void AddSelfTo(GameObject target)' needs an override");
	}
	
	protected virtual void ColliderSetup()
	{
	
	}
	
	public void FixHandle()
	{
		if(!Handle)
		{
			Handle = transform.Find("Handle");
		}
		
		Grabbable grabbable = this.GetComponent<Grabbable>();
		//if(grabbable.GrabPoints == null || grabbable.GrabPoints.Count == 0)
		//{
			grabbable.GrabPoints = new List<Transform>();
			grabbable.GrabPoints.Add(Handle);
		//}
	}
	
	
	public virtual void Update()
	{
		if(meshFilter != null)
			meshFilter.transform.localPosition = meshOffset;
			
		if(Handle != null)
			Handle.localPosition = handleOffset;
	}
	
	
	protected virtual void OnDrawGizmosSelected()
	{
		if(Handle != null)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(Handle.transform.position,Handle.transform.position+Handle.transform.forward * 0.1f);
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(Handle.transform.position,Handle.transform.position+Handle.transform.up * 0.1f);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(Handle.transform.position,Handle.transform.position+Handle.transform.right * 0.1f);
		}
		
		Gizmos.color = Color.white;
		Gizmos.DrawLine(transform.position-transform.up*0.25f,transform.position+transform.up*2f);
		Gizmos.DrawLine(transform.position+transform.up * 0.25f - transform.forward*0.2f,transform.position+transform.up * 0.25f + transform.forward*0.2f);
	}
	#endif
}
#endif