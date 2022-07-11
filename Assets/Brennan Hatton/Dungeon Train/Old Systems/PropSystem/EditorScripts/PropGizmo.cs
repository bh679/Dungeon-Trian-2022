#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;

namespace BrennanHatton.Props.Old.Editor
{

	[DisallowMultipleComponent]
	[RequireComponent(typeof(Prop))]
	public class PropGizmo : MonoBehaviour
	{
		[System.Serializable]
		public class MeshGizmoData
		{
			public Mesh mesh;
			public Vector3 localPosition = Vector3.zero;
			public Quaternion localRotation = Quaternion.identity;
			public Vector3 lossyScale = Vector3.one;
			public List<Material> materials = new List<Material>();//Graphics.DrawMeshInstanced
		}
		
		public MeshGizmoData[] meshs;
		
		public PlacerGizmo[] placers;
		
		void Reset()
		{
			Setup();
		}
		
		public int missingPlacers = 0;
		public void Setup()
		{
			//find all mesh children and make data
			MeshFilter[] filters = this.gameObject.GetComponentsInChildren<MeshFilter>();
			
			meshs = new MeshGizmoData[filters.Length];
			
			for(int i =0 ; i < filters.Length; i++)
			{
				meshs[i] = new MeshGizmoData();
				meshs[i].mesh = filters[i].sharedMesh;
				meshs[i].localPosition = filters[i].transform.position - this.transform.position;
				meshs[i].localRotation = filters[i].transform.rotation * Quaternion.Inverse(this.transform.rotation);
				meshs[i].lossyScale = filters[i].transform.lossyScale;
				MeshRenderer meshren = filters[i].gameObject.GetComponent<MeshRenderer>();
				if(meshren != null)
					meshren.GetSharedMaterials(meshs[i].materials);
				//meshs[i].localPosition = filters[i].transform.localPosition;
				//TODO Add rotation and position - figure out how to factor in heirarchy
			}
			
			Prop prop;
			prop = this.gameObject.GetComponent<Prop>();
			if(prop == null)
			{
				Debug.LogError("PropGizmo does not have prop: " + gameObject.name);
				return;
				
			}
				
			PropPlacer[] subProps = prop.GetComponentsInChildren<PropPlacer>();
			List<PropPlacer> subPropList = new List<PropPlacer>();
			
			for(int i = 0; i < subProps.Length; i++)
			{
				if(subProps[i] != prop)
					subPropList.Add(subProps[i]);
			}
			
			subProps = subPropList.ToArray();
					
			placers = new PlacerGizmo[subProps.Length];
			missingPlacers = 0;
			for(int p  = 0; p+missingPlacers < subProps.Length; p++)
			{
				
				if(subProps[p] != null)
				{
					placers[p] = subProps[p].GetComponent<PlacerGizmo>();
					
					if(placers[p] == null)
					{
						placers[p] = subProps[p].gameObject.AddComponent<PlacerGizmo>();
						placers[p].Setup();
					}else
						placers[p].GetProps();
				}else
				{
					Debug.LogError("Missing subProp Ref " + prop.name);
					p--;
					missingPlacers++;
				}
			}
		}
		
		public void DrawGizmos(Vector3 position, Quaternion rotation, bool color)
		{
			
			bool drawn = false;
				
			Vector3 m_position, m_scale;
			Quaternion m_rotation;
				
			for(int i = 0; i < meshs.Length; i++)
			{
				m_position = rotation*meshs[i].localPosition + position;
				m_rotation = meshs[i].localRotation * rotation;
				m_scale = meshs[i].lossyScale;
				
				if(color && meshs[i].materials != null && meshs[i].materials.Count > 0)
				{
					if(meshs[i].materials[0].enableInstancing)
					{
						Matrix4x4 matrix = new Matrix4x4();
						matrix.SetTRS(m_position, m_rotation, m_scale);
					
						Graphics.DrawMeshInstanced(meshs[i].mesh, 0, meshs[i].materials[0], new Matrix4x4[1] {matrix},1,new MaterialPropertyBlock(), UnityEngine.Rendering.ShadowCastingMode.Off, false, 11);
						drawn = true;
					}
				}
				
				if(!drawn)
					Gizmos.DrawMesh(meshs[i].mesh, m_position, m_rotation, m_scale);
					
				drawn = false;
			}
			
			if(placers == null || placers.Length - missingPlacers == 0)
				return;
				
			for(int p = 0; p < placers.Length - missingPlacers; p++)
			{
				placers[p].DrawGizmos(position, rotation, color);
			}
		}
		
	}
}
#endif