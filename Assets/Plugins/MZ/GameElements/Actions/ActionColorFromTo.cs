using UnityEngine;
using System;
using System.Collections;

public static partial class MZ {
	
	public partial class Actions {
	
	    public static ActionColorFromTo ColorFromTo(Color from, Color to, float duration) {
	        return new ActionColorFromTo(from, to, duration);
	    }
	
		public class ActionColorFromTo : ActionBase {
	
			public ActionColorFromTo(Color from, Color to, float duration) {
				_from = from;
				_to = to;
				this.duration = duration;
			}
	
			public override void Start() {
	            _diff = new Color(_to.r - _from.r, _to.g - _from.g, _to.b - _from.b);
	            gameObject.GetComponent<Renderer>().material.color = _from;
				base.Start();
			}
	
	        public override void Update() {
	            float lerp = MZ.Maths.Lerp(passedTime, duration);
	
	            gameObject.GetComponent<Renderer>().material.color = new Color(
					_from.r + (_diff.r * lerp),
	                _from.g + (_diff.g * lerp),
					_from.b + (_diff.b * lerp)
	            );
	
	            base.Update();
	        }
	
	        public override void End() {
	            gameObject.GetComponent<Renderer>().material.color = _to;
	            base.End();
	        }
	
	
	
			Color _from;
			
			Color _to;
			
	        Color _diff;
		}
	}
}