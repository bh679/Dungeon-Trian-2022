using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.ComponentStates
{
	public interface iState
	{
		void SetState(bool reset);
		void CopyStartState(iState stateToCopy);
		void RevertToState();
	}
}