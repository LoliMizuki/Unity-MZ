using UnityEngine;
using System.Collections.Generic;

public class TestScene : MonoBehaviour {

	void Awake() {
		var cube = GameObject.Find("Cube");
		
		MZ.Components.Apply<MZActionBehaviour>(cube).Run(
			MZ.Actions.VelocityAndAcceleration(new Vector3(10, 0, 0), new Vector3(0, 1, 1), float.PositiveInfinity)
		);		
		
//		cube.GetComponent<MZActionBehaviour>().Run(
//			MZ.Actions.RepeatTimes(MZ.Actions.ColorFromTo(Color.white, Color.blue, 1.0f), 3)
//		);

//		cube.GetComponent<MZActionBehaviour>().Run(
//			MZ.Actions.Spawn(
//				MZ.Actions.MoveEllipseFromTo(
//					180, 90, false, Vector2.zero, 4, 2, 1 
//				),		
//				
//				MZ.Actions.Sequence(
//					MZ.Actions.ColorFromTo(Color.white, Color.blue, 1.0f),
//					MZ.Actions.ColorFromTo(Color.blue,  Color.red,  1.0f),
//					MZ.Actions.ColorFromTo(Color.red,   Color.grey, 1.0f)
//				)
//			)
//		);
	
//		var v1 = new Vector3(2, 6, 4);
//		var v2 = new Vector3(15, 3, 33);
//	
//		var r1 = MZ.Vectors.Dot(v1, v2);
//		var r2 = Vector3.Dot(v1, v2);
//		
//		var d1 = MZ.Vectors.LengthOfVector(v1);
//		var d2 = v1.magnitude;
//
//		Debug.Log(d1);
//		Debug.Log(d2);
	}
}
