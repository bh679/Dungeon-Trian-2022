using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWeaponHandles : MonoBehaviour
{
	#if UNITY_EDITOR
	void Reset()
	{
		WeaponSetup[] weapons;
		
		weapons = this.GetComponentsInChildren<WeaponSetup>();
		
		for(int i = 0; i < weapons.Length; i++)
		{
			weapons[i].SetupHandle();
		}
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	#endif
}
