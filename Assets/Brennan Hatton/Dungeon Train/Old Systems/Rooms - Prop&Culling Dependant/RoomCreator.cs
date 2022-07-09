using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BrennanHatton.Props;

namespace BrennanHatton.DungeonTrain.Rooms
{
	public class RoomCreator : MonoBehaviour
	{
		
		[System.Serializable]
		public class RoomData
		{
			public string name;
			public Room room;
			public int number;
			public bool onlyAfterStart;
		}
		
		public int activeRoomsCount = 4;
		
		//list of props
		Room[] _activeRoom;
		
		//public interface for list of props
		public Room[] activeRoom { get { 
			if(_activeRoom == null)
				_activeRoom = new Room[activeRoomsCount];	
			return _activeRoom; 
		} }
		
		//private Room[] activeRoom;
		
		public Room startRoom;
		
		public RoomData[] roomData;
		
		Room[] allRooms;
		List<Room> afterStartOnlyRooms = new List<Room>();
		
		int nextRoomPos = 0;
		int roomCountTotal = 0;
		
		public GameObject DistantRoom, DistantRoomBack;
		
		public Transform player;
		
		public Transform[] worldSpaceOffsets;
		
		public UnityEvent OnNextRoom = new UnityEvent();
		
		public Transform[] rightTerrain, leftTerrain;
		public int roomsToCheck = 2;
		
		bool setup = false;
		int playerRoomId;
		bool afterStart = false;//The start carriage has been placed
		
		Camera[] cameras;
		//public UnityEngine.UI.RawImage[] cameraImages;
		//public UnityEngine.UI.RawImage[] cameraImages;
		public RenderTexture[] renText;
		
		public void CreateAllRooms()
		{
			
			int totalRooms = 0;
			for(int r = 0; r < roomData.Length; r++)
			{
				totalRooms += roomData[r].number;
			}
			
			allRooms = new Room[totalRooms];
			int i = 0;
			for(int r = 0; r < roomData.Length; r++)
			{
				for(int n = 0; n < roomData[r].number; n++)
				{
					if(n == 0)
						allRooms[i] = roomData[r].room;
					else
					{
						allRooms[i] = Instantiate(roomData[r].room, roomData[r].room.transform.parent);
					}
					
					if(roomData[r].onlyAfterStart)
						afterStartOnlyRooms.Add(allRooms[i]);
						
					allRooms[i].gameObject.SetActive(false);
					allRooms[i].Initialization();
					i++;
				}
			}
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
			cameras = new Camera[activeRoomsCount];
			
			if(Random.value > 0.5)
				createWindowsLeft = true;
			else
				createWindowsLeft = false;
			
			CreateAllRooms();
			
			//activeRoom  = new Room[activeRoomsCount];
			playerRoomId = Mathf.FloorToInt(activeRoomsCount/2)-1;
			for(int i = 0; i < activeRoom.Length; i++)
			{
				if(i == playerRoomId)
				{
					activeRoom[i] = SetStartRoom();
					activeRoom[i].EnterTrigger.SetActive(false);
				}
				else
				{
					activeRoom[i] = SetNextRoom();
					
					if (i < playerRoomId)
					{
						OffsetWorldSpaceObjects(activeRoom[i].length * activeRoom[i].tsize);
						activeRoom[i].EnterTrigger.SetActive(false);
					}
						
				}
			}
			
			
			StartCoroutine(CheckTerrainNeededAfterTime(0.1f));
			
			
			setup = true;
		}
		
		public int GetCurrentRoomRotationID(int plus = 0)
		{
			return (roomCountTotal + playerRoomId + plus) % activeRoomsCount;
		}
		
