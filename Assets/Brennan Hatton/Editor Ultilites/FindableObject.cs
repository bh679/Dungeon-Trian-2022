using System;
using UnityEngine;

namespace EqualReality.Utilities.InspectorAttributes
{
	[Serializable]
	public class FindableObject
	{
		public string[] path;
		public Transform part;

		public FindableObject(string[] path)
		{
			this.path = path;
		}
    
		public Transform Get(Transform startingPoint, string prefix = "")
		{
			if (part != null) return part;
			if (startingPoint == null) return null;
        
			foreach (var partPath in path)
			{
				var next = startingPoint.Find($"{prefix}{partPath}");
				if (next == null) {
					next = startingPoint.Find(partPath);
				}
				if (next == null) return null;
				startingPoint = next;
			}
			part = startingPoint;

			return part;
		}
	}
}