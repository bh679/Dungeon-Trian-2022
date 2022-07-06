using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCartGenerator : MonoBehaviour
{
	public TrainCart trainCartPrefab;
	
	public ArchitectureTheme[] themes;
    
	public TrainCart CreateCart()
	{
		TrainCart cart = Instantiate(trainCartPrefab, this.transform.position, this.transform.rotation, this.transform);
		
		int id = Random.RandomRange(0,themes.Length-1);
		cart.SetTheme(themes[id]);
		
		return cart;
	}
}
