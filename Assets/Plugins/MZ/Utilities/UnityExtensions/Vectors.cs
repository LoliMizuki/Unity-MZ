using UnityEngine;
using System.Collections;

static public partial class MZ {

    public class Vectors {

        static public Vector2 INVALID_2 = new Vector2(-9999, -9999);
        
        static public Vector3 INVALID_3_BUT_Z = new Vector3(-9999, -9999, 0);

        static public Vector3 InvalidButZ(float z) {
            return new Vector3(INVALID_2.x, INVALID_2.y, z);
        }

        static public Vector2 Vector2FromVector3(Vector3 vector3) {
            return new Vector2(vector3.x, vector3.y);
        }

        static public Vector3 Vector3FromVector2(Vector2 vector2) {
            return new Vector3(vector2.x, vector2.y, 0);
        }

        static public Vector3 Vector3IgnoreZ(Vector3 vector3) {
            return new Vector3(vector3.x, vector3.y, 0);
        }

        static public Vector2 Vector2FromString(string posStr, out bool success) {
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

        static public Vector2 Vector2FromString(string posStr) {
            bool success;
            return Vector2FromString(posStr, out success);
        }

        static public Vector3 Vector3FromString(string posStr, out bool success) {
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

        static public Vector2 Vector3FromString(string posStr) {
            bool success;
            return Vector3FromString(posStr, out success);
        }
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
        return MZ.Vectors.Vector3FromVector2(v);
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
    
    public static void ToInvalidButZ(this Vector3 v) {
        v = MZ.Vectors.InvalidButZ(v.z);
    }
    
    public static void ToInvalidAndZ(this Vector3 v, float z) {
        v = new Vector3(v.x, v.y, z);
    }
    
    public static Vector2 ToVector2(this Vector3 v) {
        return MZ.Vectors.Vector2FromVector3(v);
    }
    
    public static Vector3 ToVector3(this string str) {
        return MZ.Vectors.Vector3FromString(str);    
    }
}
