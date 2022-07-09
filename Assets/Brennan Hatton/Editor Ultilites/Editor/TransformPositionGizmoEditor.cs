using UnityEditor;
using UnityEngine;
using BrennanHatton.Editor;

[CustomEditor(typeof(TransformPositionGizmo)), CanEditMultipleObjects]
public class TransformPositionGizmoEditor : Editor
{
	protected virtual void OnSceneGUI()
	{
		
		TransformPositionGizmo t = (TransformPositionGizmo)target;
		
		if(t.transforms == null || t.transforms.Length == 0)
			return;
			
		for(int i = 0; i < t.transforms.Length; i++)
		{
			EditorGUI.BeginChangeCheck();
			Vector3 newTargetPosition = Handles.PositionHandle(t.transforms[i].position, Quaternion.identity);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(t, "Change Look At Target Position");
				t.transforms[i].position = newTargetPosition;
				//t.Update();
			}
		}
	}
}
