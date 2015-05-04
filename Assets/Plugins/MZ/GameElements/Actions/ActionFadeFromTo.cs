using UnityEngine;
using System.Collections;

public static partial class MZ {

	public partial class Actions {
	
		public static ActionFadeFromTo FadeFromTo(int from, int to, float duration) {
			return new ActionFadeFromTo((float)from / 255.0f, (float)to / 255.0f, duration);
		}
	
		public class ActionFadeFromTo : ActionBase {
	
			public ActionFadeFromTo(float from, float to, float duration) {
				_from = from;
				_to = to;
				this.duration = duration;
			}
	
			public override bool isActive {
				get { return passedTime < duration; }
			}
	
			public override void Update() {
				float diff = _to - _from;
	            float currLerp = MZ.Maths.Lerp(passedTime, duration);
	
				Color c = gameObject.GetComponent<Renderer>().material.color;
				gameObject.GetComponent<Renderer>().material.color = new Color(c.r, c.g, c.b, _from + diff * currLerp);
	
				base.Update();
			}
	
			public override void End() {
				Color c = gameObject.GetComponent<Renderer>().material.color;
				gameObject.GetComponent<Renderer>().material.color = new Color(c.r, c.g, c.b, _to);
			
				base.End ();
			}
	
			float _from;
			float _to;
		}
	}
}