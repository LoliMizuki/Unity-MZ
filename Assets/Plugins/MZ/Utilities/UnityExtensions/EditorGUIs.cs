using UnityEngine;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static partial class MZ {
    public interface IInspector {
		bool InspectorLayout(int indentDepth = 0);
    }
}

#if UNITY_EDITOR

public static partial class MZ {

    public static class EditorGUIs {
    
        public static Rect DrawTextureOnCurrentLayout(Texture2D texture, float width, float height, float offsetInnerBox, string title) {
            GUILayout.Box(title, GUILayout.Width(width), GUILayout.Height(height));
            Rect lastRect = GUILayoutUtility.GetLastRect();
            
			if (texture == null) return lastRect;
			
			Rect frameRect = new Rect(
	        	lastRect.x + offsetInnerBox,
	        	lastRect.y + offsetInnerBox,
	        	width - offsetInnerBox*2,
	        	height - offsetInnerBox*2
			);
			
	        UnityEngine.GUI.DrawTexture(frameRect, texture);
	        
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
            
            if (t == typeof(int) || t == typeof(uint)) {
                string strValue = (preValue != null)? preValue.ToString() : "0";
                newValue = (hasLabel)? EditorGUILayout.IntField(label, int.Parse(strValue)) : EditorGUILayout.IntField(int.Parse(strValue));
            } else if (t == typeof(float)) {
                string strValue = (preValue != null)? preValue.ToString() : "0";
                newValue = (hasLabel)? EditorGUILayout.FloatField(label, float.Parse(strValue)) : EditorGUILayout.FloatField(float.Parse(strValue));
            } else if (t == typeof(string)) {
                string strValue = (preValue != null)? preValue.ToString() : "";
                newValue = (hasLabel)? EditorGUILayout.TextField(label, strValue) : EditorGUILayout.TextField(label, strValue);
            } else if (t == typeof(bool)) {
                string strValue = (preValue != null)? preValue.ToString() : "false";
                newValue = (hasLabel)? EditorGUILayout.Toggle(label, bool.Parse(strValue)) : EditorGUILayout.Toggle(bool.Parse(strValue));
            }
            
            MZ.Debugs.AssertIfNullWithMessage(newValue, "value is null?");
            
            return (T)Convert.ChangeType(newValue, typeof(T));
        }
        
        public static T LayoutFieldWithObjectValueInDict<T>(string label, string key, Dictionary<string, object> dict) where T: IConvertible {
			dict.AutoSetValueForKey<string, object>(key, new Func<object>(() => MZ.Values.DefaultValue<T>()));
            
            object preValue = dict[key];
            T newValue = LayoutFieldWithObjectValue<T>(label, preValue);
            
            if ((object)newValue == preValue) return newValue;
            
            dict[key] = (object)newValue;
            
            return newValue;
        }
        
		public static T LayoutFieldWithObjectValue<T>(string label, int indentDepth, object preValue) where T: IConvertible {
            return LayoutFieldWithObjectValue<T>(EditorGUIs.TextLabelWithIndentDepth(label, indentDepth), preValue);
        }
        
        public static T LayoutFieldWithObjectValueInDict<T>(
        	string label, 
			int indentDepth, 
        	string key, 
        	Dictionary<string, object> dict
		) where T: IConvertible {
			return LayoutFieldWithObjectValueInDict<T>(EditorGUIs.TextLabelWithIndentDepth(label, indentDepth), key, dict);
        }
        
        public static string TextLabelWithIndentDepth(string label, string indentSymbol, int depth) {
            if (depth == 0) return label;
            
            string indent = "";
            for (int i = 1; i < depth; i++) {
                indent += "\t";
            }
            
            return indent + indentSymbol + label;
        }
        
		public static string TextLabelWithIndentDepth(string label, int indentDepth) {
            return TextLabelWithIndentDepth(label, "|- ", indentDepth);
        }
        
		public static void LayoutLabelWithIndentDepth(string label, int indentDepth = 0) {
            EditorGUILayout.LabelField(EditorGUIs.TextLabelWithIndentDepth(label, indentDepth));
        }
    
        public static string LayoutPopupStringsList(string label, string str, List<string> stringsList) {
            if (stringsList == null || stringsList.Count == 0) return null;
            if (!MZ.Verifiers.String(str)) str = stringsList[0];
           
            int nextIndex = EditorGUILayout.Popup(label, stringsList.IndexOf(str), stringsList.ToArray());
            return stringsList[nextIndex];
        }
    
		public static string LayoutPopupStringsList(string label, int indentDepth, string str, List<string> stringsList) {
            return LayoutPopupStringsList(EditorGUIs.TextLabelWithIndentDepth(label, indentDepth), str, stringsList);
        }
    }
}

#endif