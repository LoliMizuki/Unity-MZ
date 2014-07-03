
using UnityEngine;
using System.Collections;

// memo: 
// all calculate default is Vector2
// vector need three
// V2
// V3
// V2FromV3IgnoreZ

static public partial class MZ {

    public class Maths {
        public static Rect RectFromCenterAndSize(Vector2 center, Vector2 size) {
            return new Rect(center.x - size.x/2, center.y - size.y/2, size.x, size.y);
        }

        public static bool IsRectAIntersectB(Rect rectA, Rect rectB) {
            return (Mathf.Abs(rectA.x - rectB.x) < (Mathf.Abs(rectA.width + rectB.width)/2))
                && (Mathf.Abs(rectA.y - rectB.y) < (Mathf.Abs(rectA.height + rectB.height)/2));
        }

        static public float Lerp(float numerator, float denominator) {
            if (denominator == 0) {
                return 1.0f;
            }

            float lerp = numerator/denominator;
            return (lerp > 1.0f)? 1.0f : (lerp < 0)? 0 : lerp;
        }

        static public int RandomFromRange(int min, int max) {
            return Random.Range(min, max + 1);
        }

        static public float RadiansFromDegrees(float degrees) {
            return UnityEngine.Mathf.Deg2Rad*degrees;
        }

        static public float DegreesFromRadians(float radians) {
            return UnityEngine.Mathf.Rad2Deg*radians;
        }

        static public float Dot(Vector2 p1, Vector2 p2) {
            return (p1.x*p2.x) + (p1.y*p2.y);
        }

        static public float DistanceV2(Vector2 p1, Vector2 p2) {
            return Vector2.Distance(p1, p2);
        }
    
        static public float DistanceV3(Vector3 p1, Vector3 p2) {
            return Vector3.Distance(p1, p2);
        }

        static public float DistanceV3IgnoreZ(Vector3 p1, Vector3 p2) {
            return Vector2.Distance(Vector2FromVector3(p1), Vector2FromVector3(p2));
        }

        static public float DistanceV2Pow2(Vector2 p1, Vector2 p2) {
            return (p2.x - p1.x)*(p2.x - p1.x) + (p2.y - p1.y)*(p2.y - p1.y);
        }

        static public float DistanceV2Pow2FromV3(Vector3 p1, Vector3 p2) {
            return (p2.x - p1.x)*(p2.x - p1.x) + (p2.y - p1.y)*(p2.y - p1.y);
        }

        static public float DistanceV2FromV3(Vector3 p1, Vector3 p2) {
            return Mathf.Sqrt(DistanceV2Pow2FromV3(p1, p2));
        }

        static public float LengthOfVector(Vector2 vector) {
            return Mathf.Sqrt(vector.x*vector.x + vector.y*vector.y);
        }

        static public Vector2 UnitVectorV2FromP1ToP2(Vector2 p1, Vector2 p2) {
            float diffY = p2.y - p1.y;
            float diffX = p2.x - p1.x;

            if (diffY == 0) {
                if (diffX > 0) {
                    return new Vector2(1, 0);
                } else if (diffX < 0) {
                        return new Vector2(-1, 0);
                    } else {
                        return Vector2.zero;
                    }
            }

            float length = Mathf.Sqrt(Mathf.Pow(diffX, 2) + Mathf.Pow(diffY, 2));

            return new Vector2(diffX/length, diffY/length);
        }

        static public Vector2 UnitVectorV2FromV3P1ToP2IgnoreZ(Vector3 p1, Vector3 p2) {
            return UnitVectorV2FromP1ToP2(new Vector2(p1.x, p1.y), new Vector2(p2.x, p2.y));
        }

        static public Vector2 UnitVectorFromVector(Vector2 vector) {
            float length = LengthOfVector(vector);
            return new Vector2(vector.x/length, vector.y/length);
        }

        static public Vector2 UnitVectorFromVectorAddDegree(Vector2 vector, float degrees) {
            float radians = RadiansFromDegrees(degrees);

            float c = Mathf.Cos(radians);
            float s = Mathf.Sin(radians);

            Vector2 resultVetor = new Vector2(vector.x*c - vector.y*s, vector.x*s + vector.y*c);
            Vector2 unitResultVetor = UnitVectorFromVector(resultVetor);

            return unitResultVetor;
        }

        static public Vector2 UnitVectorFromVectorAddDegree(float degrees) {
            return UnitVectorFromVectorAddDegree(new Vector2(1, 0), degrees);
        }

        static public Vector2 UnitVectorFromDegrees(float degrees) {
            float degrees_ = ((int)degrees)%360;

            if (degrees_ == 90) {
                return new Vector2(0, 1);
            }
            if (degrees_ == 270) {
                return new Vector2(0, -1);
            }
            if (degrees_ == 0) {
                return new Vector2(1, 0);
            }
            if (degrees_ == 180) {
                return new Vector2(-1, 0);
            }

            float radians = RadiansFromDegrees(degrees);
            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        }

        static public float DegreesFromV1ToV2(Vector2 v1, Vector2 v2) {
            float v1Dotv2 = Dot(v1, v2);
            float v1lenMulv2len = LengthOfVector(v1)*LengthOfVector(v2);

            if (v1lenMulv2len == 0) {
                return 0;
            }

            float result = Mathf.Acos(v1Dotv2/v1lenMulv2len);
            result = DegreesFromRadians(result);

            return result;
        }

        static public float DegreesFromP1ToP2(Vector2 p1, Vector2 p2) {
            Vector2 uv = UnitVectorV2FromP1ToP2(p1, p2);
            return DegreesFromXAxisToVector(uv);
        }

        static public float DegreesFromV3P1ToP2IgnoreZ(Vector3 p1, Vector3 p2) {
            return DegreesFromP1ToP2(Vector2FromVector3(p1), Vector2FromVector3(p2));
        }

        static public float DegreesFromXAxisToVector(Vector2 vector) {
            if (vector.x == 0) {
                if (vector.y > 0) {
                    return 90;
                }
                if (vector.y < 0) {
                    return 270;
                }
            }

            if (vector.y == 0) {
                if (vector.x > 0) {
                    return 0;
                }
                if (vector.x < 0) {
                    return 180;
                }
            }

            float result = DegreesFromV1ToV2(new Vector2(1, 0), vector);
            return (vector.y >= 0)? result : 360 - result;
        }

        static public float FormatDegrees(float origin) {
            if (0 <= origin && origin < 360) {
                return origin;
            }
        
            float _origin = origin%360.0f;
            return (_origin >= 0)? _origin : 360 + _origin;
        }

        static Vector2 Vector2FromVector3(Vector3 v3) {
            return new Vector2(v3.x, v3.y);
        }
    }
}