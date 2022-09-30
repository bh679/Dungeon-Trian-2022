using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using BrennanHatton.Math;

namespace BrennanHatton.TrainCarts
{
	
	public class TrainCartManager : MonoBehaviour
	{
		public TrainCartGenerator generator;
		public GameObject endCarriage;
		
		public int totalTrainCarts = 3;
		public List<TrainCart> carts = new List<TrainCart>();
		
		float position = 0;
		
		//int roomId = 0;
		
		public BigInteger roomId;
		public int seedBase = 35;
		public int cartsSharingSeed = 2;
		
		public static TrainCartManager Instance { get; private set; }
		private void Awake() 
		{ 
			// If there is an instance, and it's not me, delete myself.
    
			if (Instance != null && Instance != this) 
			{ 
				Destroy(this); 
			} 
			else 
			{ 
				Instance = this; 
			} 
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
			SetupCarts();
		}
	    
		void SetupCarts()
		{
			
			for(int i =0 ;i < carts.Count; i++)
			{
				DestroyObject(carts[i].gameObject);
			}
			
			carts = new List<TrainCart>();
			
			for(int i =0 ;i < totalTrainCarts; i++)
			{
				AddCartToEnd();
			}
			
			carts[0].PopulateContents();
			carts[1].PopulateContents();
		}
	
		public bool cartChanged = false;
		public bool teleported = false;
	    // Update is called once per frame
	    void Update()
		{
			cartChanged = false;
		    if(carts[1].playerInside)
		    {
		    	DestroyObject(carts[0].gameObject);
		    	carts.RemoveAt(0);
		    	
		    	AddCartToEnd();
			    carts[1].PopulateContents();
			    
			    cartChanged = true;
		    }
	    }
	    
		int iCarts = 0;
		void AddCartToEnd()
		{
			bool first = (carts.Count == 0);
				
			TrainCart newCart;
			
			newCart = generator.CreateCart("0"+MyBigIntegerExtensions.ToRadixString(roomId,seedBase));
			iCarts++;
			if(iCarts == cartsSharingSeed)
			{
				roomId++;
				iCarts = 0;
			}
			//if(first)
			//	newCart - make itclose door
		    	
			newCart.transform.position += transform.forward*position;
		    	
			position += newCart.length;
			
			endCarriage.transform.position = transform.position + transform.forward*position;
			carts.Add(newCart);
		}
		
		public void TeleportToCart(string _seed)
		{
			Debug.Log(_seed);
			roomId = MyBigIntegerExtensions.Parse(_seed, seedBase);
			Debug.Log(roomId);
			string testStr2 = MyBigIntegerExtensions.ToRadixString(roomId, 36);
			Debug.Log(testStr2);
			Debug.Log(string.Compare(_seed, testStr2));
			
			position = carts[0].transform.position.z;
			
			
			SetupCarts();
			teleported = true;
			
			StartCoroutine(teleportedFalse());
		}
		
		IEnumerator teleportedFalse()
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			
			teleported = false;
		}
	}
}