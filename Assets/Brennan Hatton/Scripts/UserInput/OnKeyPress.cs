using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Brennan Hatton/Input/Key Press Event")]
public class OnKeyPress : MonoBehaviour {

    public UnityEvent myEvent;
    public KeyCode key;

    public enum PressType
    {
        Down = 0,
        Hold = 1,
        Up = 2
    }

    public PressType pressType = PressType.Down;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (pressType == PressType.Down)
        {
            if (Input.GetKeyDown(key))
            {
                myEvent.Invoke();
            }
        }
        else
        if (pressType == PressType.Hold)
        {
            if (Input.GetKey(key))
            {
                myEvent.Invoke();
            }
        }
        else
        if (pressType == PressType.Up)
        {
            if (Input.GetKeyUp(key))
            {
                myEvent.Invoke();
            }
        }
    }
}
