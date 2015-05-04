using UnityEngine;
using System;
using System.Collections.Generic;

public class MZEventsExcutor : MonoBehaviour {

	public List<MZ.Event> events { get { return _events; } }

	public void Run(MZ.Event e, Action<MZ.Event> settingAction = null) {
		if (settingAction != null) settingAction(e);
		_newEventsBuffer.Add(e);
	}
	
	public bool RunIfNotExistTheSameType(MZ.Event e, Action<MZ.Event> settingAction = null) {
		if (HasType(e.GetType())) return false;
		
		Run(e, settingAction);
		return true;
	}
	
	public bool HasType<T>() where T: MZ.Event {
		foreach (MZ.Event e in _newEventsBuffer) if (e.GetType() == typeof(T)) return true;
		foreach (MZ.Event e in _events) 		 if (e.GetType() == typeof(T)) return true;
		return false;
	}
	
	public bool HasType(Type type) {
		foreach (MZ.Event e in _newEventsBuffer) if (e.GetType() == type) return true;
		foreach (MZ.Event e in _events) 		 if (e.GetType() == type) return true;
		return false;
	}
	
	public void RemoveType<T>() where T: MZ.Event {
		for (int i = 0; i < _events.Count; i++) {
			if(_events[i].GetType() != typeof(T)) continue;
			
			if (_isUpdating) {
				_willBeRemoveBuffer.Add(_events[i]);
			} else {
				RemoveImmediately(_events[i]);
				i--;
			}
		}
	}
	
	public void ActionToType<T>(Action<MZ.Event> action) where T: MZ.Event {
		if (action == null) return;
		
		for (int i = 0; i < _events.Count; i++) {
			if(_events[i].GetType() != typeof(T)) continue;
			action(_events[i]);
		}
	}
	
	public void Clear() {
		_events.Clear();
		_newEventsBuffer.Clear();
		_willBeRemoveBuffer.Clear();
	}
	
	
	
	#region - private
	
	bool _isUpdating = false;
	
	List<MZ.Event> _events = new List<MZ.Event>();
	
	List<MZ.Event> _newEventsBuffer = new List<MZ.Event>();
	
	List<MZ.Event> _willBeRemoveBuffer = new List<MZ.Event>();
	
	void Update() {
		_isUpdating = true;
		AddNewEventsFromBuffer();
		MZ.MapReduce.MapToList(_events, (e) => { e.Update(); } );
	}
	
	void LateUpdate() {
		RemoveFromRemvoeBuffer();
		RemoveInactiveEvents();
	}
	
	void OnDrawGizmos() {
		MZ.MapReduce.MapToList(_events, (e) => { e.DrawGizmos(); } );
	}
	
	void AddNewEventsFromBuffer() {
		int count = _newEventsBuffer.Count;
		for (int i = 0; i < count; i++) {
			MZ.Event e = _newEventsBuffer[i];
			_events.Add(e);
			e.Start();
		}
		
		_newEventsBuffer.RemoveRange(0, count);
	}
	
	void RemoveImmediately(MZ.Event e) {
		e.End();
		if (_events.Contains(e)) 		  _events.Remove(e);
		if (_newEventsBuffer.Contains(e)) _newEventsBuffer.Remove(e);
	}
	
	void RemoveFromRemvoeBuffer() {
		foreach (MZ.Event e in _willBeRemoveBuffer) RemoveImmediately(e);
		_willBeRemoveBuffer.Clear();
	}
	
	void RemoveInactiveEvents() {
		for (int i = 0; i < _events.Count; i++) {
			if (_events[i].isActive == false) { 
				RemoveImmediately(_events[i]);
				i--;
			}
		}	
	}
	
	#endregion
}