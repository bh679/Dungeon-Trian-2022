using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace EqualReality.Networking
{

	public class PlayerListToText : MonoBehaviourPunCallbacks
	{
		public Text countText, maxPlayersText;//, actorText, roomText;
		public ClassmateListLine linePrefab;
		public Vector3 spacing = new Vector3(0,10,0);
		public Vector3 scaling = new Vector3(1,1,1);
		public bool selectionEnabled = true;
		
		public List<ClassmateListLine> lines = new List<ClassmateListLine>();
		
		public string[] RoomNames;
		//public bool useLocation = true;
		
		public void OnEnable()
		{
			base.OnEnable();
			
			if(PhotonNetwork.InRoom == true)
			{
				CheckListSize();
				UpdateText();
			}
		}
		
		void CheckListSize()
		{
			if(PhotonNetwork.PlayerList.Length == lines.Count)
				return;
			
			while(PhotonNetwork.PlayerList.Length < lines.Count)
			{
				Destroy(lines[0].gameObject);
				lines.RemoveAt(0);
			}
			
			
			while(PhotonNetwork.PlayerList.Length > lines.Count)
			{
				lines.Add(CreateNewItem());
			}
			
		}
	    
		public override void OnJoinedRoom()
		{
			base.OnJoinedRoom();
			UpdateText();
		}
		
		ClassmateListLine CreateNewItem()
		{
			ClassmateListLine newItem = Instantiate(linePrefab, this.transform);
			
			newItem.transform.localScale = scaling;
			
			return newItem;
			
		}
		
		public override void OnPlayerEnteredRoom(Player newPlayer)
		{
			
			base.OnPlayerEnteredRoom(newPlayer);
			lines.Add(CreateNewItem());
			UpdateText();
		}
		
		
		//Called when a remote player entered the room. This Player is already added to the playerlist. More...
	 
		public override void 	OnPlayerLeftRoom (Player otherPlayer)
		{
			base.OnPlayerLeftRoom(otherPlayer);
			
			lines.RemoveAt(0);
			UpdateText();
		}
		
		public void UpdateSelection()
		{
			for(int i = 0;i < lines.Count; i++)
			{
				lines[i].selection.gameObject.SetActive(selectionEnabled);
			}
		}
		
		public int[] GetSelectedPlayers(bool includeSelfRegardless)
		{
			List<int> selectedPLayers = new List<int>();
			
			
			for(int i = 0;i < lines.Count; i++)
			{
				if(lines[i].selection.isOn)
				{
					selectedPLayers.Add(lines[i].Data.actorNumber);
				}
			}
			
			if(includeSelfRegardless && selectedPLayers.Contains(photonView.OwnerActorNr) == false)
				selectedPLayers.Add(photonView.OwnerActorNr);
			
			return  selectedPLayers.ToArray();
			
		}
		
		public void UpdateFacilitation()
		{
			for(int i = 0;i < lines.Count; i++)
			{
				lines[i].facilitatorObj.gameObject.SetActive(GetFacilitator(i));
			}
		}
		
		
	
	    // Update is called once per frame
		public void UpdateText()
		{
			if(countText!= null) countText.text = PhotonNetwork.PlayerList.Length.ToString();
			if(maxPlayersText!= null) maxPlayersText.text = ((int)PhotonNetwork.CurrentRoom.MaxPlayers).ToString();
			
			for(int i = 0;i < PhotonNetwork.PlayerList.Length; i++)
			{
				lines[i].Set(new LearnerData(PhotonNetwork.PlayerList[i].ActorNumber, PhotonNetwork.PlayerList[i].NickName,GetRoomName(i), PhotonNetwork.PlayerList[i].IsLocal, GetFacilitator(i), selectionEnabled));
				
				lines[i].transform.localPosition = spacing * i;
			}
		}
		
		public bool GetFacilitator(int i)
		{
			if(PhotonNetwork.PlayerList[i].CustomProperties.ContainsKey(PlayerCustomProperties.Facilitator) == false)
				return false;
				
			return (bool)PhotonNetwork.PlayerList[i].CustomProperties[PlayerCustomProperties.Facilitator];
		}
	    
		public string GetRoomName(int i)
		{
			return "";
			/*if(PhotonNetwork.PlayerList[i].CustomProperties.ContainsKey(PlayerCustomProperties.ActiveScene) == false)
				return "";
				
			int id = (int)PhotonNetwork.PlayerList[i].CustomProperties[PlayerCustomProperties.ActiveScene];
			//Debug.Log(id);
			return RoomNames[id];*/
		}
	}

}