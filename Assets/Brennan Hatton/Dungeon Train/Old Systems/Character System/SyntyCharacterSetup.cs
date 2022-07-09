using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Characters;
using EqualReality.Utilities.InspectorAttributes;

[RequireComponent(typeof(BodyController))]
[RequireComponent(typeof(CharacterAnimationController))]
[RequireComponent(typeof(CharacterEquipper))]
[RequireComponent(typeof(CharacterAIAttack))]
[RequireComponent(typeof(CharacterAIColliderTargetFinder))]
public class SyntyCharacterSetup : MonoBehaviour
{
	CharacterEquipper equipper;
	string Prefix = "";
	
	void Reset()
	{
		equipper = this.GetComponent<CharacterEquipper>();
		
		FindableObject rightHand = new FindableObject(
			new []{
				"Root", "Hips", "Spine_01", "Spine_02", "Spine_03", "Clavicle_R", "Shoulder_R", "Elbow_R", "Hand_R"
			});
		equipper.RightHand = rightHand.Get(transform, Prefix);
		
		FindableObject leftHand = new FindableObject(
			new []{
				"Root", "Hips", "Spine_01", "Spine_02", "Spine_03", "Clavicle_L", "Shoulder_L", "Elbow_L", "Hand_L"
			});
		equipper.LeftHand = leftHand.Get(transform, Prefix);
	}
}
