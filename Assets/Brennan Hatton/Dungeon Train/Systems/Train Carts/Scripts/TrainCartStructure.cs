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
		public StructuralElement<Transform>[] floors, roofs, walls, contents;//, wallLeft2, wallRight, wallRight2;
		
		public StructuralElement<TrainDoorWall> entranceDoor, exitDoor;
		
		public Transform[] contentsTypes;
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
			StartCoroutine(SetContents());
			
			for(int i = 0; i < LOD.Length; i++)
			{
				LOD[i].SetActive(false);
			}
		}
		
		IEnumerator SetContents()
		{
			for(int i = 0; i < contents.Length; i++)
			{
				contents[i].SetModel(Instantiate(contentsTypes[Random.Range(0,contentsTypes.Length)]).transform, this.transform);
				contents[i].model.transform.localScale = Vector3.one;
				contents[i].model.gameObject.SetActive(true);
				yield return new WaitForFixedUpdate();
			}
			
			yield return null;
		}
	}

}