using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{

	public class ReturnBook : MonoBehaviour
	{
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	    
		private void OnTriggerEnter(Collider other)
		{
			BabelBook book = other.GetComponent<BabelBook>();
			
			if(book == null)
				return;
			
			BookReturnsManager.Instance.Return(book.data);
				
			Destroy(book.gameObject);
			/*if((layer.value & 1<< other.gameObject.layer) != 0
				|| targets.Contains(other.gameObject))
			{
				if(excludes.Contains(other.gameObject))
				{
					return;
				}
				
				//Debug.Log(other);
				onTriggerEnterEvent.Invoke();
			}*/
		}
	    
		private void OnTriggerStay(Collider other)
		{
			/*if(!useStay)
				return;
				
			if((layer.value & 1<< other.gameObject.layer) != 0
				|| targets.Contains(other.gameObject))
			{
				if(excludes.Contains(other.gameObject))
				{
					return;
				}
				//Debug.Log(other);
				onTriggerStayEvent.Invoke();
			}*/
		}
	    
		private void OnTriggerExit(Collider other)
		{
			
			/*if((layer.value & 1<< other.gameObject.layer) != 0
				|| targets.Contains(other.gameObject))
			{
				if(excludes.Contains(other.gameObject))
				{
					return;
				}
				//Debug.Log(other);
				onTriggerExitEvent.Invoke();
			}*/
		}
	}
}