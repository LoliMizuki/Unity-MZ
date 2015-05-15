using UnityEngine;
using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using MiniJSON;

public static partial class MZ {

    public interface IData {
        Dictionary<string, object> ToDictionary();
        string ToJson();
        
        void FromDictionary(Dictionary<string, object> dict);
        void FromJson(string json);
    }
    
    public static List<T> TypeListFromObject<T>(object objectsList) {
        List<object> objsList = (List<object>)objectsList;
        
        List<T> list = new List<T>();
        
        foreach (object obj in objsList) {
            list.Add((T)obj);
        }
        
        return list;
	}
	
	public static TYPE_DATA JsonDataTypeFromDictObject<TYPE_DATA>(object dictObj) where TYPE_DATA : MZ.JsonData, new() {
		TYPE_DATA data = new TYPE_DATA();
		Dictionary<string, object> dict = (Dictionary<string, object>)dictObj;
		data.FromDictionary(dict);
		
		return data;
	}
    
    public static List<Dictionary<string, object>> DictListFromDictWithKey(Dictionary<string, object> srcDict, string key) {
		List<Dictionary<string, object>> dictList = new List<Dictionary<string, object>>();
    
		if (!srcDict.ContainsKey(key)) return dictList;
		
		List<object> objList = (List<object>)srcDict[key];
		
		foreach (object obj in objList) {
			dictList.Add(obj as Dictionary<string, object>);
		}
		
		return dictList;
    }
    
    
    
    public class JsonData : IData {
    
		public delegate object ObjectFromTypedObject(Type type, string name, object obj);
        
		public delegate void ActionWithSelfAndDict(JsonData self, Dictionary<string, object> dict);
		
		public JsonData() {            
            AddDefaultRules();
		}
		
		public string ListAllDataFields() {
			string str = this.GetType().ToString() + "{" + "\n";
			
			foreach (FieldInfo fi in this.GetType().GetFields()) {
				str += fi.Name + ": " + fi.FieldType + "(field)\n";
			}
			
			foreach (PropertyInfo pi in this.GetType().GetProperties()) {
				str += pi.Name + ": " + pi.PropertyType + "(property)" + "\n";
			}
			
			str += "}";
			
			return str;
		}
		
		public void PrintAllDataFieldsToConsole() {
			Debug.Log(ListAllDataFields());
		}
    
        public virtual Dictionary<string, object> ToDictionary() {		
			Dictionary<string, object> dict = new Dictionary<string, object>();
            
            AddFieldsToDict(dict);
			AddPropertiesToDict(dict);
            foreach (ActionWithSelfAndDict extraAction in _extraActionToDict) extraAction(this, dict);
			
			return dict;
		}
		
        public virtual void FromDictionary(Dictionary<string, object> dict) {
			foreach (ActionWithSelfAndDict action in _preActionFromDictionary) action(this, dict);
        
			FieldsFromDictionary(dict);
			PropertiesFromDictionary(dict);
            foreach (ActionWithSelfAndDict extraAction in _extraActionFromDict) extraAction(this, dict);
		}
        
        public virtual void FromJson(string json) {
            Dictionary<string, object> dict = (Dictionary<string, object>)Json.Deserialize(json);
            FromDictionary(dict);
        }
        
        virtual public string ToJson() {
            return Json.Serialize(ToDictionary());
        }
        
        public void AddDataFieldFromToConvertedRuleWithName(string name, ObjectFromTypedObject fromDict, ObjectFromTypedObject toDict = null) {
			_convertedRulesFromDictByName.Add(name, fromDict);
			if (toDict != null) _convertedRulesToDictByName.Add(name, toDict);
        }
        
        public void AddDataFieldFromToConvertedRuleWithType(Type type, ObjectFromTypedObject fromDict, ObjectFromTypedObject toDict = null) {
            _convertedRulesFromDictByType.Add(type, fromDict);
            if (toDict != null) _convertedRulesToDictByType.Add(type, toDict);
		}
        
        public void AddIgnoredDataFieldName(string name) {
			AddDataFieldFromToConvertedRuleWithName(name, (t, n, o) => null, (t, n, o) => null);
        }
        
		public void AddIgnoredDataFieldNameAndExtraAction(string name, ActionWithSelfAndDict fromAction = null, ActionWithSelfAndDict toAction = null) {
			AddDataFieldFromToConvertedRuleWithName(name, (t, n, o) => null, (t, n, o) => null);
			AddFromDictionaryExtraAction(fromAction);
			AddToDictionaryExtraAction(toAction);
		}
        
        public void AddIgnoredType(Type type) {
            AddDataFieldFromToConvertedRuleWithType(type, (t, n, o) => null, (t, n, o) => null);
        }
        
        public void AddToDictionaryExtraAction(ActionWithSelfAndDict extraAction) {
			if (extraAction == null) return;
            _extraActionToDict.Add(extraAction);
        }
        
        public void AddFromDictionaryExtraAction(ActionWithSelfAndDict extraAction) {
        	if (extraAction == null) return;
            _extraActionFromDict.Add(extraAction);
        }
        
		public void AddFromDictionaryPreAction(ActionWithSelfAndDict preAction) {
			if (preAction == null) return;
			_preActionFromDictionary.Add(preAction);
		}
        
        protected virtual void AddFieldsToDict(Dictionary<string, object> dict) {
            foreach (FieldInfo fi in this.GetType().GetFields()) {
                object objValue = null;
                
                string name = fi.Name;
                
                Type type = fi.FieldType;
                
                if (_convertedRulesToDictByName.ContainsKey(name)) {
					objValue = _convertedRulesToDictByName[name](type, name, fi.GetValue(this));
                } else if (_convertedRulesToDictByType.ContainsKey(type)) {
					objValue = _convertedRulesToDictByType[type](type, name, fi.GetValue(this));
                } else {
                    objValue = fi.GetValue(this);
                }
                
                if (objValue != null) dict.Add(name, objValue);
            }
        }
        
