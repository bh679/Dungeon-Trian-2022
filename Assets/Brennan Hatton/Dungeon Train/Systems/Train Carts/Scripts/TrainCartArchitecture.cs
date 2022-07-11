using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;

namespace BrennanHatton.TrainCarts
{

	public class TrainCartArchitecture : MonoBehaviour
	{
		public ArchitectureTheme theme;
		public PropPlacer floor, roof, wallLeft, wallRight;
		public TrainDoorWall entranceDoor, exitDoor;
		
		
		public void SetTheme(ArchitectureTheme newTheme)
		{
			theme = newTheme;
			
			floor.Place();
		
			/*floor = Instantiate(theme.floors.Place(),
				this.transform.position, 
				this.transform.rotation, 
			this.transform);*/
			
			//StartCoroutine(SetNewTheme());
		}
		
		/*public void FloorPlaced(GameObject floorPlaced)
		{
			floor = floorPlaced;
		}*/
		
		IEnumerator SetNewTheme()
		{
			
			/*if(floor != null)
				DestroyObject(floor);
		
			floor = Instantiate(theme.floors.GetAFloor(),
				this.transform.position, 
				this.transform.rotation, 
				this.transform);
			
			yield return new WaitForFixedUpdate();
			
			if(roof != null)
				DestroyObject(roof);
		
			roof = Instantiate(theme.GetARoof(),
				this.transform.position + this.transform.up*ArchitectureTheme.tileSize, 
				this.transform.rotation, 
				this.transform);
			
			yield return new WaitForFixedUpdate();
			
			if(entranceDoor != null)
				DestroyObject(entranceDoor);
			entranceDoor = Instantiate(theme.GetADoor(),
				this.transform.position + this.transform.forward*ArchitectureTheme.tileSize - this.transform.right*ArchitectureTheme.tileSize, 
				this.transform.rotation* Quaternion.Euler(0, 180, 0), 
				this.transform);
			
			yield return new WaitForFixedUpdate();
			
			if(exitDoor != null)
				DestroyObject(exitDoor);
			exitDoor = Instantiate(theme.GetADoor(),
				this.transform.position, 
				this.transform.rotation, 
				this.transform);
			
			yield return new WaitForFixedUpdate();
			
			if(wallLeft != null)
				DestroyObject(wallLeft);
			wallLeft = Instantiate(theme.GetAWall(),
				this.transform.position - this.transform.right*ArchitectureTheme.tileSize,
				this.transform.rotation* Quaternion.Euler(0, 90, 0), 
				this.transform);
			//transform.Rotate(Vector3.right, 90, 1);
			
			yield return new WaitForFixedUpdate();
			
			if(wallRight != null)
				DestroyObject(wallRight);
			wallRight = Instantiate(theme.GetAWall(),
				this.transform.position + this.transform.forward*ArchitectureTheme.tileSize, 
				this.transform.rotation* Quaternion.Euler(0, -90, 0), 
			this.transform);*/
			
			yield return null;
		}
	}

}
