using System;
using System.Collections.Generic;
using UnityEngine;

static public partial class MZ {

    public class Values {

        static public Dictionary<Type, object> defaultValueByType = new Dictionary<Type, object> {
            { typeof(int), 0 },
            { typeof(float), 0.0f },
            { typeof(string), "" },
            { typeof(bool), false },
            { typeof(Vector2), Vector2.zero },
            { typeof(Vector3), Vector3.zero },
            { typeof(Rect), new Rect(0,0,0,0) },
        };
        
        static public Dictionary<Type, Func<object, object>> convertFuncByType = new Dictionary<Type, Func<object, object>> {
        
            {typeof(int), (obj) => {  
                    int result = 0;
                    bool success = int.TryParse(obj.ToString(), out result);
                    MZ.Debugs.Assert(success, "convert fail: from " + obj.ToString() + " to int");
                    return result;
                }},
    
            {typeof(float), (obj) => {          
                    float result = 0;
                    bool success = float.TryParse(obj.ToString(), out result);
                    MZ.Debugs.Assert(success, "convert fail: from " + obj.ToString() + " to float");
                    return result;
                }},
    
            {typeof(string), (obj) => {
                    return obj.ToString();
                }},
    
            {typeof(bool), (obj) => {
                    bool result = false;
                    bool success = bool.TryParse(obj.ToString(), out result);
                    MZ.Debugs.Assert(success, "convert fail: from " + obj.ToString() + " to bool");
                    return result;
                }},
    
            {typeof(Vector2), (obj) => {
                    bool success = false;
                    Vector2 result = Vectors.Vector2FromString(obj.ToString(), out success);
                    MZ.Debugs.Assert(success, "convert fail: from " + obj.ToString() + " to vector2");
                    return result;
                }},
    
            {typeof(Vector3), (obj) => {
                    bool success = false;
                    Vector3 result = Vectors.Vector3FromString(obj.ToString(), out success);
                    MZ.Debugs.Assert(success, "convert fail: from " + obj.ToString() + " to vector3");
                    return result;
                }},
                
        	{typeof(Rect), (obj) => {
					return MZ.Rects.RectFromString(obj.ToString());
        		}},
        };
    
        static public T DefaultValue<T>() {
            MZ.Debugs.Assert(
            	defaultValueByType.ContainsKey(
	            	typeof(T)), 
	            	"can not default valune of this type(" + typeof(T).ToString() + "), please define it first"
    		);
            return (T)defaultValueByType[typeof(T)];
        }

        static public T ValueFormObject<T>(object objValue) {
            if (objValue.GetType() == typeof(T)) {
                return (T)objValue;
            }

            MZ.Debugs.Assert(convertFuncByType.ContainsKey(typeof(T)), "can not convert " + typeof(T).ToString() + ", please define it first");
            return (T)convertFuncByType[typeof(T)](objValue);
        }
    }
}