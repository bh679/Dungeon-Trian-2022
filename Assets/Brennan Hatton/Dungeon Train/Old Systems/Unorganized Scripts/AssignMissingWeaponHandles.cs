using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignMissingWeaponHandles : MonoBehaviour
{
	#if UNITY_EDITOR
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	void Reset()
	{
		WeaponSetup[] weapons = this.GetComponentsInChildren<WeaponSetup>();
		
		for(int i = 0 ; i < weapons.Length; i++)
		{
			weapons[i].FixHandle();
		}
	}
	#endif
}
