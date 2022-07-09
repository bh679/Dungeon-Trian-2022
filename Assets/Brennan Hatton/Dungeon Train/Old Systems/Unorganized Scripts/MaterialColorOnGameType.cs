using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Scoring
{
	
	public class MaterialColorOnGameType : MonoBehaviour
	{
		public Color defaultColor = Color.white;
		public Color color = Color.green;
		public GameType gameType;
		public GameTypeManager manager;
		public MeshRenderer renderer;
		
		void Reset()
		{
			renderer = this.GetComponent<MeshRenderer>();
			gameType = this.GetComponent<GameType>();
			manager = this.GetComponent<GameTypeManager>();
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
		    if(manager.gameType == gameType)
			    renderer.material.color = color;
		    else
			    renderer.material.color = defaultColor;
	    }
	}

}