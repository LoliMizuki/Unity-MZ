using UnityEngine;
using System;
using System.Collections.Generic;

static public partial class MZ {

	public class Event {
	
		public string name = "";
	
		public Func<Event, bool> activeFunc;
		
		public Action<Event> startAction;
		
		public Action<Event> updateAction;
		
		public Action<Event> endAction;
	
		public float passedTime { get; internal set; }
		
		public virtual bool isActive { get { return (activeFunc != null)? activeFunc(this) : true; } }
		
		public Event() {
		}
	
		public virtual void Start() {
			passedTime = 0;
			
			if(startAction != null) {
				startAction(this);
			}
		}
	
		public virtual void Update() {
			if (isActive) UpdateWhenActive();
		}
	
		public virtual void End() {
			if(endAction != null) endAction(this);
		}
		
		public virtual void DrawGizmos() {}
		
		protected virtual void UpdateWhenActive() {
			passedTime += UnityEngine.Time.deltaTime;
			
			if(updateAction != null) updateAction(this);
		}
	}
}

