using UnityEngine;
using System;
using System.Collections.Generic;

public static partial class MZ {

	public partial class Action {
	
		public float deltaTime {
			get {
				if(deltaTimeFunc == null) { deltaTimeFunc = () => { return UnityEngine.Time.deltaTime; }; }
				return deltaTimeFunc();
			}
		}
	
		public float passedTime { get; protected set; }
		
		public Func<float> deltaTimeFunc;
		
		public Func<Action, bool> activeFunc;
		
		public Action<Action> resetAction;
		
		public Action<Action> startAction;
		
		public Action<Action> endAction;
		
		public Action<Action> updateAction;
		
		public GameObject gameObject;
		
	    public float duration;
	
		public virtual bool isActive {
			get {
				return (activeFunc != null)? activeFunc(this) : true;
			}
		}
	
	    public Action() {
	    }
	    
		public virtual void Reset() {
			if(resetAction != null) resetAction(this);
			passedTime = 0;
		}
	
		public virtual void Start() {
	     	Reset();
	        if(startAction != null) startAction(this);
		}
	
	    public virtual void End() {
			if(endAction != null) endAction(this);
		}
		
		public virtual void Update() {
			if(updateAction != null) updateAction(this);
			passedTime += deltaTime;
		}
	}
}