		void SetRoom(Room room)
		{
			room.transform.localPosition = Vector3.forward*nextRoomPos;
			nextRoomPos += room.length*room.tsize;
			DistantRoom.transform.localPosition = Vector3.forward*nextRoomPos;
			
			room.Place(null);
			
			//shuffle debug cameras along
			if(room.camera != null)
			{
				/*cameras[3] = cameras[2];
				if(cameras[3] != null)
				cameras[3].targetTexture = renText[2];*/
					
				cameras[2] = cameras[1];
				if(cameras[2] != null)
					cameras[2].targetTexture = renText[2];
				
				cameras[1] = cameras[0];
				if(cameras[1] != null)
					cameras[1].targetTexture = renText[1];
				
				cameras[0] = room.camera;
				if(cameras[0] != null)
					cameras[0].targetTexture = renText[0];
			}
			/*PropPlacer[] subPlacers = room.GetComponentsInChildren<PropPlacer>();
				
			for(int i = 0; i < subPlacers.Length; i++)
			{
				subPlacers[i].Place();
			}*/
		}
		
		Room SetNextRoom()
		{
			Room room = allRooms[PickRoomIDNotActive()];
			
			SetRoom(room);
			
			afterStart = true;//shouldthis be in SetStartRoom? - does matter becuase there isnt enough rooms any more
			
			
			return room;
		}
		
		/// <summary>
		/// Used on start to wait for terrain values to be populated
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		IEnumerator CheckTerrainNeededAfterTime(float time)
		{
			yield return new WaitForSeconds(time);
			
			CheckTerrainNeeded();
		}
		
		bool leftTerrainActive  = false, rightTerrainActive = false;
		int leftTerrainid = 0, rightTerrainid = 0;
		void CheckTerrainNeeded()
		{
			if(activeRoom.Length < 3)
				return;
				
			if(leftTerrain.Length == 0)
				return;
				
			//Is it active?
			//left
			bool lastActiveLeft = leftTerrainActive;
			leftTerrainActive = (!activeRoom[(roomCountTotal) % activeRoomsCount].leftWallSolid
				|| !activeRoom[(roomCountTotal+1) % activeRoomsCount].leftWallSolid);
				
			//right
			bool lastActiveRight = rightTerrainActive;
			rightTerrainActive = (!activeRoom[(roomCountTotal) % activeRoomsCount].rightWallSolid
				|| !activeRoom[(roomCountTotal+1) % activeRoomsCount].rightWallSolid);
				
				
			
			//if it has just turned on
			if(lastActiveLeft != leftTerrainActive && leftTerrainActive == true)
			{
				//if the other side is on
				if(rightTerrainActive == true)
				{
					//chance to make it water
					if(Random.value > 0.75f)
					{
						if(leftTerrainid == 0)
							leftTerrainid = Random.Range(0, leftTerrain.Length*10)%leftTerrain.Length;
						else
							leftTerrainid = 0;
					}
					else //make it the same
						leftTerrainid = rightTerrainid;
				}
				else // random terrain
					leftTerrainid = Random.Range(0, leftTerrain.Length*10)%leftTerrain.Length;
				
				//turn all terrain off
				for(int i = 0; i < leftTerrain.Length; i++)
					leftTerrain[i].gameObject.SetActive(false);
			}
			
			//turn the correct terrain on
			leftTerrain[leftTerrainid].gameObject.SetActive(leftTerrainActive);
			
				
			//if it has just turned on
			if(lastActiveRight != rightTerrainActive && rightTerrainActive == true)
			{
				//if the other side is on
				if(leftTerrainActive == true)
				{
					//chance to make it water
					if(Random.value > 0.75f)
					{
						if(rightTerrainid == 0)
							rightTerrainid = Random.Range(0, rightTerrain.Length*10)%rightTerrain.Length;
						else
							rightTerrainid = 0;
					}
					else//make it the same
						rightTerrainid = leftTerrainid;
				}
				else// random terrain
					rightTerrainid = Random.Range(0, rightTerrain.Length*10)%rightTerrain.Length;
					
				//turn all terrain off
				for(int i = 0; i < leftTerrain.Length; i++)
				{
					rightTerrain[i].gameObject.SetActive(false);
				}
			}
			
			//turn the correct terrain on
			rightTerrain[rightTerrainid].gameObject.SetActive(rightTerrainActive);
			
		}
		
