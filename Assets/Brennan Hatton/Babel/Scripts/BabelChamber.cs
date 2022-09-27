using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.TrainCarts;

namespace BrennanHatton.LibraryOfBabel
{
	public class BabelChamber : MonoBehaviour
	{
		public CartSeed cartSeed;
		public List<BabelBookcase> bookcases = new List<BabelBookcase>();
		BookPosition position = new BookPosition();
		
		public CartTitle title;
		
		void Reset()
		{
			cartSeed = this.GetComponentInParent<CartSeed>();
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    if(!cartSeed) cartSeed = this.GetComponentInParent<CartSeed>();
		    
		    position.room  = cartSeed.seed;
		    title.text.text = cartSeed.seed;
	    }
	    
		public void SetupBookShelves()
		{
			
			position.room  = cartSeed.seed;
			title.text.text = cartSeed.seed;
			
			for(int i = 0; i < bookcases.Count; i++)
			{
				position.wall = i+1;
				bookcases[i].Setup(position);
			}
		}
	
	    // Update is called once per frame
	    void Update()
	    {
	    }
	}
}