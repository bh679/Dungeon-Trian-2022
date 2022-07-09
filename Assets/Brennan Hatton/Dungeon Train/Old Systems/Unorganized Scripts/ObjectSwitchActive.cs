using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Utilities
{
	public class ObjectSwitchActive : MonoBehaviour
	{
		
		public GameObject[] objects;
		
		public int numberToTurnOn = 1;
		
		int activeId = 0;
		
		public bool onEnable = false;
		
		void Reset()
		{
			objects = new GameObject[this.transform.childCount];
			
			for(int i = 0; i < this.transform.childCount; i++)
			{
				objects[i] = transform.GetChild(i).gameObject;
			}
		}
		
		// Start is called before the first frame update
		void Start()
		{
			if(!onEnable)
				Switch();
		}
		
		void OnEnable()
		{
			if(onEnable)
				Switch();
		}
	
	    
		public void Switch()
		{
			for(int i = 0; i < objects.Length; i++)
			{
				objects[i].SetActive(false);
			}
			
			for(int i = 0; i < numberToTurnOn; i++)
			{
				activeId = Random.RandomRange(0,objects.Length); 
				
				objects[activeId].SetActive(true);
			}
		}
	}
}
