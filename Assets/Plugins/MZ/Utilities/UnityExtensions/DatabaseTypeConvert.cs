// TODO: some unity type here, how to manage them?
// TODO: parse is simple algorithm now, need enhance

using UnityEngine;
using System;
using System.Collections.Generic;

static public partial class MZ {

    public class DatabaseTypeConvert {
        static public int IntFromDBValue(object dbValue) {
            if( dbValue == null ) {
                MZ.Debugs.GetTraceStackName( 3 );
                return 0;
            }
            return (int)dbValue;
        }
    
        static public float FloatFromDBValue(object dbValue) {
            return (float)(Double)dbValue;
        }
    
        static public Vector2 Vector2FromDBValue(object dbValue) {
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
    
        static public List<Vector2> Vector2ListFromDBValue(object dbValue) {
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
    
        static public string StringFromDBValue(object dbValue) {
            return ( dbValue != null )? dbValue.ToString() : "";
        }
    
        static public Color ColorFromDBValue(object dbValue) {
            string temp = dbValue.ToString();
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
    
        static public string SQLTextWrapFromString(string str) {
            return "'" + str + "'";
        }
    }
}