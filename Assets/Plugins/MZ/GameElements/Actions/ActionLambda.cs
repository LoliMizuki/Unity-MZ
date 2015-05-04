using UnityEngine;
using System;
using System.Collections;

static public partial class MZ {

	public partial class Action {
	
	    public delegate void WithLambdaSignature(ActionLambda lambda);
	
	    static public ActionLambda Lambda(WithLambdaSignature action) {
	        return new ActionLambda(action);
	    }
	
	    public class ActionLambda : Action {
	
			public override bool isActive { get { return false; } }
			
	        public ActionLambda(WithLambdaSignature action) {
	            _action = action;
	        }
	
	        public override void End() {
	            _action(this);
	            base.End();
	        }
	
	        WithLambdaSignature _action;
	    }
	}
}