using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
#if PUNVOICE
using Photon.Voice.Unity;
#endif
namespace BrennanHatton.Networking.PUN.Voice
{

	public class Mute : MonoBehaviour
		{
#if PUNVOICE
		static Recorder recorder;
		
		void Start()
		{
			recorder = GameObject.FindObjectOfType<Recorder>();
		}
		
		public void MuteFromToggle(Toggle toggle)
		{
			recorder.TransmitEnabled = !toggle.isOn;
		}
		
		public void ToggleFromMute(Toggle toggle)
		{
			toggle.isOn = !recorder.TransmitEnabled;
		}
		
		public void mute()
		{
			recorder.TransmitEnabled = false;
		}
		
		public void unmute()
		{
			recorder.TransmitEnabled = true;
			}
#endif
	}
	}