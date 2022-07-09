using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BrennanHatton.Scoring
{

	public class GameTypeSwitch : MonoBehaviour
	{
		public GameTypeManager gametypeManager;
		public GameType[] gameTypes;
		public int id = 0;
		
		void Reset()
		{
			gametypeManager = this.GetComponent<GameTypeManager>();
		}
		
		public void Next()
		{
			id = (id + 1) % gameTypes.Length;
			
			gametypeManager.SetGameType(gameTypes[id]);
		}
	}
}