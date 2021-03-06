using UnityEngine;
using System;
using System.Collections;

// (x - h)^2/(a^2) + (y - k)^2/(b^2) = 1, (h,k) is center, a, b is radius
// x = h + a*cos(theta)
// y = k + b*cos(theta)

public static partial class MZ {

	public partial class Actions {
	
	    public static ActionMoveEllipseFromTo MoveEllipseFromTo(
	    	float fromDeg,
	    	float toDeg, 
	    	bool isCCW, 
	    	Vector2 center, 
	    	float a, 
	    	float b, 
	    	float duration
    	) {
	        return new ActionMoveEllipseFromTo(fromDeg, toDeg, isCCW, center, a, b, duration);
	    }
	
	    public class ActionMoveEllipseFromTo : ActionBase {
	       
			public ActionMoveEllipseFromTo(float fromDeg, float toDeg, bool isCCW, Vector2 center, float a, float b, float duration) {
				this.duration = duration;
				
				_fromDeg = fromDeg;
				_toDeg = toDeg;
				_isCCW = isCCW;
				_center = center;
				_a = a;
				_b = b;
	
	            _fixedToDeg = _toDeg;
	            if(_isCCW && _toDeg <= _fromDeg) _fixedToDeg += 360;
				if(!_isCCW && _toDeg >= _fromDeg) _fixedToDeg -= 360;
			}
	
			public override bool isActive { get { return passedTime < duration; } }
	
			public override void Start() {
	            base.Start();
				_backupZ = gameObject.transform.localPosition.z;
	            gameObject.transform.localPosition = PoisitionFromDegrees(_fromDeg);
			}
	
	        public override void Update() {
				base.Update();
				
				float progress = MZ.Maths.Lerp(passedTime, duration);
				float currDeg = _fromDeg + ((_fixedToDeg - _fromDeg) * progress);
				gameObject.transform.localPosition = PoisitionFromDegrees(currDeg);	
	        }
	
	        public override void End() {
	            gameObject.transform.localPosition = PoisitionFromDegrees(_toDeg);
	            base.End();
	        }
	       
	       
	
			float _fromDeg;
			
			float _toDeg;
	        
	        float _fixedToDeg;
	
			bool _isCCW;
			
			Vector2 _center;
			
			float _a;
			
			float _b;
	
			float _backupZ;
	
	        Vector3 PoisitionFromDegrees(float deg) {
	            float _deg = deg % 360.0f;
	            if(_deg == 0) return new Vector3(_center.x + _a, _center.y, _backupZ);
	            if(_deg == 180) return new Vector3(_center.x - _a, _center.y, _backupZ);
	            if(_deg == 90) return new Vector3(_center.x, _center.y + _b, _backupZ);
	            if(_deg == 270) return new Vector3(_center.x, _center.y - _b, _backupZ);
	        
	            float ang = MZ.Degrees.RadiansFromDegrees(_deg);
	
	            float x = _center.x + (_a * Mathf.Cos(ang));
	            float y = _center.y + (_b * Mathf.Sin(ang));
	           
				return new Vector3(x, y, _backupZ);
	        } 
		}
	}
}