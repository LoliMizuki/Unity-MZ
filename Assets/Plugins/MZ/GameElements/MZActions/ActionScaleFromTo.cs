using UnityEngine;
using System.Collections;

static public partial class MZ {

	public partial class Action {
	
		static public ActionScaleFromTo ScaleFromTo(Vector3 from, Vector3 to, float duration) {
			return new ActionScaleFromTo(from, to, duration);
		}
	
		public class ActionScaleFromTo : Action {
	
			public ActionScaleFromTo(Vector3 from, Vector3 to, float duration) {
				_from = from;
				_to = to;
				this.duration = duration;
			}
	
	        public override bool isActive {
	            get {
	                return passedTime < duration;
	            }
	        }
	
	        public override void Start() {
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
		}
	}
}