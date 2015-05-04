using UnityEngine;
using System;
using System.Collections;

public static partial class MZ {

	public partial class Action {
	
	    public static ActionMoveTo MoveTo(Vector3 to, float duration) {
	        return new ActionMoveTo(to, duration);
	    }
	
	    public class ActionMoveTo : Action {
	
	        public ActionMoveTo(Vector3 to, float duration) {
	            _to = to;
	            this.duration = duration;
	        }
	
	        public override bool isActive {
	            get {
	                return passedTime < duration;
	            }
	        }
	
	        public override void Start() {
	            _from = gameObject.transform.localPosition;
	            base.Start();
	        }
	
	        public override void Update() {
	            Vector3 diff = _to - _from;
	            float currLerp = MZ.Maths.Lerp(passedTime, duration);
	            
	            gameObject.transform.localPosition = new Vector3(_from.x + diff.x*currLerp,
	                                                             _from.y + diff.y*currLerp,
	                                                             _from.z + diff.z*currLerp);
	
	            base.Update();
	        }
	
	        public override void End() {
	            gameObject.transform.localPosition = _to;
	            base.End();
	        }
	
	        Vector3 _from;
	        Vector3 _to;
	    }
	}
}