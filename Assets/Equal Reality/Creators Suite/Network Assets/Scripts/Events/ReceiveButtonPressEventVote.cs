using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using EqualReality.UserModes;
using EqualReality.VoteBars;

namespace EqualReality.Networking.Events
{
	

	public class ReceiveButtonPressEventVote : MonoBehaviour, IOnEventCallback
	{
		public SendNetworkedButtonEvent senderButton;
		public bool toggle = true;
		public UnityEvent onNetworkedPressed;
		public bool facilitatorsCanVote = false;
		public Text numberOfVotesText;
		bool youVoted;
		public VoteBar voteBar;
		public VoteIconManager voteIconManager;
		
		public bool YouVoted
		{
			get {
				return youVoted;
			}
		}
		
		public int numberOfVotes{
			get{
				return voters.Count;
			}}
		
		public List<int> voters = new List<int>();
		
		void Reset()
		{
			senderButton = this.GetComponent<SendNetworkedButtonEvent>();
			if(!senderButton)
				senderButton = transform.parent.GetComponentInChildren<SendNetworkedButtonEvent>();
			
			numberOfVotesText = this.GetComponent<Text>();
		}
		
		private void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
		}
	
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}
		
		public void OnEvent(EventData photonEvent)
		{
			byte eventCode = photonEvent.Code;
			if (eventCode == SendEventManager.NetworkedButtonCode)
			{
				object[] data = (object[])photonEvent.CustomData;
					
				int senderId = (int)data[0];
				int id = (int)data[1];
				
				if(id == senderButton.id)
				{
					
					int sceneId = (int)data[2];
					
					//if you got a message from another scene, exit
					if(sceneId != SceneManager.GetActiveScene().buildIndex)// why is this needed?
						return;
					
					//Are you the facilitator?
					if(!Facilitator.mode || facilitatorsCanVote)
					{
						//are you already counted as a voter?
						if(voters.Contains(senderId))
							//than take back your vote!
							voters.Remove(senderId);
						else
							//Add your vote to the list
							voters.Add(senderId);
							
						youVoted = voters.Contains(PhotonNetwork.LocalPlayer.ActorNumber);
							
							
						UpdateUI();
					}
				}
				
			}
		}
			
		void UpdateUI()
		{
			if(numberOfVotesText != null)
				numberOfVotesText.text = voters.Count.ToString() + (youVoted ? " (including you)" : "");
				
			voteBar.UpdateVotes(youVoted, numberOfVotes, PhotonNetwork.PlayerList.Length);
			
			voteIconManager.UpdateVoteIcons(numberOfVotes);
		}
		
	}
}
