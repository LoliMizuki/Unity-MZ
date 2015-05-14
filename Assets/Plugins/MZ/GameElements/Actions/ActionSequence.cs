using UnityEngine;
using System;
using System.Collections.Generic;

public static partial class MZ {

	public partial class Actions {
	
	    public static ActionSequence Sequence(params ActionBase[] actions) {
	        return new ActionSequence(actions);
	    }
	
	    public class ActionSequence : ActionBase {
	        
			public override bool isActive {
				get { return _currAction != null || (_actionsRunningQueue != null && _actionsRunningQueue.Count > 0); }
			}
			
	        public ActionSequence(params ActionBase[] actions) {
	            _actions = new List<ActionBase>(actions);
	            _currAction = null;
	        }
	        
			public override void Start() {
				base.Start();
				
				_actionsRunningQueue = new Queue<ActionBase>(_actions);
			}	
	
	        public override void Update() {
	            if (_currAction == null && _actionsRunningQueue.Count > 0) {
	                _currAction = _actionsRunningQueue.Dequeue();
	                _currAction.gameObject = gameObject;
	                _currAction.deltaTimeFunc = deltaTimeFunc;
	                _currAction.Start();
	            }
	            
	            if (_currAction == null) return;
	            
	            _currAction.Update();
	            
	            if (!_currAction.isActive) {
	                _currAction.End();
	                _currAction = null;
	            }
	
	            base.Update();
	        }
	
	        public override void End() {
	            _actionsRunningQueue.Clear();
	            _currAction = null;
	
	            base.End();
	        }
	        
	        
	        
			List<ActionBase> _actions;
			
			Queue<ActionBase> _actionsRunningQueue;
			
			ActionBase _currAction;
	
	    }
	}
}