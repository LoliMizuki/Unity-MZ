using UnityEngine;
using System;
using System.Collections.Generic;

public static partial class MZ {

    public class DatabaseTypeConvert {
    
        public static int IntFromDBValue(object dbValue) {
            return (int)dbValue;
        }
    
        public static float FloatFromDBValue(object dbValue) {
            return (float)(Double)dbValue;
        }
    
        public static Vector2 Vector2FromDBValue(object dbValue) {
            string originString = dbValue as string;
    
            originString = originString.Replace( "(", "" );
            originString = originString.Replace( ")", "" );
            originString.Replace( " ", "" );
    
            string[] token = originString.Split( ',' );
    
            if( token.Length != 2 ) {
                return Vector2.zero;
            }
    
            return new Vector2( float.Parse( token[ 0 ] ), float.Parse( token[ 1 ] ) );
        }
    
        public static List<Vector2> Vector2ListFromDBValue(object dbValue) {
            string originString = dbValue as string;
    
            originString = originString.Replace( " ", "" );
            originString = originString.Replace( "),(", "|" );
            originString = originString.Replace( "(", "" );
            originString = originString.Replace( ")", "" );
    
            string[] tokens = originString.Split( '|' );
            if( tokens.Length == 0 ) {
                return null;
            }
    
            List<Vector2> posList = new List<Vector2>();
    
            foreach( string token in tokens ) {
                string[] valuesToken = token.Split( ',' );
    
                if( valuesToken.Length != 2 ) {
                    continue;
                }
    
                float x;
                if( float.TryParse( valuesToken[ 0 ], out x ) == false ) {
                    x = 0;
                }
    
                float y;
                if( float.TryParse( valuesToken[ 1 ], out y ) == false ) {
                    y = 0;
                }
    
                posList.Add( new Vector2( x, y ) );
            }
    
            return posList;
        }
    
        public static string StringFromDBValue(object dbValue) {
            return ( dbValue != null )? dbValue.ToString() : "";
        }
    
        public static Color ColorFromDBValue(object dbValue) {
            string temp = dbValue.ToString().Replace("RGBA", "");
            temp = temp.Replace( "(", "" );
            temp = temp.Replace( ")", "" );
    
            string[] colorValuesString = temp.Split( ',' );
    
    
            Color color = new Color(
                float.Parse( colorValuesString[ 0 ] ),
                float.Parse( colorValuesString[ 1 ] ),
                float.Parse( colorValuesString[ 2 ] )
            );
    
            return color;
        }
    
        public static string SQLTextWrapFromString(string str) {
            return "'" + str + "'";
        }
    }
}