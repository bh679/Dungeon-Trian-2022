using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RegionToText : MonoBehaviour
{
	public Text text;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		if(text.gameObject.activeInHierarchy)
	    	text.text = PhotonNetwork.CloudRegion;
    }
}
