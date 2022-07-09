using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Scoring;

public class FloatToColorChanger : MonoBehaviour
{
	public MaterialColorBlender matBlender;
	public ScoringFloat scoringFloat;
	public Color maxColor, minColor;
	public float maxVal = 10, minVal = 1;
	
	void Reset(){
		matBlender = this.GetComponent<MaterialColorBlender>();
		scoringFloat = this.GetComponent<ScoringFloat>();
	}

	public void Lerp()
	{
		matBlender.defaultColor = Color.Lerp(maxColor, minColor, scoringFloat.GetScore() / (maxVal-minVal));
	}

}
