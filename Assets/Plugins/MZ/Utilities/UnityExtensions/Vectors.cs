using UnityEngine;
using System.Collections;

public static partial class MZ {

    public class Vectors {

        public static Vector2 INVALID_2 = new Vector2(-9999, -9999);
        
        public static Vector3 INVALID_3_BUT_Z = new Vector3(-9999, -9999, 0);

        public static Vector3 InvalidButZ(float z) {
            return new Vector3(INVALID_2.x, INVALID_2.y, z);
        }
        
		public static float Dot(Vector2 v1, Vector2 v2) { // === UnityEngine.Vector2.Dot
			return (v1.x*v2.x) + (v1.y*v2.y);
		}
		
		public static float Dot(Vector3 v1, Vector3 v2) { // === UnityEngine.Vector3.Dot
			return (v1.x*v2.x) + (v1.y*v2.y) + (v1.z*v2.z);
		}
		
		public static float LengthOfVector(Vector2 vector) { // === vector.magnitude
			return Mathf.Sqrt(vector.x*vector.x + vector.y*vector.y);
		}
		
		public static float LengthOfVector(Vector3 vector) { // === vector.magnitude
			return Mathf.Sqrt(vector.x*vector.x + vector.y*vector.y + vector.z*vector.z);
		}
		
		public static Vector2 UnitVectorV2FromP1ToP2(Vector2 p1, Vector2 p2) {
			float diffY = p2.y - p1.y;
			float diffX = p2.x - p1.x;
			
			if (diffY == 0) {
				if (diffX > 0) return new Vector2(1, 0); 
				else if (diffX < 0) return new Vector2(-1, 0);
				else return Vector2.zero;
			}
			
			float length = Mathf.Sqrt(Mathf.Pow(diffX, 2) + Mathf.Pow(diffY, 2));
			
			return new Vector2(diffX/length, diffY/length);
		}

        public static Vector2 Vector2From3(Vector3 vector3) {
            return new Vector2(vector3.x, vector3.y);
        }

        public static Vector3 Vector3From2(Vector2 vector2) {
            return new Vector3(vector2.x, vector2.y, 0);
        }

        public static Vector3 Vector3IgnoreZ(Vector3 vector3) {
            return new Vector3(vector3.x, vector3.y, 0);
        }

        public static Vector2 Vector2FromString(string posStr, out bool success) {
            posStr = posStr.Replace("(", "").Replace(")", "");
            string[] splitedPosStrs = posStr.Split(',');

            if (splitedPosStrs.Length < 2) {
                success = false;
                return Vector2.zero;
            } 

            float x, y;
            if (float.TryParse(splitedPosStrs[0], out x) == false || 
                float.TryParse(splitedPosStrs[1], out y) == false) {
                success = false;
                return Vector2.zero;
            }

            success = true;
            return new Vector2(x, y);
        }

        public static Vector2 Vector2FromString(string posStr) {
            bool success;
            return Vector2FromString(posStr, out success);
        }

        public static Vector3 Vector3FromString(string posStr, out bool success) {
            posStr = posStr.Replace("(", "").Replace(")", "");
            string[] splitedPosStrs = posStr.Split(',');
        
            if (splitedPosStrs.Length < 3) {
                success = false;
                return Vector2.zero;
            } 
        
            float x, y, z;
            if (float.TryParse(splitedPosStrs[0], out x) == false || 
                float.TryParse(splitedPosStrs[1], out y) == false || 
                float.TryParse(splitedPosStrs[2], out z) == false) {
                success = false;
                return Vector2.zero;
            }
        
            success = true;
            
            return new Vector3(x, y, z);
        }

        public static Vector2 Vector3FromString(string posStr) {
            bool success;
            return Vector3FromString(posStr, out success);
        }
        
		public static Vector2 UnitVectorFromVectorAddDegree(Vector2 vector, float degrees) {
			float radians = Degrees.RadiansFromDegrees(degrees);
			
			float c = Mathf.Cos(radians);
			float s = Mathf.Sin(radians);
			
			Vector2 resultVetor = new Vector2(vector.x*c - vector.y*s, vector.x*s + vector.y*c);
			Vector2 unitResultVetor = UnitVectorFromVector(resultVetor);
			
			return unitResultVetor;
		}
		
		public static Vector2 UnitVectorV2FromV3P1ToP2IgnoreZ(Vector3 p1, Vector3 p2) {
			return UnitVectorV2FromP1ToP2(new Vector2(p1.x, p1.y), new Vector2(p2.x, p2.y));
		}
		
		public static Vector2 UnitVectorFromVector(Vector2 vector) {
			float length = LengthOfVector(vector);
			return new Vector2(vector.x/length, vector.y/length);
		}
		
		public static Vector2 UnitVectorFromVectorAddDegree(float degrees) {
			return UnitVectorFromVectorAddDegree(new Vector2(1, 0), degrees);
		}
		
		public static Vector2 UnitVectorFromDegrees(float degrees) {
			float degrees_ = ((int)degrees)%360;
			
			if (degrees_ == 90) return new Vector2(0, 1);
			if (degrees_ == 270) return new Vector2(0, -1);
			if (degrees_ == 0) return new Vector2(1, 0);
			if (degrees_ == 180) return new Vector2(-1, 0);
			
			float radians = Degrees.RadiansFromDegrees(degrees);
			return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
		}
    }    
}

public struct Vector2i {
	public int x;
	public int y;
	
	public Vector2i(int x, int y) {
		this.x = x;
		this.y = y;
	}
	
	override public string ToString() {
		return string.Format("(x: {0}, y: {1})", x, y);
	}
}

public static class MZVectorsExtensions {
    
    // Vector2
    
    public static bool IsInvalid(this Vector2 v) {
        return v.x == MZ.Vectors.INVALID_2.x && v.y == MZ.Vectors.INVALID_2.y;
    }
    
    public static void ToInvalid(this Vector2 v) {
        v = MZ.Vectors.INVALID_2;
    }
    
    public static Vector3 ToVector3(this Vector2 v) {
        return MZ.Vectors.Vector3From2(v);
    }
    
    public static Vector3 ToVector3AndZ(this Vector2 v, float z) {
        return new Vector3(v.x, v.y, z);
    }
    
    public static Vector2 ToVector2(this string str) {
        return MZ.Vectors.Vector2FromString(str);
    }
    
    // Vector3
    
    public static bool IsInvalid(this Vector3 v) {
        return v.x == MZ.Vectors.INVALID_2.x && v.y == MZ.Vectors.INVALID_2.y;
    }
    
	public static Vector3 ToInvalidButZ(this Vector3 v) {
        v = MZ.Vectors.InvalidButZ(v.z);
        return v;
    }
    
	public static Vector3 ToInvalidSetZ(this Vector3 v, float z) {
		v = new Vector3(MZ.Vectors.INVALID_2.x, MZ.Vectors.INVALID_2.y, z);
		return v;
    }
    
    public static Vector2 ToVector2(this Vector3 v) {
        return MZ.Vectors.Vector2From3(v);
    }
    
    public static Vector3 ToVector3(this string str) {
        return MZ.Vectors.Vector3FromString(str);    
    }
}
