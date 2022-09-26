using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.LibraryOfBabel
{
	public class BookSpawner : MonoBehaviour
	{
		public BabelBook bookPrefab;
		public Transform spawnPoint, releasePoint;
		public Vector3 startVelocity, spitForce;
		public float forceDelay = 2f;
		
		public void SpawnRandomBook()
		{
			SpawnBook(new BookPosition(true));
		}
		
		public void SpawnBook(BookPosition position)
		{
			BabelBook newBook  = Instantiate(bookPrefab,spawnPoint.position,spawnPoint.rotation, this.transform);
			
			StartCoroutine(SpitOutBook(newBook.GetComponent<Rigidbody>()));
		}
		
		IEnumerator SpitOutBook(Rigidbody Book)
		{
			Collider col = Book.GetComponent<Collider>();
			col.isTrigger = true;
			Book.isKinematic = false;
			Book.velocity = startVelocity;
			Book.useGravity = false;
			
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
	        
	    }
	}
}