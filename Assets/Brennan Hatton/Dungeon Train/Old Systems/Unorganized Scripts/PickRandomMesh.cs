using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRandomMesh : MonoBehaviour
{
	public Mesh[] meshes;
	public bool onEnable = true;
	MeshFilter filter;
	
    // Start is called before the first frame update
	void OnEnable()
	{
		if(onEnable)
			SetRandomMesh();
	}
    
	public void SetRandomMesh()
	{
		if(filter == null)
			filter = this.GetComponent<MeshFilter>();
			
		if(filter == null)
			return;
	    	
		filter.sharedMesh = meshes[Random.Range(0,meshes.Length*10) % meshes.Length];
	}
}
