using UnityEngine;
using System.Collections;

public class TestScene : MonoBehaviour {

	void Awake() {
		var v1 = new Vector3(2, 6, 4);
		var v2 = new Vector3(15, 3, 33);
	
		var r1 = MZ.Vectors.Dot(v1, v2);
		var r2 = Vector3.Dot(v1, v2);
		
		var d1 = MZ.Vectors.LengthOfVector(v1);
		var d2 = v1.magnitude;

		Debug.Log(d1);
		Debug.Log(d2);
	}
}
