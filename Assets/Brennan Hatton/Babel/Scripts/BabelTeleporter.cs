using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using BrennanHatton.TrainCarts;

namespace BrennanHatton.LibraryOfBabel
{

	public class BabelTeleporter : MonoBehaviour
	{
		
		public void TeleportFromSnap(SnapZone snapZone)
		{
			BabelBook book = snapZone.GetComponentInChildren<BabelBook>();
			
			Teleport(book.page.position);
		}
		
		public void Teleport(BookPosition position)
		{
			TrainCartManager.Instance.TeleportToCart(position.room);
			TrainCartManager.Instance.carts[0].GetComponentInChildren<BookmarkManager>().HighlightBook(position);
		}
	}

}