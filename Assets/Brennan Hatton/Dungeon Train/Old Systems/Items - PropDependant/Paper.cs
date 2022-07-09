using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;

public class Paper : Prop
{
	
	public string text;
	public TextMesh textMesh;
	public float maxLineWidth = 7*25;
	public PropPlacer shredder;
	
	#if UNITY_EDITOR
	void Reset()
	{
		TextMesh[] textMeshs = this.GetComponentsInChildren<TextMesh>();
		
		if(textMeshs.Length > 0)
			textMesh = textMeshs[0];
		
		base.Reset();
	}
	#endif
	
	public void Shred()
	{
		if(shredder != null)
			shredder.Place();
		this.gameObject.SetActive(false);
	}
	
	public void SetText(string newText)
	{
		text = newText;
		textMesh.richText = true;
		text = text.Replace("<br>", "\n").Replace("<v>", Application.version);
		
		float lineWidth = 0;
		int lastSpaceId = 0;
		CharacterInfo info;
		for(int i = 0; i < text.Length; i++)
		{
			if (textMesh.font.GetCharacterInfo(text[i], out info))
			{
				lineWidth += info.width;
			}
			
			if(text[i] == '\n')
			{
				lineWidth = 0;
				lastSpaceId = i;
				
			}
			else if(text[i] == ' ')
			{
				if(i > 0 && text[i - 1] == '\n')
					text = text.Remove(i,1);
				lastSpaceId = i;
			}
			
			if(lineWidth > maxLineWidth && text[lastSpaceId] != '\n')
			{
				text = text.Remove(lastSpaceId,1);
				text = text.Insert(lastSpaceId,"\n");
				//Debug.Log("<color=red>Adding Space</color> at id " + lastSpaceId);
				lineWidth = 0;
				lastSpaceId = i;
			}
		}
		
		
		textMesh.text = text;
	}
	
	public static float GetWidht(TextMesh mesh)
 {
	 float width = 0;
	 foreach( char symbol in mesh.text)
	 {
		 CharacterInfo info;
		 if (mesh.font.GetCharacterInfo(symbol, out info))
		 {
			 width += info.width;
		 }
	 }
	 return width * mesh.characterSize * 0.1f * mesh.transform.lossyScale.x;
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
