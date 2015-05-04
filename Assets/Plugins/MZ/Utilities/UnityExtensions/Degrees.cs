using UnityEngine;
using System.Collections;

public static partial class MZ {

	public static class Degrees {
		
		public static float RadiansFromDegrees(float degrees) {
			return UnityEngine.Mathf.Deg2Rad*degrees;
		}
		
		public static float DegreesFromRadians(float radians) {
			return UnityEngine.Mathf.Rad2Deg*radians;
		}
		
		public static float DegreesFromV1ToV2(Vector2 v1, Vector2 v2) {
			float v1Dotv2 = Vectors.Dot(v1, v2);
			float v1lenMulv2len = Vectors.LengthOfVector(v1)*Vectors.LengthOfVector(v2);
			
			if (v1lenMulv2len == 0) return 0;
		
			
			float result = Mathf.Acos(v1Dotv2/v1lenMulv2len);
			result = DegreesFromRadians(result);
			
			return result;
		}
		
		public static float DegreesFromP1ToP2(Vector2 p1, Vector2 p2) {
			Vector2 uv = Vectors.UnitVectorV2FromP1ToP2(p1, p2);
			return DegreesFromXAxisToVector(uv);
		}
		
		public static float DegreesFromV3P1ToP2IgnoreZ(Vector3 p1, Vector3 p2) {
			return DegreesFromP1ToP2(Vectors.Vector2From3(p1), Vectors.Vector2From3(p2));
		}
		
		public static float DegreesFromXAxisToVector(Vector2 vector) {
			if (vector.x == 0) {
				if (vector.y > 0) return 90;
				if (vector.y < 0) return 270;
			}
			
			if (vector.y == 0) {
				if (vector.x > 0) return 0;
				if (vector.x < 0) return 180;
			}
			
			float result = DegreesFromV1ToV2(new Vector2(1, 0), vector);
			return (vector.y >= 0)? result : 360 - result;
		}
		
		public static float FormatDegrees(float origin) {
			if (0 <= origin && origin < 360) {
				return origin;
			}
			
			float _origin = origin%360.0f;
			return (_origin >= 0)? _origin : 360 + _origin;
		}
	}
}