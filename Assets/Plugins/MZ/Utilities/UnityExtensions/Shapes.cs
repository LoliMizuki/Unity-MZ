using UnityEngine;
using System.Collections;

public static partial class MZ {

	public static class Rects {
	
		public static Rect RectFromCenterAndSize(Vector2 center, Vector2 size) {
			return new Rect(center.x - size.x/2, center.y - size.y/2, size.x, size.y);
		}
		
		public static Rect RectFromString(string str) {
			string _str = str.Replace("C", "").Replace(")", "");
			string[] attr = _str.Split(',');
			
			float x = float.Parse(attr[0].Split(':')[1]);
			float y = float.Parse(attr[1].Split(':')[1]);
			float w = float.Parse(attr[2].Split(':')[1]);
			float h = float.Parse(attr[3].Split(':')[1]);
			
			return new Rect(x, y, w, h);
		}
		
		public static bool IsIntersect(Rect rectA, Rect rectB) {
			return (Mathf.Abs(rectA.x - rectB.x) < (Mathf.Abs(rectA.width + rectB.width)/2))
				&& (Mathf.Abs(rectA.y - rectB.y) < (Mathf.Abs(rectA.height + rectB.height)/2));
		}
	}
	
	public class Ellipse {
		public Vector2 center;
		
		public float a;
		
		public float b;
		
		public float XWithRadians(float radians) { return center.x + a*Mathf.Cos(radians); }
		
		public float YWithRadians(float radians) { return center.y + b*Mathf.Sin(radians); }
		
		public Vector2 PositionWithRadians(float radians) { 
			return new Vector2(XWithRadians(radians), YWithRadians(radians)); 
		}
		
		public Vector2 PositionWithDegrees(float degrees) { 
			float radians = MZ.Degrees.RadiansFromDegrees(degrees);
			return PositionWithRadians(radians);
		}
	}
}
