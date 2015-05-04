using UnityEngine;
using System.Collections;

public static partial class MZ {

	public partial class Action {
    
        public static ActionVelAcc VelocityAndAcceleration(Vector3 velocity, Vector3 acceleration, float duration) {
            return new ActionVelAcc(velocity, acceleration, duration);
        }  
	
		public class ActionVelAcc : Action {
        
            public ActionVelAcc(Vector3 velocity, Vector3 acceleration, float duration) {
                this.duration = duration;
                _velocity = velocity;
                _acceleration = acceleration;
            }
            
            public override bool isActive { get { return passedTime < duration; } }
			
			public override void Start() {
				base.Start();
                                
                _initPosition = gameObject.transform.localPosition;
			}
            
            public override void Update() {
                base.Update();
                
                float passTimePow2 = Mathf.Pow(passedTime, 2);
                
                gameObject.transform.localPosition = new Vector3(
                    _initPosition.x + (_velocity.x * passedTime) + ((_acceleration.x * passTimePow2) / 2.0f),
                    _initPosition.y + (_velocity.y * passedTime) + ((_acceleration.y * passTimePow2) / 2.0f),
                    _initPosition.z + (_velocity.z * passedTime) + ((_acceleration.z * passTimePow2) / 2.0f)
                );
            }
            
            public override void End() {
                base.End();
                
                float passTimePow2 = Mathf.Pow(duration, 2);
                
                gameObject.transform.localPosition = new Vector3(
                    _initPosition.x + (_velocity.x * passedTime) + ((_acceleration.x * passTimePow2) / 2.0f),
                    _initPosition.y + (_velocity.y * passedTime) + ((_acceleration.y * passTimePow2) / 2.0f),
                    _initPosition.z + (_velocity.z * passedTime) + ((_acceleration.z * passTimePow2) / 2.0f)
                );
            }
		}
        
        #region - private
        
        Vector3 _initPosition;
        Vector3 _velocity;
        Vector3 _acceleration;
        
        #endregion
	}

}