		bool windowSwitchReady = false;
		public bool createWindowsLeft = false;
		void SwitchWindowCreateSides(Room mostRecentRoom)
		{
			
			
			//if there are no windows
			if(mostRecentRoom.AreTerrainSidesSame())
			{
				if(windowSwitchReady)
					createWindowsLeft = !createWindowsLeft;
					
				windowSwitchReady = false;
			}else
				windowSwitchReady = true;
			
		}
		
		
		Room SetStartRoom()
		{
			Room room = startRoom;
			
			startRoom.Initialization();
			
			SetRoom(room);
			
			player.transform.position = room.transform.position + new Vector3(-room.width/2f, 0, room.length/4f) * room.tsize;
			
			return room;
		}
		
		void OffsetWorldSpaceObjects(float distance)
		{
			for(int i = 0; i < worldSpaceOffsets.Length; i++)
			{
				worldSpaceOffsets[i].position += this.transform.forward * distance;
			}
			
		}
		
		Room RoomToRemove; 
	    
		float updateTime = 0;
		//is this being invoked twice in one frame by OnTriggerEnter, causing problems
		public void NextRoom()
		{
			//has it been called this update?
			if(updateTime == Time.time)
			{
				Debug.LogError("Next Room is being called twice. this si probably from mutliple player cpllisions into the next room setter. The code around this error should fix this, so maybe just remove this errror and leave a comment to say this has been validated");
				
				//exit to skip this call
				return;
			}
			
			//save timestamp
			updateTime = Time.time;
			
			
			OnNextRoom.Invoke();
			
			roomCountTotal++;
			
			activeRoom[(roomCountTotal-1) % activeRoomsCount].ReturnToPool();
			//RemoveRoomAfterTime(activeRoom[(roomCountTotal-1) % activeRoomsCount].Remove(),);//NULL REF HERE
			
			activeRoom[(roomCountTotal-1) % activeRoomsCount] = SetNextRoom();
			
			SwitchWindowCreateSides(activeRoom[(roomCountTotal-1) % activeRoomsCount]);
			
			if(DistantRoomBack)
				DistantRoomBack.transform.position = activeRoom[(roomCountTotal) % activeRoomsCount].transform.position - activeRoom[(roomCountTotal) % activeRoomsCount].transform.forward*5;
			
			OffsetWorldSpaceObjects(activeRoom[(roomCountTotal + playerRoomId) % activeRoom.Length].length * activeRoom[(roomCountTotal + playerRoomId) % activeRoom.Length].tsize);
			
			CheckTerrainNeeded();
			
		}
		
		
		//####### Querys ########
		
		//returns train width (max width of current or next cart)
		public int TrainWidth()
		{
			int width = activeRoom[(roomCountTotal + playerRoomId) % activeRoom.Length].width;
			
			if(width < activeRoom[(roomCountTotal + playerRoomId+1) % activeRoom.Length].width)
				width = (roomCountTotal + playerRoomId+1) % activeRoom.Length;
				
			return width;
		}
		
		
		int PickRoomIDNotActive()
		{
			int id = Random.Range(0,allRooms.Length);
			
			while(RoomIsActive(id) && (!afterStart || afterStartOnlyRooms.Contains(allRooms[id]) == false))
			{
				id = (id + 1) % allRooms.Length;
			}
			
			return id;
		}
		
		/// <summary>
		/// Is the room of provided id active and in use
		/// </summary>
		/// <param name="id">id of room to check</param>
		/// <returns>true if is in use</returns>
		/// //this could be opatmized with a bool matrix
		bool RoomIsActive(int id)
		{
			//for all active rooms
			for(int i = 0; i < activeRoom.Length; i++)
			{
				//this room is the room we are looking for
				if(activeRoom[i] != null && activeRoom[i] == allRooms[id])
					//yes it is active
					return true;
			}
			
			//no it is not active, we checked all active rooms
			return false;
		}
	}
}