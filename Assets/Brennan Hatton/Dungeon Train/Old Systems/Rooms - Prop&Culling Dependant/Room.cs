using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BrennanHatton.Props;
using BrennanHatton.Props.Extentions;
using BrennanHatton.Culling;

namespace BrennanHatton.DungeonTrain.Rooms
{

	public class Room : Prop
	{
		
		public int width = 1, height = 1, length = 3;
		
		public GameObject[,] floor, leftSideWall, rightSideWall, frontWall, backWall, roof, teleArea;
		
		[Tooltip("Tile size")]
		public int tsize = 5;
		
		
		public bool GenerateFromParts = false;
		public RoomParts roomParts;
		public GameObject EnterTrigger;
		
		public Door door;
		public CullingManager cullingManager;
		public bool leftWallSolid, rightWallSolid;
		public UnityEvent onRemove;
		public WindowPositionSwitcher windowPositionSwitcher;
		public RoomCreator roomManager;
		
		
		//Debugging tools
		public Camera camera;
		
		#if UNITY_EDITOR
		protected override void Reset()
		{
			cullingManager = this.GetComponentInChildren<CullingManager>();
			door = this.GetComponentInChildren<Door>();
			roomParts = this.GetComponentInChildren<RoomParts>();
			windowPositionSwitcher = this.GetComponentInChildren<WindowPositionSwitcher>();
			roomManager = FindObjectOfType<RoomCreator>();
			camera = this.GetComponentInChildren<Camera>();
			base.Reset();
		}
		#endif
		
		
		//public UnityEvent onRoomReset = new UnityEvent();
		
		//public bool windowsAreLeft = false;
		public override void Place(PropPlacer newPlacer)
		{
			if(camera == null)
				camera = this.GetComponentInChildren<Camera>();
			
			
			if(camera)
				camera.gameObject.SetActive(Debugging && Application.isEditor);
			
			EnterTrigger.SetActive(true);
			door.openner.gameObject.SetActive(true);
			door.Unlock();
			//onRoomReset.Invoke();
			
			//look for a windows position switcher
			windowPositionSwitcher = this.GetComponentInChildren<WindowPositionSwitcher >();
			
			//if there is one
			if(windowPositionSwitcher != null)
			{
				//set windows on appropritate side
				if(roomManager.createWindowsLeft)
					windowPositionSwitcher.SetWindowLeft();
				else
					windowPositionSwitcher.SetWindowRight();
			}
			
			//place walls and all things
			base.Place(newPlacer);
			
			//check which side the windows ended up on
			GetWallSolidSides();
			
		}
		
		public bool AreTerrainSidesSame()
		{
			//if there are no windows
			return (leftWallSolid == rightWallSolid);
		}
		
		void GetWallSolidSides()
		{
			
			Wall[] walls = this.GetComponentsInChildren<Wall>();
			
			//Debug.Log(walls.Length);
			
			if(walls.Length >= 2)
			{
				leftWallSolid = true;
				rightWallSolid = true;
				
				for(int i = 0 ;i < walls.Length; i++)
				{
					//if x >= 0 - its right
					if(walls[i].transform.localPosition.x >= 0)
					{
						if(windowPositionSwitcher == null || !windowPositionSwitcher.WindowIsLeft)
						{
							if(rightWallSolid == true)
							{
								rightWallSolid = walls[i].isSolid;
							}
						}
					}else
					{
						if(windowPositionSwitcher == null || windowPositionSwitcher.WindowIsLeft)
						{
							if(leftWallSolid == true)
							{
								leftWallSolid = walls[i].isSolid;
							}
						}
					}
				
				}
				
			}else
			{
				leftWallSolid = false;
				rightWallSolid = false;
			}
		}
		
