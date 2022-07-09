using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerKeyboard : MonoBehaviour
{
	public KeyCode forwardKey = KeyCode.W, 
		backwardsKey = KeyCode.S, 
		leftKey = KeyCode.A, 
		rightKey = KeyCode.D, 
		jump = KeyCode.Space,
		runKey = KeyCode.LeftShift;
		
	public CharacterController character;
	
	public float walkSpeed = 1;
	public float runSpeed = 1;
	
	float speed;
	
	void Reset()
	{
		character = this.transform.root.GetComponentInChildren<CharacterController>();
	}
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		
		if(Input.GetKey(runKey))
			speed =runSpeed;
		else
			speed = walkSpeed;
		
		if(Input.GetKey(forwardKey))
			character.Move(character.transform.forward*speed);
			
		if(Input.GetKey(backwardsKey))
			character.Move(-character.transform.forward*speed);
			
		if(Input.GetKey(leftKey))
			character.Move(-character.transform.right*speed);
			
		if(Input.GetKey(rightKey))
			character.Move(character.transform.right*speed);
			
		if(Input.GetKey(jump))
			character.Move(character.transform.up*speed);
    }
}
