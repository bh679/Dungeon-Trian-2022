using UnityEditor;
using UnityEngine;

namespace BrennanHatton.Props.Old.Editor
{
	[CustomEditor(typeof(PlacerGizmo)), CanEditMultipleObjects]
	public class PlacerGizmoEditor : UnityEditor.Editor
	{

		public override void OnInspectorGUI()
		{
			var t = (target as PlacerGizmo);
		
			base.OnInspectorGUI();
			
			if(GUILayout.Button("Refresh"))
				t.Setup();
		}
	}
}
