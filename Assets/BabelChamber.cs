using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.TrainCarts;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelChamber : MonoBehaviour
	{
		public TrainCart cart;
		public AddShelvesToWalls shelves;
		
		public CartTitle title;
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    cart = this.GetComponentInParent<TrainCart>();
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
		    title.text.text = cart.seed;
	    }
	}
}