using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.TrainCarts
{
	
	public class TrainCartManager : MonoBehaviour
	{
		public TrainCartGenerator generator;
		public GameObject endCarriage;
		
		public int totalTrainCarts = 3;
		public List<TrainCart> carts = new List<TrainCart>();
		
		float position = 0;
		
		int roomId = 0;
		
	    // Start is called before the first frame update
	    void Start()
		{
			
		    for(int i =0 ;i < totalTrainCarts; i++)
		    {
		    	AddCartToEnd();
		    }
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
		    if(carts[1].playerInside)
		    {
		    	DestroyObject(carts[0].gameObject);
		    	carts.RemoveAt(0);
		    	
		    	AddCartToEnd();
		    }
	    }
	    
		void AddCartToEnd()
		{
			bool first = (carts.Count == 0);
				
			TrainCart newCart;
			
			newCart = generator.CreateCart(roomId);
			roomId++;
			
			//if(first)
			//	newCart - make itclose door
		    	
			newCart.transform.position += transform.forward*position;
		    	
			position += newCart.length;
			
			endCarriage.transform.position = transform.position + transform.forward*position;
			carts.Add(newCart);
		}
	}
}