using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Props
{

	public class RandomNumberRange : MonoBehaviour
	{
		
		//Places have a range of min and max objects to place
		public Vector2 range = new Vector2(1,2);//TODO - make a matrix to make higher numbers rare, and an enum to define how the matrixis made (x=y, x=2*y, x=length-i*y, x=y*y, x=y*y*y, etc. This will be applied in CalRandomNumberOfProps
		
		//Places have a value for chance of not placing anything.
		[Range(0,100)]
		public float ChangeOfZero = 50;
		public bool ChanceofZeroForEach = false;
		/// <summary>
		/// Calulate how many props to place based on range and chance of zero
		/// </summary>
		/// <param name="runChanceOfZero">Does it use the chance of zero? Defualt is yes/true</param>
		/// <returns>number of props to place</returns>
		public int CalRandomNumberOfProps(bool runChanceOfZero = true)
		{
			//hold the return value here
			int numberOfProps;
			
			//run chance of zero
			if(runChanceOfZero)
				if(Random.Range(0,100) < ChangeOfZero)
					return 0;
				
			if(ChanceofZeroForEach)
			{
				numberOfProps = (int)range.x;
				for(int i = (int)range.x; i < range.y; i++)
				{
					if(Random.Range(0,100) >= ChangeOfZero)
						numberOfProps++;
				}
				
				return numberOfProps;
				
			}
			
			//calculate how many props to pick
			if(range.y == range.x)
				numberOfProps = Mathf.CeilToInt(range.y);
			else	
				numberOfProps = Random.Range(Mathf.CeilToInt(range.x),Mathf.CeilToInt(range.y)+1);
				
			return numberOfProps;
		}
	}

}