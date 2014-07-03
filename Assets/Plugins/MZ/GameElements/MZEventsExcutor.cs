using UnityEngine;
using System.Collections.Generic;

public class MZEventsExcutor : MonoBehaviour {
	
	public List<MZ.Event> events {
		get {
			if(_events == null) {
				_events = new List<MZ.Event>();
			}

			return _events;
		}
	}
	List<MZ.Event> _events;

	public void Run(MZ.Event e) {
		if(_newEventsBuffer == null) {
			_newEventsBuffer = new List<MZ.Event>();
		}
	
		_newEventsBuffer.Add(e);
		e.Start();
	}
	
	public bool HasType<T>() {
		if(events == null || events.Count == 0) {
			return false;
		}
		
		foreach(MZ.Event e in events) {
			if(e.GetType() == typeof(T)) {
				return true;
			}
		}
		
		return false;
	}
	
	public void RemoveType<T>() {
		if(events == null || events.Count == 0) {
			return;
		}
		
		for(int i = 0; i < events.Count; i++) {
			MZ.Debugs.Log(events[i].GetType().ToString());
		
			if(events[i].GetType() != typeof(T)) {
				continue;
			}
			
			events[i].End();
			events.RemoveAt(i);
			i--;
		}
	}
	
	public void Clear() {
		events.Clear();
	}
	
	void Update() {
		AddNewEventsFromBuffer();
		MZ.MapReduce.Map(events, (e) => { e.Update(); } );
	}
	
	void AddNewEventsFromBuffer() {
		if(_newEventsBuffer == null || _newEventsBuffer.Count <= 0) {
			return;
		}
		
		foreach(MZ.Event e in _newEventsBuffer) {
			events.Add(e);
		}
		
		_newEventsBuffer.Clear();
	}

	void LateUpdate() {
		for(int i = 0; i < events.Count; i++) {
			if(!events[i].isActive) {
				events[i].End();
				events.RemoveAt(i);
				i--;
			}
		}
	}
	
	#region - private
	
	List<MZ.Event> _newEventsBuffer;
	
	#endregion
}