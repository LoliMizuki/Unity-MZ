using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using MiniJSON;

public static partial class MZ {
	
	public static partial class Datas {
	
		public static class JsonDatas {
		
			public interface IJsonSerializable: IDictionarySerializable {
				string ToJson();
				void FromJson(string json);
			}
		
			public class Data : IJsonSerializable {
				
				public delegate object ObjectFromTypedObject(Type type, string name, object obj);
				
				public delegate void ActionWithSelfAndDict(Data self, Dictionary<string, object> dict);
				
				public Data() {            
				}
				
				public string ListAllDataFields() {
					string str = this.GetType().ToString() + "{" + "\n";
					
					foreach (FieldInfo fi in this.GetType().GetFields()) str += fi.Name + ": " + fi.FieldType + "(field)\n";
					foreach (PropertyInfo pi in this.GetType().GetProperties()) str += pi.Name + ": " + pi.PropertyType + "(property)" + "\n";
					
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
				
				public virtual string ToJson() {
					return Json.Serialize(ToDictionary());
				}
				
				public void AddDataFieldFromToConvertedRuleWithName(string name, ObjectFromTypedObject fromDict, ObjectFromTypedObject toDict = null) {
					_convertedRulesFromDictByName.Add(name, fromDict);
					if (toDict != null) _convertedRulesToDictByName.Add(name, toDict);
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
//					AddDataFieldFromToConvertedRuleWithType(type, (t, n, o) => null, (t, n, o) => null);
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
					foreach (FieldInfo fieldInfo in this.GetType().GetFields()) {
						object objValue = null;
						
						string name = fieldInfo.Name;
						
						Type type = fieldInfo.FieldType;
						
						if (_convertedRulesToDictByName.ContainsKey(name)) {
							objValue = _convertedRulesToDictByName[name](type, name, fieldInfo.GetValue(this));
						} else if (MZ.Datas.Converts.HasObjectFromTypedValueFunc(type)) {
							objValue = MZ.Datas.Converts.ObjectFromTypedValue(type, fieldInfo.GetValue(this));
						} else {
							objValue = fieldInfo.GetValue(this);
						}

						if (objValue != null) dict.Add(name, objValue);
					}
				}
				
				protected virtual void AddPropertiesToDict(Dictionary<string, object> dict) {
					foreach (PropertyInfo propertyInfo in this.GetType().GetProperties()) {
						object objValue = null;
						
						string name = propertyInfo.Name;
						Type type = propertyInfo.PropertyType;
						
						if (_convertedRulesToDictByName.ContainsKey(name)) {
							objValue = _convertedRulesToDictByName[name](type, name, propertyInfo.GetValue(this, null));
						} else if (MZ.Datas.Converts.HasObjectFromTypedValueFunc(type)) {
							objValue = MZ.Datas.Converts.ObjectFromTypedValue(type, propertyInfo.GetValue(this, null));
						} else {
							objValue = propertyInfo.GetValue(this, null);
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
						} else if (MZ.Datas.Converts.HasValueFromTypedObjectFunc(type)) {
							objectValue = MZ.Datas.Converts.ValueFromTypedObject(type, dict[name]);
						} else {
							objectValue = dict[name];
						}
						
						#if UNITY_EDITOR
						try {
							if (objectValue != null) fieldInfo.SetValue(this, objectValue);
						} catch (ArgumentException ae) {
							MZ.Debugs.Log(name + "(type = " + type.ToString() + ") + " + " convert fail " + ", message = " + ae.Message);
							MZ.Debugs.AssertAlwaysFalse("");
						}
						#else
						if (objectValue != null) fieldInfo.SetValue(this, objectValue);
						#endif
					}
				}
				
				protected virtual void PropertiesFromDictionary(Dictionary<string, object> dict) {
					foreach (PropertyInfo propertyInfo in this.GetType().GetProperties()) {
						string name = propertyInfo.Name;
						if (!dict.ContainsKey(name)) continue;
						
						Type type = propertyInfo.PropertyType;
						
						object objectValue = null;
						
						if (_convertedRulesFromDictByName.ContainsKey(name)) {
							objectValue = _convertedRulesFromDictByName[name](type, name, dict[name]);
						} else if (MZ.Datas.Converts.HasValueFromTypedObjectFunc(type)) {
							objectValue = MZ.Datas.Converts.ValueFromTypedObject(type, dict[name]);
						} else {
							objectValue = dict[name];
						}

						#if UNITY_EDITOR
						try {
							if (objectValue != null) propertyInfo.SetValue(this, objectValue, null);
						} catch (ArgumentException ae) {
							MZ.Debugs.Log(name + "(type = " + type.ToString() + ") + " + " convert fail " + ", message = " + ae.Message);
							MZ.Debugs.AssertAlwaysFalse("");
						}
						#else
						if (objectValue != null) propertyInfo.SetValue(this, objectValue, null);
						#endif
					}
				}
				
				
				
				Dictionary<string, ObjectFromTypedObject> _convertedRulesFromDictByName = new Dictionary<string, ObjectFromTypedObject>();
				
				Dictionary<string, ObjectFromTypedObject> _convertedRulesToDictByName = new Dictionary<string, ObjectFromTypedObject>();
				
				List<ActionWithSelfAndDict> _preActionFromDictionary = new List<ActionWithSelfAndDict>();
				
				List<ActionWithSelfAndDict> _extraActionFromDict = new List<ActionWithSelfAndDict>();
				
				List<ActionWithSelfAndDict> _extraActionToDict = new List<ActionWithSelfAndDict>();
			}
		}
	}
}