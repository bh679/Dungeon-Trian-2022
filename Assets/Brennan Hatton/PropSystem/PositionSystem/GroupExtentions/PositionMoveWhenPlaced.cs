using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class should not use inherentence but instead delegates / events.

namespace BrennanHatton.Positions.Extentions
{
	/// <summary>
	/// Moves the position after placing an object
	/// </summary>
	public class PositionMoveWhenPlaced : Position
	{
		/// <summary>
		/// Direction to move group
		/// </summary>
		public Vector3 direction = Vector3.up * 0.1f;
		
		/// <summary>
		/// Starting position of group
		/// </summary>
		Vector3 orignialPosition;
		
		/// <summary>
		/// Called on start by monobehaviour
		/// </summary>
		void Start(){
			
			//set starting position
			orignialPosition = this.transform.localPosition;
			
		}
		
		/// <summary>
		/// Called On Enable by monobehaviour
		/// </summary>
		void OnEnable()
		{
			//sets starting position
			this.transform.localPosition = orignialPosition;
		}
		
		/// <summary>
		/// Positions group in new position, assigning new starting position
		/// </summary>
		/// <param name="newLocalPosition"></param>
		public void Reposition(Vector3 newLocalPosition)
		{
			//set new starting position
			orignialPosition = newLocalPosition;
			
			//move group there
			this.transform.localPosition = orignialPosition;
		}
		
		/// <summary>
		/// PLaces object into free position and move group
		/// </summary>
		/// <param name="objectToPlace"></param>
		/// <returns>position object was placed</returns>
		public override void Place(Transform objectToPlace)
		{
			//place object into position
			base.Place(objectToPlace);
			
			//offset group
			this.transform.localPosition += direction;
			
		}
	}

}