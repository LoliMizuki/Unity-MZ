#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

static public partial class MZ {

    public class EditorGUI {
    
        static public Rect DrawTextureOnCurrentLayout(Texture2D texture, float width, float height, float offsetInnerBox, string title) {
            GUILayout.Box(title, GUILayout.Width(width), GUILayout.Height(height));
            Rect lastRect = GUILayoutUtility.GetLastRect();
            
            if(texture != null) {
                Rect frameRect = new Rect(lastRect.x + offsetInnerBox, lastRect.y + offsetInnerBox, width - offsetInnerBox*2, height - offsetInnerBox*2);
                GUI.DrawTexture(frameRect, texture);
            }
            
            return lastRect;
        }
        
        static public Rect DrawTextureToRect(Texture2D texture, Rect rect) {
            GUI.DrawTexture(rect, texture);
            return rect;        
        }
        
        static public T LayoutFieldWithObjectValue<T>(string label, object preValue) where T: IConvertible {
            Type t = typeof(T);
            
            object newValue = null;
    
            bool hasLabel = MZ.Verifiers.String(label);
            
            if(t == typeof(int)) {
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
        
        static public T LayoutFieldWithObjectValueInDict<T>(string label, string key, Dictionary<string, object> dict) where T: IConvertible {
            if(dict.ContainsKey(key) == false) {
                MZ.Dictionaries.SetDefaultValueInDictIfNotContainKey<T>(dict, key);
            }
            
            object preValue = dict[key];
            T newValue = LayoutFieldWithObjectValue<T>(label, preValue);
            
            if((object)newValue == preValue) {
                return newValue;
            }
            
            dict[key] = (object)newValue;
            
            return newValue;
        }
        
        static public T LayoutFieldWithObjectValueInDict<T>(string label, int deep, string key, Dictionary<string, object> dict) where T: IConvertible {
            return LayoutFieldWithObjectValueInDict<T>(EditorGUI.TextLabelWithIndentDeep(label, deep), key, dict);
        }
        
        static public string TextLabelWithIndentDeep(string label, string indentSymbol, int deep) {
            if(deep == 0) {
                return label;
            }
            
            string indent = "";
            for(int i = 1; i < deep; i++) {
                indent += "\t";
            }
            
            return indent + indentSymbol + label;
        }
        
        static public string TextLabelWithIndentDeep(string label, int deep) {
            return TextLabelWithIndentDeep(label, "|- ", deep);
        }
        
        static public void LayoutLabelWithDeep(string label, int deep) {
            EditorGUILayout.LabelField(EditorGUI.TextLabelWithIndentDeep(label, deep));
        }
    
        static public string LayoutPopupStringsList(string label, string str, List<string> stringsList) {
            if(stringsList == null || stringsList.Count == 0) {
                return null;
            }
    
            if(!MZ.Verifiers.String(str)) {
                str = stringsList[0];
            }
    
            int nextIndex = EditorGUILayout.Popup(label, stringsList.IndexOf(str), stringsList.ToArray());
            return stringsList[nextIndex];
        }
    
        static public string LayoutPopupStringsList(string label, int deep, string str, List<string> stringsList) {
            return LayoutPopupStringsList(EditorGUI.TextLabelWithIndentDeep(label, deep), str, stringsList);
        }
    }
}

#endif