using UnityEngine;
using System.Collections;

public static partial class MZ {

	public partial class Actions {
	
		public static ActionMoveFromTo MoveTo(Vector3 to, float duration) {
			return new ActionMoveFromTo(Vector3.zero, to, duration, fromTargetPosition: true);
		}
	   
	    public static ActionMoveFromTo MoveFromTo(Vector3 from, Vector3 to, float duration) {
	        return new ActionMoveFromTo(from, to, duration);
	    }
	
	    public class ActionMoveFromTo : ActionBase {
	
	        public ActionMoveFromTo(Vector3 from, Vector3 to, float duration, bool fromTargetPosition = false) {
	            _from = from;
	            _to = to;
				_fromTargetPosition = fromTargetPosition;
	            this.duration = duration;
	        }
	
			public override bool isActive { get { return passedTime < duration; } }
			
			public override void Start() {
				base.Start();
				if (_fromTargetPosition) _from = gameObject.transform.localPosition;
			}
	
			public override void Update() {
	            Vector3 diff = _to - _from;
	            float currLerp = MZ.Maths.Lerp(passedTime, duration);
	            
	            gameObject.transform.localPosition = new Vector3(_from.x + diff.x * currLerp,
	                                                             _from.y + diff.y * currLerp,
	                                                             _from.z + diff.z * currLerp);
	            base.Update();
			}
	
	        public override void End() {
	            gameObject.transform.localPosition = _to;
	            base.End();
	        }
	        
	        
	
	        Vector3 _from;
	        
	        Vector3 _to;
	        
			bool _fromTargetPosition;
	    }
	}
}