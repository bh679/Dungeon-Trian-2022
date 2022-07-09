using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActive : MonoBehaviour
{
	public void SwitchActivePlz()
	{
		this.gameObject.SetActive(!this.gameObject.activeSelf);
	}
}
