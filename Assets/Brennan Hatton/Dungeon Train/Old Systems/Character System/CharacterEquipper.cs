using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Characters
{
		
	public class CharacterEquipper : MonoBehaviour
	{
		
		public Transform RightHand, LeftHand;
		
		/*//Weapons Animation usage
		To animate the weapons you will need to set mechanim parameters in the character Animator and parent the weapon mesh under the right hand jnt
	
		Parent weapon meshlocation -
		/Simple_Characters
		/Root_jnt
		/Hips_jnt
		/Body_jnt
		/Spine_jnt
		/UpperArm_Right_jnt
		/LowerArm_Right_jnt
		/Hand_Right_jnt*/
		List<Transform> leftHandObejcts = new List<Transform>(), rightHandObjects = new List<Transform>();
			
		//holds weapon
		public Transform[] Hold(Transform objectToHold, Hand handToHoldItIn)
		{
			List<Transform> objectsInhand = GetObjectsInHand(handToHoldItIn);
				
			//if it isnt in the hand
			if(objectsInhand.Contains(objectToHold))
			{
				//save list of all other objects holding
				Transform[] otherObjectsInhand = GetObjectsInHand(handToHoldItIn).ToArray();
				
				//add to hand
				objectsInhand.Add(objectToHold);
				
				//return list
				return otherObjectsInhand;
			}
			
			//if it is already in his hand
			//dont add to hand
			//return full list
			return objectsInhand.ToArray();
		}
		
		public List<Transform> GetObjectsInHand(Hand hand)
		{
			if(hand == Hand.Left)
				return leftHandObejcts;
			else
				return rightHandObjects;
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