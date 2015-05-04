using UnityEngine;
using System;
using System.Collections;

public static partial class MZ {

	public partial class Actions {
	
	    public delegate void WithLambdaSignature(ActionLambda lambda);
	
	    public static ActionLambda Lambda(WithLambdaSignature action) {
	        return new ActionLambda(action);
	    }
	
	    public class ActionLambda : ActionBase {
	
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