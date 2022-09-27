using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Audio;

namespace BrennanHatton.LibraryOfBabel
{
	public class BookSpawner : MonoBehaviour
	{
		public BabelBook bookPrefab;
		public Transform spawnPoint, releasePoint;
		public Vector3 startVelocity, spitForce;
		public float forceDelay = 2f;
		public PlayRandomClip clips;
		public bool instantSpawnFromReturns;
		
		public void SpawnRandomBook()
		{
			SpawnBook(new BookPosition(true));
		}
		
		public void SpawnNextBook()
		{
			SpawnBook(BookReturnsManager.Instance.GetBook());
		}
		
		public void SpawnBook(BookPosition position)
		{
			Debug.Log("spawning");
			BabelBook newBook  = Instantiate(bookPrefab,spawnPoint.position,spawnPoint.rotation, this.transform);
			newBook.Setup(position);
			
			StartCoroutine(SpitOutBook(newBook.GetComponent<Rigidbody>()));
		}
		
		IEnumerator SpitOutBook(Rigidbody Book)
		{
			Collider col = Book.GetComponent<Collider>();
			col.isTrigger = true;
			Book.isKinematic = false;
			Book.velocity = startVelocity;
			Book.useGravity = false;
			clips.playRandomClip();
			
			yield return new WaitForSeconds(forceDelay);
			
			Book.AddForce(spitForce);
			Book.useGravity = true;
			
			col.isTrigger = false;
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
		    if(instantSpawnFromReturns && BookReturnsManager.Instance.bookCount > 0)
			    SpawnNextBook();
	    }
	}
}