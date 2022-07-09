using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BrennanHatton.Scoring;

public class CoinPouch : MonoBehaviour
{
	
	public float size;
	public ScoringFloat coins;
	public Text coinsUI;
	public float collisionRadius = 0.5f;
	
	void Start()
	{
		
		if(size > 0 && coins.GetScore() > size)
		{
			coins.SetScore(size);
		}
		
		if(coinsUI != null)
		{
			if(size > 0)
				coinsUI.text = coins.GetScore() +  "/" + size;
			else
				coinsUI.text = coins.GetScore().ToString();
		}
	}
	
	
	public void CollectCoins(float amount)
	{
		coins.UpdateScore(amount);
		
		if(size > 0 && coins.GetScore() > size)
		{
			coins.SetScore(size);
		}
		
		if(coinsUI != null)
		{
			if(size > 0)
				coinsUI.text = coins.GetScore() +  "/" + size;
			else
				coinsUI.text = coins.GetScore().ToString();
		}
	}
	
	
}
