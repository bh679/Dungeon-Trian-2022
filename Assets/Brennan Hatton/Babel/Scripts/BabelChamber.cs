using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.TrainCarts;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelChamber : MonoBehaviour
	{
		public TrainCart cart;
		public List<BabelBookcase> bookcases = new List<BabelBookcase>();
		BookPosition position;
		
		public CartTitle title;
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    cart = this.GetComponentInParent<TrainCart>();
		    
		    position.room  = cart.seed;
	    }
	    
		public void SetupBookShelves()
		{
			
			position.room  = cart.seed;
			for(int i = 0; i < bookcases.Count; i++)
			{
				position.wall = i+1;
				bookcases[i].Setup(position);
			}
		}
	
	    // Update is called once per frame
	    void Update()
	    {
		    title.text.text = cart.seed;
	    }
	}
}