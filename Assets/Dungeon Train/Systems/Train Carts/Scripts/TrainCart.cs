using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCart : MonoBehaviour
{
	public ArchitectureTheme theme;
	public GameObject floor, roof, wallFront, wallBack, wallLeft, wallRight;
	
	public float tileSize = 5f;
	public float tilesLength = 1;
	public float length
	{
		get{
			return tileSize * tilesLength;
		}
	}
	
	public void SetTheme(ArchitectureTheme newTheme)
	{
		theme = newTheme;
		
		StartCoroutine(SetNewTheme());
	}
	
	IEnumerator SetNewTheme()
	{
		
		if(floor != null)
			DestroyObject(floor);
	
		floor = Instantiate(theme.GetAFloor(),
			this.transform.position, 
			this.transform.rotation, 
			this.transform);
		
		yield return new WaitForFixedUpdate();
		
		if(roof != null)
			DestroyObject(roof);
	
		roof = Instantiate(theme.GetARoof(),
			this.transform.position + this.transform.up*tileSize, 
			this.transform.rotation, 
			this.transform);
		
		yield return new WaitForFixedUpdate();
		
		if(wallFront != null)
			DestroyObject(wallFront);
		wallFront = Instantiate(theme.GetAWall(),
			this.transform.position + this.transform.forward*tileSize - this.transform.right*tileSize, 
			this.transform.rotation* Quaternion.Euler(0, 180, 0), 
			this.transform);
		
		yield return new WaitForFixedUpdate();
		
		if(wallBack != null)
			DestroyObject(wallBack);
		wallBack = Instantiate(theme.GetAWall(),
			this.transform.position, 
			this.transform.rotation, 
			this.transform);
		
		yield return new WaitForFixedUpdate();
		
		if(wallLeft != null)
			DestroyObject(wallLeft);
		wallLeft = Instantiate(theme.GetAWall(),
			this.transform.position - this.transform.right*tileSize,
			this.transform.rotation* Quaternion.Euler(0, 90, 0), 
			this.transform);
		//transform.Rotate(Vector3.right, 90, 1);
		
		yield return new WaitForFixedUpdate();
		
		if(wallRight != null)
			DestroyObject(wallRight);
		wallRight = Instantiate(theme.GetAWall(),
			this.transform.position + this.transform.forward*tileSize, 
			this.transform.rotation* Quaternion.Euler(0, -90, 0), 
			this.transform);
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
