using UnityEngine;
using System;
using System.Collections;

static public partial class MZ {

	public partial class Action {
	
	    public delegate void LambdaTimeSignature(ActionLambdaWithTime lambdaTime, float deltaTime, float passedTime);
	
	    static public ActionLambdaWithTime LambdaWithTime(LambdaTimeSignature action, float duration) {
	        return new ActionLambdaWithTime(action, duration);
	    }
	
		public class ActionLambdaWithTime : Action {
	
	        public override bool isActive {
	            get {
	                return passedTime < duration;
	            }
	        }
	
	        public ActionLambdaWithTime(LambdaTimeSignature action, float duration) {
	            _action = action;
	            this.duration = duration;
	        }
	
	        public override void Update() {
	            _action(this, deltaTime, passedTime);
	            base.Update();
	        }
	
	        LambdaTimeSignature _action;
		}
	}
}