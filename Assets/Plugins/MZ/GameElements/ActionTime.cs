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
		
		public float delta { get { return UnityEngine.Time.deltaTime * scale; } }
		
		public Action<float, float> scaleDidSetAction = null;
		
		
		
		float _scale = 1;
	}
}