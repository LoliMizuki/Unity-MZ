using UnityEngine;
using System.Collections;

static public partial class MZ {

	public static class Maths {
    
    	// rects?
        public static Rect RectWithCenterAndSize(Vector2 center, Vector2 size) {
            return new Rect(center.x - size.x/2, center.y - size.y/2, size.x, size.y);
        }

		// rects?
        public static bool IsRectIntersect(Rect rectA, Rect rectB) {
            return (Mathf.Abs(rectA.x - rectB.x) < (Mathf.Abs(rectA.width + rectB.width)/2))
                && (Mathf.Abs(rectA.y - rectB.y) < (Mathf.Abs(rectA.height + rectB.height)/2));
        }

        static public float Lerp(float numerator, float denominator) {
            if (denominator == 0) return 1.0f;

            float lerp = numerator/denominator;
            return (lerp > 1.0f)? 1.0f : (lerp < 0)? 0 : lerp;
        }

        static public int RandomFromRange(int min, int max) {
            return Random.Range(min, max + 1);
        }

        static public float DistanceV2(Vector2 p1, Vector2 p2) {
            return Vector2.Distance(p1, p2);
        }
    
        static public float DistanceV3(Vector3 p1, Vector3 p2) {
            return Vector3.Distance(p1, p2);
        }

        static public float DistanceV3IgnoreZ(Vector3 p1, Vector3 p2) {
			return Vector2.Distance(p1.ToVector2(), p2.ToVector2());
        }

        static public float DistanceV2Pow2(Vector2 p1, Vector2 p2) {
            return (p2.x - p1.x)*(p2.x - p1.x) + (p2.y - p1.y)*(p2.y - p1.y);
        }

        static public float DistanceV2Pow2FromV3(Vector3 p1, Vector3 p2) {
            return (p2.x - p1.x)*(p2.x - p1.x) + (p2.y - p1.y)*(p2.y - p1.y);
        }
    }
}