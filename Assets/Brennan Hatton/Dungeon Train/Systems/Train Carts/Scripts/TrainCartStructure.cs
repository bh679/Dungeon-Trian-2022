using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.TrainCarts
{
	[System.Serializable]
	public class StructuralElement<T>
	{
		[HideInInspector]
		public T model;
		public Transform position;
		
		public void SetModel(Transform newModel, Transform parent)
		{
			newModel.position = position.position;
			newModel.rotation = position.rotation;
			newModel.SetParent(parent);
			
			model = newModel.GetComponent<T>();
		}
	}
	
	public class TrainCartStructure : MonoBehaviour
	{
		public StructuralElement<Transform>[] floor, roof, wallLeft, wallLeft2, wallRight, wallRight2;
		
		public StructuralElement<TrainDoorWall> entranceDoor, exitDoor;
	}

}