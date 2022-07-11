using System.Collections.Generic;

namespace BrennanHatton.Props
{
	//use this for placer class
	public class ChanceMultiplier
	{
		//Pools have dataset, grouping object and weight
		public int chanceMultiplier = 100;
		
		public ChanceMultiplier()
		{
			chanceMultiplier = 100;
		}
		
		/// <summary>
		/// Creates a matrix of ids for picking objects based on chance value
		/// </summary>
		/// <param name="chanceObjects"></param>
		/// <returns>A chance matrix of ids</returns>
		static public int[] CreateChanceMatrix(ChanceMultiplier[] chanceObjects)
		{
			List<int> chanceList = new List<int>();
			
			for(int i = 0; i < chanceObjects.Length; i++)
			{
				//check for zero and make it one
				if(chanceObjects[i].chanceMultiplier == 0)
					chanceObjects[i].chanceMultiplier = 1;
					
				for(int c = 0; c < chanceObjects[i].chanceMultiplier; c++)
				{
					chanceList.Add(i);
				}
			}
			
			return chanceList.ToArray();
		}
	}
}