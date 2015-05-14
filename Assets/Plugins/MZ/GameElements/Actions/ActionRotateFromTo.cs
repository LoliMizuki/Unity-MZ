// Simple now
// Ignore "rotation direction"(clockwise or counterclockwise)

using UnityEngine;
using System.Collections;

public static partial class MZ {

	public partial class Actions {
    
        public static ActionRotateFromTo RotateFromTo(Vector3 from, Vector3 to, float duration) {
            return new ActionRotateFromTo(from, to, duration);
        }
        
        public static ActionRotateFromTo RotateAtZFromTo(float from, float to, float duration) {
			Vector3 from3 = new Vector3(0, 0, from);
            Vector3 to3 = new Vector3(0, 0, to);
        
            return new ActionRotateFromTo(from3, to3, duration);
        }
    
        public class ActionRotateFromTo : ActionBase {
        
            public Vector3 from;
            
            public Vector3 to;
        
            public ActionRotateFromTo(Vector3 from, Vector3 to, float duration) {
                this.from = from;
                this.to = to;
                this.duration = duration;
            }
            
            public override bool isActive { get { return passedTime < duration; } }
            
            public override void Start() {
                base.Start();
                gameObject.transform.localRotation = Quaternion.Euler(from);
            }
            
            public override void Update() {
                base.Update();
                float lerp = MZ.Maths.Lerp(passedTime, duration);
                gameObject.transform.localRotation = Quaternion.Euler(from + ((to - from)*lerp));
            }
            
            public override void End() {
                base.End();
                gameObject.transform.localRotation = Quaternion.Euler(to);
            }
        }   
    }
}