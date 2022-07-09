#if BNG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Props;
using BNG;

namespace BrennanHatton.DungeonTrain.Rooms
{
	public class DroppedItemCleaner : MonoBehaviour
	{
		public RoomCreator creator;
		List<Prop>[] roomPropsList;
		public Transform dropBin;
		
		void Reset()
		{
			creator = this.GetComponent<RoomCreator>();
			dropBin = this.transform;
		}
		
		void Start()
		{
			roomPropsList = new List<Prop>[creator.activeRoomsCount];
			
			for(int i = 0; i < roomPropsList.Length; i++)
			{
				roomPropsList[i] = new List<Prop>();
			}
			//props = new KeyValuePair<int, Prop>[creator.activeRoomsCount];
		}
		
		public void GrabProp(Prop prop, Grabbable grabbale)
		{
			prop.ChangePlacer(null);//.Deatach();
			
			grabbale.UpdateOriginalParent(dropBin);
			RemoveFromDropBins(prop);
			
		}
		
		void RemoveFromDropBins(Prop propToRemove)
		{
			int id = creator.GetCurrentRoomRotationID();
			for(int i = 0; i < roomPropsList.Length; i++)
			{
				id = (id + i) % roomPropsList.Length;
				if(roomPropsList[id].Contains(propToRemove))
				{
					roomPropsList[id].Remove(propToRemove);
					return;
				}
			}
		}
		
		public void DropProp(Prop prop)
		{
			roomPropsList[creator.GetCurrentRoomRotationID()].Add(prop);
			prop.transform.SetParent(dropBin);
		}
		
		public void CleanUpCart()
		{
			int id = (creator.GetCurrentRoomRotationID() - (int)(creator.activeRoomsCount/2f) + creator.activeRoomsCount + 1) % roomPropsList.Length;
			
			for(int i = 0; i < roomPropsList[id].Count; i++)
			{
				//roomPropsList[id][i].detached = false;
				roomPropsList[id][i].ResetToOriginalParent();
				roomPropsList[id][i].Remove();
			}
		}
		
	}
}
#endif