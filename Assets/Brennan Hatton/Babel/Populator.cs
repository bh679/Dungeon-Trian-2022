using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Populator : MonoBehaviour
{
	public bool runOnStart = true;
	public Transform[] bookspots;
	public GameObject[] books;
	public GameObject bookPrefab;
	public float yRotation = 0;
	public bool checkSubPopulator = false;
	
	public bool finished = false;
	
	
	void Reset()
	{
		bookspots = new Transform[transform.childCount];
		
		for(int i = 0; i < transform.childCount; i++)
			bookspots[i] = this.transform.GetChild(i);
	}
	
	void Start()
	{
		if(runOnStart)
			MakeDaBookz();
	}
	
	public void MakeDaBookz()
	{
		StartCoroutine(_MakeDaBookz());
	}
	
	IEnumerator _MakeDaBookz()
	{
		books = new GameObject[bookspots.Length];
		for(int i = 0; i < bookspots.Length; i++)
		{
			int id = InstantiateList.Instance.GetLine();
					
			while(InstantiateList.Instance.IsMyTurn(id) == false)
			{
				yield return new WaitForEndOfFrame();
			}
			
			books[i] = Instantiate(bookPrefab, bookspots[i].position, bookspots[i].rotation, bookspots[i]);
			books[i].transform.RotateAroundLocal(Vector3.up,yRotation*Mathf.Deg2Rad);
			
			
			
			if(checkSubPopulator)
			{
				Populator subPop = books[i].GetComponentInChildren<Populator>();//might need to turn this into an array in the future
				
				if(subPop != null)
				{
					while(!subPop.finished)
						yield return new WaitForEndOfFrame();
				}
			}
		}
		
		finished = true;
		yield return null;
	}
}