        virtual protected void AddPropertiesToDict(Dictionary<string, object> dict) {
            foreach (PropertyInfo pi in this.GetType().GetProperties()) {
                object objValue = null;
                
                string name = pi.Name;
                Type type = pi.PropertyType;
                
                if (_convertedRulesToDictByName.ContainsKey(name)) {
					objValue = _convertedRulesToDictByName[name](type, name, pi.GetValue(this, null));
                } else if (_convertedRulesToDictByType.ContainsKey(type)) {
					objValue = _convertedRulesToDictByType[type](type, name, pi.GetValue(this, null));
                } else { 
                    objValue = pi.GetValue(this, null);
                }
            
                if (objValue != null) dict.Add(name, objValue);
            }
        }
		
        protected virtual void FieldsFromDictionary(Dictionary<string, object> dict) {
			foreach (FieldInfo fieldInfo in this.GetType().GetFields()) {
				string name = fieldInfo.Name;
				if (!dict.ContainsKey(name)) continue;
				
				Type type = fieldInfo.FieldType;
				
				object objectValue = null;
				
				if (_convertedRulesFromDictByName.ContainsKey(name)) {
					objectValue = _convertedRulesFromDictByName[name](type, name, dict[name]);
				} else if (_convertedRulesFromDictByType.ContainsKey(type)) {
					objectValue = _convertedRulesFromDictByType[type](type, name, dict[name]);
				} else {
					objectValue = dict[name];
				}
			#if UNITY_EDITOR
				try {
					if (objectValue != null) fieldInfo.SetValue(this, objectValue);
				} catch (ArgumentException ae) {
					Debug.Log("convert fail " + name + " (type = " + type.ToString() + "), message = " + ae.Message);
					MZ.Debugs.AssertAlwaysFalse("");
				}
			#else
				if (objectValue != null) fieldInfo.SetValue(this, objectValue);
			#endif
			}
		}
		
		virtual protected void PropertiesFromDictionary(Dictionary<string, object> dict) {
			foreach (PropertyInfo propertyInfo in this.GetType().GetProperties()) {
				string name = propertyInfo.Name;
				if (!dict.ContainsKey(name)) continue;
				
				Type type = propertyInfo.PropertyType;
				
				object objectValue = null;
				
				if (_convertedRulesFromDictByName.ContainsKey(name)) {
					objectValue = _convertedRulesFromDictByName[name](type, name, dict[name]);
				} else if (_convertedRulesFromDictByType.ContainsKey(type)) {
					objectValue = _convertedRulesFromDictByType[type](type, name, dict[name]);
				} else {
					objectValue = dict[name];
				}
               
				if (objectValue != null) propertyInfo.SetValue(this, objectValue, null);
			}
		}
        
        
        
		Dictionary<string, ObjectFromTypedObject> _convertedRulesFromDictByName = new Dictionary<string, ObjectFromTypedObject>();

		Dictionary<Type, ObjectFromTypedObject> _convertedRulesFromDictByType = new Dictionary<Type, ObjectFromTypedObject>();
		
		Dictionary<string, ObjectFromTypedObject> _convertedRulesToDictByName = new Dictionary<string, ObjectFromTypedObject>();
		
		Dictionary<Type, ObjectFromTypedObject> _convertedRulesToDictByType = new Dictionary<Type, ObjectFromTypedObject>();
		
		List<ActionWithSelfAndDict> _preActionFromDictionary = new List<ActionWithSelfAndDict>();
        
        List<ActionWithSelfAndDict> _extraActionToDict = new List<ActionWithSelfAndDict>();
        
        List<ActionWithSelfAndDict> _extraActionFromDict = new List<ActionWithSelfAndDict>();
        
        void AddDefaultRules() {
            // int
            AddDataFieldFromToConvertedRuleWithType(typeof(System.Int32), (type, name, objValue) => int.Parse(objValue.ToString()));
            
            // float
			AddDataFieldFromToConvertedRuleWithType(typeof(System.Single), (type, name, objValue) => float.Parse(objValue.ToString()));
            
            // color
            AddDataFieldFromToConvertedRuleWithType(
                typeof(UnityEngine.Color),
				(type, name, objValue) => MZ.DatabaseTypeConvert.ColorFromDBValue(objValue.ToString()),
				(type, name, objValue) => objValue.ToString()
            );
            
            // Rect
			AddDataFieldFromToConvertedRuleWithType(
				typeof(UnityEngine.Rect),
				(type, name, objValue) => MZ.Rects.RectFromString(objValue.ToString()),
				(type, name, objValue) => objValue.ToString()
			);
            
            
            // int list
            AddDataFieldFromToConvertedRuleWithType(
                typeof(List<int>),
				(type, name, objValue) => {
                    List<object> rawObjList = (List<object>)objValue;
                    
                    List<int> resultList = new List<int>();
					foreach (object rawObj in rawObjList) {
						resultList.Add(int.Parse(rawObj.ToString()));
					}
                     
                    return resultList;
                }
            );
            
            // float list
            AddDataFieldFromToConvertedRuleWithType(
                typeof(List<float>),
				(type, name, objValue) => {
                    List<object> rawObjList = (List<object>)objValue;
                    
                    List<float> resultList = new List<float>();
                    foreach (object rawObj in rawObjList) {
                    	resultList.Add(float.Parse(rawObj.ToString()));
            		}
                    
                    return resultList;
                }
            );
        }
    }
}