		public void Create()
		{
			if(!roomParts)
			{
				Debug.LogError("Missing Room Parts Reference - cant generate room");
				return;
			}
			
			GameObject newTile;
			
			floor = new GameObject[width, length];
			teleArea = new GameObject[width, length];
			for(int w = 0; w < width; w++)
			{
				for(int l = 0; l < length; l++)
				{
					newTile = Instantiate(roomParts.GetFloor(),this.transform);
					newTile.name = "Floor " + w + "," + l + newTile.name;
					newTile.transform.localPosition = new Vector3(w*tsize,0,l*tsize);
					newTile.transform.localEulerAngles = new Vector3(0,0,0);
					
					floor[w,l] = newTile;
					
					if(roomParts.teleAreaTile != null)
					{
						newTile = Instantiate(roomParts.teleAreaTile,floor[w,l].transform);
						newTile.name = "Tele " + w + "," + l + newTile.name;
						newTile.transform.localPosition = Vector3.zero;
						newTile.transform.localEulerAngles = Vector3.zero;;
						teleArea[w,l] = newTile;
					}
				}
			}
			
			roof = new GameObject[width, length];
			for(int w = 0; w < width; w++)
			{
				for(int l = 0; l < length; l++)
				{
					newTile = Instantiate(roomParts.GetRoof(),this.transform);
					newTile.name = "Roof " + w + "," + l + newTile.name;
					newTile.transform.localPosition = new Vector3(w*tsize,height*tsize,l*tsize);
					newTile.transform.localEulerAngles = new Vector3(0,0,0);
					roof[w,l] = newTile;
				}
			}
			
			
			leftSideWall = new GameObject[length, height];
			for(int l = 0; l < length; l++)
			{
				for(int h = 0; h < height; h++)
				{
					newTile = Instantiate(roomParts.GetWall(),this.transform);
					newTile.name = "Left Wall " + l + "," + h+ newTile.name;
					newTile.transform.localPosition = new Vector3(
						-Mathf.Ceil(width/2f)*tsize,
						h*tsize,
						l*tsize
					);
					newTile.transform.localEulerAngles = new Vector3(0,90,0);
					leftSideWall[l,h] = newTile;
				}
			}
			
			rightSideWall = new GameObject[length, height];
			for(int l = 0; l < length; l++)
			{
				for(int h = 0; h < height; h++)
				{
					
					newTile = Instantiate(roomParts.GetWall(),this.transform);
					newTile.name = "Right Wall " + l + "," + h+ newTile.name;
					newTile.transform.localPosition = new Vector3(
						Mathf.Floor(width/2f)*tsize,
						h*tsize,
						(l+1)*tsize
					);
					newTile.transform.localEulerAngles = new Vector3(0,-90,0);
					rightSideWall[l,h] = newTile;
				}
			}
			
			frontWall = new GameObject[width, height];
			int x = (int)(width / 2f);
			for(int w = 0; w < width; w++)
			{
				for(int h = 0; h < height; h++)
				{
					if(w == x)
						newTile = Instantiate(roomParts.GetDoor(),this.transform);
					else
						newTile = Instantiate(roomParts.GetWall(),this.transform);
						
					newTile.name = "front Wall " + w + "," + h+ newTile.name;
					newTile.transform.localPosition = new Vector3(
						w*tsize,
						h*tsize,
						0
					);
					newTile.transform.localEulerAngles = new Vector3(0,0,0);
					frontWall[w,h] = newTile;
					
					
					if(w == x)
					{
						newTile = Instantiate(newTile,this.transform);
						newTile.name = "front Wall " + w + "," + h+ newTile.name;
						newTile.transform.localPosition = new Vector3(
							(w-1)*tsize,
							h*tsize,
							0
						);
						newTile.transform.localEulerAngles = new Vector3(0,180,0);
						backWall[w,h] = newTile;
					}
				}
			}
			
			
			backWall = new GameObject[width, height];
			for(int w = 0; w < width; w++)
			{
				for(int h = 0; h < height; h++)
				{
					if(w != x)
					{
						newTile = Instantiate(roomParts.GetWall(),this.transform);
						
						newTile.name = "front Wall " + w + "," + h+ newTile.name;
						newTile.transform.localPosition = new Vector3(
							(w-1)*tsize,
							h*tsize,
							length*tsize
						);
						newTile.transform.localEulerAngles = new Vector3(0,180,0);
						backWall[w,h] = newTile;
					}
				}
			}
			backWall[x,0] = roomParts.GetDoor();
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
			if(GenerateFromParts)
				Create();
			    
			if(roomManager == null)
				roomManager = FindObjectOfType<RoomCreator>();
	    }

	}
}
