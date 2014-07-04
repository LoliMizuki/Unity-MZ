using UnityEngine;
using System;
using System.Collections.Generic;

static public partial class MZ {

	public partial class Action {
	
	    static public ActionSequence Sequence(params Action[] actions) {
	        return new ActionSequence(actions);
	    }
	
	    public class ActionSequence : Action {
	
	        List<Action> _actions;
	        Queue<Action> _actionsRunningQueue;
	        Action _currAction;
	        
	        public ActionSequence(params Action[] actions) {
	            _actions = new List<Action>(actions);
	            _currAction = null;
	        }
	
	        public override bool isActive {
	            get {
	                return _currAction != null || (_actionsRunningQueue != null && _actionsRunningQueue.Count > 0);
	            }
	        }
	
	        public override void Reset() {
	            base.Reset();
	
	            _actionsRunningQueue = new Queue<Action>(_actions);
	            _currAction = null;
	        }
	
	        public override void Update() {
	            if(_currAction == null && _actionsRunningQueue.Count > 0) {
	                _currAction = _actionsRunningQueue.Dequeue();
	                _currAction.gameObject = gameObject;
	                _currAction.deltaTimeFunc = deltaTimeFunc;
	                _currAction.Start();
	            }
	            
	            if(_currAction == null) {
	                return;
	            }
	            
	            _currAction.Update();
	            
	            if(!_currAction.isActive) {
	                _currAction.End();
	                _currAction = null;
	            }
	
	            base.Update();
	        }
	
	        public override void End() {
	            _actionsRunningQueue.Clear();
	            _actionsRunningQueue = null;
	            
	            _currAction = null;
	
	            base.End();
	        }
	
	    }
	}
}