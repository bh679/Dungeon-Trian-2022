
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;
#if BNG
using BNG;
#endif

namespace BrennanHatton.Scoring
{

	public class Food : MonoBehaviour
	{
		public Prop prop;
		public float value = 1;
		float setupTime = 1f;
		bool setupDone = false;
#if BNG
		public Grabbable grabbable;
#endif
		
		public GameObject eatingEffect;
		
		void Reset()
		{
			prop = this.GetComponent<Prop>();		
#if BNG
			grabbable = this.GetComponent<Grabbable>();
#endif
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
			if(prop == null)
				prop = this.GetComponent<Prop>();
			
			OnCollisionEvent collisionEvent = this.GetComponent<OnCollisionEvent>();
			if(collisionEvent != null)
				collisionEvent.onCollisionEnterEvent.AddListener(() => Eat());
				
#if BNG
			if(grabbable == null)
				grabbable = this.GetComponent<Grabbable>();
#endif
	    }
	    
		void OnEnable()
		{
			setupDone = false;
			
			StartCoroutine(SetupTimer());
		}
	
		IEnumerator SetupTimer()
		{
			yield return new WaitForSeconds(setupTime);
			setupDone = true;
		}
    
	    
		public void Eat()
		{
			if(!setupDone)
				return;
			
#if BNG
			if(grabbable != null)
				grabbable.DropItem(true, true);
#endif
			
			eatingEffect.transform.position = this.transform.position;
			eatingEffect.SetActive(true);
			
			prop.DestoryProp();
		}
	}

}