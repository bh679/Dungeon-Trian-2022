using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.TrainCarts
{


	public class TrainCartArchitecture : MonoBehaviour
	{
		public ArchitectureTheme theme;
		public TrainCartStructure trainCartStructure;
		//public StructuralElement floor, roof, wallLeft, wallLeft2, wallRight, wallRight2;
		//public TrainDoorWall entranceDoor, exitDoor;
		
		
		public void SetThemeAndStructure(ArchitectureTheme newTheme, TrainCartStructure newtrainCartStructure)
		{
			theme = newTheme;
			trainCartStructure = newtrainCartStructure;
			
			StartCoroutine(SetTheme());
		}
		
		IEnumerator SetTheme()
		{
		
			for(int i = 0; i < trainCartStructure.floors.Length; i++)
			{
				trainCartStructure.floors[i].SetModel(Instantiate(theme.GetAFloor()).transform, this.transform);
				trainCartStructure.floors[i].model.transform.localScale = Vector3.one;
				yield return new WaitForFixedUpdate();
			}
			
		
		
			for(int i = 0; i < trainCartStructure.roofs.Length; i++)
			{
				trainCartStructure.roofs[i].SetModel(Instantiate(theme.GetARoof()).transform, this.transform);
				trainCartStructure.roofs[i].model.transform.localScale = Vector3.one;
				yield return new WaitForFixedUpdate();
			}
				
			trainCartStructure.entranceDoor.SetModel(Instantiate(theme.GetADoor()).transform, this.transform);
			trainCartStructure.entranceDoor.model.transform.localScale = Vector3.one;
			
			yield return new WaitForFixedUpdate();
			
			trainCartStructure.exitDoor.SetModel(Instantiate(theme.GetADoor()).transform, this.transform);
			trainCartStructure.exitDoor.model.transform.localScale = Vector3.one;
			
			yield return new WaitForFixedUpdate();
			
			
		
		
			for(int i = 0; i < trainCartStructure.walls.Length; i++)
			{
				trainCartStructure.walls[i].SetModel(Instantiate(theme.GetAWall()).transform, this.transform);
				trainCartStructure.walls[i].model.transform.localScale = Vector3.one;
				yield return new WaitForFixedUpdate();
			}
		}
	}

}
