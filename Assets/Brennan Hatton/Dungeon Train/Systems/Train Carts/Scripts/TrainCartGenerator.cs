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
		newObj.transform.localPosition = Vector3.zero;
		
		TrainCart cart = newObj.AddComponent<TrainCart>();
		
		
		int id = Random.RandomRange(0,themes.Length-1);
		cart.SetTheme(themes[id]);
		
		return cart;
	}
	
	public void ResetCart(TrainCart trainCart)
	{
		
	}
}
