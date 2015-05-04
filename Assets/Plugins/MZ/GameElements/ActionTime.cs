using System;
using System.Collections;
using UnityEngine;

public partial class MZ {

	public class ActionTime {
		
		public float scale {
			get { return _scale; }
			set {
				float pre = _scale;
				float curr = value;
				_scale = curr;
				
				if (scaleDidSetAction != null) scaleDidSetAction(pre, curr);
			}
		}
		float _scale = 1;
		
		public float delta { get { return UnityEngine.Time.deltaTime * scale; } }
		
		public Action<float, float> scaleDidSetAction = null;
	}
}