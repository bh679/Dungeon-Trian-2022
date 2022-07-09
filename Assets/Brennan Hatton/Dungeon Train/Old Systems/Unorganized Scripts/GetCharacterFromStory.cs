using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PsychoticLab;
using BrennanHatton.Props;
using BrennanHatton.Props.Story;

public class GetCharacterFromStory : MonoBehaviour
{
	public CharacterRandomizer character;
	public Transform propPlacer;
	public NPCCharacterBehaviour NPCBehaviour;
	
	public void OnEnable()
	{
		StartCoroutine(randomzieAfter());
	}
	
	IEnumerator randomzieAfter()
	{
		while(propPlacer.gameObject.activeInHierarchy == false)
			yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		
		StoryPopulator storyPop = propPlacer.GetComponentInChildren<StoryPopulator>();
		character.Randomize(storyPop.storySeed);
		NPCBehaviour.ResetBehaviour();
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
