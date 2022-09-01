using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Utilities;

namespace BrennanHatton.Props
{

		public class Instantiator : MonoBehaviour
		{
			
			[System.Serializable]
			class InstantiateData
			{
				public TransformData transform;
				public GameObject objectToCreate;
				public string name;
				public Action<GameObject>[] callback;
				
				public void RunCallbacks(GameObject go)
				{
					for(int i =0 ; i < callback.Length; i++)
					{
						callback[i](go);
					}
				}
			}
			List<InstantiateData> objectsToCreate = new List<InstantiateData>();
			
			public static Instantiator Instance { get; private set; }
			private void Awake() 
			{ 
				// If there is an instance, and it's not me, delete myself.
		    
				if (Instance != null && Instance != this) 
				{ 
					Destroy(this); 
				} 
				else 
				{ 
					Instance = this; 
				} 
			}
			
			public void CreateObject(GameObject go, TransformData transData, Action<GameObject>[] callback, string newName = null)
			{
				InstantiateData data = new InstantiateData();
				data.objectToCreate = go;
				data.transform = transData;
				data.name = newName;
				
				objectsToCreate.Add(data);
			}
			
		    // Start is called before the first frame update
		    void Start()
		    {
			    StartCoroutine(createFromList());
		    }
		    
			IEnumerator createFromList()
			{
				while(true)
				{
					yield return new WaitForEndOfFrame();
					
					if(objectsToCreate.Count > 0)
					{
						GameObject go = Instantiate(objectsToCreate[0].objectToCreate);
						
						objectsToCreate[0].transform.ApplyGlobalData(go.transform,true);
						
						if(objectsToCreate[0].name != null)
							go.name = objectsToCreate[0].name;
							
						objectsToCreate[0].RunCallbacks(go);
						
						objectsToCreate.RemoveAt(0);
					}
				}
			}
		}
}
