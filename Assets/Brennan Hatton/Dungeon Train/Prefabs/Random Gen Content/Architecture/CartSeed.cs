using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartSeed : MonoBehaviour
{
	
	public int seedBase;
	public bool dividBy2;
	public string seed;
	//public bool randomStart = true;
	//static string seedStart;
	
	
	//public string seed;
	
	public virtual string ProcessSeed(int intSeed)
	{
		//string seed;
		
		if(dividBy2)
			intSeed = intSeed/2;
		
		seed = ToBase(intSeed,seedBase);
		
		return seed;
	}
		
	string ToBase(int number, int b)
	{
		if (number == 0)
			return "0";
			
		List<char> digits = new List<char>();
		int num;
		while(number > 0)
		{
			num = (char)(number % b);
			if(num < 10)
				digits.Add((char)(48+num));
			else
				digits.Add((char)(65-10+num));
				
			number -= b;
		}
		
			
		//number //= b
		string retVal = new string(digits.ToArray());
		return retVal;
	}
}
