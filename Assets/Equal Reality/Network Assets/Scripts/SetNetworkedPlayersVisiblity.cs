using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNetworkedPlayersVisiblity : MonoBehaviour
{
	public bool HideAllPlayers = true;
	public bool ShowLocal = false;
	
    // Start is called before the first frame update
	void OnEnable()
	{
		PlayerSetActive.SetPlayersEnabledInScene(!HideAllPlayers);
		PlayerSetActive.SetLocalPlayerEnabled(ShowLocal);
    }
}
