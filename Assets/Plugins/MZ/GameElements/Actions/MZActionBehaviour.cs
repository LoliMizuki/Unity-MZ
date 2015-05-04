using UnityEngine;
using System.Collections.Generic;

public class MZActionBehaviour : MonoBehaviour {

	public void Clear() {
		_action = null;
	}
	
	public void Run(MZ.Actions.ActionBase action) {
		_action = action;
        _action.gameObject = gameObject;
		_action.Start();
	}



	MZ.Actions.ActionBase _action;

	void Update() {
		if (_action == null) return;
	
		_action.Update();

		if (!_action.isActive) {
			_action.End();
            _action = null;
		}
	}
}
