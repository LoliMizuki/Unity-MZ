using UnityEngine;
using System.Collections.Generic;

public class MZActionBehaviour : MonoBehaviour {

	public void Clear() {
		_action = null;
	}
	
	public void Run(MZ.Action action) {
		_action = action;
        _action.gameObject = gameObject;
		_action.Start();
	}

	#region - private

	MZ.Action _action;

	void Update() {
		if(_action == null) {
			return;
		}

		_action.Update();

		if(!_action.isActive) {
			_action.End();
            _action = null;
		}
	}

	#endregion
}
