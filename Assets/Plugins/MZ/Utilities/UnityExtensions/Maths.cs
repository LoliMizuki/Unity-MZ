using UnityEngine;
using System.Collections;

public static partial class MZ {

	public static class Maths {

		public static float Lerp(float numerator, float denominator) {
            if (denominator == 0) return 1.0f;

            float lerp = numerator/denominator;
            return (lerp > 1.0f)? 1.0f : (lerp < 0)? 0 : lerp;
        }

		public static int RandomWithRange(int min, int max) {
            return Random.Range(min, max + 1);
        }
        
        public static float DistaceV2(Vector2 a, Vector2 b) {
        	return Vector2.Distance(a, b);
        }
        
		public static float DistaceV3(Vector3 a, Vector3 b) {
			return Vector3.Distance(a, b);
		}
    }
}