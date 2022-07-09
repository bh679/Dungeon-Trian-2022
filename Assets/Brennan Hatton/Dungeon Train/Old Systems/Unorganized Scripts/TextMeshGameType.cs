using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Scoring;
using TMPro;

public class TextMeshGameType : MonoBehaviour
{
	public TextMeshPro[] text;
	public GameTypeManager typeManager;
	
	void Reset()
	{
		text = this.GetComponentsInChildren<TextMeshPro>();
		typeManager = this.GetComponent<GameTypeManager>();
	}
	
	public void CompareAndChangeText()
	{
		if(text == null || text.Length == 0)
			return;
		
		if(string.Compare(text[0].text, typeManager.gameType.name) != 0)
			ChangeText();
	}
	public void ChangeText()
	{
		for(int i = 0; i < text.Length; i++)
			text[i].text = typeManager.gameType.name;
	}
}
