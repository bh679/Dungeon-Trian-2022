using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCartManager : MonoBehaviour
{
	public TrainCartGenerator generator;
	
	public int totalTrainCarts = 3;
	public TrainCart[] carts;
	
	float position = 0;
	
    // Start is called before the first frame update
    void Start()
	{
		carts = new TrainCart[totalTrainCarts];
		
	    for(int i =0 ;i < totalTrainCarts; i++)
	    {
	    	carts[i] = generator.CreateCart();
	    	
	    	carts[i].transform.position += transform.forward*position;
	    	
	    	position += carts[i].length;
	    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
