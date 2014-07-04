using UnityEngine;
using System.Collections;

// (x - h)^2/(a^2) + (y - k)^2/(b^2) = 1, (h,k) is center, a, b is radius
// x = h + a*cos(theta)
// y = k + b*cos(theta)

static public partial class MZ {

	public partial class Action {
	
	    static public ActionEllipse EllipseFromTo(float fromDeg, float toDeg, bool isCCW, Vector2 center, float a, float b, float duration) {
	        return new ActionEllipse(fromDeg, toDeg, isCCW, center, a, b, duration);
	    }
	
	    public class ActionEllipse : Action {
	       
			public ActionEllipse(float fromDeg, float toDeg, bool isCCW, Vector2 center, float a, float b, float duration) {
				_fromDeg = fromDeg;
				_toDeg = toDeg;
				_isCCW = isCCW;
				_center = center;
				_a = a;
				_b = b;
				_duration = duration;
	
	            _fixedToDeg = _toDeg;
	            if(_isCCW && _toDeg <= _fromDeg) {
	                _fixedToDeg += 360;
	            }
	
	            if(!_isCCW && _toDeg >= _fromDeg) {
	                _fixedToDeg -= 360;
	            }
			}
	
			public override bool isActive { get { return passedTime < _duration; } }
	
			public override void Start() {
	            base.Start();
				_backupZ = gameObject.transform.localPosition.z;
	            gameObject.transform.localPosition = PoisitionFromDegrees(_fromDeg);
			}
	
	        public override void Update() {
	            float lerp = MZ.Maths.Lerp(passedTime, _duration);
	            float currDeg = _fromDeg + ((_fixedToDeg - _fromDeg) * lerp);
	
	            gameObject.transform.localPosition = PoisitionFromDegrees(currDeg);
	
	            base.Update();
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
			float _duration;
	
			float _backupZ;
	
	        Vector3 PoisitionFromDegrees(float deg) {
	            float _deg = deg % 360.0f;
	            if(_deg == 0) return new Vector3(_center.x + _a, _center.y, _backupZ);
	            if(_deg == 180) return new Vector3(_center.x - _a, _center.y, _backupZ);
	            if(_deg == 90) return new Vector3(_center.x, _center.y + _b, _backupZ);
	            if(_deg == 270) return new Vector3(_center.x, _center.y - _b, _backupZ);
	        
	            float ang = MZ.Maths.RadiansFromDegrees(_deg);
	
	            float x = _center.x + (_a * Mathf.Cos(ang));
	            float y = _center.y + (_b * Mathf.Sin(ang));
	           
				return new Vector3(x, y, _backupZ);
	        } 
		}
	}
}