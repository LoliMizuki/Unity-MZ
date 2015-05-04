using UnityEngine;
using System;
using System.Collections;

public static partial class MZ {

	public partial class Actions {
	
	    public static ActionRepeat RepeatTimes(ActionBase action, int times) {
			return new ActionRepeat(action, times: times);
	    }
	
		public static ActionRepeat RepeatForever(ActionBase action) {
			return new ActionRepeat(action, isForever: true);
		}
	    
	    public class ActionRepeat : ActionBase {
				
			public override bool isActive { get { return (_isForever)? true : _timesCount > 0; } }
			
			public ActionRepeat(ActionBase action, int times = 1, bool isForever = false) {
				_times = times;
	            _action = action;
	
				_isForever = isForever;
			}
	
			public override void Start() {
				base.Start();
				
				if (!_isForever) _timesCount = _times;
	
	            _action.gameObject = gameObject;
	            _action.Start();
			}
	
	        public override void Update() {
	            base.Update();
	            _action.Update();
	
	            if (!_action.isActive) {
					if (!_isForever) _timesCount--;
					
	                _action.Start();
	            }
	        }
	
	        public override void End() {
	            base.End();
	            _action.End();
	        }
	    
	    
	   
			bool _isForever;
	      
			int _times;
			
	        int _timesCount;
	        
	        ActionBase _action;
		}
	}
}