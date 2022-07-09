using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TwoBoolsRequiredEvent : MonoBehaviour
{
	public bool bool1 = false, bool2 = false;
	public float holdTime = 0;
	
	public UnityEvent onBothBoolsTrue;
    
	public void SetBool1(bool value)
	{
		bool1 = value;
		executeCondition();
	}
	
	public void SetBool2(bool value)
	{
		bool2 = value;
		executeCondition();
	}
	
	public void executeCondition()
	{
		if(bool1 && bool2)
		{
			if(holdTime <= 0)
				onBothBoolsTrue.Invoke();
			else
				StartCoroutine(waitForSecondsExecute());
		}
	}
	
	IEnumerator waitForSecondsExecute()
	{
		yield return new WaitForSeconds(holdTime);
		
		if(bool1 && bool2)
			onBothBoolsTrue.Invoke();
		
	}
}
