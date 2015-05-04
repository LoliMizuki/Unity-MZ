using UnityEngine;
using System;
using System.Collections;

public static partial class MZ {

	public partial class Action {
	
	    public static ActionRepeat RepeatTimes(Action action, int times) {
	        return new ActionRepeat(action, times);
	    }
	
		public static ActionRepeat RepeatForever(Action action) {
			ActionRepeat a = new ActionRepeat(action, 0);
			a.isForever = true;
	
			return a;
		}
	    
	    public class ActionRepeat : Action {
				
			public bool isForever;
	
			public ActionRepeat(Action action, int times) {
				_times = times;
	            _action = action;
	
				isForever = false;
			}
	
	        public override bool isActive {
	            get {
					return (isForever)? true : _timesCount > 0;
	            }
	        }
	
			public override void Start() {
				base.Start();
				
				if(!isForever) {
					_timesCount = _times;
				}
	
	            _action.gameObject = gameObject;
	            _action.Start();
			}
	
	        public override void Update() {
	            base.Update();
	            _action.Update();
	
	            if(!_action.isActive) {
					if(!isForever) _timesCount--;
	                _action.Start();
	            }
	        }
	
	        public override void End() {
	            base.End();
	            _action.End();
	        }
	    
			int _times;
	        int _timesCount;
	        Action _action;
		}
	}
}