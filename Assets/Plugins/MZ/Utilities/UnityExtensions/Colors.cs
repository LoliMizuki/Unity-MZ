using UnityEngine;
using System.Collections;

public static partial class MZ {
	
	public static class Colors {
	
		public static Color ColorFromString(string str) {
			string temp = str.Replace("RGBA", "");
			temp = temp.Replace( "(", "" );
			temp = temp.Replace( ")", "" );
			
			string[] colorValuesString = temp.Split( ',' );
			
			Color color = new Color(
				float.Parse(colorValuesString[0]),
				float.Parse(colorValuesString[1]),
				float.Parse(colorValuesString[2])
			);
			
			return color;
		}
	
	}	
}
