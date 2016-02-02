using UnityEngine;
using System.Collections;

public static partial class MZ {

	public partial class Actions {
	
		public static ActionScaleFromTo ScaleFromTo(Vector3 from, Vector3 to, float duration) {
			return new ActionScaleFromTo(from, to, duration);
		}
		
		public static ActionScaleFromTo ScaleTo(Vector3 to, float duration) {
			return new ActionScaleFromTo(Vector3.zero, to, duration, true);
		}
	
		public class ActionScaleFromTo : ActionBase {
	
			public ActionScaleFromTo(Vector3 from, Vector3 to, float duration, bool fromTargetScale = false) {
				_from 			 = from;
				_to 			 = to;
				_fromTargetScale = fromTargetScale;
				this.duration 	 = duration;
			}
	
	        public override bool isActive { get { return passedTime < duration; } }
	
	        public override void Start() {
				if (_fromTargetScale) _from = gameObject.transform.localScale;
	            gameObject.transform.localScale = _from;
	            
	            base.Start();
	        }
	
	        public override void Update() {
	            Vector3 diff = _to - _from;
	            float currLerp = MZ.Maths.Lerp(passedTime, duration);
	            
	            gameObject.transform.localScale = new Vector3(_from.x + diff.x * currLerp,
	                                                          _from.y + diff.y * currLerp,
	                                                          _from.z + diff.z * currLerp);
	
	            base.Update();
	        }
	
	        public override void End() {
	            gameObject.transform.localScale = _to;
	            base.End();
	        }
	
	
	
			Vector3 _from;
			
			Vector3 _to;
			
			bool _fromTargetScale;
		}
	}
}