using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCartGenerator : MonoBehaviour
{
	public ArchitectureTheme[] themes;
    
	public TrainCart CreateCart()
	{
		GameObject newObj = new GameObject("Cart");
		newObj.transform.SetParent(this.transform);
		
		TrainCart cart = newObj.AddComponent<TrainCart>();
		
		
		int id = Random.Range(0,themes.Length-1);
		cart.SetTheme(themes[id]);
		
		return cart;
	}
	
	public void ResetCart(TrainCart trainCart)
	{
		
	}
}
