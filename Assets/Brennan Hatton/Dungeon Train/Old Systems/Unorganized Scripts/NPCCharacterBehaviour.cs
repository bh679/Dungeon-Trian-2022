using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;

public class NPCCharacterBehaviour : MonoBehaviour
{
	RPGCharacterController rpgCharacterController;
	
	private Vector3 currentAim;
	
	private void Awake()
	{
		rpgCharacterController = GetComponent<RPGCharacterController>();
		currentAim = Vector3.zero;
	}
	
	void Start()
	{
		ResetBehaviour();
	}
	
	
    // Start is called before the first frame update
	public void ResetBehaviour()
	{
		if(rpgCharacterController==null)
			rpgCharacterController = GetComponent<RPGCharacterController>();
			
		rpgCharacterController.StartAction("Relax");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
