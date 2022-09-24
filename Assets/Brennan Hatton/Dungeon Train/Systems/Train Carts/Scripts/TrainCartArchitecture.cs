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
		
			trainCartStructure.floor.SetModel(Instantiate(theme.GetAFloor()).transform, this.transform);
			
			yield return new WaitForFixedUpdate();
		
			trainCartStructure.roof.SetModel(Instantiate(theme.GetARoof()).transform, this.transform);
			
			yield return new WaitForFixedUpdate();
				
			trainCartStructure.entranceDoor.SetModel(Instantiate(theme.GetADoor()).transform, this.transform);
			
			yield return new WaitForFixedUpdate();
			
			trainCartStructure.exitDoor.SetModel(Instantiate(theme.GetADoor()).transform, this.transform);
			
			yield return new WaitForFixedUpdate();
			
			trainCartStructure.wallLeft.SetModel(Instantiate(theme.GetAWall()).transform, this.transform);
			
			yield return new WaitForFixedUpdate();
			
			trainCartStructure.wallRight.SetModel(Instantiate(theme.GetAWall()).transform, this.transform);
			
			yield return new WaitForFixedUpdate();
			
			trainCartStructure.wallLeft2.SetModel(Instantiate(theme.GetAWall()).transform, this.transform);
			
			yield return new WaitForFixedUpdate();
			
			trainCartStructure.wallRight2.SetModel(Instantiate(theme.GetAWall()).transform, this.transform);
		}
	}

}
