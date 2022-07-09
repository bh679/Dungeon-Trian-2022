using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton
{

	public class SunToShader : MonoBehaviour
	{
		public MeshRenderer renderer;
		public SkinnedMeshRenderer skinedRenderer;
		public string colorValueName = "_TintColor";
		public float SunSetStart = 0.01f;
		public float maxTransparency = 1f;
		public Transform sun;
		
		// Start is called before the first frame update
		void Reset()
		{
			renderer = this.GetComponent<MeshRenderer>(); 
			if(renderer != null)
			{
				Color color = renderer.sharedMaterial.GetColor(colorValueName);
				maxTransparency = color.a;
			}else
			{
				skinedRenderer = this.GetComponent<SkinnedMeshRenderer>(); 
				if(skinedRenderer != null)
				{
					Color color = skinedRenderer.sharedMaterial.GetColor(colorValueName);
					maxTransparency = color.a;
				}
			}
		}
	
		// Update is called once per frame
		void Update()
		{
			if(sun.transform.forward.y >= -SunSetStart)
			{
				float alpha = Mathf.Max(0,Mathf.Min(1,sun.transform.forward.y/-SunSetStart)) * maxTransparency;
				Color color;
				
				if(renderer != null)
				{
					color= renderer.sharedMaterial.GetColor(colorValueName);
					color.a = alpha;
					renderer.material.SetColor(colorValueName, color);
				}
				else if(skinedRenderer != null)
				{
					color= skinedRenderer.sharedMaterial.GetColor(colorValueName);
					color.a = alpha;
					skinedRenderer.material.SetColor(colorValueName, color);
				}
				
			}
		}
	}

}