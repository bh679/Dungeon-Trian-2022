using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if PUNVOICE
using Photon.Voice.Unity;
#endif

namespace EqualReality.Networking.Voice
{
	#if PUNVOICE
	[RequireComponent(typeof(VoiceConnection))]
	public class NetworkVoiceManager : MonoBehaviour
	{
		public static NetworkVoiceManager instance;
		
		public Transform remoteVoiceParent;
	
		public VoiceConnection voiceConnection;
		public Recorder recorder;
		
		void Reset()
		{
			voiceConnection = GetComponent<VoiceConnection>();
			recorder = GetComponent<Recorder>();
		}
	    
		void Awake()
		{
			if (instance == null)
				instance = this;
			else
				DestroyImmediate(this.gameObject);
			
			if(voiceConnection == null)
				voiceConnection = GetComponent<VoiceConnection>();
				
			SetVoiceChannel(0x01);
		}
	
		private void OnEnable()
		{
			voiceConnection.SpeakerLinked += this.OnSpeakerCreated;
		}
	
		private void OnDisable()
		{
			voiceConnection.SpeakerLinked -= this.OnSpeakerCreated;
		}
	
		private void OnSpeakerCreated(Speaker speaker)
		{
			Debug.Log("OnSpeakerCreated");
			speaker.transform.SetParent(this.remoteVoiceParent);
			speaker.OnRemoteVoiceRemoveAction += OnRemoteVoiceRemove;
		}
	
		private void OnRemoteVoiceRemove(Speaker speaker)
		{
			if(speaker != null)
			{
				Destroy(speaker.gameObject);
			}
		}
		
		public void SetVoiceChannel(int voiceCHannel)
		{
			SetVoiceChannel(Convert.ToByte(voiceCHannel));
		}
		
		public void SetVoiceChannel(byte voiceChannel)
		{
			/*byte[] previousChannel = new byte[1] {recorder.InterestGroup};
			//voiceConnection.Client.OpChangeGroups(new byte[0], null);
			voiceConnection.Client.OpChangeGroups(previousChannel, new byte[1] {voiceChannel});
			recorder.InterestGroup = voiceChannel;*/
			voiceConnection.Client.GlobalAudioGroup = voiceChannel;
		}
		
	}
	#endif
}