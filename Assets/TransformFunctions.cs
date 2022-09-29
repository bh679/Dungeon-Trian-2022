using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Utilities
{
	public class TransformFunctions : MonoBehaviour
	{
		public void SetParent(Transform parent)
		{
			this.transform.SetParent(parent);
		}
	}

}