using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnerData
{
	public int actorNumber;
	public string name, room;
	public bool local, facilitator, toggleEnabled;
	
	public LearnerData(int _actorNumber, string _name, string _room, bool _local, bool _facilitator, bool _toggleEnabled)
	{
		actorNumber = _actorNumber;
		name = _name;
		room = _room;
		local = _local;
		facilitator = _facilitator;
		toggleEnabled = _toggleEnabled;
	}
}

public class ClassmateListLine : MonoBehaviour
{
	//public int number;
	//public string name, room;
	
	public Text numberText, nameText, roomText;
	public Toggle selection;
	public GameObject you, facilitatorObj;
	
	LearnerData data;
	public LearnerData Data {get{return data;}}
	
	public void Set(LearnerData newData)//int actorNumber, string name, string room, bool local, bool facilitator, bool toggleEnabled)
	{
		data = newData;
		
		numberText.text = data.actorNumber.ToString();
		nameText.text = data.name;
		roomText.text = data.room;
		selection.gameObject.SetActive(data.toggleEnabled);
		you.SetActive(data.local);
		facilitatorObj.SetActive(data.facilitator);
	}
	
	/*/ Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
	}*/
}
