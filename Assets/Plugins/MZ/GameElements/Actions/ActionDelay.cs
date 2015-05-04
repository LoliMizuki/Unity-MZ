using UnityEngine;
using System.Collections;

public static partial class MZ {
	
	public partial class Action {
	
	    public static ActionDelay Delay(float time) {
	        return new ActionDelay(time);
	    }
	
	    public class ActionDelay : Action {
	       
	        float _time;
	
	        public ActionDelay(float time) {
	            _time = time;
	        }
	
			public override bool isActive {
				get {
					return passedTime < _time; 
				}
			}
	    }
	}
}