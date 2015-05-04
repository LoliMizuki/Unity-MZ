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
    }
}