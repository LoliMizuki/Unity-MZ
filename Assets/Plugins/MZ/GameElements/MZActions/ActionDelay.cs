using UnityEngine;
using System.Collections;

static public partial class MZ {
	
	public partial class Action {
	
	    static public ActionDelay Delay(float time) {
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