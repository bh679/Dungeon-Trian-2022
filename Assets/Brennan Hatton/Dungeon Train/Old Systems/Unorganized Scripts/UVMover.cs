using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Rendering
{
	public class UVMover : MonoBehaviour
	{
		[System.Serializable]
		public class ReplacementUV
		{
			public Vector2 find,replace;
		}
		
		public ReplacementUV[] replaceUV = new ReplacementUV[0];
		
		Mesh mesh;
		Vector3[] vertices;
		Vector2[] uvs;
		public Vector2[] defaultUV;
		public bool onEnable;
		
	    // Start is called before the first frame update
		void OnEnable()
		{
			if(onEnable)
				ReplaceUVs();
		}
		
		public void ReplaceUVs()
		{
			StartCoroutine(replaceUVs());
		}
		
		public void _replaceUV()
		{
			if(mesh == null)
			{
				
				mesh = GetComponent<MeshFilter>().mesh;
				vertices = mesh.vertices;
				uvs = new Vector2[vertices.Length];
			}
			
			if(defaultUV == null || defaultUV.Length == 0)
				defaultUV = uvs;
				
	
			for (int i = 0; i < uvs.Length; i++)
			{
				for(int r = 0; r < replaceUV.Length; r++)
				{
					if(uvs[i] == replaceUV[r].find)
						uvs[i] = replaceUV[r].replace;
				}
			}
			
			if(replaceUV.Length > 0)
				mesh.uv = uvs;
		}
		
	    
	    
		IEnumerator replaceUVs()
		{
			_replaceUV();
			
			return null;
		}
	
	}
}