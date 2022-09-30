using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
#if PUNVOICE
using Photon.Voice.PUN;
#endif

namespace BrennanHatton.Networking.PUN.Voice
{
	
	
	public class NetworkedPlayerSpeaking : MonoBehaviour
	{
		public Image recorderImage;
#if PUNVOICE
		public PhotonVoiceView photonVoiceView;
		
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
		    recorderImage.enabled = photonVoiceView.IsRecording;
		    if(recorderImage.enabled)
		    	recorderImage.transform.localScale = Vector3.one * Random.Range(1,1.2f);
		}
#endif
	}

}