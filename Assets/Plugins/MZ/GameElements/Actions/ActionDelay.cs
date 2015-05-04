using UnityEngine;
using System.Collections;

public static partial class MZ {
	
	public partial class Actions {
	
	    public static ActionDelay Delay(float time) {
	        return new ActionDelay(time);
	    }
	
	    public class ActionDelay : ActionBase {
	       
			public override bool isActive { get { return passedTime < this.duration; } }
			
	        public ActionDelay(float time) {
	            this.duration = time;
	        }
	    }
	}
}