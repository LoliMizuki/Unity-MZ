using UnityEngine;
using System;
using System.Collections.Generic;

public static partial class MZ {

	public partial class Actions {

		public partial class ActionBase {
		
			public float deltaTime {
				get {
					if (deltaTimeFunc == null) { deltaTimeFunc = () => { return UnityEngine.Time.deltaTime; }; }
					return deltaTimeFunc();
				}
			}
		
			public float passedTime { get; protected set; }
			
			public Func<float> deltaTimeFunc;
			
			public Func<ActionBase, bool> activeFunc = (a) => {
				if (a.duration < 0) return true;
				return a.passedTime <= a.duration;
			};
			
			public Action<ActionBase> startAction;
			
			public Action<ActionBase> endAction;
			
			public Action<ActionBase> updateAction;
			
			public GameObject gameObject;
			
		    public float duration;
		
			public virtual bool isActive {
				get {
					return (activeFunc != null)? activeFunc(this) : true;
				}
			}
		
		    public ActionBase() {
		    }
		
			public virtual void Start() {
				passedTime = 0;
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
}