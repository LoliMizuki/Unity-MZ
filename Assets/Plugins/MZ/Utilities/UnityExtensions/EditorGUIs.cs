using UnityEngine;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static partial class MZ {
    public interface IInspector {
		bool InspectorLayout(int deep = 0);
    }
}

#if UNITY_EDITOR

public static partial class MZ {

    public class EditorGUI {
    
        public static Rect DrawTextureOnCurrentLayout(Texture2D texture, float width, float height, float offsetInnerBox, string title) {
            GUILayout.Box(title, GUILayout.Width(width), GUILayout.Height(height));
            Rect lastRect = GUILayoutUtility.GetLastRect();
            
            if(texture != null) {
                Rect frameRect = new Rect(lastRect.x + offsetInnerBox, lastRect.y + offsetInnerBox, width - offsetInnerBox*2, height - offsetInnerBox*2);
                UnityEngine.GUI.DrawTexture(frameRect, texture);
            }
            
            return lastRect;
        }
        
        public static Rect DrawTextureToRect(Texture2D texture, Rect rect) {
			UnityEngine.GUI.DrawTexture(rect, texture);
            return rect;        
        }
        
        public static T LayoutFieldWithObjectValue<T>(string label, object preValue) where T: IConvertible {
            Type t = typeof(T);
            
            object newValue = null;
    
            bool hasLabel = MZ.Verifiers.String(label);
            
            if(t == typeof(int) || t == typeof(uint)) {
                string strValue = (preValue != null)? preValue.ToString() : "0";
                newValue = (hasLabel)? EditorGUILayout.IntField(label, int.Parse(strValue)) : EditorGUILayout.IntField(int.Parse(strValue));
            } else if(t == typeof(float)) {
                string strValue = (preValue != null)? preValue.ToString() : "0";
                newValue = (hasLabel)? EditorGUILayout.FloatField(label, float.Parse(strValue)) : EditorGUILayout.FloatField(float.Parse(strValue));
            } else if(t == typeof(string)) {
                string strValue = (preValue != null)? preValue.ToString() : "";
                newValue = (hasLabel)? EditorGUILayout.TextField(label, strValue) : EditorGUILayout.TextField(label, strValue);
            } else if(t == typeof(bool)) {
                string strValue = (preValue != null)? preValue.ToString() : "false";
                newValue = (hasLabel)? EditorGUILayout.Toggle(label, bool.Parse(strValue)) : EditorGUILayout.Toggle(bool.Parse(strValue));
            }
            
            MZ.Debugs.AssertIfNull(newValue);
            
            return (T)Convert.ChangeType(newValue, typeof(T));
        }
        
        public static T LayoutFieldWithObjectValueInDict<T>(string label, string key, Dictionary<string, object> dict) where T: IConvertible {
			dict.AutoSetValueForKey<string, object>(key, new Func<object>(() => MZ.Values.DefaultValue<T>()));
            
            object preValue = dict[key];
            T newValue = LayoutFieldWithObjectValue<T>(label, preValue);
            
            if((object)newValue == preValue) {
                return newValue;
            }
            
            dict[key] = (object)newValue;
            
            return newValue;
        }
        
        public static T LayoutFieldWithObjectValue<T>(string label, int deep, object preValue) where T: IConvertible {
            return LayoutFieldWithObjectValue<T>(EditorGUI.TextLabelWithIndentDeep(label, deep), preValue);
        }
        
        public static T LayoutFieldWithObjectValueInDict<T>(string label, int deep, string key, Dictionary<string, object> dict) where T: IConvertible {
            return LayoutFieldWithObjectValueInDict<T>(EditorGUI.TextLabelWithIndentDeep(label, deep), key, dict);
        }
        
        public static string TextLabelWithIndentDeep(string label, string indentSymbol, int deep) {
            if(deep == 0) {
                return label;
            }
            
            string indent = "";
            for(int i = 1; i < deep; i++) {
                indent += "\t";
            }
            
            return indent + indentSymbol + label;
        }
        
        public static string TextLabelWithIndentDeep(string label, int deep) {
            return TextLabelWithIndentDeep(label, "|- ", deep);
        }
        
        public static void LayoutLabelWithDeep(string label, int deep = 0) {
            EditorGUILayout.LabelField(EditorGUI.TextLabelWithIndentDeep(label, deep));
        }
    
        public static string LayoutPopupStringsList(string label, string str, List<string> stringsList) {
            if(stringsList == null || stringsList.Count == 0) {
                return null;
            }
    
            if(!MZ.Verifiers.String(str)) {
                str = stringsList[0];
            }
    
            int nextIndex = EditorGUILayout.Popup(label, stringsList.IndexOf(str), stringsList.ToArray());
            return stringsList[nextIndex];
        }
    
        public static string LayoutPopupStringsList(string label, int deep, string str, List<string> stringsList) {
            return LayoutPopupStringsList(EditorGUI.TextLabelWithIndentDeep(label, deep), str, stringsList);
        }
    }
}

#endif