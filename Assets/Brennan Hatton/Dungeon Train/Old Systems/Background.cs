using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BrennanHatton.DungeonTrain.Envionments
{

	public class Background : MonoBehaviour
	{
		public int length;
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	    
		void OnDrawGizmosSelected()
		{
			// Draw a yellow sphere at the transform's position
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(transform.position, new Vector3(1,1,length));
		}
	    
	}

}