using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generic.Move;
using BrennanHatton.Props;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
	public UnityEvent onLock = new UnityEvent(), onUnLock = new UnityEvent();
	public MoveTargetTowardsTarget openner, closer;
	public AudioSource openAudio, closedAudio;
	public PropPlacer lockPlacer;
	bool locked = false;
	bool closed = true;
	
	public void Open()
	{
		openner.enabled = true;
		closer.enabled = false;
		openAudio.Play();
		closed = false;
	}
	
	public void Close()
	{
		openner.enabled = false;
		closer.enabled = true;
		openAudio.Stop();
		closedAudio.Play((ulong)2.4);
		closed = true;
	}
	
	public void Lock()
	{
		if(!closed)
			Close();
		
		if(locked)
			return;
			
		locked = true;
		lockPlacer.gameObject.SetActive(true);
		lockPlacer.Place();
		openner.gameObject.SetActive(false);
		
		onLock.Invoke();
	}
	
	public void Unlock()
	{		
		onUnLock.Invoke();
		if(!locked)
			return;
			
		locked = false;
		lockPlacer.ReturnProps();
		lockPlacer.gameObject.SetActive(false);
		openner.gameObject.SetActive(true);
		
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
