using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BrennanHatton.TrainCarts
{
	[System.Serializable]
	public class StructuralElement<T>
	{
		[HideInInspector]
		public T model;
		public Transform position;	
		public bool done = false;
		
		/*public delegate void OnStructureCreated(Transform position);
		public List<OnStructureCreated> onStructureCreated = new List<OnStructureCreated>();// = DelegateMethod;*/
		
		public void SetModel(Transform newModel, Transform parent)
		{
			newModel.position = position.position;
			newModel.rotation = position.rotation;
			newModel.SetParent(position);
			
			model = newModel.GetComponent<T>();
			done = true;
			
			/*Debug.Log(onStructureCreated.Count);
			for(int i = 0 ; i < onStructureCreated.Count; i++)
			onStructureCreated[i](position);*/
		}
	}
	
	public class TrainCartStructure : MonoBehaviour
	{
		public StructuralElement<Transform>[] floors, roofs, walls;//, wallLeft2, wallRight, wallRight2;
		
		public StructuralElement<TrainDoorWall> entranceDoor, exitDoor;
		
		public GameObject[] Contents;
		public GameObject[] LOD;
		
		[Range(0,1)]
		public float windowChance = 0;
		
		public float length = 5;
	
		public void SetPlayerInside()
		{
			exitDoor.model.CloseDoor();
		}
		
		public void PopulateContents()
		{
			for(int i = 0; i < Contents.Length; i++)
			{
				Contents[i].SetActive(true);
			}
			for(int i = 0; i < LOD.Length; i++)
			{
				LOD[i].SetActive(false);
			}
		}
		
	}

}