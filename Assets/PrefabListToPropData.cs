using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Props.Extensions
{

	[RequireComponent(typeof(PropType))]
	public class PrefabListToPropData : MonoBehaviour
	{
		PropType propType;
		GameObjectList list;
		
		void Reset()
		{
			propType = this.GetComponent<PropType>();
				
			list = this.GetComponent<GameObjectList>();
			
			for(int i = 0; i < list.gameObjects.Length; i++)
			{
				PropType.PropData propData = new PropType.PropData();
				
				propData.prop = list.gameObjects[i];
				
				propType.propData.Add(propData);
			}
			
			list.gameObjects = new GameObject[0];
		}
	}

}