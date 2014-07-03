using UnityEngine;
using System;
using System.Collections.Generic;

static public partial class MZ {

	public class Event {
	
		public Func<Event, bool> activeFunc;
		public Action<Event> resetAction;
		public Action<Event> startAction;
		public Action<Event> updateAction;
		public Action<Event> endAction;
	
		public float passedTime {
			get { return _passedTime; } internal set { _passedTime = value; } 
		}
		float _passedTime;
		
		public virtual bool isActive {
			get { return (activeFunc != null)? activeFunc(this) : true; }
		}
	
		public Event() {
			Reset();
		}
		
		public virtual void Reset() {
			passedTime = 0;
	
			if(resetAction != null) {
				resetAction(this);
			}
		}
	
		public virtual void Start() {
			Reset();
	
			if(startAction != null) {
				startAction(this);
			}
		}
	
		public virtual void Update() {
			if(updateAction != null) {
				updateAction(this);
			}
	
			passedTime += UnityEngine.Time.deltaTime;
		}
	
		public virtual void End() {
			if(endAction != null) {
				endAction(this);
			}
		}
	}
}