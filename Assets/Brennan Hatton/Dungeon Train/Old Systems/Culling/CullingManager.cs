using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BrennanHatton.Culling
{

	public class CullingManager : MonoBehaviour
	{
		public CullingTrigger[] cullingTriggers = new CullingTrigger[0];
		
		void Reset()
		{
			cullingTriggers = this.GetComponentsInChildren<CullingTrigger>();
		}
		
		public void ResetCulling()
		{
			for(int i = 0; i < cullingTriggers.Length; i++)
			{
				cullingTriggers[i].ResetCulling();
			}
		}
		
		public void RemoveCullingObjects(CullingObject[] meshesToRemove, bool enabled)
		{
			for(int i = 0; i < meshesToRemove.Length; i++)
				meshesToRemove[i].EnableRenderers(enabled);
			
			
			for(int t = 0; t < cullingTriggers.Length; t++)
				for(int b = 0; b < cullingTriggers[t].cullingBounds.Length; b++)
				{
					if(!cullingTriggers[t].cullingBounds[b].priorityCuller)
					{
						for(int m = 0; m < meshesToRemove.Length; m++)
						{
							cullingTriggers[t].cullingBounds[b].RemoveCullingObject(meshesToRemove[m]);
						}
					}
				}
		}
		
		public void RemoveCullingObject(CullingObject meshToRemove, bool enabled)
		{
			meshToRemove.EnableRenderers(enabled);
			
			for(int t = 0; t < cullingTriggers.Length; t++)
				for(int b = 0; b < cullingTriggers[t].cullingBounds.Length; b++)
					if(!cullingTriggers[t].cullingBounds[b].priorityCuller)
						cullingTriggers[t].cullingBounds[b].RemoveCullingObject(meshToRemove);
		}
	}

}