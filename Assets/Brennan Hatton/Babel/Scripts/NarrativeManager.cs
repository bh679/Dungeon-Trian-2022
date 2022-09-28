using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel.Narrative
{

public class NarrativeManager : MonoBehaviour
{
	public List<BookData> books;
	int  index = 0;
	
	
		
	public static NarrativeManager Instance { get; private set; }
	private void Awake() 
	{ 
		// If there is an instance, and it's not me, delete myself.
    
		if (Instance != null && Instance != this) 
		{ 
			Destroy(this); 
		} 
		else 
		{ 
			Instance = this; 
		} 
	}
	
	public void FeedNextBook()
	{
		
		if(index >= books.Count)
			return;
		
		BookReturnsManager.Instance.Return(books[index]);
		
		index++;
		
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

}