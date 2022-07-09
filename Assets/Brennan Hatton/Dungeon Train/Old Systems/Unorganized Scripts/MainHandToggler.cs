using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHandToggler : MonoBehaviour
{
	public Toggle toggle;
	public MainHandAssigner HandAssigner;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
	void Update()
	{
		if((int)HandAssigner.mainHand == 1)
			toggle.isOn = true;
		else
			toggle.isOn = false;
    }
    
    
	public void ChangeMainHand()
	{
		if(toggle.isOn)
			HandAssigner.SetMainHandLeft();
		else
			HandAssigner.SetMainHandRight();
	}
}
