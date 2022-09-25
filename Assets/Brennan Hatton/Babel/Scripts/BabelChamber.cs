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
		public string id;
		
		public CartTitle title;
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    cart = this.GetComponentInParent<TrainCart>();
		    
		    id = cart.seed;
		    //StartCoroutine(SetupShelvesId());
	    }
	    
		public void SetupBookShelves()
		{
			Debug.Log("SetupBookShelves");
			id = cart.seed;
			for(int i = 0; i < bookcases.Count; i++)
			{
				bookcases[i].Setup(id, i);
			}
		}
	    
		/*IEnumerator SetupShelvesId()
		{
			int i = 0;
			while(bookcases.shelvesReady == false)
			{
				yield return new WaitForEndOfFrame();
			}
			id = cart.seed;
			shelves.
			yield return null;
		}*/
	
	    // Update is called once per frame
	    void Update()
	    {
		    title.text.text = cart.seed;
	    }
	}
}