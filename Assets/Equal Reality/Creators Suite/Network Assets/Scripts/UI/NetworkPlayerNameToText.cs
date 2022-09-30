using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkPlayerNameToText : MonoBehaviour
{
	public PhotonView photonView;
	public Text text;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    text.text = photonView.Owner.NickName;
    }
}
