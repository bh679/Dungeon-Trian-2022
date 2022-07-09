using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BrennanHatton.Editor
{

	public class TransformPositionGizmo : MonoBehaviour
	{
		public Transform[] transforms;
		//	public bool OnlyOnSelected = true;
		
		/*
	    
		bool selected = false;
		void OnDrawGizmosSelected()
		{
			selected = true;
			DrawGizmos();
		}
		void OnDrawGizmos()
		{
			
			if(!selected && !OnlyOnSelected)
				DrawGizmos();
				
			selected = false;
		}
		
		public void DrawGizmos()
		{
			if(transforms == null || transforms.Length == 0)
				return;
			
			EditorGUI.BeginChangeCheck();
			Vector3 newTargetPosition = Handles.PositionHandle(transforms[0].position, Quaternion.identity);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(this, "Change Look At Target Position");
				transforms[0].position = newTargetPosition;
				//example.Update();
			}
		}*/
	}
}