using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateShelf : MonoBehaviour
{
	public Transform[] bookspots;
	public GameObject[] books;
	public GameObject bookPrefab;
	
	
	void Reset()
	{
		bookspots = new Transform[transform.childCount];
		
		for(int i = 0; i < transform.childCount; i++)
			bookspots[i] = this.transform.GetChild(i);
	}
	
	public void MakeDaBookz()
	{
		books = new GameObject[bookspots.Length];
		for(int i = 0; i < bookspots.Length; i++)
		{
			books[i] = Instantiate(bookPrefab, bookspots[i].position, bookspots[i].rotation, bookspots[i]);
			books[i].transform.RotateAroundLocal(Vector3.up,180);
		}
	}
	
    // Start is called before the first frame update
    void Start()
    {
	    MakeDaBookz();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
